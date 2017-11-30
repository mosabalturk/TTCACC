//	Çaðatay Çiftçi
//	1306150050
//	Date : 20.05.2016
//	Development Environment : Visual Studio2015


#include <stdlib.h>
#include <stdio.h>
#include <string.h>

typedef struct siparis {
	int siparisKodu;
	int miktar;
	int teslimSuresi;
	int operasyonKodu;
	int duration;
	int completed;
}siparis;

typedef struct operasyon {
	int operationCode;
	int sure;
}operasyon;

typedef struct setup {
	int from;
	int to;
	int duration;
}setup;

int fact(int a) {
	int sonuc = 1;
	for (int i = 1; i <= a; i++)
	{
		sonuc *= i;
	}
	return sonuc;
}

int main() {

	FILE *orders = NULL;
	FILE *operations = NULL;
	FILE *setupDurations = NULL;
	FILE *sonuc = fopen("Schedule.txt", "a+");
	//Kayýtlarýn adetini tutacaklar
	int siparisSayisi = 1;
	int operasyonSayisi = 1;
	int setupSayisi = 1;

	setup * setups = NULL;
	siparis * siparisler = NULL;
	operasyon * operasyonlar = NULL;

	char *token = NULL;//Dosyalarý parçalerken kullanacaðým
	char tempBase[20];//Satýrý tutacak geçici deðiþken

	int sira = 0;//Dosyalarý parçalarken hangi elemanda olduðumu gösterecek

	orders = fopen("Orders.txt", "r");
	if (orders != NULL) {
		printf("Orders.txt dosyasi basariyla acildi.\n");

		//Siparis sayisini hesaplar
		while (!feof(orders)) {
			if (fgetc(orders) == '\n') {
				siparisSayisi++;
			}
		}
		rewind(orders);

		//Siparis Sayisina Gore Yer Ayirir
		siparisler = (siparis *)malloc(siparisSayisi * sizeof(siparis));
		//Siparis verilerini dosyadan deðiþkene aktarýr
		for (int i = 0; i < siparisSayisi; i++)
		{
			if (fgets(tempBase, 20, orders) != NULL) {
				token = strtok(tempBase, ";,\n");

				while (token != NULL) {
					switch (sira)
					{
					case 0:
						(siparisler + i * sizeof(siparis))->siparisKodu = atoi(token);
						sira++;
						break;
					case 1:
						(siparisler + i * sizeof(siparis))->miktar = atoi(token);
						sira++;
						break;
					case 2:
						(siparisler + i * sizeof(siparis))->operasyonKodu = atoi(token);
						sira++;
						break;
					case 3:
						(siparisler + i * sizeof(siparis))->teslimSuresi = atoi(token);
						sira = 0;
						break;
					default:
						break;
					}

					token = strtok(NULL, ";,\n");
				}
			}
		}
		fclose(orders);
	}
	else {
		printf("Orders.txt dosyasi acilamadi.\n");
	}

	sira = 0;//Sayacý sýfýrladým.

	operations = fopen("Operations.txt", "r");
	if (operations != NULL) {
		printf("Operations.txt dosyasi basariyla acildi.\n");

		//Operasyon sayisini hesaplar
		while (!feof(operations)) {
			if (fgetc(operations) == '\n') {
				operasyonSayisi++;
			}
		}
		rewind(operations);

		//Operasyon Sayisina Gore Yer Ayirir
		operasyonlar = (operasyon *)malloc(operasyonSayisi * sizeof(operasyon));

		for (int i = 0; i < operasyonSayisi; i++)
		{
			if (fgets(tempBase, 20, operations) != NULL) {
				token = strtok(tempBase, ";,\n");

				while (token != NULL) {
					switch (sira)
					{
					case 0:
						(operasyonlar + (i * sizeof(operasyon)))->operationCode = atoi(token);
						sira++;
						break;
					case 1:
						(operasyonlar + (i * sizeof(operasyon)))->sure = atoi(token);
						sira = 0;
						break;
					default:
						break;
					}
					token = strtok(NULL, ";,\n");
				}
			}
		}

		fclose(operations);
	}
	else {
		printf("Operations.txt dosyasi acilamadi.\n");
	}

	sira = 0;//Sayacý sýfýrladým.

	setupDurations = fopen("SetupDuration.txt", "r");
	if (setupDurations != NULL) {
		printf("SetupDuration.txt dosyasi basariyla acildi.\n");

		//Setup sayisini hesaplar
		while (!feof(setupDurations)) {
			if (fgetc(setupDurations) == '\n') {
				setupSayisi++;
			}
		}
		rewind(setupDurations);

		//Setup Sayisina Gore Yer Ayirir
		setups = (setup *)malloc(setupSayisi * sizeof(setup));

		for (int i = 0; i < setupSayisi; i++)
		{
			if (fgets(tempBase, 20, setupDurations) != NULL) {
				token = strtok(tempBase, ";,\n");

				while (token != NULL) {
					switch (sira)
					{
					case 0:
						(setups + (i * sizeof(setup)))->to = atoi(token);
						sira++;
						break;
					case 1:
						(setups + (i * sizeof(setup)))->from = atoi(token);
						sira++;
						break;
					case 2:
						(setups + (i * sizeof(setup)))->duration = atoi(token);
						sira = 0;
						break;
					default:
						break;
					}
					token = strtok(NULL, ";,\n");
				}
			}
		}

		fclose(setupDurations);
	}
	else {
		printf("SetupDuration.txt dosyasi acilamadi.\n");
	}
	//
	//
	//Ýþlemleri burada yap.
	//
	//


	//Süreleri ve tamamlanmadý statülerini atayacak
	for (int i = 0; i < siparisSayisi; i++)
	{
		for (int j = 0; j < operasyonSayisi; j++)
		{
			if ((siparisler + i * sizeof(siparis))->operasyonKodu == (operasyonlar + j*sizeof(operasyon))->operationCode) {
				int sipMiktari = (siparisler + i * sizeof(siparis))->miktar;
				int operasyonSuresi = (operasyonlar + j*sizeof(operasyon))->sure;
				if (sipMiktari % operasyonSuresi == 0) {
					(siparisler + i * sizeof(siparis))->duration = sipMiktari / operasyonSuresi;
				}
				else {
					(siparisler + i * sizeof(siparis))->duration = sipMiktari / operasyonSuresi + 1;
				}
				(siparisler + i * sizeof(siparis))->completed = 0;
				break;
			}
		}

	}

	//Teslim tarihlerine göre sýralayýp iþleyecek

	int time = 0;//Zaman
	int tempTime = 0;
	int bitmisIslemler = 0;

	int currentOperation = 0;
	int previousOperation = 0;
	int latestDelivery = 0;

	int setupDuration = 0;

	while (bitmisIslemler < siparisSayisi) {

		int minDur = 9999999;

		for (int i = 0; i < siparisSayisi; i++)
		{
			//minimum deathline lý iþlemi bul
			if (minDur >(siparisler + i*sizeof(siparis))->teslimSuresi && (siparisler + i*sizeof(siparis))->completed == 0) {
				currentOperation = (siparisler + i*sizeof(siparis))->operasyonKodu;
				latestDelivery = (siparisler + i*sizeof(siparis))->siparisKodu;
				minDur = (siparisler + i*sizeof(siparis))->teslimSuresi;
				if (previousOperation == 0)//Ýlk iþlemse
					previousOperation = currentOperation;
			}
		}

		for (int i = 0; i < siparisSayisi; i++)
		{
			if (latestDelivery == (siparisler + i*sizeof(siparis))->siparisKodu && (siparisler + i*sizeof(siparis))->completed == 0) {
				if (previousOperation != currentOperation) {//Setup süresi var mý
					for (int j = 0; j < setupSayisi; j++)//Setup süresini ekle
					{
						if ((setups + j*sizeof(setup))->to == previousOperation && (setups + j*sizeof(setup))->from == currentOperation) {
							setupDuration = (setups + j*sizeof(setup))->duration;
							time += setupDuration;
							tempTime += setupDuration;
						}
						if ((setups + j*sizeof(setup))->from == previousOperation && (setups + j*sizeof(setup))->to == currentOperation) {
							setupDuration = (setups + j*sizeof(setup))->duration;
							time += setupDuration;
							tempTime += setupDuration;
						}
					}
				}
				time += (siparisler + i*sizeof(siparis))->duration;//iþlemi yap
				(siparisler + i*sizeof(siparis))->completed = 1;
				bitmisIslemler++;

				fprintf(sonuc, "%d;%d;%d;%d;%d\n", tempTime, (siparisler + i*sizeof(siparis))->operasyonKodu, (siparisler + i*sizeof(siparis))->siparisKodu, (siparisler + i*sizeof(siparis))->miktar, setupDuration);
				tempTime = time;
				setupDuration = 0;
				previousOperation = currentOperation;


			}
		}

	}

	//GG WP
	fclose(sonuc);

	return 0;
}

