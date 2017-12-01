#include <stdio.h>
#include <math.h>
#define MAX 1000000
int time_ = 0,op=0;//op=son yap�lan 
struct Database
{
	struct Op
	{
		int kod;
		int hiz;
	}Op[10];
	struct Or
	{
		int kod;
		int miktar;
		int islem;
		int teslimat;
		int yapims�resi;
		char durum;// 'O' means ordered, 'S' means scheduled

	}Or[MAX];
	struct SetD
	{
		int birincil;
		int ikincil;
		int sure;
	}SetD[20];
}Db;
main()
{
	FILE *reader;
	
	if ((reader = fopen("C:\\Users\\BURAK\\Desktop\\Homework\\Operations.txt", "r")) == NULL)
	{
		printf("ERROR");
	}
	else
	{
		okut_islem(reader);
	}
	if ((reader = fopen("C:\\Users\\BURAK\\Desktop\\Homework\\SetupDuration.txt", "r")) == NULL)
	{
		printf("ERROR");
	}
	else
	{
		okut_yukleme(reader);
	}
	if ((reader = fopen("C:\\Users\\BURAK\\Desktop\\Homework\\Orders.txt", "r")) == NULL)
	{
		printf("ERROR");
	}
	else
	{
		okut_siparis(reader);
	}

	int i = 0;
	while (Db.Op[i].kod != NULL )
	{
		printf("Islem : %d Hiz: %d \n", Db.Op[i].kod, Db.Op[i].hiz);
		i++;
	}
	i = 0;
	while (Db.SetD[i].birincil != NULL)
	{
		printf("%d' den  %d'ye gecis suresi :%d \n", Db.SetD[i].birincil, Db.SetD[i].ikincil , Db.SetD[i].sure);
		i++;
	}
	i = 0;

	while (Db.Or[i].islem != NULL)
	{
		printf("kod : %d  miktar: %d islem: %d  teslimat: %d \n", Db.Or[i].kod, Db.Or[i].miktar, Db.Or[i].islem , Db.Or[i].teslimat);
		i++;
	}

	cikti();
	
	getch();
 }
okut_islem(FILE *read) // operation lar� okutma
{
	

		int i = 0;
		while(!feof(read))
		{
			fscanf(read, "%d;%d", &Db.Op[i].kod,&Db.Op[i].hiz);
			i++;
		}
	

	
}
okut_yukleme(FILE *read) // SetupDuration lar� okutma
{

	int i = 0;
	while (!feof(read))
	{
		fscanf(read, "%d;%d;%d", &Db.SetD[i].birincil, &Db.SetD[i].ikincil,&Db.SetD[i].sure);
		i++;
	}


	
}
okut_siparis(FILE *read) // orders lar� okutma
{

	int i = 0;
	while (!feof(read))
	{
		fscanf(read, "%d;%d;%d;%d", &Db.Or[i].kod, &Db.Or[i].miktar, &Db.Or[i].islem, &Db.Or[i].teslimat);
		Db.Or[i].yapims�resi = surehesaplama(Db.Or[i].islem, Db.Or[i].miktar);
		
		i++;
	}


	
}
siralama() // deadline s�relerine g�re s�ralama
{
	int tempkod;
	int tempmiktar;
	int tempislem;
	int tempteslimat;
	int tempyapims�resi;
	char tempdurum;

	int i, j;
	for (i = 0; i < (MAX - 1); ++i) {
		for (j = 0; j < MAX - 1 - i; ++j) {
			if (Db.Or[j + 1].teslimat != NULL && Db.Or[j].teslimat > Db.Or[j + 1].teslimat) {

				tempkod = Db.Or[j + 1].kod;
				tempmiktar = Db.Or[j + 1].miktar;
				tempislem = Db.Or[j + 1].islem;
				tempteslimat = Db.Or[j + 1].teslimat;
				tempyapims�resi = Db.Or[j + 1].yapims�resi;
				tempdurum = Db.Or[j + 1].durum;

				Db.Or[j + 1].kod = Db.Or[j].kod;
				Db.Or[j + 1].miktar = Db.Or[j].miktar;
				Db.Or[j + 1].islem = Db.Or[j].islem;
				Db.Or[j + 1].teslimat = Db.Or[j].teslimat;
				Db.Or[j + 1].yapims�resi = Db.Or[j].yapims�resi;
				Db.Or[j + 1].durum = 'S';

				Db.Or[j].kod = tempkod;
				Db.Or[j].miktar = tempmiktar;
				Db.Or[j].islem = tempislem;
				Db.Or[j].teslimat = tempteslimat;
				Db.Or[j].yapims�resi = tempyapims�resi;
				Db.Or[j].durum = tempdurum;
			}
		}
	}
}
int opdegisme(int k, int l) // islem deg�s�rken makinen bekleme sures�n� hesaplama ve t�m zamana ekletme
{
	int a = 0;
	int i = 0;
	if (k == l ||op==0)
	{
		time_ += 0;
		
		return 0;
	}
	else {
		while (Db.SetD[i].birincil != NULL)
		{

			if ((Db.SetD[i].birincil == k && Db.SetD[i].ikincil == l) || (Db.SetD[i].birincil == l && Db.SetD[i].ikincil == k))
			{
				time_ += Db.SetD[i].sure;
				return Db.SetD[i].sure;

			}

			i++;

		}
	}


}
int surehesaplama(int islem, int miktar) // gelen siparislere g�re islem s�relerinin hesaplanmas�

{
	int i = 0;


	while (Db.Op[i].kod != islem)
	{
		
			i++;
		

	}
	if (miktar%Db.Op[i].hiz == 0)
	{
		float total = miktar / Db.Op[i].hiz;
		return (int)ceil(total);
	}
	else{
	float total = miktar / Db.Op[i].hiz;
	
	return (int)ceil(total+1);
	}
}



cikti() 
{
	FILE *dosya;
	dosya = fopen("C:\\Users\\BURAK\\Desktop\\Shedule.txt", "a+");
	int i = 0;
	while (Db.Or[i].kod != NULL)
	{
		
		fprintf(dosya, "%d;%d;%d;%d;%d\n", time_, Db.Or[i].islem, Db.Or[i].kod, Db.Or[i].miktar, opdegisme(op, Db.Or[i].islem));
		op = Db.Or[i].islem;
		time_ += Db.Or[i].yapims�resi;

		i++;
	}
	fcloseall();
}

