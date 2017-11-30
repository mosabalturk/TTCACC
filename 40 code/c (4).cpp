//Burcu DEMÝRALP   
//1306140072
//Date : 20.05.2016
//Development Enviorement : Visual Studio2013
#include<stdio.h>
#include<conio.h>
#include<stdlib.h>
#include<string.h>
#include<math.h>
int op_sayisi = 1;
int sip_sayisi = 1;
typedef struct
{
	int opcode1;
	int opcode2;
	int sure;
}gecissureleri;
typedef struct
{
	int op_turu;
	int op_hizi;
}inf_opr;
typedef struct
{
	int siparis_kodu;
	int siparis_adedi;
	int opr_turu;
	int sontarih;
	int haz_zamani;
}inf_siparis;
//******************************************************OPERASYON TURUNUN HIZINI DÖNDÜREN FONKSÝYON*****************************************
int hizhesapla(int opturu)
{
	FILE *dosya6;
	dosya6 = fopen("operations.txt", "r");
	inf_opr *operasyon = NULL;
	operasyon = (inf_opr*)malloc(sizeof(inf_opr)*op_sayisi);
	for (int i = 0; i < op_sayisi; i++)
	{
		fscanf(dosya6, "%d;%d", &operasyon[i].op_turu, &operasyon[i].op_hizi);
	}
	fclose(dosya6);
	int a = 0;
	for (int i = 0; i < op_sayisi; i++)
	{
		if (opturu == operasyon[i].op_turu)
		{
			a = operasyon[i].op_hizi;
		}

	}
	if (a == 0)
		return 1;
	else
		return a;
}
//***********************************************************************************************************************************************
int main()
{
	//*********************************************************OPERASYON SAYISINI BULMA**********************************************************

	FILE *dosya1;
	dosya1 = fopen("operations.txt", "r");
	while (!feof(dosya1))
	{
		char a;
		a = fgetc(dosya1);
		if (a == '\n')
			op_sayisi++;
	}
	fclose(dosya1);
	//*******************************************************************************************************************************************
	//********************************************************OPERASYON KODLARINI VE HIZLARINI OKUMA********************************************
	FILE *dosya2;
	dosya2 = fopen("operations.txt", "r");
	inf_opr *operasyon = NULL;
	operasyon = (inf_opr*)malloc(sizeof(inf_opr)*op_sayisi);
	for (int i = 0; i < op_sayisi; i++)
	{
		fscanf(dosya2, "%d;%d", &operasyon[i].op_turu, &operasyon[i].op_hizi);

	}
	fclose(dosya2);
	//*******************************************************************************************************************************************

	//************************************************************SETUP SÜRELERÝNÝ OKUMA****************************************************
	FILE *dosya3;
	dosya3 = fopen("setupduration.txt", "r");
	gecissureleri *gsureleri = NULL;
	gsureleri = (gecissureleri*)malloc(sizeof(gecissureleri)*(op_sayisi*op_sayisi));
	int op1, op2, sure;
	while (!feof(dosya3))
	{

		fscanf(dosya3, "%d;%d;%d", &op1, &op2, &sure);
		int a = (op_sayisi*(op2 - 1) + (op1 - 1));
		int b = (op_sayisi*(op1 - 1) + (op2 - 1));
		gsureleri[a].opcode1 = op1;
		gsureleri[a].opcode2 = op2;
		gsureleri[a].sure = sure;
		gsureleri[b].opcode1 = op2;
		gsureleri[b].opcode2 = op1;
		gsureleri[b].sure = sure;

	}
	fclose(dosya3);
	//******************************************************************************************************************************************
	//***************************************SÝPARÝÞ SAYISINI BULMA*************************************************************************
	FILE *dosya4;
	dosya4 = fopen("orders.txt", "r");
	while (!feof(dosya4))
	{
		char k;
		k = fgetc(dosya4);
		if (k == '\n')
			sip_sayisi++;
	}
	fclose(dosya4);
	//******************************************************************************************************************************************

	//************************************************SÝPARÝÞ AYRINTILARINI OKUMA***************************************************************
	FILE *dosya5;
	dosya5 = fopen("orders.txt", "r");
	inf_siparis *siparis = NULL;
	siparis = (inf_siparis*)malloc(sizeof(inf_siparis)*(sip_sayisi));
	int i = 0;
	while (!feof(dosya5))
	{
		fscanf(dosya5, "%d;%d;%d;%d", &siparis[i].siparis_kodu, &siparis[i].siparis_adedi, &siparis[i].opr_turu, &siparis[i].sontarih);
		i++;
	}
	fclose(dosya5);
	//******************************************************************************************************************************************

	//*************************************************SÝPARÝÞLERÝ SON TESLÝM TARÝHLERÝNE GÖRE SIRALAMA*******************************************
	inf_siparis *gecici = NULL;
	gecici = (inf_siparis*)malloc(sizeof(inf_siparis)*(sip_sayisi));
	for (int j = 0; j < sip_sayisi - 1; j++)
	{
		for (int i = 0; i < sip_sayisi - 1; i++)
		{
			if (siparis[i].sontarih>siparis[i + 1].sontarih)
			{
				*gecici = siparis[i];
				siparis[i] = siparis[i + 1];
				siparis[i + 1] = *gecici;
			}
		}
	}


	//********************************************************************************************************************************************

	//********************************************HER BÝR SÝPARÝÞÝN HAZIRLANIÞ SÜRESÝNÝ HESAPLAMA*************************************************
	for (i = 0; i < sip_sayisi; i++)
	{
		float g = (float(siparis[i].siparis_adedi) / float(hizhesapla(siparis[i].opr_turu)));
		siparis[i].haz_zamani = round(g);
	}
	//*******************************************************************************************************************************************
	//*********************************************ÇIKTI VERME***********************************************************************************
	FILE *dosya6;
	dosya6 = fopen("schedule.txt", "w");
	int total = 0;
	int overhead = 0;
	for (int j = 0; j < sip_sayisi; j++)
	{
		fprintf(dosya6, "%d;%d;%d;%d;%d\n", total, siparis[j].opr_turu, siparis[j].siparis_kodu, siparis[j].siparis_adedi, overhead);
		total += siparis[j].haz_zamani;
		overhead = gsureleri[op_sayisi*(siparis[j + 1].opr_turu - 1) + (siparis[j].opr_turu - 1)].sure;
	}
	fflush(dosya6);
	fclose(dosya6);
	//********************************************************************************************************************************************

	getch();
	return 0;
}