//			  Said ÖZGAT        
//            1306140056         
//            Date : 20.05.2016          
//            Development Enviorement : Visual Studio 2013 
#include <stdio.h>
#include <stdlib.h>
struct struct_operations
{
	int operation_code;							//operasyonlarý bir struct içine alalým dedik.Bu sayede onlara eriþim daha iyi olacak.
	int duration;								
	struct struct_operations *next;				//next ile ileri bir veriye gidiyoruz.
};
struct queue_operations
{
	struct struct_operations *head;				//kuyruk structi oluþturduk ve operasyon structuna aldýðýmýz verileri bunun içine pointer olarak aldýk.
	struct struct_operations *tail;				//pointerlar var.Onlarla kuyruk içerisinde verileri tutuyoruz.baþa ekliyoruz ve sýrayla eklemeye devam 
												//ediyoruz.
	int size;
};
struct struct_setupDuration
{
	int operation_code_1;		
	int operation_code_2;
	int duration;
	struct struct_setupDuration *next;
};
struct queue_setupDuration
{
	struct struct_setupDuration *head;
	struct struct_setupDuration *tail;

	int size;
};
struct struct_order
{
	int orderCode;
	int amountOfWork;
	int operationCode;
	int deadLine;

	int avgDoneTime;

	struct struct_order *next;
	struct struct_order *prev;
};
struct queue_orders
{
	struct struct_order *head;
	struct struct_order *tail;

	

	int size;
};
struct struct_output
{
	int scheduleTime;
	int operationCode;
	int orderCode;
	int amountOfWork;
	int setupOverhead;

	struct struct_output *next;
};
struct queue_output
{
	struct struct_output *head;
struct struct_output *tail;

int size;
};


struct queue_setupDuration *queue_stpdrt;
struct queue_operations *queue_opr;
struct queue_orders *queue_order;
struct queue_output *queue_result;

char filename_opr[] = "Operations.txt";
char format_opr[] = "%d;%d";
char filename_stpdrt[] = "SetupDuration.txt";
char format_stpdrt[] = "%d;%d;%d";
char filename_orders[] = "Orders.txt";
char format_orders[] = "%d;%d;%d;%d";
char filename_output[] = "Schedule.txt";
char format_output[] = "%d;%d;%d;%d;%d\n";
int totaltime;


void readFile_operations(){				//bir tane readFile fonksiyonu oluþturduk.
	FILE *file = fopen(filename_opr, "r");

	if (file != NULL)
	{
		printf("OPERATIONS\n");
		while (!feof(file))				//dosyadaki karakter bitene kadar çalýþ demek burasý
		{
			struct struct_operations *n = (struct struct_operations*)malloc(sizeof(struct struct_operations));
			fscanf(file, format_opr, &n->operation_code, &n->duration);  //dosya adý, hangi formatta olacaðý %d;%d formatýnda bizde ,deðerler
			printf("%d --- %d", n->operation_code, n->duration);	//ekrana yazdýrdýk
			printf("\n");				//aslýnda üstteki kod yapýsý ile ramde belirli bi yer ayýrdýk ve onlarý bir struct içine attýk.
			n->next = NULL;

			if (queue_opr->head == NULL) {
				queue_opr->head = n;
			}
			else {
				queue_opr->tail->next = n;
			}

			queue_opr->tail = n;
			queue_opr->size++;
		}

		fclose(file);
	}
	else
	{
		perror(filename_opr); /* why didn't the file open? */
	}


}

void readFile_setupDuration(){
	FILE *file = fopen(filename_stpdrt, "r");

	if (file != NULL)
	{
		printf("SETUPDURATIONS\n");
		while (!feof(file))				//dosyadaki karakter bitene kadar çalýþ demek burasý
		{
			struct struct_setupDuration *n = (struct struct_setupDuration*)malloc(sizeof(struct struct_setupDuration));
			fscanf(file, format_stpdrt, &n->operation_code_1, &n->operation_code_2, &n->duration);
			printf("%d --- %d ---- %d", n->operation_code_1, n->operation_code_2, n->duration);
			printf("\n");
			n->next = NULL;

			if (queue_stpdrt->head == NULL) {
				queue_stpdrt->head = n;
			}
			else {
				queue_stpdrt->tail->next = n;
			}

			queue_stpdrt->tail = n;
			queue_stpdrt->size++;
		}

		fclose(file);
	}
	else
	{
		perror(filename_stpdrt); /* why didn't the file open? */
	}


}
/*
BU FONKSÝYON KUYRUKTA HÝÇ ELEMAN YOKSA NULL DÖNDÜRÜR.
deadline'DAN DAHA BÜYÜK BÝR DEÐER YOKSA NULL DÖNER.

*/
struct struct_order* getMaxDeadlineOrder( int deadline){
	if (queue_order->head == NULL){
		return NULL;
	}
	struct struct_order *item = queue_order->head;
	for (int i = 0; i < queue_order->size; i++)
	{
		if (item->deadLine > deadline){
			return item;
		}
		item = item->next;
	}
	return item;
}

int getDurationTime(int operationCode){
	struct struct_operations *item = NULL;
	if (queue_opr->head == NULL){
		return 0;
	}
	else{
		item = queue_opr->head;
	}
	while (item->operation_code != operationCode){
		item = item->next;
	}
	return item->duration;
}

void readFile_orders(){
	FILE *file = fopen(filename_orders, "r");

	if (file != NULL)
	{
		printf("ORDERS\n");
		while (!feof(file))				//dosyadaki karakter bitene kadar çalýþ demek burasý
		{
			struct struct_order *n = (struct struct_order*)malloc(sizeof(struct struct_order));
			fscanf(file, format_orders, &n->orderCode, &n->amountOfWork, &n->operationCode, &n->deadLine);
			n->next = NULL;
			n->prev = NULL;

			int durationTime = getDurationTime(n->operationCode);

			float avgTime = (float)n->amountOfWork / (float)durationTime;

			int dec = (int)avgTime;
			float flo = avgTime - dec;

			if (flo > 0){
				dec++;
			}

			n->avgDoneTime = dec;

			struct struct_order *maxDeadLineItem = getMaxDeadlineOrder(n->deadLine);

			struct struct_order *temp = NULL;
			if (maxDeadLineItem == NULL){
				
				temp = queue_order->tail;

				if (queue_order->head == NULL) {
					queue_order->head = n;
				}else{
					queue_order->tail->next = n;
				}

				queue_order->tail = n;
				queue_order->tail->prev = temp;
			}
			else{
				n->prev = maxDeadLineItem->prev;
				n->next = maxDeadLineItem;

				if (maxDeadLineItem->prev == NULL){
					maxDeadLineItem->prev = n;

					queue_order->head = n;
					if (maxDeadLineItem->next == NULL){
						queue_order->tail = maxDeadLineItem;
					}
				}
				else{
					maxDeadLineItem->prev->next = n;
					maxDeadLineItem->prev = n;
				}
			}

			queue_order->size++;
			//printf("%d --- %d ---- %d ----- %d --->>> AVGTIME: %d \n", n->orderCode, n->amountOfWork, n->operationCode, n->deadLine, n->avgDoneTime);
		}
		
		printf("Tailden Yazdik \n");
		struct struct_order *n = queue_order->tail;
		
		for (int i = 0; i < queue_order->size; i++)
		{
			printf("%d-- - %d---- %d---- - %d---> >> AVGTIME: %d \n", n->orderCode, n->amountOfWork, n->operationCode, n->deadLine, n->avgDoneTime);
			n = n->prev;
		}
		

		fclose(file);
	}
	else
	{
		perror(filename_orders); /* why didn't the file open? */
	}


}

void writeFile_output(){
	FILE *file = fopen(filename_output, "w");
	if (file != NULL)
	{	
		struct struct_output *item = NULL;
		if (queue_result->head == NULL){
			return;
		}
		else{
			item = queue_result->head;
		}

		while (item != NULL){
			fprintf(file, format_output, item->scheduleTime, item->operationCode, item->orderCode, item->amountOfWork, item->setupOverhead);
			item = item->next;
		}
		fprintf(file, "\n\tTOTAL TIME: %d", totaltime);
		fclose(file);
	}
}

int getSetupDuration(int from, int to){
	
	if (from == to){
		return 0;
	}
	
	struct struct_setupDuration *n = queue_stpdrt->head;
	for (int i = 0; i < queue_stpdrt->size; i++)
	{
		if ((n->operation_code_1 == from && n->operation_code_2 == to) || (n->operation_code_1 == to && n->operation_code_2 == from)){
			return n->duration;
		}
		n = n->next;
	}
}

void addScheduleQueue(struct struct_output *n){
	if (queue_result->head == NULL) {
		queue_result->head = n;
	}
	else {
		queue_result->tail->next = n;
	}
	queue_result->tail = n;
	queue_result->size++;
}

struct struct_order* getNextProcessItem(struct struct_order *order, int currentTime){
	
	if (order->next == NULL){
		return NULL;
	}
	//return order->next;
	// þuandan itibaren sonraki order'in avgTime'ýný sonraki order'in deadline'ýndan çýkartýyoruz
	// þuanki orderden sonraki order deadline'ý arasýnda ne kadar zaman var? cevap -> nextCalculatedTime
	int nextCalculatedTime = (order->next->deadLine) - (order->next->avgDoneTime + currentTime + getSetupDuration(order->operationCode,order->next->operationCode));
	struct struct_order *item = order->next->next;
	int diff = nextCalculatedTime;
	struct struct_order *temp = NULL;
	while (item != NULL){

		if (item->operationCode == order->operationCode && (nextCalculatedTime - (item->avgDoneTime) >= 0 && nextCalculatedTime - (item->avgDoneTime) < diff)){
			diff = nextCalculatedTime - (item->avgDoneTime);
			temp = item;
		}

		item = item->next;
	}

	//Bulunan elemaný(temp) order sýrasýnda yer deðiþtirir.
	if (temp!=NULL)
	{
		temp->prev->next = temp->next;

		if (temp->next != NULL){
			temp->next->prev = temp->prev;
		}

		temp->prev = order;
		temp->next = order->next;

		if (order->next != NULL){
			order->next->prev = temp;
		}
		order->next = temp;

		printf("SIRA: %d OP CODE: %d \n", temp->orderCode, temp->operationCode);
		return temp;
		
	}
	else
	{

	}


	return order->next;
}

int prepareOutputElementAndCalculateTime(struct struct_output *n, struct struct_order *o, int currentTime){
	n->scheduleTime = currentTime;
	n->operationCode = o->operationCode;
	n->orderCode = o->orderCode;
	n->amountOfWork = o->amountOfWork;
	if (o->prev == NULL){
		n->setupOverhead = 0;
	}
	else{
		n->setupOverhead = getSetupDuration(o->prev->operationCode, o->operationCode);
	}
	return (currentTime + o->avgDoneTime + n->setupOverhead);
}

int processOrders(){
	int currentTime=0;
	struct struct_order *o  = queue_order->head;

	for (int i = 0; i < queue_order->size; i++)
	{
		struct struct_output *n = (struct struct_output*)malloc(sizeof(struct struct_output));
		n->next = NULL;

		currentTime = prepareOutputElementAndCalculateTime(n, o, currentTime);
		addScheduleQueue(n);
		o = getNextProcessItem(o, currentTime);
	}

	return currentTime;
}

int main(){
	
	queue_opr = (struct queue_operations*)malloc(sizeof(struct queue_operations));
	queue_opr->head = NULL;
	queue_opr->size = 0;
	readFile_operations();

	queue_stpdrt = (struct queue_setupDuration*)malloc(sizeof(struct queue_setupDuration));
	queue_stpdrt->head = NULL;
	queue_stpdrt->size = 0;
	readFile_setupDuration();

	queue_order = (struct queue_orders*)malloc(sizeof(struct queue_orders));
	queue_order->head = NULL;
	queue_order->tail = NULL;
	queue_order->size = 0;
	readFile_orders();
	
	queue_result = (struct queue_output*)malloc(sizeof(struct queue_output));
	queue_result->head = NULL;
	queue_result->size = 0;

	totaltime = processOrders();

	writeFile_output();
	return 0;
}