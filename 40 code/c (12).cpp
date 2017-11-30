//Ertuðrul ÝSLAMOÐLU
//1306150040
//Date : 20.05.2016
//Development Environment : Visual Studio 2015
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>
int stpovrhdcalculate(int maincounter, int stpvariation, int rndmhold[], int orderop[], int setop1[], int setop2[], int setdur[])
{
	int w;
	{
		for (w = 1;w <= stpvariation;w++)
		{
			if (orderop[rndmhold[maincounter]] == orderop[rndmhold[maincounter - 1]])
			{
				return 0;
			}
			else if (((orderop[rndmhold[maincounter - 1]] == setop1[w]) && (orderop[rndmhold[maincounter]] == setop2[w])) || ((orderop[rndmhold[maincounter]] == setop1[w]) && (orderop[rndmhold[maincounter - 1]] == setop2[w])))
			{
				return setdur[w];
			}
		}
	}
}
void main()
{
	struct counter
	{
		int a = 1;
		int b = 1;
		int d = 1;
		int e = 1;
		int f = 1;
		int g = 1;
		int h = 1;
		int i = 1;
		int j = 1;
		int k = 1;
	}c;
	struct operation
	{
		int o[1000];
		int w[1000];
	}o;
	struct order
	{
		int a[1000];
		float b[1000];
		int c[1000];
		int d[1000];
	}r;
	struct temporary
	{
		int temp = 0;
		int random = 0;
		int temp1[1000];
		int temp2[1000];
		int temp3[1000];
		int temp4[1000];
		int temp5 = 0;
		int temp6[1000];
		int temp7 = 0;
	}t;
	struct setup
	{
		int setdur[1000];
		int setovrhd = 0;
		int setop1[1000];
		int setop2[1000];
	}s;
	FILE *op, *sd, *od, *sc;
	op = fopen("E:\Operations.txt", "r");
	do
	{
		fscanf(op, "%d;%d\n\r", &o.o[c.a], &o.w[c.a]);
		c.a++;
	} while (!feof(op));
	fclose(op);
	sd = fopen("E:\SetupDuration.txt", "r");
	for (c.b = 1; c.b <= ((c.a - 1)*(c.a - 2) / 2); c.b++)
	{
		fscanf(sd, "%d;%d;%d\n\r", &s.setop1[c.b], &s.setop2[c.b], &s.setdur[c.b]);
	}while (!feof(sd));
	fclose(sd);
	od = fopen("E:\Orders.txt", "r");
	do
	{
		fscanf(od, "%d;%f;%d;%d\n\r", &r.a[c.d], &r.b[c.d], &r.d[c.d], &r.c[c.d]);
		c.d++;
	} while (!feof(op));
	fclose(od);
	float flotime[1000];
	int inttime[1000];
	int a = 0;
	for (c.e = 1; c.e <= c.d - 1; c.e++)
	{
		flotime[c.e] = r.b[c.e] / o.w[r.d[c.e]];
		inttime[c.e] = ceilf(flotime[c.e]);
	}
	int schtime = 0;
	srand(time(NULL));
	sc = fopen("E:\Schedule.txt", "w +");
	for (c.k = 1;c.k < c.e*3;c.k++)
	{
		for (c.j = 1;c.j <= c.e - 1;c.j++)
		{
			t.random = rand() % (c.e - 1) + 1;
			t.temp4[c.j] = t.random;
			for (c.i = 1;c.i < c.j;c.i++)
			{
				if (t.temp4[c.j] == t.temp4[c.i])
				{
					c.j = 1;
					rewind(sc);
					system("cls");
				}
				else
					continue;
			}
		}
		for (c.f = 1;c.f <= c.e - 1;c.f++)
		{
			if (c.f >= 2)
			{
				s.setovrhd = stpovrhdcalculate(c.f, c.b, t.temp4, r.d, s.setop1, s.setop2, s.setdur);
				schtime = schtime + s.setovrhd + inttime[t.temp4[c.f - 1]];
			}
			fprintf(sc, "%d;%d;%d;%.0lf;%d\n\r", schtime, r.d[t.temp4[c.f]], r.a[t.temp4[c.f]], r.b[t.temp4[c.f]], s.setovrhd);
		}
		t.temp6[c.k] = schtime;
		schtime = 0;
		rewind(sc);
		
	}
	int j,k;
	for (j = 1; j < c.e*3;j++)
		for (k = 1;k < j;k++)
		{
			if (t.temp6[k] < t.temp6[j])
			{
				t.temp7 = t.temp6[k];

			}
			else if (t.temp6[k] == t.temp6[j])
				t.temp7 = t.temp6[k];
		}
	do
	{
		for (c.j = 1;c.j <= c.e - 1;c.j++)
		{
			t.random = rand() % (c.e - 1) + 1;
			t.temp4[c.j] = t.random;
			for (c.i = 1;c.i < c.j;c.i++)
			{
				if (t.temp4[c.j] == t.temp4[c.i])
				{
					c.j = 1;
					rewind(sc);
					system("cls");
				}
				else
					continue;
			}
		}
		for (c.f = 1;c.f <= c.e - 1;c.f++)
		{
			if (c.f >= 2)
			{
				s.setovrhd = stpovrhdcalculate(c.f, c.b, t.temp4, r.d, s.setop1, s.setop2, s.setdur);
				schtime = schtime + s.setovrhd + inttime[t.temp4[c.f - 1]];
			}
			fprintf(sc, "%d;%d;%d;%.0lf;%d\n\r", schtime, r.d[t.temp4[c.f]], r.a[t.temp4[c.f]], r.b[t.temp4[c.f]], s.setovrhd);
		}
		if (schtime < t.temp7)
			break;
		schtime = 0;
		
	} while (schtime<t.temp7);
	fclose(sc);
	int i;
	for (i = 1;i <= c.k;i++)
	printf("%d\t%d\n", t.temp6[i], t.temp7);
	system("pause");
}
