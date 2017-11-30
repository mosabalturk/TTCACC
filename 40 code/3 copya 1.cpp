//Enes Mahmut ÇAKMAK
//1306140051
//Date:20.05.2016
//Devolopment Enviorement: Visual Studio 2015

#include <stdio.h>
#include <stdlib.h>

//Structs

struct orders
{
	int ordercode;
	int orderamount;
	int orderoptype;
	int orderdeadline;
};

struct operations
{
	int opcode;
	int opspeed;
};

struct setupdurations
{
	int op1;
	int op2;
	int duration;
};

struct schedule
{
	int scheduletime;
	int optype;
	int ordercode;
	int amount;
	int setup;
};

//Functions

int siparissay()
{
	FILE* orders = fopen("Orders.txt", "r");
	int satirsayisi = 0;
	int ch = fgetc(orders);
	while (!feof(orders))
	{
		if (ch == '\n')
		{
			satirsayisi++;
		}
		ch = fgetc(orders);
	}
	fclose(orders);
	return satirsayisi + 1;
}
int operasyonsay()
{
	FILE* operations = fopen("Operations.txt", "r");
	int satirsayisi = 0;
	int ch = fgetc(operations);
	while (!feof(operations))
	{
		if (ch == '\n')
		{
			satirsayisi++;
		}
		ch = fgetc(operations);
	}
	fclose(operations);
	return satirsayisi + 1;
}
int setupdurationsay()
{
	FILE* setupdurations = fopen("SetupDuration.txt", "r");
	int satirsayisi = 0;
	int ch = fgetc(setupdurations);
	while (!feof(setupdurations))
	{
		if (ch == '\n')
		{
			satirsayisi++;
		}
		ch = fgetc(setupdurations);
	}
	fclose(setupdurations);
	return satirsayisi + 1;
}
int setupdurationshesapla(int order1, int order2, int setupsayisi, struct setupdurations* setupdurations)
{
	if (order1 == order2)
	{
		return 0;
	}
	if (order1 > order2)
	{
		int temp;
		temp = order1;
		order1 = order2;
		order2 = temp;
	}
	for (int i = 0; i < setupsayisi; i++)
	{
		if (order1 == setupdurations[i].op1 && order2 == setupdurations[i].op2)
			return setupdurations[i].duration;
	}
}

//Copy Functions
	void copyorderdata(int siparissayisi, struct orders* ptrorder)
{
	int x;
	FILE* orders = fopen("Orders.txt", "r");
	for (x = 0; x < siparissayisi;x++)
	{
		fscanf(orders, "%d;%d;%d;%d", &ptrorder[x].ordercode, &ptrorder[x].orderamount, &ptrorder[x].orderoptype, &ptrorder[x].orderdeadline);
	}
	fclose(orders);

}

	void copyopdata(int operasyonsayisi, struct operations* ptrop)
{
	int x;
	FILE* operations = fopen("Operations.txt", "r");
	for (x = 0; x < operasyonsayisi;x++)
	{
		fscanf(operations, "%d;%d;", &ptrop[x].opcode, &ptrop[x].opspeed);
	}
	fclose(operations);
}

	void copysetupdurationdata(int setupsayisi, struct setupdurations* ptrsetup)
{
	int x;
	FILE* setupdurations = fopen("SetupDurations.txt", "r");
	for (x = 0;x < setupsayisi;x++)
	{
		fscanf(setupdurations, "%d;%d;%d", &ptrsetup[x].op1, &ptrsetup[x].op2, &ptrsetup[x].duration);
	}
	fclose(setupdurations);
}

	struct schedule* scheduletimecalculation(struct orders* ptrorder, struct operations* ptrop, struct setupdurations* ptrsetup, struct schedule* ptrschedule, int setupsayisi, int siparissayisi)
{
	ptrorder[-1].orderamount = 0;
	ptrschedule[-1].scheduletime = 0;
	ptrorder[-1].orderoptype = ptrorder[0].orderoptype;
	for (int k = 0; k < siparissayisi; k++)
	{
		ptrschedule[k].scheduletime = ptrschedule[k - 1].scheduletime + ptrorder[k - 1].orderamount / ptrop[ptrorder[k - 1].orderoptype - 1].opspeed + setupdurationshesapla(ptrorder[k - 1].orderoptype, ptrorder[k].orderoptype, setupsayisi, ptrsetup);
	}
	return ptrschedule;
}

void createschedulefile(struct schedule* ptrschedule, int siparissayisi)
{
	FILE* schedule = fopen("Schedule.txt", "w");
	if (schedule == NULL)
	{
		printf("Schedule.txt acilamadi!!!\n");
	}
	else
	{
		for (int x = 0;x < siparissayisi;x++)
		{
			fprintf(schedule, "%d;%d;%d;%d;%d", ptrschedule[x].scheduletime, ptrschedule[x].optype, ptrschedule[x].ordercode, ptrschedule[x].amount, ptrschedule[x].setup);
		}
	}
	fclose(schedule);
}



void main()
{
	struct orders* ptrorder, temp, temp2;
	struct operations* ptrop;
	struct setupdurations* ptrsetup;
	struct schedule* ptrschedule;
	int setupcount, operationcount, orderscount;
	FILE* operations = NULL, *setupdurations = NULL, *orders = NULL;
	//operations take the file
	//setupdurations take the file
	//orders take the file
	{
		operations = fopen("Operations.txt", "r");
		if (operations = NULL)
		{
			printf("Operations.txt acilamadi!!!\n");
			exit(1);
		}
		orders = fopen("Orders.txt", "r");
		if (orders = NULL)
		{
			printf("Orders.txt acilamdi!!!\n");
			exit(1);
		}
		setupdurations = fopen("SetupDurations.txt", "r");
		if (setupdurations = NULL)
		{
			printf("SetupDurations.txt acilamadi!!!\n");
			exit(1);
		}
	}


	//Copy Operations
	{
		orderscount = siparissay();
		ptrorder = (struct orders*)malloc(sizeof(struct orders)*orderscount);

		operationcount = operasyonsay();
		ptrop = (struct operations*)malloc(sizeof(struct operations)*operationcount);

		setupcount = setupdurationsay();
		ptrsetup = (struct setupdurations*)malloc(sizeof(struct setupdurations)*setupcount);

		ptrschedule = (struct schedule*)malloc(sizeof(struct schedule)*orderscount);
	}
	//Sorting
	for (int a = 0;a < orderscount;a++)
	{
		for (int b = 0;b < orderscount - a - 1;b++)
		{
			if (ptrorder[b].orderdeadline > ptrorder[b + 1].orderdeadline)
			{
				temp = ptrorder[a];
				ptrorder[a] = ptrorder[a + 1];
				ptrorder[a + 1] = temp;
			}
		}
	}

	for (int x = 0;x < orderscount;x++)
	{
		for (int y = 0;y < orderscount - x - 1;y++)
		{
			scheduletimecalculation(ptrorder, ptrop, ptrsetup, ptrschedule, setupcount, orderscount);
			if (ptrschedule[y].scheduletime + ptrorder[y].orderamount / ptrop[ptrorder[y].orderoptype].opspeed + setupdurationshesapla(ptrorder[y].orderoptype, ptrorder[y + 1].orderoptype, setupcount, ptrsetup)<ptrorder[y].orderdeadline &&
				ptrorder[y].orderamount / ptrop[ptrorder[y].orderoptype].opspeed < ptrorder[y + 1].orderamount / ptrop[ptrorder[y + 1].orderoptype].opspeed)
			{
				temp2 = ptrorder[y];
				ptrorder[y] = ptrorder[y + 1];
				ptrorder[y + 1] = temp2;
			}
		}
	}

	scheduletimecalculation(ptrorder, ptrop, ptrsetup, ptrschedule, setupcount, orderscount);

	//Create Schedule File
	{
		ptrorder[-1].orderamount = 0;
		ptrschedule[-1].scheduletime = 0;
		ptrorder[-1].orderoptype = ptrorder[0].orderoptype;
		for (int i = 0;i < orderscount;i++)
		{
			ptrschedule[i].scheduletime = ptrschedule[i-1].scheduletime + ptrorder[i - 1].orderamount / ptrop[ptrorder[i - 1].orderoptype-1].opspeed + setupdurationshesapla(ptrorder[i - 1].orderoptype, ptrorder[i].orderoptype, setupcount, ptrsetup);
			ptrschedule[i].amount = ptrorder[i].orderamount;
			ptrschedule[i].optype = ptrorder[i].orderoptype;
			ptrschedule[i].ordercode = ptrorder[i].ordercode;
			ptrschedule[i].setup = setupdurationshesapla(ptrorder[i - 1].orderoptype, ptrorder[i].orderoptype, setupcount, ptrsetup);
		}
		createschedulefile(ptrschedule, orderscount);
	}

	/*{
	printf("Order File: \n-----------\n");
	for (int siparissayaci = 0; siparissayaci < orderscount; siparissayaci++)
	{
	printf("%d ", ptrorder[siparissayaci].ordercode);
	printf("%d ", ptrorder[siparissayaci].orderamount);
	printf("%d ", ptrorder[siparissayaci].orderoptype);
	printf("%d ", ptrorder[siparissayaci].orderdeadline);
	printf("\n");
	}

	printf("\nOperations File: \n-----------\n");

	for (int opsayaci = 0; opsayaci < operationscount; opsayaci++)
	{
	printf("%d ", ptrop[opsayaci].opcode);
	printf("%d ", ptrop[opsayaci].opspeed);
	printf("\n");
	}

	printf("\nSetupDurations File: \n---------------\n");

	for (int setupsayaci = 0; setupsayaci < setupcount; setupsayaci++)
	{
	printf("%d ", ptrsetup[setupsayaci].op1);
	printf("%d ", ptrsetup[setupsayaci].op2);
	printf("%d ", ptrsetup[setupsayaci].duration);
	printf("\n");
	}
	printf("\nSchedule File: \n----------\n");
	for (int schedulesayaci = 0; schedulesayaci < operationscount; schedulesayaci++)
	{
	printf("Scheduletime:%d ", ptrschedule[schedulesayaci].scheduletime);
	printf("Optype:%d ", ptrschedule[schedulesayaci].optype);
	printf("Ordercode:%d ", ptrschedule[schedulesayaci].ordercode);
	printf("Amount:%d ", ptrschedule[schedulesayaci].amount);
	printf("Setup:%d ", ptrschedule[schedulesayaci].setup);
	printf("\n");
	}

	printf("\n");
	}*/





	printf("Schedule Time: %d\n", ptrschedule[orderscount - 1].scheduletime + ptrorder[orderscount - 1].orderamount / ptrop[ptrorder[orderscount - 1].orderoptype - 1].opspeed);
	//Check Free functions
	free(ptrop);
	free(ptrsetup);
	free(ptrschedule);
	free(ptrorder);



	system("PAUSE");
}

