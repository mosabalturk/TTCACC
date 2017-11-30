// Beyza �NCE
//1306140074
//20.05.2016
// Development Enviorement : Visual Studio2013
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
//Operations SetupDurations ve Orders � Struct seklinde tan�mlad�k.
struct nodeOperation {
	int opType;
	double power;
	struct nodeOperation *next;

};
struct nodesetUp {
	int op1;
	int op2;
	int time;
	struct nodesetUp *next;
};

struct nodeOrder {
	int orderID;
	double workA;
	int opType;
	int deadline;

	struct nodeOrder *next;
};
// Structlar� ba�l� listeye ve ba�l� listelerin  ba�lang�� degerlerini  kaybolmamas� i�in headlere atay�p global olarak saklad�k.

struct nodeOrder *head1 = NULL;
struct nodeOrder *curr1 = NULL;

struct nodesetUp *head2 = NULL;
struct nodesetUp *curr2 = NULL;

struct nodeOperation *head3 = NULL;
struct nodeOperation *curr3 = NULL;

int orders()
{
	FILE *fp;
	int o, p, q, r;
	// Dosyalar� okuduk. Ald�g�m�z verileri structlara atay�p malloc ile yer ay�rd�k
	errno_t err;
	err = fopen_s(&fp, "C:\\Users\\Sony\\Desktop\\Orders.txt", "r");

	if (err == 0) {
		while (fscanf(fp, "%d; %d; %d; %d", &o, &p, &q, &r) > 0)
		{
			if (head1 == NULL) {
				struct nodeOrder *ptr1 = (struct nodeOrder*)malloc(sizeof(struct nodeOrder));

				if (ptr1 == NULL)
				{
					printf("\n Node creation failed \n");
					return NULL;
				}
				ptr1->orderID = o;
				ptr1->workA = p;
				ptr1->opType = q;
				ptr1->deadline = r;
				ptr1->next = NULL;

				head1 = curr1 = ptr1;

			}
			else {
				struct nodeOrder *ptr1 = (struct nodeOrder*)malloc(sizeof(struct nodeOrder));

				ptr1->orderID = o;
				ptr1->workA = p;
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
	err = fopen_s(&fp, "C:\\Users\\Sony\\Desktop\\SetupDuration.txt", "r");

	if (err == 0) {
		while (fscanf(fp, "%d; %d; %d", &o, &p, &q) > 0)
		{
			if (head2 == NULL) {
				struct nodesetUp *ptr2 = (struct nodesetUp*)malloc(sizeof(struct nodesetUp));
				if (ptr2 == NULL)
				{
					printf("\n Node creation failed \n");
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
	err = fopen_s(&fp, "C:\\Users\\Sony\\Desktop\\Operations.txt", "r");

	if (err == 0) {
		while (fscanf(fp, "%d; %d", &o, &p) > 0)
		{
			if (head3 == NULL) {
				struct nodeOperation *ptr3 = (struct nodeOperation*)malloc(sizeof(struct nodeOperation));
				if (ptr3 == NULL)
				{
					printf("\n Node creation failed \n");
					return NULL;
				}
				ptr3->opType = o;
				ptr3->power = p;

				head3 = curr3 = ptr3;


			}

			else {
				struct nodeOperation *ptr3 = (struct nodeOperation*)malloc(sizeof(struct nodeOperation));

				ptr3->opType = o;
				ptr3->power = p;
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
// Operation type yoluyla bir dakikada yap�lan i� miktar�n� okuduk
struct nodeOperation* getdurations(int myid)
{
	struct  nodeOperation* tmp = head3;
	while (tmp != NULL)
	{
		if (tmp->opType == myid)
			return tmp;
		tmp = tmp->next;
	}

}

// Yer Degisterme Fonksiyonu tan�mlad�k
void swap(struct nodeOrder *a, struct nodeOrder *b)
{
	struct nodeOrder* temp = a;
	a = b;
	b = temp;
}
 

// �ki Order aras�ndaki setupDurationlar� bulduk
struct nodesetUp* getsetUp(int id1, int id2)
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

// Herbir orderin i� s�resini deadline dan c�kar�p bir s�ralama algoritmas� olusturduk.

//  Bu Bubble sortta zaman� daha k�s�tl� olan siparisi �st s�ralara tas�d�k.

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
			if (nd->deadline - ceil(nd->workA / getdurations(nd->opType)->power)> nx->deadline - ceil(nx->workA / getdurations(nx->opType)->power))
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


int main()
{

	orders();
	setupduration();
	operations();
	
	//Bubble sortta elde ettigimiz current timelar� ve verileri Schedule1 dosyas�na yazd�k.
	// Orderlarda verilen i� miktarlar�n� operasyon y�ntemlerini ve iki operasyon aras�ndaki bekleme s�relerini ba�l� listelerden alabilmek i�im tmp2 adl� bir struct tan�mlad�k.
	double current_time = 0;
	struct nodeOrder* tmp = head1;
	FILE*fp;
	errno_t err2;
	err2 = fopen_s(&fp, "C:\\Users\\Sony\\Desktop\\Schedule.txt", "w");
	
	//ba�l� listenin son eleman�na varana kadar d�ng�ye sokup i� s�relerini bulduk ve anl�k zamana ekledik.
	bubbleSort(&tmp);
	fprintf(fp,"cUrrTime=0\n\n");
	while (tmp != NULL)
	{
	
		
		
		if (tmp->next != NULL){
			//ba�l� listenin son eleman� null a e�it olaca�� i�in hesaplamlar� ikiye b�ld�k.
			//her g�revden sonra anl�k ge�en zaman� hesaplad�k.
			current_time = current_time + ceil((double)tmp->workA / (double)getdurations(tmp->opType)->power) + (double)getsetUp(tmp->opType, tmp->next->opType)->time;
			// Orderlar�n yeni s�ras�n� ve zaman� dosyaya yazd�rd�k.
			fprintf(fp, "CUrrTime=%f\nOpType=%d\norder=%d\nworka=%f\n\nsetupOverhead=%d\n", current_time, tmp->opType, tmp->orderID, tmp->workA, getsetUp(tmp->opType, tmp->next->opType)->time);
		}
		else
		{

			current_time = current_time + ceil(tmp->workA / getdurations(tmp->opType)->power);
			fprintf(fp, "CUrrTime=%f\nOpType=%d\norder=%d\nworka=%f\n\n", current_time, tmp->opType, tmp->orderID, tmp->workA);
		}
	
		//Ba�l� listede bir sonraki g�reve ge�tik.
		tmp = tmp->next;
	}
	fclose(fp);
	system("pause");
	return 0;


}



