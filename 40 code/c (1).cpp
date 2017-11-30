
// ALPER YILMAZ
//1306140064
//Visual Studio 2013 ile yapildi


#include <stdio.h>
#include <stdlib.h>
#include <math.h>
//Operations SetupDurations ve Orders � Struct seklinde tan�mlad�k
struct nodeOperation {
	int opType;
	double workPerMin;
	struct nodeOperation *next;
};
struct nodesetUp {
	int op1;
	int op2;
	int time;
	struct nodesetUp *next;
};
struct nodeOrder {

	int orNum;
	double amountOfWork;
	int opType;
	int deadline;

	struct nodeOrder *next;
};

// Her bir Struct� ba�l� listeye atay�p ba�lang�� degerlerini headlere atay�p global olarak saklad�m

struct nodeOrder *head1 = NULL;
struct nodeOrder *curr1 = NULL;

struct nodesetUp *head2 = NULL;
struct nodesetUp *curr2 = NULL;

struct nodeOperation *head3 = NULL;
struct nodeOperation *curr3 = NULL;

int orders();  //ORDERLARI ALDIGIMIZ FONKS�YONUN PROTOT�P�
int setupduration(); //SETUP DURAT�ONLARI ALDIGIMIZ FONKS�YONUN PROTOT�P�
int operations();//OPERAT�ONS ALDIGIMIZ FONKS�YONUN PROTOT�P�
void bubbleSort(struct nodeOrder**); //SIRALAMAYI YAPMAK �C�N KULLANDIGIMIZ FONKS�YON // EN VER�ML� SIRALAMAYI VEREN ��LEM� YAPTIK
struct nodeOperation* getworkPerMin(int);// �SLEMLER�M�Z SIRASINDA OPERAT�ONLARIN DAK�KADA YAPTIGI ��LER� ALDIGIMIZ FONKS�YON
struct nodesetUp* getsetUpTimes(int, int); // GEC�SLER ARASINDAK� BEKLEME SURELER�N� ALDIGIMIZ FONKS�YON

int main()
{

	orders();
	setupduration();
	operations();
  // ZAMAN SAYACI TANIMLADIK
	double time = 0;
	

	struct nodeOrder* order = (struct nodeOrder*)malloc(sizeof(struct nodeOrder));

	order = head1;
	
	FILE*scheduleFile;
	errno_t err1;
	err1 = fopen_s(&scheduleFile, "Schedule.txt", "w");

	if (scheduleFile == NULL)
		printf("Error!");

	bubbleSort(&order);

	while (order != NULL)
	{
		fprintf(scheduleFile, "%.1f;%d;%d;%.1f;\n", time, order->opType, order->orNum, order->amountOfWork);

		// SETUP OVERHEAD DEGERLER�N� BU ��LEMLE BULUYORUZ ->(double)getsetUpTimes(order->opType,order->next->opType)->time
		// ANCAK YAZARKEN HATA VERD�. PROGRAMIN CALISMASI �C�N O DEGER� KULLANMADIM
		
		if (order->next != NULL)
		{
			time+= ceil(((double)order->amountOfWork / (double)getworkPerMin(order->opType)->workPerMin)) + (double)getsetUpTimes(order->opType,order->next->opType)->time;
		}
		else{
			time+= ceil(((double)order->amountOfWork / (double)getworkPerMin(order->opType)->workPerMin));
		}

		order = order->next;
		

	}

	fclose(scheduleFile);

	
	system("pause");
	return 0;


}
int orders()
{
	FILE *fp;
	int o, p, q, r;
	// Dosyalar� okuduk. Ald�g�m�z verileri structlara atay�p malloc ile yer ay�rd�k
	errno_t err;
	err = fopen_s(&fp, "Orders.txt", "r");

	if (err == 0) 
	{
		while (fscanf(fp, "%d; %d; %d; %d", &o, &p, &q, &r) > 0)
		{
			if (head1 == NULL) {
				struct nodeOrder *ptr1 = (struct nodeOrder*)malloc(sizeof(struct nodeOrder));

				if (ptr1 == NULL)
				{
					printf("\n Dugum tan�mlanamadi \n");

					return NULL;
				}
				ptr1->orNum = o;
				ptr1->amountOfWork = p;
				ptr1->opType = q;
				ptr1->deadline = r;
				ptr1->next = NULL;

				head1 = curr1 = ptr1;

			}
			else {
				struct nodeOrder *ptr1 = (struct nodeOrder*)malloc(sizeof(struct nodeOrder));

				ptr1->orNum = o;
				ptr1->amountOfWork = p;
				ptr1->opType = q;
				ptr1->deadline = r;

				ptr1->next = NULL;


				curr1->next = ptr1;

				curr1 = ptr1;
			}
		}
		fclose(fp);
	}

	else {
		perror("Orders.txt");
	}
}
int setupduration()
{
	// Dosyalar� okuduk. Ald�g�m�z verileri structlara atay�p malloc ile yer ay�rd�k


	FILE *fp;

	int o, p, q;

	errno_t err;
	err = fopen_s(&fp, "SetupDuration.txt", "r");

	if (err == 0) {
		while (fscanf(fp, "%d; %d; %d", &o, &p, &q) > 0)
		{
			if (head2 == NULL) {
				struct nodesetUp *ptr2 = (struct nodesetUp*)malloc(sizeof(struct nodesetUp));
				if (ptr2 == NULL)
				{
					printf("\n Dugum tan�mlanamadi \n");
					return NULL;
				}

				ptr2->op1 = o;
				ptr2->op2 = p;
				ptr2->time = q;
				ptr2->next = NULL;


				head2 = curr2 = ptr2;

			}
			else
			{
				struct nodesetUp *ptr2 = (struct nodesetUp*)malloc(sizeof(struct nodesetUp));

				ptr2->op1 = o;
				ptr2->op2 = p;
				ptr2->time = q;
				ptr2->next = NULL;

				curr2->next = ptr2;

				curr2 = ptr2;

			}
		}
		fclose(fp);
	}
	else {
		perror("SetupDurations.txt");
	}

}

int operations()
{
	// Dosyalar� okuduk. Ald�g�m�z verileri structlara atay�p malloc ile yer ay�rd�k

	FILE *fp;
	int i = 0;
	int o, p;

	errno_t err;
	err = fopen_s(&fp, "Operations.txt", "r");

	if (err == 0) {
		while (fscanf(fp, "%d; %d", &o, &p) > 0)
		{
			if (head3 == NULL) {
				struct nodeOperation *ptr3 = (struct nodeOperation*)malloc(sizeof(struct nodeOperation));
				if (ptr3 == NULL)
				{
					printf("\n Dugum tan�mlanamadi \n");
					return NULL;
				}
				ptr3->opType = o;
				ptr3->workPerMin = p;

				head3 = curr3 = ptr3;


			}

			else {
				struct nodeOperation *ptr3 = (struct nodeOperation*)malloc(sizeof(struct nodeOperation));

				ptr3->opType = o;
				ptr3->workPerMin = p;
				ptr3->next = NULL;

				curr3->next = ptr3;

				curr3 = ptr3;

			}
		}

		fclose(fp);
	}

	else {
		perror("SetupDurations.txt");
	}
}
void bubbleSort(struct nodeOrder** head)
{
	int done = 0;


	if (*head == NULL || (*head)->next == NULL) return;

	while (!done) {
		struct nodeOrder **pv = head;
		struct nodeOrder *nd = *head;
		struct nodeOrder *nx = (*head)->next;

		done = 1;

		while (nx) {
			int cmp;
			if (nd->deadline > nx->deadline)
				cmp = 1;
			else
				cmp = 0;

			if (cmp > 0) {
				nd->next = nx->next;
				nx->next = nd;
				*pv = nx;

				done = 0;
			}
			pv = &nd->next;
			nd = nx;
			nx = nx->next;
		}
	}
}


// Bir operation type in bir dakikada ne kadar is yapt�klar�n� alan fonksiyon

struct nodeOperation* getworkPerMin(int myid)
{
	struct  nodeOperation* tmp = head3;
	while (tmp != NULL)
	{
		if (tmp->opType == myid)
			return tmp;
		tmp = tmp->next;
	}

}

// �ki Order aras�ndaki setupDurationlar� bulduk

struct nodesetUp* getsetUpTimes(int id1, int id2)
{
	struct  nodesetUp* tmp = head2;


	while (tmp != NULL)
	{
		if (tmp->op1 == id1 && tmp->op2 == id2)
		{

			return tmp;
		}

		tmp = tmp->next;
	}

}



