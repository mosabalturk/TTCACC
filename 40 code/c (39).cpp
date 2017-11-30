#include <stdio.h>
#define MAX 1000000
struct operasyon
{
	int operasyonkodu[MAX];
	int operasyonhizi[MAX];

}operasyon;

struct kurulumsuresi
{

	int operasyonbirinci[MAX];
	int operasyonikinci[MAX];
	int kurulumsuresi[MAX];

}kurulum;

struct siparisler
{
	int sipariskodu[MAX];
	int siparismiktari[MAX];
	int operasyonkodu[MAX];
	int teslimtarihi[MAX];
	int yapimsuresi[MAX];

}siparis;

int zaman = 0;
int sonoperasyon = 0;
/* yukarýda dosyalarýn içinde kullanacaðýmýz stracklar bulunmakta */
main()
{
	okumalar();
	TeslimTarihisiralama();
	yazSchedule();
	getch();

}
/* Fonksiyonlarý Çaðýrmak Ýçin Main Ýçine Ýsimlerini Ekledik */
okumalar()
{
	FILE *okuma;
	okuma = fopen("C:\\Users\\Furkan\\Desktop\\x\\Operations.txt", "r");
	for (int i = 0; !feof(okuma); i++)
	{
		fscanf(okuma, "%d;%d", &operasyon.operasyonkodu[i], &operasyon.operasyonhizi[i]);
	}
	fclose(okuma);
	okuma = fopen("C:\\Users\\Furkan\\Desktop\\x\\SetupDuration.txt", "r");
	for (int j = 0; !feof(okuma); j++)
	{
		fscanf(okuma, "%d;%d;%d", &kurulum.operasyonbirinci[j], &kurulum.operasyonikinci[j], &kurulum.kurulumsuresi[j]);
	}
	fclose(okuma);
	okuma = fopen("C:\\Users\\Furkan\\Desktop\\x\\Orders.txt", "r");
	for (int k = 0; !feof(okuma); k++)
	{
		fscanf(okuma, "%d;%d;%d;%d", &siparis.sipariskodu[k], &siparis.siparismiktari[k], &siparis.operasyonkodu[k], &siparis.teslimtarihi[k]);
		siparis.yapimsuresi[k] = yapimsuresi(siparis.operasyonkodu[k], siparis.siparismiktari[k]);
	}
	fclose(okuma);
}
/* Yukarýda Dosyalarý Okuyoruz */
int yapimsuresi(int op, int miktar)
{
	int i = 0, hiz = 0;
	while (operasyon.operasyonkodu[i] != NULL)
	{
		if (operasyon.operasyonkodu[i] == op)
		{
			hiz = operasyon.operasyonhizi[i];
			break;
		}
		i++;
	}
	if (miktar%hiz == 0)
	{
		return miktar / hiz;
	}
	else
	{
		return (miktar / hiz) + 1;
	}
}
/* Ýþlemlerin Süreleri Toplamý Ýçin Fonksiyon  */

int Gecikmesuresi(int sonislem, int sonrakiislem)
{
	int i = 0;
	if (sonislem == sonrakiislem || sonislem == 0)
	{
		return 0;
	}
	else {
		while (kurulum.operasyonbirinci[i] != NULL)
		{

			if ((kurulum.operasyonbirinci[i] == sonislem && kurulum.operasyonikinci[i] == sonrakiislem) || (kurulum.operasyonbirinci[i] == sonrakiislem && kurulum.operasyonikinci[i] == sonislem))
			{
				zaman += kurulum.kurulumsuresi[i];
				return kurulum.kurulumsuresi[i];

			}

			i++;

		}
	}
}

/* Gecikme Hesaplama Fonksiyonu */
yazSchedule()
{
	FILE *yazma;
	yazma = fopen("C:\\Users\\Furkan\\Desktop\\x\\Shedule.txt", "a+");
	int i = 0;
	while (siparis.sipariskodu[i] != NULL)
	{

		fprintf(yazma, "%d;%d;%d;%d;%d\n", zaman, siparis.operasyonkodu[i], siparis.sipariskodu[i], siparis.siparismiktari, Gecikmesuresi(sonoperasyon, siparis.sipariskodu[i]));
		sonoperasyon = siparis.operasyonkodu[i];
		zaman += siparis.yapimsuresi[i];
		i++;
	}
	fcloseall();
}
/* Dosyaya Yazma Fonksiyonu */
TeslimTarihisiralama()
{
	int i = 0;
	while (siparis.teslimtarihi[i] != NULL)
	{				i++;

	}		int c, d;
	int sAmount, sDeadline, sOperation, sOrder, sDuration;
	for (c = 0; c < (i - 1); c++)
	{		for (d = 0; d < i - c - 1; d++)
		{						if (siparis.teslimtarihi[d] > siparis.teslimtarihi[d + 1])
			{				sAmount = siparis.siparismiktari[d];
				sDeadline = siparis.teslimtarihi[d];
				sOperation = siparis.operasyonkodu[d];
				sOrder = siparis.sipariskodu[d];
				sDuration = siparis.yapimsuresi[d];

				siparis.siparismiktari[d] = siparis.siparismiktari[d + 1];
				siparis.teslimtarihi[d] = siparis.teslimtarihi[d + 1];
				siparis.operasyonkodu[d] = siparis.operasyonkodu[d + 1];
				siparis.sipariskodu[d] = siparis.sipariskodu[d + 1];
				siparis.yapimsuresi[d] = siparis.yapimsuresi[d + 1];

				siparis.siparismiktari[d + 1] = sAmount;
				siparis.teslimtarihi[d + 1] = sDeadline;
				siparis.operasyonkodu[d + 1] = sOperation;
				siparis.sipariskodu[d + 1] = sOrder;
				siparis.yapimsuresi[d + 1] = sDuration;	}	}	}}
/*  Sipariþlerin Son Teslim Tarihine Göre Sýralanýþý  */