#include<stdio.h>
#include <stdlib.h>

struct operation{
	double kod,hiz;
}opr[4];
struct setup{
	int kd1,kd2,spd;
}stp[6];
struct order{
	double sra,mktr,dead;
	double kd;

}ord[50];

double *sreBul(double);

int main(){
	
	FILE *jd,*md,*ad,*sd;
	double *y;
	int p=0,x=1,i=1,z=1,sure[50],srre[50];
	int sree[50];
	
	jd = fopen("Operations.txt","r");
	
	if(jd==NULL){
		printf("Operation dosyasi acilamadi.\n");
		exit(1);
	}
	
	//Operations.txt dosyasý programda deðiþenlere atanýyor
	 while( !feof(jd) ){
		fscanf(jd,"%d;%d",&opr[i].kod,&opr[i].hiz);
		i++;
	}
		fclose(jd);
	
		
		md = fopen("SetupDuration.txt","r");
		
		if(md == NULL){
			printf("Setup dosyasi acilamadi\n");
			exit(1);
		}
		
		//SetupDuration.txt dosyasý programda deðiþenlere atanýyor
		while (!feof(md) ){
			fscanf(md,"%d;%d;%d",&stp[z].kd1,&stp[z].kd2,&stp[z].spd);
			z++;
		
		}
	
		
		ad = fopen("Orders.txt","r");
		
		if(ad == NULL){
			printf("Orders dosyasi acilamadi.\n");
			exit(1);
		}
		
		//Orders.txt dosyasý programda deðiþenlere atanýyor
		while( !feof(ad) ){
			fscanf(ad,"%d;%d;%d;%d",&ord[x].sra,&ord[x].mktr,&ord[x].kd,&ord[x].dead);
			x++;
				p++;
		}
		
		
		//Ýþlerin yapýlma sürelerini bulan fonksiyon ana fonksiyona dahil ediliyor
		y=sreBul(p);
		int a=*(y+6);
		
		for(int o=1;o<=p;o++){
			sure[o]=*(y+o);
		}
		
		

		
		int c,d;
		double degis;
		
		int cc,dd;
		double swap;
		
		for(c=0;c<=(p-1);c++){
			for(d=0;d<=(p-c-1);d++){
				if(ord[d].dead>ord[d+1].dead){
						for(cc=0;cc<=(p-1);cc++){
							for(dd=0;dd<=(p-c-1);dd++)
								if(ord[dd].kd==ord[(dd+1)].kd){
									
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)];
										
										dd++;
								}
								else if(stp[5].kd1==ord[dd].kd && stp[5].kd2==ord[(dd+1)].kd ||stp[5].kd1==ord[(dd+1)].kd && stp[5].kd2==ord[dd].kd ){
									if(stp[5].spd+sure[2]>ord[2].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
											sree[dd]=sure[(dd+1)]+stp[5].spd;
										
										dd++;
									}
									
									
								}
									else if(stp[4].kd1==ord[dd].kd && stp[4].kd2==ord[(dd+1)].kd ||stp[4].kd1==ord[(dd+1)].kd && stp[4].kd2==ord[dd].kd ){
										if(stp[5].spd+sure[2]+stp[4].spd+sure[2]>ord[2].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)]+stp[4].spd;
										
										dd++;
									}
								}
									else if(stp[3].kd1==ord[dd].kd && stp[3].kd2==ord[(dd+1)].kd ||stp[3].kd1==ord[(dd+1)].kd && stp[3].kd2==ord[dd].kd ){
										if(stp[5].spd+sure[2]+stp[4].spd+sure[2]+stp[3].spd+sure[1]>ord[1].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)]+stp[3].spd;
										
										dd++;
									}
								}
									else if(stp[2].kd1==ord[dd].kd && stp[2].kd2==ord[(dd+1)].kd ||stp[2].kd1==ord[(dd+1)].kd && stp[2].kd2==ord[dd].kd ){
											if(stp[5].spd+sure[2]+stp[4].spd+sure[2]+stp[3].spd+sure[1]+stp[2].spd+sure[1]>ord[1].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)]+stp[2].spd;
										
										dd++;
									}
								}
									else if(stp[1].kd1==ord[dd].kd && stp[1].kd2==ord[(dd+1)].kd ||stp[1].kd1==ord[(dd+1)].kd && stp[1].kd2==ord[dd].kd ){
										if(stp[5].spd+sure[2]+stp[4].spd+sure[2]+stp[3].spd+sure[1]+stp[2].spd+sure[1]+stp[1].spd+sure[1]>ord[1].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)]+stp[1].spd;
										
										dd++;
									}
								}
									else if(stp[6].kd1==ord[dd].kd && stp[6].kd2==ord[(dd+1)].kd ||stp[6].kd1==ord[(dd+1)].kd && stp[6].kd2==ord[dd].kd ){
										if(stp[5].spd+sure[2]+stp[4].spd+sure[2]+stp[3].spd+sure[1]+stp[2].spd+sure[1]+stp[1].spd+sure[1]+stp[6].spd+sure[3]>ord[3].dead){
									break;
									}
									else{
										degis=ord[dd].dead;
										ord[dd].dead=ord[(dd+1)].dead;
										ord[(dd+1)].dead=degis;
						
										degis=ord[dd].kd;
										ord[dd].kd=ord[(dd+1)].kd;
										ord[(dd+1)].kd=degis;
					
										degis=ord[dd].mktr;
										ord[dd].mktr=ord[(dd+1)].mktr;
										ord[(dd+1)].mktr=degis;
										
										sree[dd]=sure[(dd+1)]+stp[6].spd;
										
										dd++;
									}
								}
			
					degis=ord[d].dead;
					ord[d].dead=ord[d+1].dead;
					ord[d+1].dead=degis;
						
					degis=ord[d].kd;
					ord[d].kd=ord[d+1].kd;
					ord[d+1].kd=degis;
					
					degis=ord[d].mktr;
					ord[d].mktr=ord[d+1].mktr;
					ord[d+1].mktr=degis;
					
					sree[dd]=sure[d];
					
					
				}
			}
		}
	}


		int fb=1,e;
		int cod[50];
			
		
     for(int fb=0;fb<=50;fb++){
			cod[fb]=0;
        }
      
    
	
	
		int fbi=1;				
		
		for(int ag=1;ag<=p;ag++){
			for(fbi=1;fbi<=p;){
				
				if(ord[ag].kd==1.0){
					if(ord[ag+1].kd==0.0){
						printf("\n\n1-Algoritmanin sonuna gelinmis.\n\n");
						break;
					}
					else if(ord[ag+1].kd==1.0){
						cod[fbi]=0;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==2.0){
						cod[fbi]=10;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==3.0){
						cod[fbi]=10;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==4.0){
						cod[fbi]=12;
						fbi++;
						break;
					}
					else{
						printf("\n\n4. If'te Sorun var.\n\n");
					}
				}
				
				else if(ord[ag].kd==2.0){
				if(ord[ag+1].kd==0){
						printf("\n\n2-Algoritmanin sonuna gelinmis.\n\n");
						break;
					}
					else if(ord[ag+1].kd==1.0){
						cod[fbi]=10;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==2.0){
						cod[fbi]=0;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==3.0){
						cod[fbi]=5;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==4.0){
						cod[fbi]=3;
						fbi++;
						break;
					}
					else{
						printf("\n\n4. If'te Sorun var.\n\n");
					}
				}
				
				else if(ord[ag].kd==3.0){
				if(ord[ag+1].kd==0){
						printf("\n\n3-Algoritmanin sonuna gelinmis.\n\n");
						break;
					}
					else if(ord[ag+1].kd==1.0){
						cod[fbi]=10;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==2.0){
						cod[fbi]=5;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==3.0){
						cod[fbi]=0;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==4.0){
						cod[fbi]=20;
						fbi++;
						break;
					}
					else{
						printf("\n\n4. If'te Sorun var.\n\n");
					}
				}
				
				else if(ord[ag].kd==4.0){
				if(ord[ag+1].kd==0){
						printf("\n\n 4-Algoritmanin sonuna gelinmis.\n\n");
						break;
					}
					else if(ord[ag+1].kd==1.0){
						cod[fbi]=12;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==2.0){
						cod[fbi]=3;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==3.0){
						cod[fbi]=20;
						fbi++;
						break;
					}
					else if(ord[ag+1].kd==4.0){
						cod[fbi]=0;
						fbi++;
						break;
					}
					else{
						printf("\n\n4. If'te Sorun var.\n\n");
					}
				}
				
				else{
					
					break;
				}
			}
   		}
   		
	for(int u=0;u<=50;u++){
			srre[u]=0;
					    	}
		
		int ba=0;
		sree[1]=0;
	
		for(int v=1;v<=p;v++){
			ba=ba+sree[v];
			srre[v]=ba;
			
		}
		int bob=0;
		int cdi[50];
		
		for(int v=0;v<=50;v++){
			cdi[v]=0;
		}
		for(int v=1;v<=p;v++){
			bob=bob+cod[v];
			cdi[v]=bob;
		}
		
		sd = fopen("Schedule.txt","w");
		
		if( sd==NULL ){
			printf("Schedule dosyasý acilamadi.\n\n");
			exit(1);
		}
			for(int u=0;u<p;u++){
			fprintf(sd,"%d;%d;%d;%d;%d\n",(srre[u+1]),ord[u+1].kd,ord[u+1].sra,ord[u+1].mktr,cdi[u+1]);
		}
		
		system("PAUSE");
		return 0;
}

// iþlerin yapýlma sürelerini hesaplayan fonksiyon
double *sreBul(double p){
	int s=1;
	double sure[50];
	double *abc=&sure[0];
	
	
	
	for(s;s<=p;s++){
		if(opr[1].kod==ord[s].kd){
		  sure[s] =  ord[s].mktr /  opr[1].hiz;
		}
		
		else if(opr[2].kod==ord[s].kd){
		 sure[s]=ord[s].mktr / opr[2].hiz;
		}
		
		else if(opr[3].kod==ord[s].kd){
		 sure[s]=ord[s].mktr / opr[3].hiz;
		}
		
		else if(opr[4].kod==ord[s].kd){
		 sure[s]=ord[s].mktr / opr[4].hiz;
		}
		
		else {
		printf("Ýslem yapilamadi.\n\n");
		}
		int s=1;
   }		
	return abc;
   
}


