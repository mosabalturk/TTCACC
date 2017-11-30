//Volkan Burak Coþkun
//1306140076     
//Date : 20.05.2016 
//Development Environment : Visual Studio 2015 


#include <stdio.h>
#include <stdlib.h>
typedef struct a {
	int  ordername;
	int  speed;
	int  start;
	int  amount;
	int  end;
}process;
typedef struct speeds {
	int speed;
}sp;
sp * speed();
void islemci();
process * data();
int siralama(int,int*);
int get_order_number();//Kaç adet sipariþ olduðunu bulan fonksiyon(Func that can find order_number)//
int **get_setup_duration();//setup_durationslarin saklandigi dizi.(Array that has setup durations.)//
int*get_order_name();
int *get_op_names();
int get_op_number();
int setup_duration_toplatma(); //Setup durationslarý toplamý en yüksek olan ilk baþta.En yüksek olanýný bulan fonksiyon//
sp * speed()
{
	FILE*speed;
	int	o_number = get_op_number(), i = 1, temp, a;
	sp *list = (sp*)malloc(sizeof(sp*)*o_number);
	speed = fopen("Operations.txt", "r");
	while (!feof(speed))
	{
		fscanf(speed, "%d;%d\n", &a, &temp);
		list[a].speed = temp;
		i++;
	}

	return list;
	fclose(speed);
}

int main()
{
	islemci();
	system("PAUSE");
}
int get_op_number()
{

	FILE*op;
	int op_number = 0; int i = 0;
	char a;

	op = fopen("Operations.txt", "r");
	
	
		while (!feof(op))
		{

			a = getc(op);
			if (a == '\n')
			{
				++op_number;
			}
		}
	
	return op_number + 1;
}
int get_order_number() {

	FILE*di;
	char a, number = 0;
	di = fopen("Orders.txt", "r");
	if ((di = fopen("Orders.txt", "r")) == NULL)
	{
		printf("Error..Orders.txt empty or not valid.Please create a file that named as 'orders.txt' or write orders.\n");
	}
	else
	{
		while (feof(di) == NULL)
		{
			a = getc(di);
			if (a == '\n')
			{
				number++;
			}

		}
	}
	fclose(di);
	return number + 1;
} 
int toplama(int);
int **get_setup_duration()
{
	FILE*duration;
	int source, destiny, sd;
	int op_number = get_op_number();
	int **durat = (int**)malloc(sizeof(int*) *op_number);
	int temp = op_number;
	for (int i = 1; i <= op_number; i++)
	{
		durat[i] = (int*)malloc(sizeof(int)*temp);
		temp++;
	}

	duration = fopen("SetupDuration.txt", "r");

	while (!feof(duration))
	{
		fscanf(duration, "%d;%d;%d", &source, &destiny, &sd);
		durat[source][destiny] = sd;
		durat[destiny][source] = sd;
		durat[source][source] = 0;
		durat[destiny][destiny] = 0;
		fscanf(duration, "\n");
	}

	fclose(duration);
	return durat;
}
int *get_order_name()
{
	int o_n = get_order_number(), t, t2, t3, name, i = 0;
	FILE*di;
	int *names = (int*)malloc(sizeof(int*)*o_n);
	di = fopen("Orders.txt", "r");
	
	while (!feof(di))
	{
		fscanf(di, "%d;%d;%d;%d\n", &t, &t2, &name, &t3);
		names[i] = name;
		i++;
	}
	fclose(di);
	return names;
}
int * get_op_names()
{
	FILE*orders;
	char a;
	int temp;
	int op_number = get_op_number(), i = 0, j = 0, temp2;
	int *o_names = (int*)malloc(sizeof(int)*op_number);
	orders = fopen("Operations.txt", "r");
	while (feof(orders) == NULL)
	{

		fscanf(orders, "%d;%d", &temp, &temp2);

		o_names[i] = temp;
		i++;


	}

	fclose(orders);
	return o_names;
}
process * data()
{
	int o_n = get_order_number(), t, t2, t3, name, i = 0;
	FILE*di;

	int *ordername_copy = get_order_name();
	int on = get_order_number();
	int * opnames = get_op_names();
	process * array = (process*)malloc(sizeof(process)*on);
	di = fopen("Orders.txt", "r");
	while (!feof(di))
	{
		fscanf(di, "%d;%d;%d;%d\n", &t, &array[i].amount, &array[i].ordername, &t2);
		i++;
	}
	return array;
}
int setup_duration_toplatma()
{
	int i = 0, order_number = get_order_number();
	int *namearray = NULL;
	namearray = (int *)malloc(sizeof(int)*(order_number + 1));
	process *array = data();
	int maxname = 0, temp = 0, toplam;
	for (i = 0; i < order_number; i++)
	{
		namearray[i] = array[i].ordername;

	}
	for (i = 0; i < order_number; i++)
	{

		toplam = toplama(namearray[i]);
		if (toplam>temp)
		{

			temp = toplam;
			maxname = namearray[i];
		}

	}
	return maxname;

}
int toplama(int a)
{
	int *op_names = get_op_names();
	int **durations = get_setup_duration();
	int i = 0, op_number = get_op_number(), toplam = 0;
	durations = get_setup_duration();
	for (i = 1; i <= op_number; i++)
	{
		toplam += durations[op_names[i - 1]][a];

	}
	return toplam;
}
int siralama(int a,int *namearray)
{
	int op_number = get_op_number();
	int **sd = get_setup_duration();
	int order_number = get_order_number();
	int *temp = (int*)malloc(sizeof(int)*op_number + 1);
	int name=0,gecici = 10000;
	
	for (int j = 1; j <=order_number; j++)
	{
		if (namearray[j-1] != 0) {
			for (int k = 1; k <=op_number; k++) 
			{
				if (temp[namearray[j-1]] < gecici&&namearray[j-1] != a)
				{
					gecici = temp[namearray[j-1]];
					name = namearray[j-1];
				}
			}
		}
		
	}

	return name;
}
void islemci()
{
	sp*hiz = speed();
	FILE*son;
	son = fopen("Schedule.txt", "a");
	process *arraycopy = data();
	
	int i = 0, j = 0, order_number = get_order_number(), ilk_operasyon = setup_duration_toplatma(), amount, time = 0, hizz, start, end = 0, kontro = 1, oncekiislemadi, k = 0;
	int *namearray = (int *)malloc(sizeof(int)*order_number);
	int setup_duration,siradakiislem,op_number=get_op_number();
	int **sd = get_setup_duration();
	for (int i = 0; i < order_number; i++)
	{
		namearray[i] = arraycopy[i].ordername;
	}
	
	for (int i = 0; i <order_number; i++)
	{
		if (namearray[i] == ilk_operasyon&&namearray[i] != 0)
		{
			amount = arraycopy[i].amount;
			hizz = hiz[arraycopy[i].ordername].speed;
			time = amount / hizz;
			start = end;
			end = time + start;
			namearray[i] = 0;
			oncekiislemadi = arraycopy[i].ordername;
			fprintf(son, "%d;%d;%d;%d;%d\n", start, arraycopy[i].ordername, i + 1, amount, 0);
		}
	}
	
	for (int k = 0; k < order_number; k++)
	{
		
		siradakiislem = siralama(ilk_operasyon,namearray);

		for (int j = 0; j <= order_number; j++)
		{
			if (namearray[j] == siradakiislem && namearray[j]!=0)
			{
				amount = arraycopy[j].amount;
				hizz = hiz[arraycopy[j].ordername].speed;
				time = amount / hizz;
				setup_duration = sd[ilk_operasyon][siradakiislem];
				start = end;
				setup_duration = sd[ilk_operasyon][siradakiislem];
				end = time + start+setup_duration;
				fprintf(son, "%d;%d;%d;%d;%d\n\r", start, namearray[j], j + 1, amount, sd[ilk_operasyon][siradakiislem]);
				ilk_operasyon = siradakiislem;

				namearray[j] = 0;

			}
		}

	}

}
//Kodum 60 sipariþe kadar saðlam çalýþýyor ama sonrasýnda anlamadýðým þekilde e posta ile yolladýðým hatayý veriyor. 
//Kodumun mantýðý da sipariþlere giden setup durationlarý toplatýp
//O sipariþe giden setup durationlar en fazla olandan baþlamak.Daha sonra basit bir sýralama algoritmasýyla
//en düþük setup duration olanla devam etmek.
