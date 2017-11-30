//  Samet ÖZÜ
//  1306140060
//  Date : 20.05.2016
//  Development Enviorement : Code Blocks 16.01


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <conio.h>

typedef struct order {
	int no;
	int amount;
	int optype;
	int deadline;
	float time;
}ord;

int main()
{
	FILE *di = NULL;
	int counter = 0;
	int counter2 = 0;
	int counter3 = 0;
	char temp[100];
	int *op = NULL;
	int **dur = NULL;
	ord *orderlist = NULL;
	int x, k;
	int v, y, z;
	int i, u, d, o, a, f, g, s, h, b, c, p,l;
	float total = 0.0;
	int duration = 0;
	float generaltime = 0.0;

	if ((di = fopen("Operations.txt", "r")) != NULL)
	{
		while (!feof(di))
		{
			fgets(temp, 100, di);
			counter++;
		}
	}
	else
	{
		printf("Dosya bulunamadi...");
	}
	fclose(di);

	op = (int *)malloc(sizeof(int)*counter);

	if ((di = fopen("Operations.txt", "r")) != NULL)
	{
		while (!feof(di))
		{
			fscanf(di, "%d;%d", &x, &k);
			op[x - 1] = k;
		}

	}
	else
	{
		printf("Operations dosyasi bulunamadi...");
	}
	fclose(di);

	dur = (int **)malloc(sizeof(int*)*counter);
	for (i = 0; i<counter; i++)
	{
		dur[i] = (int *)malloc(sizeof(int)*counter);
	}

	if ((di = fopen("SetupDuration.txt", "r")) != NULL)
	{
		while (!feof(di))
		{
			fscanf(di, "%d;%d;%d", &v, &y, &z);
			dur[v - 1][y - 1] = z;
			dur[y - 1][v - 1] = z;
		}
	}
	else
	{
		printf("SetupDuration dosyasi bulunamadi...");
	}
	fclose(di);

	for (d = 0; d < counter; d++)
	{
		dur[d][d] = 0;
	}

	if ((di = fopen("Orders.txt", "r")) != NULL)
	{
		while (!feof(di))
		{
			fgets(temp, 100, di);
			counter2++;
		}

	}
	else
	{
		printf("Orders dosyasi bulunamadi...");
	}

	fclose(di);

	orderlist = (ord *)malloc(sizeof(ord)*counter2);

	counter3 = counter2;
	counter2 = 0;

	if ((di = fopen("Orders.txt", "r")) != NULL)
	{
		while (!feof(di))
		{
			fscanf(di, "%d;%d;%d;%d", &orderlist[counter2].no, &orderlist[counter2].amount, &orderlist[counter2].optype, &orderlist[counter2].deadline);
			orderlist[counter2].time = (float)orderlist[counter2].amount / op[(orderlist[counter2].optype) - 1];
			counter2++;

		}

	}
	else
	{
		printf("Orders dosyasi bulunamadi");
	}

	fclose(di);

	for (a = 0; a<=counter+1; a++)
	{
		f = orderlist[a].optype;
		g = orderlist[a+1].optype;
		duration = duration + dur[f-1][g-1];
		//printf("%d\n",dur[f-1][g-1]);             //SetupDuration kontrolü
	}



	for (s = 0; s<counter3; s++)
	{
		total = total + ceil(orderlist[s].time);
	}

	generaltime = total + duration;
	printf("According to the list of work of minutes       : %.2f\n", ceil(total));
	printf("Total prep time of machines                    : %d\n\n", duration);
	printf("*******************************************************\n\n");
	printf("Total time of order to delivery                : %.2f\n", generaltime);

	int buu = orderlist[0].optype-1;

	if ((di = fopen("Schedule.txt", "w")) != NULL)
	{
		for (p = 0; p<counter3; p++)

		{
			int l = orderlist[p].optype;
			fprintf(di, "%d;%d;%d;%d\n", orderlist[p].optype, orderlist[p].no, orderlist[p].amount, dur[l-1][buu]);
			buu = l-1;
		}
	}
	else
	{
		printf("Dosya acilamadi...");
	}

	fclose(di);

}
