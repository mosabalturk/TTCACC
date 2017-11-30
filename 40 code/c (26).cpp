//KAMÝL YARBAÞI
//1306140092
//22.05.2016
//VISUAL STUDIO 2015



#include<stdio.h>
#include<stdlib.h>

struct schedule {
	int *schduletime;
	int *operationcode;
	int *ordercode;
	int *setupoverhead;
	int *amountofwork;
}schedule;


struct operation {
	int *opno;
	int *optime;
}operation;

struct setupduration {
	int *firstop;
	int *lastop;
	int *settime;
}setup;

struct orders {
	int *ordnum;
	int *amount;
	int *opcode;
	int *deadline;
}siparisler;



int main()
{
	int count = 0,i=0,j=0,sayac=0;
	int countop = 0;
	int x, y;
	int sayi, islem;
	char ch;
	struct orders temp;
	
	FILE *fptr;
	if ((fptr = fopen("Orders.txt", "r")) == NULL)
		printf("Dosya acilamadi\n");
	//*Orders dosyasýnýn iþlemleri*//
	else
	{
		
		//*Order sayýsý hesaplama*//
		while(!feof(fptr))
		{
			ch = fgetc(fptr);
			if (ch == '\n')
				count++;
		}
		count += 1;

		
		//*Bellekten yer isteme*//
		operation.opno= (int*)malloc((count)*sizeof(int));
		operation.optime = (int*)malloc((count)*sizeof(int));

		setup.firstop= (int*)malloc((count)*sizeof(int));
		setup.lastop= (int*)malloc((count)*sizeof(int));
		setup.settime= (int*)malloc((count)*sizeof(int));

		siparisler.ordnum = (int*)malloc((count)*sizeof(int));
		siparisler.amount= (int*)malloc((count)*sizeof(int));
		siparisler.opcode= (int*)malloc((count)*sizeof(int));
		siparisler.deadline= (int*)malloc((count)*sizeof(int));

		temp.ordnum = (int*)malloc((count)*sizeof(int));
		temp.amount = (int*)malloc((count)*sizeof(int));
		temp.opcode = (int*)malloc((count)*sizeof(int));
		temp.deadline = (int*)malloc((count)*sizeof(int));
		
		schedule.operationcode = (int*)malloc((count)*sizeof(int));
		schedule.ordercode = (int*)malloc((count)*sizeof(int));
		schedule.schduletime = (int*)malloc((count)*sizeof(int));
		schedule.setupoverhead = (int*)malloc((count)*sizeof(int));
		schedule.operationcode = (int*)malloc((count)*sizeof(int));


		rewind(fptr);
		while (!feof(fptr))
		{
			fscanf(fptr, "%d;%d;%d;%d\n", &siparisler.ordnum[sayac], &siparisler.amount[sayac], &siparisler.opcode[sayac], &siparisler.deadline[sayac]);
			sayac++;
		}
	
	

		
		//*Kabarcýk Sýralama*//
		for ( i = 0; i < count; i++)
		{

			for ( j = 0; j < count-i-1; j++)
			{
				if (siparisler.deadline[j]>siparisler.deadline[j + 1])
				{
					temp.deadline[j] = siparisler.deadline[j];
					siparisler.deadline[j] = siparisler.deadline[j + 1];
					siparisler.deadline[j + 1] = temp.deadline[j];

					temp.ordnum[j] = siparisler.ordnum[j];
					siparisler.ordnum[j] = siparisler.ordnum[j + 1];
					siparisler.ordnum[j + 1] = temp.ordnum[j];

					temp.amount[j] = siparisler.amount[j];
					siparisler.amount[j] = siparisler.amount[j + 1];
					siparisler.amount[j + 1] = temp.amount[j];

					temp.opcode[j] = siparisler.opcode[j];
					siparisler.opcode[j] = siparisler.opcode[j + 1];
					siparisler.opcode[j + 1] = temp.opcode[j];
					

				}
			}
		}
	

	

		//*Operationcode için kabarcýk*//
		for (i = 0; i < count; i++)
		{

			for (j = 0; j < count -i+1; j++)
			{
				if (siparisler.deadline[j]==siparisler.deadline[j + 1])
				{
					if (siparisler.opcode[j] != siparisler.opcode[j + 1])
					{
						temp.deadline[j] = siparisler.deadline[j];
						siparisler.deadline[j] = siparisler.deadline[j + 1];
						siparisler.deadline[j + 1] = temp.deadline[j];

						temp.ordnum[j] = siparisler.ordnum[j];
						siparisler.ordnum[j] = siparisler.ordnum[j + 1];
						siparisler.ordnum[j + 1] = temp.ordnum[j];

						temp.amount[j] = siparisler.amount[j];
						siparisler.amount[j] = siparisler.amount[j + 1];
						siparisler.amount[j + 1] = temp.amount[j];

						temp.opcode[j] = siparisler.opcode[j];
						siparisler.opcode[j] = siparisler.opcode[j + 1];
						siparisler.opcode[j + 1] = temp.opcode[j];
					}

				}
			}
		}


		schedule.setupoverhead[0] = 0;

		FILE *aPtr;
		if ((aPtr = fopen("SetupDuration.txt", "r")) == NULL)
			printf("SetupDuration.txt acilamadi\n");
		else
		{
			sayac = 0;
			while (!feof(aPtr))
			{
				fscanf(aPtr, "%d;%d;%d\n", &setup.firstop[sayac], &setup.lastop[sayac], &setup.settime[sayac]);
				sayac++;
			}
			for ( i = 0; i < count-1; i++)
			{
				x = siparisler.opcode[i];
				y =siparisler.opcode[i + 1];
				if (x == y)
					schedule.setupoverhead[i + 1] = 0;
				rewind(aPtr);
				for ( j = 0; j < count; j++)
				{
					if (((setup.firstop[j] = x )|| (setup.lastop[j] = y)) && ((setup.firstop[j] = y) || (setup.lastop[j] = x)))
						schedule.setupoverhead[i + 1] = setup.settime[j];
				}


				


			}

		}
		int degisken;
		FILE *oPtr;
		if ((oPtr = fopen("Operations.txt", "r+")) == NULL)
			printf("Operations.txt acilamadi\n");
		else
		{
			sayac = 0;
			while (!feof(oPtr))
			{
				fscanf(oPtr, "%d;%d\n", operation.opno[sayac], operation.optime[sayac]);
				sayac++;
			}
			schedule.schduletime[0] = 0;
			int sure2;
			for ( i = 0; i < count; i++)
			{
				schedule.amountofwork[i] = siparisler.amount[i];
				sayi = siparisler.amount[i];
				islem = siparisler.opcode[i];
				for (int  a = 0; a < sayac; a++)
				{
					if (islem == operation.opno[a])
						sure2 = operation.opno[a];
				}
				for ( j = 1; j < count; j++)
				{
					schedule.schduletime[j] = (sayi / sure2 + schedule.setupoverhead[j] + schedule.schduletime[j - 1]);
				}

			}

		}

		FILE *sPtr;
		if ((sPtr = fopen("Schedule.txt", "w")) == NULL)
			printf("Schedule.txt oluþturulamadi\n");
		else
		{
			for ( i = 0; i < count; i++)
			{
				fprintf(sPtr, "%d;%d;%d;%d;%d\n", schedule.schduletime[i], schedule.operationcode[i], schedule.ordercode[i], schedule.amountofwork[i], schedule.setupoverhead[i]);
			}
		}





	}
}
