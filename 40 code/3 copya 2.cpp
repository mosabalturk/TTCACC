


//	Emin KIVANÇ
//	1306140040
//	Date: 
//	Devolopment Enviorement: Visual Studio 2015





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


//Counting Functions

int siparissay()
{
	FILE* orders= fopen("Orders.txt", "r");
	int satirsayisi = 0;
	int ch = fgetc(orders);
	while (!feof(orders))
	{
		if (ch=='\n')
		{
			satirsayisi++;
		}
		ch = fgetc(orders);
	}
	fclose(orders);
	return satirsayisi+1;
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

int setupsay()
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

int setupdurationhesapla(int orderop1, int orderop2, int setupsayisi, struct setupdurations* setupdurations)
{
	if (orderop1 == orderop2)
	{
		return 0;
	}
	if (orderop1 > orderop2)
	{
		int temp;
		temp = orderop1;
		orderop1 = orderop2;
		orderop2 = temp;
	}
	for (int i = 0; i < setupsayisi; i++)
	{
		if (orderop1 == setupdurations[i].op1 && orderop2 == setupdurations[i].op2)
			return setupdurations[i].duration;
	}
}


//Copy Functions

void copyorderdata(int siparissayisi, struct orders* ptrorder)
{
	
	int i;
	
	FILE* orders = fopen("Orders.txt", "r");

	for (i = 0; i < siparissayisi;i++)
	{
		fscanf(orders, "%d;%d;%d;%d", &ptrorder[i].ordercode, &ptrorder[i].orderamount, &ptrorder[i].orderoptype, &ptrorder[i].orderdeadline);
	}
	fclose(orders);
	
}

void copyopdata(int operasyonsayisi, struct operations* ptrop)
{
	
	int i;
	
	FILE* operations = fopen("Operations.txt", "r");

	for (i = 0; i < operasyonsayisi; i++)
	{
		fscanf(operations, "%d;%d", &ptrop[i].opcode, &ptrop[i].opspeed);
	}
	fclose(operations);

}

void copysetupdata(int setupsayisi, struct setupdurations* ptrsetup)
{
	
	int i;
	
	FILE* setupdurations = fopen("SetupDuration.txt","r");
	for (i = 0; i < setupsayisi; i++)
	{
		fscanf(setupdurations, "%d;%d;%d", &ptrsetup[i].op1, &ptrsetup[i].op2,&ptrsetup[i].duration);
	}
	fclose(setupdurations);
	
}

struct schedule* scheduletimehesapla(struct orders* ptrorder, struct operations* ptrop, struct setupdurations* ptrsetup, struct schedule* ptrschedule, int setupsayisi, int siparissayisi)
{
	ptrorder[-1].orderamount = 0;
	ptrschedule[-1].scheduletime = 0;
	ptrorder[-1].orderoptype = ptrorder[0].orderoptype;
	for (int k = 0; k < siparissayisi; k++)
	{
		ptrschedule[k].scheduletime = ptrschedule[k - 1].scheduletime + ptrorder[k - 1].orderamount / ptrop[ptrorder[k - 1].orderoptype - 1].opspeed + setupdurationhesapla(ptrorder[k - 1].orderoptype, ptrorder[k].orderoptype, setupsayisi, ptrsetup);
	}
	return ptrschedule;
}

void createschedulefile(struct schedule* ptrschedule, int siparissayisi)
{
	FILE* schedule = fopen("Schedule.txt", "w");

	if (schedule == NULL)
	{
		printf("Schedule.txt acilamadi.\n");
	}
	else
	{
		for (int i = 0; i < siparissayisi; i++)
		{
			fprintf(schedule, "%d;%d;%d;%d;%d\n", ptrschedule[i].scheduletime, ptrschedule[i].optype, ptrschedule[i].ordercode, ptrschedule[i].amount, ptrschedule[i].setup);
		}
	}
	fclose(schedule);
}




void main()
{
	struct orders* ptrorder,temp,temp2;
	struct operations* ptrop;
	struct setupdurations* ptrsetup;
	struct schedule* ptrschedule;
	int siparissayisi,operasyonsayisi,setupsayisi;
	FILE *operations=NULL, *setupdurations=NULL, *orders=NULL;


	//File Opening Controls

	{
		operations = fopen("Operations.txt", "r");
		if (operations = NULL)
		{
			printf("Operations.txt acilamadi.\n");
			exit(0);
		}
		setupdurations = fopen("SetupDurations.txt", "r");
		if (setupdurations = NULL)
		{
			printf("SetupDurations.txt acilamadi.\n");
			exit(0);
		}
		orders = fopen("Orders.txt", "r");
		if (orders = NULL)
		{
			printf("Orders.txt acilamadi.\n");
			exit(0);
		}
	}
	

	//Copy Operations

	{
		siparissayisi = siparissay();
		ptrorder = (struct orders*)malloc(sizeof(struct orders)*siparissayisi);
		copyorderdata(siparissayisi,ptrorder);


		operasyonsayisi = operasyonsay();
		ptrop = (struct operations*)malloc(sizeof(struct operations)*operasyonsayisi);
		copyopdata(operasyonsayisi,ptrop);


		setupsayisi = setupsay();
		ptrsetup = (struct setupdurations*)malloc(sizeof(struct setupdurations)*setupsayisi);
		copysetupdata(setupsayisi,ptrsetup);


		ptrschedule = (struct schedule*)malloc(sizeof(struct schedule)*siparissayisi);
	}


	//Sorting

	for (int m = 0; m < siparissayisi;m++)
	{
		for (int n = 0; n < siparissayisi - m - 1;n++)
		{
			if (ptrorder[n].orderdeadline>ptrorder[n+1].orderdeadline)
			{
				temp2 = ptrorder[n];
				ptrorder[n] = ptrorder[n+1];
				ptrorder[n + 1] = temp2;
			}

		}


	}
	
	scheduletimehesapla(ptrorder, ptrop, ptrsetup, ptrschedule, setupsayisi, siparissayisi);

	for (int i = 0; i < siparissayisi; i++)
	{
		for (int j = 0; j < siparissayisi - i - 1; j++)
		{
			scheduletimehesapla(ptrorder, ptrop, ptrsetup, ptrschedule, setupsayisi, siparissayisi);

			if (ptrschedule[j].scheduletime+ptrorder[j].orderamount / ptrop[ptrorder[j].orderoptype].opspeed+ setupdurationhesapla(ptrorder[j].orderoptype, ptrorder[j + 1].orderoptype, setupsayisi, ptrsetup)<ptrorder[j].orderdeadline &&
				ptrorder[j].orderamount/ptrop[ptrorder[j].orderoptype].opspeed/*+setupdurationhesapla(ptrorder[j].orderoptype,ptrorder[j+1].orderoptype,setupsayisi,ptrsetup)*/ < ptrorder[j+1].orderamount / ptrop[ptrorder[j+1].orderoptype].opspeed/*+setupdurationhesapla(ptrorder[j+1].orderoptype, ptrorder[j + 2].orderoptype, setupsayisi, ptrsetup)*/)
			{
						temp = ptrorder[j];
						ptrorder[j] = ptrorder[j + 1];
						ptrorder[j + 1] = temp;
			}
			scheduletimehesapla(ptrorder, ptrop, ptrsetup, ptrschedule, setupsayisi, siparissayisi);
		}
	}
	
	scheduletimehesapla(ptrorder, ptrop, ptrsetup, ptrschedule, setupsayisi, siparissayisi);


	//Create Shedule File

	{
		ptrorder[-1].orderamount = 0;
		ptrschedule[-1].scheduletime = 0;
		ptrorder[-1].orderoptype = ptrorder[0].orderoptype;
		for (int k = 0; k < siparissayisi; k++)
		{
			ptrschedule[k].scheduletime = ptrschedule[k - 1].scheduletime + ptrorder[k - 1].orderamount / ptrop[ptrorder[k - 1].orderoptype - 1].opspeed + setupdurationhesapla(ptrorder[k - 1].orderoptype, ptrorder[k].orderoptype, setupsayisi, ptrsetup);
			ptrschedule[k].optype = ptrorder[k].orderoptype;
			ptrschedule[k].ordercode = ptrorder[k].ordercode;
			ptrschedule[k].amount = ptrorder[k].orderamount;
			ptrschedule[k].setup = setupdurationhesapla(ptrorder[k - 1].orderoptype, ptrorder[k].orderoptype, setupsayisi, ptrsetup);
		}
		createschedulefile(ptrschedule, siparissayisi);
	}
	
	
	//Display Operations
	
	{
		for (int siparissayaci = 0; siparissayaci < siparissayisi; siparissayaci++)
		{
			printf("%d ", ptrorder[siparissayaci].ordercode);
			printf("%d ", ptrorder[siparissayaci].orderamount);
			printf("%d ", ptrorder[siparissayaci].orderoptype);
			printf("%d ", ptrorder[siparissayaci].orderdeadline);
			printf("\n");
		}

		printf("\n");

		for (int opsayaci = 0; opsayaci < operasyonsayisi; opsayaci++)
		{
			printf("%d ", ptrop[opsayaci].opcode);
			printf("%d ", ptrop[opsayaci].opspeed);
			printf("\n");
		}

		printf("\n");

		for (int setupsayaci = 0; setupsayaci < setupsayisi; setupsayaci++)
		{
			printf("%d ", ptrsetup[setupsayaci].op1);
			printf("%d ", ptrsetup[setupsayaci].op2);
			printf("%d ", ptrsetup[setupsayaci].duration);
			printf("\n");
		}
		printf("\n");
		for (int schedulesayaci = 0; schedulesayaci < siparissayisi; schedulesayaci++)
		{
			printf("Scheduletime:%d ", ptrschedule[schedulesayaci].scheduletime);
			printf("Optype:%d ", ptrschedule[schedulesayaci].optype);
			printf("Ordercode:%d ", ptrschedule[schedulesayaci].ordercode);
			printf("Amount:%d ", ptrschedule[schedulesayaci].amount);
			printf("Setup:%d ", ptrschedule[schedulesayaci].setup);
			printf("\n");
		}

		printf("\n");
	}
	

	printf("Schedule Time: %d\n" ,ptrschedule[siparissayisi-1].scheduletime+ ptrorder[siparissayisi-1].orderamount / ptrop[ptrorder[siparissayisi - 1].orderoptype - 1].opspeed);
	

	//free(ptrorder);
	//free(ptrop);
	//free(ptrsetup);	
	//free(ptrschedule);


	system("pause");
}
