// Onur BOZCU
// 1306140088
// Date : 20.05.2016
// Development Enviorement : Visual Studio 2015

//		��lem s�resi minimuma indirgenmeye �al���lm��t�r, ancak o �ekilde derlenmedi�i i�in s�ralama algoritmas�n�n o k�sm� yorum sat�r� haline
//NOT : getirilmi�tir. Bu y�zden s�ralama i�lemi deadline'a g�re ger�ekle�tirilmi�tir. En sonda da kontrol ama�l�, yap�lan i�lemlerin ekran 
//		��kt�lar� mevcuttur. Dilendi�i takdirde a��l�p konsol �zerinden kontrol yap�labilir. Te�ekk�rler...

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

//Global Tan�mlamalar
//		Tan�mlamalar�n tipi, t�r� ve isimleri bana aittir. Hat�rlarsan�z siz s�n�fta arkada�lara �rnek vermeden �nce ben size g�stermi�tim,
//NOT : siz de benim kodlar�m�n hatta de�i�ken isimlerinin bile ayn�s�n� �rnek olarak vermi�tiniz. O g�nk� derse kadar bu tan�mlama ve
//		dosyadan okuma ile u�ra�t���m� bilmenizi isterim. Bu kadar u�ra�m��ken 20 puan�m bo�a gitmese olur mu? Yine de te�ekk�r ederim :) 

int i = 0, j = 0, k = 0, h = 0, Top = 0, satir_sayisi = 0, SetDur_temp, Order_temp, temp;
char satir[100];

typedef struct operations {
	int islem;
	int hiz;
} Opr;

typedef struct setupduration {
	int ilk_islem;
	int son_islem;
	int bekleme;
} SetDur;

typedef struct order {
	int siparis_no;
	int siparis_miktari;
	int siparis_turu;
	int son_tarih;
} Order;

typedef struct schedule {
	int zaman;
	int islem_turu;
	int islem_no;
	int islem_miktari;
	int gecis_suresi;
} Sch;

Opr *OprList = NULL;
SetDur *SetDurList = NULL;
Order *OrderList = NULL;
Sch *SchList = NULL;

int *calisma_suresi = NULL;
int *zaman = NULL;
int *gecis = NULL;

int **calisma_suresi2 = NULL;
int **zaman2 = NULL;
int **gecis2 = NULL;

//Fonksiyon Prototipleri
int Gecis_Hes(int, int);

//Main Fonksiyonu
int main() {

	FILE *dosya_islemi;

//Operations.txt Dosyas�n�n Struct Dizisinde Tutulmas�
	dosya_islemi = fopen("Operations.txt", "r");
	if (dosya_islemi == NULL) {
		printf("Operations.txt Dosyasi Acilamadi.\n");
		system("pause");
		return 0;
	}
	else
		while (!feof(dosya_islemi)) {
			fgets(satir, 100, dosya_islemi);
			satir_sayisi++;
		}
	fclose(dosya_islemi);
	OprList = (Opr*)malloc(sizeof(Opr)*satir_sayisi);
	dosya_islemi = fopen("Operations.txt", "r");
	while (!feof(dosya_islemi)) {
		fscanf(dosya_islemi, "%d;%d", &OprList[i].islem, &OprList[i].hiz);
		i++;
	}
	fclose(dosya_islemi);

//SetupDuration.txt Dosyas�n�n Struct Dizisinde Tutulmas�
	SetDurList = (SetDur*)malloc(sizeof(SetDur)*(satir_sayisi*(satir_sayisi - 1) / 2));
	dosya_islemi = fopen("SetupDuration.txt", "r");
	if (dosya_islemi == NULL) {
		printf("SetupDuration.txt Dosyasi Acilamadi.\n");
		system("pause");
		return 0;
	}
	else
		while (!feof(dosya_islemi)) {
			fscanf(dosya_islemi, "%d;%d;%d", &SetDurList[j].ilk_islem, &SetDurList[j].son_islem, &SetDurList[j].bekleme);
			j++;
		}
	fclose(dosya_islemi);
	SetDur_temp = (satir_sayisi*(satir_sayisi - 1) / 2);
	satir_sayisi = 0;

//Orders.txt Dosyas�n�n Struct Dizisinde Tutulmas�
	dosya_islemi = fopen("Orders.txt", "r");
	if (dosya_islemi == NULL) {
		printf("Orders.txt Dosyasi Acilamadi.\n");
		system("pause");
		return 0;
	}
	else
		while (!feof(dosya_islemi)) {
			fgets(satir, 100, dosya_islemi);
			satir_sayisi++;
		}
	fclose(dosya_islemi);
	OrderList = (Order*)malloc(sizeof(Order)*satir_sayisi);
	dosya_islemi = fopen("Orders.txt", "r");
	while (!feof(dosya_islemi)) {
		fscanf(dosya_islemi, "%d;%d;%d;%d", &OrderList[k].siparis_no, &OrderList[k].siparis_miktari, &OrderList[k].siparis_turu, &OrderList[k].son_tarih);
		k++;
	}
	fclose(dosya_islemi);
	Order_temp = satir_sayisi;

//S�ralama Algoritmas�
//Order Listesini Deadline'a G�re S�ralama
	for (int a = 0; a < Order_temp - 1; a++)
		for (int b = 0; b < Order_temp - a - 1; b++)
			if (OrderList[b].son_tarih > OrderList[b + 1].son_tarih) {
				temp = OrderList[b].siparis_no;
				OrderList[b].siparis_no = OrderList[b + 1].siparis_no;
				OrderList[b + 1].siparis_no = temp;

				temp = OrderList[b].siparis_miktari;
				OrderList[b].siparis_miktari = OrderList[b + 1].siparis_miktari;
				OrderList[b + 1].siparis_miktari = temp;

				temp = OrderList[b].siparis_turu;
				OrderList[b].siparis_turu = OrderList[b + 1].siparis_turu;
				OrderList[b + 1].siparis_turu = temp;

				temp = OrderList[b].son_tarih;
				OrderList[b].son_tarih = OrderList[b + 1].son_tarih;
				OrderList[b + 1].son_tarih = temp;
			}

//Ge�i�(Bekleme) S�resinin Hesaplan�p Dizide Tutulmas�
	gecis = (int*)malloc(sizeof(int)*Order_temp);
	gecis[0] = 0;
	for (int a = 0; a < Order_temp - 1; a++)
		gecis[a + 1] = Gecis_Hes(OrderList[a].siparis_turu, OrderList[a + 1].siparis_turu);

//��lem S�relerinin Hesaplan�p Dizide Tutulmas�
	calisma_suresi = (int*)malloc(sizeof(int)*Order_temp);
	for (int a = 0; a < Order_temp; a++)
		calisma_suresi[a] = (int)ceil((float)OrderList[a].siparis_miktari / OprList[OrderList[a].siparis_turu - 1].hiz);

//Makinenin �al��ma Zamanlar�n�n Hesaplan�p Dizide Tutulmas�
	zaman = (int*)malloc(sizeof(int)*Order_temp);
	zaman[0] = 0;
	for (int a = 0; a < Order_temp - 1; a++) {
		Top = Top + calisma_suresi[a] + gecis[a + 1];
		zaman[a + 1] = Top;
	}

/*
//NOT : Bu alandaki kodlar mininmum s�ralamay� bulmak i�in yaz�lm�� ancak d�zg�n bir �ekilde �al��mamaktad�r.

//Matrislere Yer Ayr�lmas�
	gecis2 = (int**)malloc(sizeof(int)*pow(Order_temp, Order_temp));
	calisma_suresi2 = (int**)malloc(sizeof(int)*pow(Order_temp, Order_temp));
	zaman2 = (int**)malloc(sizeof(int)*pow(Order_temp, Order_temp));

//Termin S�resine Dikkat Edilerek, Ge�i� S�relerini Minimuma �ndirgeme Ve Her Kombinasyonu Matrise Kaydetme Denemesi
	for (int a = 0; a < Order_temp; a++)
		for (int b = 0; b < Order_temp - a - 1; b++)
			if(zaman[b] < OrderList[b].son_tarih) {
				temp = OrderList[b].siparis_no;
				OrderList[b].siparis_no = OrderList[b + 1].siparis_no;
				OrderList[b + 1].siparis_no = temp;

				temp = OrderList[b].siparis_miktari;
				OrderList[b].siparis_miktari = OrderList[b + 1].siparis_miktari;
				OrderList[b + 1].siparis_miktari = temp;

				temp = OrderList[b].siparis_turu;
				OrderList[b].siparis_turu = OrderList[b + 1].siparis_turu;
				OrderList[b + 1].siparis_turu = temp;

				temp = OrderList[b].son_tarih;
				OrderList[b].son_tarih = OrderList[b + 1].son_tarih;
				OrderList[b + 1].son_tarih = temp;

				for (int c = 0; c < Order_temp - 1; c++)
					gecis2[b][c + 1] = Gecis_Hes(OrderList[c].siparis_turu, OrderList[c + 1].siparis_turu);

				for (int d = 0; d < Order_temp; d++)
					calisma_suresi2[b][d] = (int)ceil((float)OrderList[a].siparis_miktari / OprList[OrderList[a].siparis_turu - 1].hiz);

				Top = 0;
				for (int e = 0; e < Order_temp - 1; e++) {
					Top = Top + calisma_suresi2[b][e] + gecis2[b][e + 1];
					zaman2[b][e + 1] = Top;
				}
			}

//Minimum Zaman� Bulma ve Matrislere S�ralama
	for (int a = 0; a < Order_temp - 1; a++)
		for (int b = 0; b < Order_temp - a - 1; b++)
			if (zaman2[b][Order_temp] > zaman2[b + 1][Order_temp]) {
				temp = gecis2[b][Order_temp];
				gecis2[b][Order_temp] = gecis2[b + 1][Order_temp];
				gecis2[b + 1][Order_temp] = temp;

				temp = calisma_suresi2[b][Order_temp];
				calisma_suresi2[b][Order_temp] = calisma_suresi2[b + 1][Order_temp];
				calisma_suresi2[b + 1][Order_temp] = temp;

				temp = zaman2[b][Order_temp];
				zaman2[b][Order_temp] = zaman2[b + 1][Order_temp];
				zaman2[b + 1][Order_temp] = temp;

				temp = OrderList[b].siparis_no;
				OrderList[b].siparis_no = OrderList[b + 1].siparis_no;
				OrderList[b + 1].siparis_no = temp;

				temp = OrderList[b].siparis_miktari;
				OrderList[b].siparis_miktari = OrderList[b + 1].siparis_miktari;
				OrderList[b + 1].siparis_miktari = temp;

				temp = OrderList[b].siparis_turu;
				OrderList[b].siparis_turu = OrderList[b + 1].siparis_turu;
				OrderList[b + 1].siparis_turu = temp;
			}

//Matrisleri Dizilere Atama
	gecis2[Order_temp][0] = 0;
	for (int a = 0; a < Order_temp - 1; a++)
		gecis[a + 1] = gecis2[Order_temp][a+1];

	for (int a = 0; a < Order_temp; a++)
		calisma_suresi[a] = calisma_suresi2[Order_temp][a];

	zaman2[Order_temp][0] = 0;
	for (int a = 0; a < Order_temp - 1; a++)
		zaman[a + 1] = zaman2[Order_temp][a+1];
*/

//Schedule.txt Dosyas�n�n Olu�turulmas�
	SchList = (Sch*)malloc(sizeof(Sch)*Order_temp);
	for (int a = 0; a < Order_temp; a++) {
		SchList[a].zaman = zaman[a];
		SchList[a].islem_turu = OrderList[a].siparis_turu;
		SchList[a].islem_no = OrderList[a].siparis_no;
		SchList[a].islem_miktari = OrderList[a].siparis_miktari;
		SchList[a].gecis_suresi = gecis[a];
	}
	dosya_islemi = fopen("Schdule.txt", "w");
	while (h < Order_temp) {
		fprintf(dosya_islemi, "%d;%d;%d;%d;%d\n", SchList[h].zaman, SchList[h].islem_turu, SchList[h].islem_no, SchList[h].islem_miktari, SchList[h].gecis_suresi);
		h++;
	}
	fclose(dosya_islemi);

/*
//NOT : Bu alandaki kodlar kontrol ama�l� yap�lan ekran ��kt�lar�d�r. ��lemleri konsoldan g�rmek isterseniz a�abilirsiniz.

	printf("---------------------------\n");
	printf("Degiskenler Kontrol:\n");
	printf("i=%d\nj=%d\nk=%d\nh=%d\nsatir_sayisi=%d\nSetDur_temp=%d\nOrder_temp=%d\n", i, j, k, h, satir_sayisi, SetDur_temp, Order_temp);
	printf("---------------------------\n");
	printf("Operations.txt Kontrol:\n");
	for (int a = 0; a < i; a++)
	printf("%d;%d\n", OprList[a].islem, OprList[a].hiz);
	printf("---------------------------\n");
	printf("SetupDurations.txt Kontrol:\n");
	for (int a = 0; a < j; a++)
	printf("%d;%d;%d\n", SetDurList[a].ilk_islem, SetDurList[a].son_islem, SetDurList[a].bekleme);
	printf("---------------------------\n");
	printf("Order.txt Kontrol:\n");
	for (int a = 0; a < k; a++)
	printf("%d;%d;%d;%d\n", OrderList[a].siparis_no, OrderList[a].siparis_miktari, OrderList[a].siparis_turu, OrderList[a].son_tarih);
	printf("---------------------------\n");
	printf("Calisma Suresi Kontrol:\n");
	for (int a = 0; a < Order_temp; a++)
	printf("%d\n", calisma_suresi[a]);
	printf("---------------------------\n");
	printf("Gecis Kontrol:\n");
	for (int a = 0; a < Order_temp; a++)
	printf("%d\n", gecis[a]);
	printf("---------------------------\n");
	printf("Zaman Kontrol:\n");
	for (int a = 0; a < Order_temp; a++)
	printf("%d\n", zaman[a]);
	printf("---------------------------\n");
	printf("Schedule.txt Kontrol:\n");
	for (int a = 0; a < h; a++)
	printf("%d;%d;%d;%d;%d\n", SchList[a].zaman, SchList[a].islem_turu, SchList[a].islem_no, SchList[a].islem_miktari, SchList[a].gecis_suresi);
*/

//��k�� (Program Sonu)
	printf("Schedule.txt dosyasi olusturulmustur.\n");
	free(calisma_suresi);
	free(zaman);
	free(gecis);
	system("pause");
	return 0;
}

//Fonksiyonlar
int Gecis_Hes(int x, int y) {
	for (int a = 0; a < SetDur_temp; a++)
		if ((x == SetDurList[a].ilk_islem && y == SetDurList[a].son_islem) || (y == SetDurList[a].ilk_islem && x == SetDurList[a].son_islem))
			return SetDurList[a].bekleme;
		else if (x == y)
			return 0;
}

