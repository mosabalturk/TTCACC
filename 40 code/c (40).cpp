// Miray Asena AYGÜZEL
// 1306140038
// Date:20.06.2016
// Development Enviorement : Visual Studio2015 





#include<stdio.h>
#include<stdlib.h>

typedef struct operation {
	int operationcode;
	int duration;
	
}operation;
typedef struct setupduration {
	int operationcode1;
	int operationcode2;
	int setupduration;

}setup;
typedef struct orders {
	int ordercode;
	int amountofwork;
	int operationcode;
	int deadline;

}orders;
typedef struct schedule {
	int scheduletime;
	int operationcode;
	int ordercode;
	int amountofwork;
	int setupoverhead;


}schedule;

int operationfile() {
	FILE *di = NULL;
	int counter = 0;
	char temp[100];
	int oprcount = 0;
	operation *operationlist = NULL;
	di = fopen("Operation.txt", "r");
	if (di == NULL) {
		printf("Dosya bulunamadi.\n");
	}
	while (!feof(di)) {
		fgets(temp, 100, di);
		counter++;
	}
	fclose(di);
	oprcount = counter;

	operationlist = (operation*)malloc(sizeof(operation)*counter);
	di = fopen("operation.txt", "r");
	counter = 0;
	while (!feof(di)) {
		fscanf(di, "%d;%d", &operationlist[counter].operationcode, &operationlist[counter].duration);
		counter++;
	}

	fclose(di);

}
int setupfile() {
	FILE *di = NULL;
	int counter = 0;
	int stpcount = 0;
	char temp[100];
	setup*setuplist = NULL;
	di = fopen("SetupDuration.txt", "r");
	if (di == NULL) {
		printf("Dosya bulunamadi\n");
	}
	while (!feof(di)) {
		fgets(temp, 100, di);
		counter++;

	}
	fclose(di);
	stpcount = counter;
	setuplist = (setup*)malloc(sizeof(setup)*counter);
	di = fopen("SetupDuration.txt", "r");
	counter = 0;
	while (!feof(di)) {

		fscanf(di, "%d;%d;%d", &setuplist[counter].operationcode1, &setuplist[counter].operationcode2, &setuplist[counter].setupduration);
		fscanf(di, "%d;%d;%d", &setuplist[counter].operationcode2, &setuplist[counter].operationcode1, &setuplist[counter].setupduration);
		counter++;
	}
	fclose(di);

}
int ordersfile() {
	FILE *di = NULL;
	int counter = 0;
	int ordcounter = 0;
	char temp[100];
	orders *orderslist = NULL;
	di = fopen("Orders.txt", "r");
	if (di == NULL) {
		printf("Dosya bulunamadi\n");

	}
	while (!feof(di)) {
		fgets(temp, 100, di);
		counter++;
	}
	fclose(di);
	ordcounter = counter;
	orderslist = (orders*)malloc(sizeof(orders)*counter);
	di = fopen("Orders.txt", "r");
	counter = 0;
	while (!feof(di)) {

		fscanf(di, "%d;%d;%d;%d", &orderslist[counter].ordercode, &orderslist[counter].amountofwork, &orderslist[counter].operationcode, &orderslist[counter].deadline);

		counter++;

	}
	fclose(di);
	sýralama(int orderslist.deadline, int orderslist.ordercode, int orderslist.amountofwork, int orderslist.operationcode, int ordcounter);
	scheduleolustur(ordcounter);
}
int sýralama(int dizi1[],int dizi2[],int dizi3[],int dizi4[],int elemansayisi) {
	
	int temp1=0,temp2=0,temp3=0,temp4=0;
	int i, j;

	for (i = 0; i < elemansayisi; i++) {
		for (j = 0; j < elemansayisi - i; j++) {

			if (dizi1[j] > dizi1[j + 1]) {
				 
				temp1 = dizi1[j];
				dizi1[j] = dizi1[j + 1];
				dizi1[j + 1] = temp1;

				temp2 = dizi2[j];
				dizi2[j] = dizi2[j + 1];
				dizi2[j + 1] = temp2;

				temp3 = dizi3[j];
				dizi3[j] = dizi3[j + 1];
				dizi3[j + 1] = temp3;
				temp4 = dizi4[j];
				dizi4[j] = dizi4[j + 1];
				dizi4[j + 1] = temp4;
			}
		}

	}

}
int scheduleolustur(int elemansayisi) {
	int scheduletime = 0, setupoverhead = 0,amountofwork=0,int a=0,b=0,c=1;
	schedule *schedulelist;
	schedulelist = (schedule*)malloc(sizeof(schedule)*elemansayisi);
	for (int k = 0; k < elemansayisi; k++) {

		schedulelist[k].ordercode = orderslist[k].ordercode;
		schedulelist[k].operationcode = orderslist[k].operationcode;
		schedulelist[k].amountofwork = orderslist[k].amountofwork;
		a = orderslist[k].operationcode;
		if (a == operationlist[k].operationcode) {
			b = operationlist[k].duration;
			scheduletime= + schedulelist[k].amountofwork / b;
			if (c == k) {
				schedulelist[k].setupoverhead = setuplist[k].setupduration;
				c++;
			}

		}

		printf("%d;%d;%d;%d",schedulelist[k].scheduletime, schedulelist[k].operationcode, schedulelist[k].ordercode, schedulelist[k].amountofwork, schedulelist[k].setupoverhead);

		schedulelist[k].scheduletime = schedulelist[k].setupoverhead;

	}

}

int main() {

	operationfile();
	setupfile();
	ordersfile();
	return 0;
}