//    Melek UZUN
//    1306140063
//    Date : 19.05.2016
//    Development Enviorement : Visual Studio2013
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#define MAX_OPERATION  4+1

typedef struct ORDER{
	int order_number;
	int amount;
	int operation_code;
	int deadline;
	struct ORDER * next;
}ORDER;

int setup_duration[MAX_OPERATION][MAX_OPERATION]={0};
int operations[MAX_OPERATION];


int LineParse(char *line,int * values){
	char tmp[100];
	int i=0;
	int j=0;
	
	while(*line && *line!='\n' && *line!='\r'){
		if(*line!=';'){

			tmp[i]=*line;
			i++;
		}
		else{
			
			tmp[i]='\0';
			
			values[j]=atoi(tmp);
			i=0;
			j++;
		}
		line++;
	}

	if(i!=0){
		tmp[i]='\0';
		values[j]=atoi(tmp);
		j++;
	}
	return j;
}



int ReadSetup(char * file_name){
   FILE * input;
   char  line[100];
   int values[20];

  
   if ((input = fopen(file_name, "r+t"))== NULL)
   {
      printf("Cannot open input file: %s\n",file_name);
      return 1;
   }

   
   while(fgets(line, 100, input)){
	   
	   LineParse(line,values);
	   
	   setup_duration[values[0]][values[1]]=values[2];
	   setup_duration[values[1]][values[0]]=values[2];
   }
   return 0;
}



int ReadOperations(char * file_name){
   FILE * input;
   char  line[100];
   int values[20];

   
   if ((input = fopen(file_name, "r+t"))== NULL)
   {
      printf("Cannot open input file: %s\n",file_name);
      return 1;
   }

   
   while(fgets(line, 100, input)){
	   
	   LineParse(line,values);
	   
	   operations[values[0]]=values[1];
   }
	return 0;
}




int ReadOrderList(char * file_name,ORDER **order_list){
   FILE * input;
   char  line[100];
   int values[20];
   ORDER * ol=NULL;
   ORDER *tmp=NULL;

   if ((input = fopen(file_name, "r+t"))== NULL)
   {
      printf("Cannot open input file: %s\n",file_name);
      return 1;
   }

   
   while(fgets(line, 100, input)){
	   LineParse(line,values);

	   tmp=(ORDER *)malloc(sizeof(ORDER));
	   if(!tmp){
		   printf("Memory error.\n");
		   return 1;
	   }


	   tmp->order_number  =values[0];
	   tmp->amount        =values[1];
	   tmp->operation_code=values[2];
	   tmp->deadline      =values[3];
	   tmp->next          =NULL;

	   if(ol){
		   
		   ol->next=tmp;
		   ol=ol->next;
	   }
	   else{
		   *order_list=tmp;
		   ol=tmp;
	   }

   }
	return 0;
}




int CheckDeadline(ORDER * start,ORDER * end,int duration,int operation_code){
	int code=operation_code;
	int dr=duration;
	while(start && start!=end){
		
		dr+=(int)(ceil(start->amount/(float)operations[start->operation_code])+
			setup_duration[code][start->operation_code]);

		
		if(dr>start->deadline){
			return 0;
		}
		
		code=start->operation_code;
		start=start->next;
	}
	return 1;
}



int Calculate(ORDER * order_list){

	ORDER * list=order_list;
	ORDER * tmp, * parent , *p;
	ORDER * swap=NULL;
	ORDER * lt;
	int s_d=0;
	int tt=0;
	int parent_code=0;

	parent_code=list->operation_code;

	
	while(list){
		swap=NULL;
		tt +=(int)(setup_duration[parent_code][list->operation_code]+
		ceil(list->amount/(float)operations[list->operation_code]));

		tmp=list->next;
		if(tmp){
			lt=tmp;
			s_d=setup_duration[list->operation_code][tmp->operation_code];
		}

		p=list;

		while(tmp && s_d){
			if(setup_duration[list->operation_code][tmp->operation_code]<s_d){
				if(CheckDeadline(lt,tmp,tt,list->operation_code)){
					swap=tmp;
					parent=p;
					s_d=setup_duration[list->operation_code][tmp->operation_code];
				}
				else{
					
					break;
				}
			}
			p=tmp;
			tmp=tmp->next;
		}

		
		if(swap){
			tmp=list->next;
			parent->next=swap->next;
			swap->next=tmp;
			list->next=swap;
		}


		list=list->next;
	}
	return 0;
}



void SortByDeadline(ORDER ** orders){
	int swap=0;
    ORDER * p1=NULL,*p2;
	ORDER *list=*orders;

	while(list){
		p2=list->next;
		if(p2){
			if(list->deadline>p2->deadline){
				if(p1){
					
					p1->next=p2;
					list->next=p2->next;
					p2->next=list;
					list=p2;
					swap=1;
				}
				else{
					
					*orders=p2;
					list->next=p2->next;
					p2->next=list;
					list=p2;
					swap=1;
				}
			}
		}
		p1=list;
		list=list->next;

		if(!list){
			if(swap){
				
				list=*orders;
				swap=0;
				p1=NULL;
			}
		}
	}
	return;
}



int WriteFile(char * file_name,ORDER * list){
   FILE *output;
   int tt=0;
   int code;
   char buf[100];

   if ((output = fopen(file_name, "w+t"))== NULL)
   {
      printf("Cannot open output file: %s\n",file_name);
      return 1;
   }

   code=list->operation_code;
   while(list){

		sprintf(buf,"%d;%d;%d;%d;%d \r\n",
			     tt,
				 list->operation_code,
				 list->order_number,
				 list->amount,
				 setup_duration[code][list->operation_code]);

		fwrite(buf,strlen(buf),1,output);

	    tt+=(int)(ceil(list->amount/(float)operations[list->operation_code]) +
		   setup_duration[code][list->operation_code]);

		code=list->operation_code;
		list=list->next;
   }
   return 0;
}




void FreeOrderList(ORDER * list){
	ORDER * p;
	while(list){
		p=list->next;
		free(list);
		list=p;
	}
	return;
}



int main(void){
	ORDER * order_list=NULL;

	if(ReadSetup("SetupDuration.txt")){
		printf("Setup file read error.\n");
		return 1;
	}

	if(ReadOperations("Operations.txt")){
		printf("Operation  read error.\n");
		return 1;
	}

	if(ReadOrderList("Orders.txt",&order_list)){
		printf("Order list read error.\n");
		return 1;
	}

	SortByDeadline(&order_list);

	Calculate(order_list);

	WriteFile("Schedule.txt",order_list);

	FreeOrderList(order_list);

	return 0;
}
