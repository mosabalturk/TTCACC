#include <stdio.h>
#include <stdlib.h>

//      HASAN AK
//      1306140087

typedef struct operations
{
	int oprCode;
	int speed;

}opr;


typedef struct
{
	int orderCode;
	int amountofWork;
	int operationCode;
	int deadline;

}order;


typedef struct setupDuration
{
	int opCode1;
	int opCode2;
	int dur;

}setup;


opr *oprlist = NULL;
order *orderlist = NULL;
setup *setuplist = NULL;

int *zaman;
int *gecissuresi;
int gecissayilari;
int *totalzaman;


int bekleme_süreleri_hesaplama(int x, int y)
{
	int a;
	for (a = 0; a < gecissayilari; a++)
	{
		if ((x == setuplist[a].opCode1 && y == setuplist[a].opCode2) || (y == setuplist[a].opCode1 && x == setuplist[a].opCode2))
		{
			return setuplist[a].dur;
		}
		else if (x==y)
		{
			return 0;
		}
	}
}




int main()
{

	FILE *dosya;
	int counter = 0,islemsayisi, siparissayisi, satirsayisi;
	char temp[100];

	dosya = fopen("Operations.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fgets(temp, 100, dosya);
			counter++;
		}
	}
	else
	{
		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);


	islemsayisi = counter;
	oprlist = (opr*)malloc(sizeof(opr)*islemsayisi);
	counter = 0;

	dosya = fopen("Operatiýns.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fscanf(dosya, "%d;%d", &oprlist[counter].oprCode, &oprlist[counter].speed);
			counter++;
		}
	}

	else
	{
		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);

	counter = 0;

	dosya = fopen("Orders.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fgets(temp, 100, dosya);
			counter++;
		}
	}

	else
	{
		printf("Bilgi:Doya acilirken hata olustu...");
	}

	fclose(dosya);

	siparissayisi = counter;
	orderlist = (order*)malloc(sizeof(order)*siparissayisi);
	counter = 0;


	dosya = fopen("Orders.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fscanf(dosya, "%d;%d;%d;%d", &orderlist[counter].orderCode, &orderlist[counter].amountofWork, &orderlist[counter].operationCode, &orderlist[counter].deadline);
			counter++;
		}
	}

	else
	{

		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);

	counter = 0;

	dosya = fopen("SetupDuration.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fgets(temp, 100, dosya);
			counter++;
		}
	}

	else
	{
		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);

	satirsayisi = counter;
	setuplist = (setup*)malloc(sizeof(setup)*satirsayisi);
	counter = 0;

	dosya = fopen("SetupDuration.txt", "r");
	if (dosya != NULL)
	{
		while (!feof(dosya))
		{
			fscanf(dosya, "%d;%d;%d", &setuplist[counter].opCode1, &setuplist[counter].opCode2, &setuplist[counter].dur);
			counter++;
		}
	}

	else
	{
		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);

	gecissayilari = satirsayisi;



	zaman = (int*)malloc(sizeof(int)*siparissayisi);
	int i, j;
	for (i = 0; i < siparissayisi; i++)
	{
		for (j = 0; j < islemsayisi; j++)
		{
			if (orderlist[i].operationCode == oprlist[j].oprCode)
			{
				zaman[i] = orderlist[i].amountofWork / oprlist[j].speed;
			}
		}
	}




	gecissuresi = (int*)malloc(sizeof(int)*siparissayisi);

	for (i = 0; i < siparissayisi; i++)
	{
		if (i == 0 || i == siparissayisi)
		{
			gecissuresi[i] = 0;
		}
		else
		{
			gecissuresi[i] = bekleme_süreleri_hesaplama(orderlist[i].operationCode, orderlist[i + 1].operationCode);
		}
	}



	totalzaman = (int*)malloc(sizeof(int)*siparissayisi);
	for (i = 0; i <=siparissayisi; i++)
	{
		totalzaman[i] = zaman[i] + gecissuresi[i];

	}


	int toplamgecensure = 0;

	for (i = 0; i <= siparissayisi; i++)
	{
		toplamgecensure = totalzaman[i];

	}

	printf("Toplam gecen sure %d", toplamgecensure);


	dosya = fopen("Schedule.txt", "r");
	if (dosya == NULL)
	{
		for (i = 0; i <= siparissayisi; i++)
		{
			fprintf(dosya, "%d;%d;%d;%d;%d", totalzaman[i], orderlist[i].operationCode, orderlist[i].orderCode, orderlist[i].amountofWork, gecissuresi[i]);
		}
	}

	else
	{
		printf("Bilgi:Dosya acilirken hata olustu...");
	}

	fclose(dosya);


	system("pause");
}
