#include<stdio.h>
#include<string.h>
typedef struct operation{
	int opCode;
	int duration;
}opr;
typedef struct setupDuration{
	int opCode1;
	int opCode2;
	int dur;
}setup;
typedef struct orders{
	int orderCode;
	int amountofwork;
	int oprCode;
	int deadline;
}order;
typedef struct schedule{
	int schedule times;
	int opra code;
	int ord code;
	int amountofworks;
	int setupoverhead;
}schedule;

void txtDosyasiCoz(txadi, dizidegiskenim[])
{
	FILE*f;
	int i = 0, k = 0;
	strint satir = "";
	f = fopen(txai, "r");
	while (feof(f) == NULL)
	{
		ch = getc(f);
		if (ch == '\n')
		{
			k++;
			dizidegiskenim[k - 1] = satir;
			satir = "";
		}
		satir = satir + ch;
	}
}

main()
{
	FILE*operation;
	int i = 0, k = 0, satirsayisi = 0;
	char ch;
	f = fopen("operation.txt", "r");
	while (feof(operation) == NULL)
	{
		ch = getc(operation);
		if (ch == '\n')
			++k;
	}
	satirsayisi = k + 1;
	txtDosyasiCoz(operation, satirsayisi);
	fclose(operation);

	FILE*setupdur;
	int i = 0, k = 0, satirsayi = 0;
	char kr;
	f = fopen("setupduration.txt", "r");
	while (feof(setupdur) == NULL)
	{
		ch = getc(setupdur)
			if (ch == '\n')
				++k.,
	}
	satirsayisi = k + 1;
	txtDosyasiCoz(setupdur, satirsayisi);
	fclose(setupdur);

	FILE*order;
	int i = 0, k = 0, SatirSayisi = 0;
	char ef;
	f = fopen("orders.txt", "r");
	while (feof(order == NULL))
	{
		ch = getc(order)
			if (ch == '\n')
				++k;
	}
	SatirSayisi = k + 1
		txtDosyasiCoz(order, SatirSayisi);
	fclose(orders);

	fopen("orders.txt", "r");
	char ch, rk, cha, ab, cd;
	int SatirSayisi = 0; a, b, m, n, t;
	char Dizii[100], ddizi[100], temp[100], chararray[100], charDizi[100];
	while (feof(orders) == NULL)
	{
		for (SatirSayisi = 0; SatirSayisi <= 100; ++SatirSayisi)
		{
			ch = getc(orders);
			if (ch == ';')
				++a;
			ch = getc(orders);
			if (ch == ';')
				++b;
			ch = getc(orders);
			if (ch == ';')
				while (!ch == '\n')
				{
				ch = fetc(orders);
				chartemp[100] = ch;
				}
		}
	}

	fclose(orders);

	fopen("operation.txt", "r");
	while (feof(operation) == NULL)
	{
		for (satirsayisi = 0; satirsayisi <= 100; ++satirsayisi)
		{
			rk = getc(operation);
			if (rk == ';')
				while (!rk == '\n')
				{
				rk = getc(operation);
				chararray[] = rk;
				}
		}
	}
	fclose(operation);
	fopen("orders.txt", "r")
		while (feof(order) == NULL)
		{
		for (SatirSayisi = 0; SatirSayisi <= 100; ++SatirSayisi)
		{
			cha = getc(order);
			if (cha == ';')
				while (!cha == '\n')
				{
				cha = getc(order);
				charDizi[] = cha;
				cha = getc(order);
				if (cha == ';'
					while (!cha == '\n')
					{
					charDizi[] = ';';
					charDizi[] = cha;

					}
				}
		}
		}
	fclose(orders);

	fopen("operation.txt", "r")
		while (feof(operation) == NULL)
		{
		for (satirsayisi = 0; satirsayisi <= 100; ++satirsayisi)
		{
			ab = getc(operation);
			while (!(ab == ';' || ab == '\n'))
			{
				ab = getc(operation);
				charDizii[] = ab;
			}
		}
		}
	fclose(operation);

	fopen("orders.txt", "r");
	while (feof(operation) == NULL)
	{
		for (SatirSayisi = 0; SatirSayisi <= 100; ++SatirSayisi)
		{
			cd = getc(order);
			if (cd == ';')
				++t;
			cd = getc(order);
			if (cd == '\n')
				while (!cd == '\n')
				{
				cd = getc(order);
				charddizi[] = cd;
				}
		}
	}
	fclose(order);

	for (n = 0; n <= 100; ++n)
	{
		if (chartemp[n] >= chartemp[n + 1])
			fopen("schedule.txt", "a")
		{
			scheduletime[n] = 0;
			opacode[n] = charDizii[n];
			ordcode[n] = chartemp[n];
			amounofwork[n] = chardizi[n];
			setupoverhead[n] = chararray[n];
		}
	}

	for (m = 0; m <= 100; ++m)
	{
		fputs(scheduletime[n], schedule);
		fputs(';', schedule);
		fputs(opacode[n], schedule);
		fputs(';', schedule);
		fputs(ordcode[n], schedule);
		fputs(';', schedule);
		fputs(amountowork[n], schedule);
		fputs(';', schedule);
		fputs(setupoverhead[n], schedule)
	}
	fwrite(&schedule, sizeof(*schedule), 100ü schedule);
	fclose(schedule);
}
}


