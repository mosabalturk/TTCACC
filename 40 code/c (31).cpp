#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#define BOYUT 100
typedef struct SD
{
	int op_code1;
	int op_code2;
	int duration;
}SETUPDURATION;

typedef struct OP
{
	int op_code;
	int minute;
}OPERATION;

typedef struct OR
{
	int order_code;
	int amount;
	int op_code;
	int deadline;
}ORDERS;


typedef struct T_OR
{
	int order_code;
	int amount;
	int op_code;
	int deadline;
}T_ORDERS;

int main()
{
	ORDERS orders[BOYUT];
	SETUPDURATION setup[BOYUT];
	OPERATION operation[BOYUT];
	T_ORDERS t_orders[BOYUT];
	FILE *filePtr;

	int orders_length;
	int setup_length;

	filePtr = fopen("SetupDuration.txt", "r");
	if (filePtr == NULL)
		printf("Dosya is not found!!!");
	else
	{
		int row = 1;
		printf("\nSetupDuration.txt:\n");
		do
		{
			fscanf(filePtr, "%d;%d;%d", &setup[row].op_code1, &setup[row].op_code2, &setup[row].duration);
			printf("%d;%d;%d\n", setup[row].op_code1, setup[row].op_code2, setup[row].duration);
			row++;
		} while (!feof(filePtr));
		setup_length = row;
		fclose(filePtr);
	}

	filePtr = fopen("Operations.txt", "r");
	if (filePtr == NULL)
		printf("Dosya is not found!!!");
	else
	{
		int row = 1;
		printf("\nOperation.txt:\n");
		do
		{
			fscanf(filePtr, "%d;%d", &operation[row].op_code, &operation[row].minute);
			printf("%d;%d\n", operation[row].op_code, operation[row].minute);
			row++;

		} while (!feof(filePtr));
		fclose(filePtr);
	}

	filePtr = fopen("Orders.txt", "r");
	if (filePtr == NULL)
		printf("Dosya is not found!!!");
	else
	{
		int row = 1;
		printf("\nOrders.txt:\n");
		do
		{

			fscanf(filePtr, "%d;%d;%d;%d", &orders[row].order_code, &orders[row].amount, &orders[row].op_code, &orders[row].deadline);
			printf("%d;%d;%d;%d\n", orders[row].order_code, orders[row].amount, orders[row].op_code, orders[row].deadline);
			row++;
		} while (!feof(filePtr));

		orders_length = row;
		fclose(filePtr);
	}
	printf("\nBitirme Siralarina Gore Sira\n");
	
	
	filePtr = fopen("enkýsa.txt", "w");
	if (filePtr == NULL)
		printf("File is not found!!\n");
	else
	{	
		for (int i = 1; i< orders_length; i++)
		{	
			for (int j = 1; j < orders_length - 1; j++)
			{	
				if ((orders[j].deadline > orders[j + 1].deadline))
				{	
					t_orders[j].order_code = orders[j + 1].order_code;
					t_orders[j].amount = orders[j + 1].amount;
					t_orders[j].op_code = orders[j + 1].op_code;
					t_orders[j].deadline = orders[j + 1].deadline;

					orders[j + 1].order_code = orders[j].order_code;
					orders[j + 1].amount = orders[j].amount;
					orders[j + 1].op_code = orders[j].op_code;
					orders[j + 1].deadline = orders[j].deadline;

					orders[j].order_code = t_orders[j].order_code;
					orders[j].amount = t_orders[j].amount;
					orders[j].op_code = t_orders[j].op_code;
					orders[j].deadline = t_orders[j].deadline;

				}
			}
		}
		for(int i=1;i<orders_length;i++)
		{
			fprintf(filePtr, "%d;%d;%d;%d\n", orders[i].order_code, orders[i].amount, orders[i].op_code, orders[i].deadline);
			printf("%d;%d;%d;%d\n", orders[i].order_code, orders[i].amount, orders[i].op_code, orders[i].deadline);
		}
	}
	fclose(filePtr);
	

		
	

	filePtr = fopen("Schedule.txt", "w");
	if (filePtr == NULL)
		printf("File is not found!!\n");
	else
	{
		printf("\nSchedule.txt:\n");
		int sch = 0;
		int sayac = 1;
		int oh = 0;
		while (sayac < orders_length)
		{
			int opcode = orders[sayac].op_code;
			double div = (double)(orders[sayac].amount) / (double)(operation[opcode].minute);
			oh = sch + ceil(div);

			fprintf(filePtr, "%d;%d;%d;%d;%d\n", sch, opcode, sayac, orders[sayac].amount, oh);
			printf("%d;%d;%d;%d;%d\n", sch, opcode, sayac, orders[sayac].amount, oh);

			if (orders[sayac].op_code == orders[sayac + 1].op_code)
			{
				sch = oh;
			}
			else
			{
				for (int i = 1; i < setup_length; i++)
				{
					if ((setup[i].op_code1 == orders[sayac].op_code && setup[i].op_code2 == orders[sayac + 1].op_code) || (setup[i].op_code2 == orders[sayac].op_code && setup[i].op_code1 == orders[sayac + 1].op_code))
					{
						sch = setup[i].duration + oh;
					}
				}
			}
			sayac++;
		}

		fclose(filePtr);
	}



	system("pause");
}
