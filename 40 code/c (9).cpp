//     Fýrat ÖNDER
//     1306150062
// 	   Date : 20.5.2016
//     Development Enviorement: Dev-C++


#include <stdio.h>
#include <stdlib.h>

int main ()
{

//operations veri çekme
int dzopr[1000],opr=0,tmp=0;
FILE* operations;

operations=fopen("Operations.txt", "r+"); 

for(tmp=1;;tmp++){
	
		fscanf(operations,"%d;%d",&opr,&dzopr[tmp]); 
	
		if(opr!=tmp) break;
}

fclose(operations);
///////////////////////////////////////////////////////////////////////

//orders veri çekme
int dzwork[1000],dztype[1000],dzdeadline[1000],ord=0,cmp=0;
FILE* orders;
orders=fopen("Orders.txt","r+");

for(cmp=1;;cmp++){
		fscanf(orders,"%d;%d;%d;%d",&ord,&dzwork[cmp],&dztype[cmp],&dzdeadline[cmp]);
		if(dzwork[cmp]==0) break;
}
fclose(orders);
//////////////////////////////////////////////////////////////////////

//SetupDuration   veri çekme
int from=0,to=0,drt=0,kmp=0,dzduration[100][100];
FILE* duration;
duration=fopen("SetupDuration.txt","r+");

while(1){
	fscanf(duration,"%d;%d;%d",&from,&to,&drt);
	if(dzduration[from][to]==0){

	dzduration[from][to]=drt;
	dzduration[to][from]=drt;
		}

	else break;
		
}

fclose(duration);
//////////////////////////////////////////////////////////////////////

//Sipariþ hazýrlanma süreleri

int work=0,typework=0,a=0,dzwt[1000],speed=0;

for(a=1;a<ord+1;a++){
	work=dzwork[a];
	typework=dztype[a];
	if(work%dzopr[typework]==0) speed=work/dzopr[typework];
	else speed=(work/dzopr[typework])+1;
	dzwt[a]=speed;


	
}









}
