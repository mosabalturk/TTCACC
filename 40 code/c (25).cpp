
// Ýlyas GÜLEN
// 1306130095
// Date : 28.04.2016
// Development Enviorement : Visual Studio2013

#include <stdio.h>
#include <string.h>
#include <math.h>
#include <stdlib.h>

int DeadLineControl(int);
int OperationDuration(int);
int SetupDuration(int, int);
int OperationCount(char[]);
void OrdersOperation();
void Schedule();
int operationFirst[4];
int operationSecond[4];
int lineNumber = 0;
int ScheduleTime = 0;
bool IsLineNumberInc = false;
float FirstOperationTime = 0;
float operationTime = 0;
int *ordersArray2;
int *deadLineArray;
int main(void)
{
	static char filename[] = "Orders.txt";
	int operationsSize = 4 * OperationCount(filename);
	ordersArray2 = (int *)malloc(sizeof(int)*operationsSize);
	OrdersOperation();
	if (ordersArray2 == NULL)
	{
		printf("Orders.txt dosyasi okunurken hata olustu");
		getchar();
		return 0;
	}
	int j = 0;
	while (j < operationsSize)
	{
		if (operationSecond[0] != 0 && operationSecond[0] >= 0)
			for (int i = 0; i < 4; i++)
			{
				operationFirst[i] = operationSecond[i];
			}
		int i = 0;
		while (i <= 3)
		{
			int deger = *(ordersArray2 + j + i);
			operationSecond[i] = *(ordersArray2 + j + i);
			i++;
			IsLineNumberInc = true;

		}
		if (IsLineNumberInc)
		{
			if (operationSecond[0] != 0 && operationSecond[0] >= 0)
				Schedule();
			IsLineNumberInc = false;
			lineNumber++;
		}
		j += 4;
	}
	printf("Scheduling Operation is completed.\nGo to Schedule.txt");
	getchar();
	return 0;
}
int OperationDuration(int operationCode) {

	static char filename[] = "Operations.txt";
	//int const operationsSize =8;//OperationCount(filename) * 2;	
	//int operations[8];
	int operationsSize = 2 * OperationCount(filename);
	int *operations = (int *)malloc(sizeof(int)*operationsSize);
	FILE *file = fopen(filename, "r");
	if (file != NULL)
	{
		char *p;
		int i = 0;
		char oprcode[100]; /* or other suitable maximum line size */
		while (fgets(oprcode, sizeof oprcode, file) != NULL) /* read a line */
		{
			//printf("Alinan %s mesajini parcaliyoruz:\n ", oprcode);
			p = strtok(oprcode, ";\n");
			while (p != NULL)
			{
				//printf("%s\n", p);
				int y;
				sscanf(p, "%d", &y);
				*(operations + i) = y;
				i++;
				p = strtok(NULL, ";");

			}
		}
		fclose(file);
		/*for (int j = 0; j < 8; j++) {
			printf("Element[%d] = %d\n", j, operations[j]);
		}*/
		for (int j = 0; j < operationsSize; j++) {
			int a = *(operations);
			if (operationCode == a) {
				operations++;
				a = *(operations);
				return a;
			}
			operations++;
		}
	}
	else
	{
		perror(filename); /* why didn't the file open? */
	}
//	free(operations);
	return 0;
}
int DeadLineControl(int orderCode) {

	static char filename[] = "Orders.txt";
	//int const operationsSize =8;//OperationCount(filename) * 2;	
	//int operations[8];
	int operationsSize = 2 * OperationCount(filename);
	int orderInfo[4];
	int OperationCode , OrderCode , AmountOfWork, DeadLine ;
	
	if (deadLineArray==NULL)
	{
		deadLineArray = (int *)malloc(sizeof(int)*operationsSize);
		FILE *file = fopen(filename, "r");
		if (file != NULL)
		{
			char *p;
			int y = 0;
			char oprcode[100]; /* or other suitable maximum line size */
			while (fgets(oprcode, sizeof oprcode, file) != NULL) /* read a line */
			{
				int i = 0;
				//printf("Alinan %s mesajini parcaliyoruz:\n ", oprcode);
				p = strtok(oprcode, ";\n");
				while (p != NULL)
				{
					//printf("%s\n", p);
					int y;
					sscanf(p, "%d", &y);
					//*(deadLineArray + i) = y;
					
					orderInfo[i] = y;
					i++;
					p = strtok(NULL, ";");

				}
				int OperationCode = orderInfo[2], OrderCode = orderInfo[0], AmountOfWork = orderInfo[1], DeadLine = orderInfo[3];
				if (p==NULL)
				{
					*(deadLineArray + y) = OrderCode;
					float operationDuration = OperationDuration(OperationCode);
					float operationTime2 = ceil(AmountOfWork / operationDuration);
					*(deadLineArray + y + 1) = operationTime2;
					y += 2;
				}
			}
			fclose(file);
			/*for (int j = 0; j < 8; j++) {
			printf("Element[%d] = %d\n", j, operations[j]);
			}*/
			/*int a = *(deadLineArray+(2*orderCode-2));
			if (orderCode == a) 
			{
				a = *(deadLineArray + (2 * orderCode-1));
				return a;
			}*/
			
		}
	}
		/*else
		{
			int a = *(deadLineArray + (2 * orderCode - 2));
			if (orderCode == a)
			{
				a = *(deadLineArray + (2 * orderCode - 1));
				return a;
			}
		}*/
		//	free(operations);
	
	
	return 0;
}
int SetupDuration(int operationCodePrev, int operationCodeNext) {

	static char filename[] = "SetupDuration.txt";
	//int const operationsSize =8;//OperationCount(filename) * 2;	
	int operationsSize = 3 * OperationCount(filename);
	int *operations = (int *)malloc(sizeof(int)*operationsSize);
	FILE *file = fopen(filename, "r");
	if (file != NULL)
	{
		char *p;
		int i = 0;
		char oprcode[100]; /* or other suitable maximum line size */
		while (fgets(oprcode, sizeof oprcode, file) != NULL) /* read a line */
		{
			//printf("Alinan %s mesajini parcaliyoruz:\n ", oprcode);
			p = strtok(oprcode, ";\n");
			while (p != NULL)
			{
				//printf("%s\n", p);
				int y;
				sscanf(p, "%d", &y);
				*(operations + i) = y;
				i++;
				p = strtok(NULL, ";");

			}
		}
		fclose(file);

		for (int j = 0; j < operationsSize; j++) {
			int a = *(operations + j);
			int b = *(operations + j + 1);
			int c = *(operations + j + 2);
			if ((operationCodePrev == a && operationCodeNext == b) || (operationCodePrev == b && operationCodeNext == a))
			{
				free(operations);
				return c;
			}
			j += 2;
		}
	}
	else
	{
		perror(filename); /* why didn't the file open? */
	}
	free(operations);
	return 0;
}
int OperationCount(char fileName[])
{
	FILE* myfile = fopen(fileName, "r");
	int ch, number_of_lines = 0;

	do
	{
		ch = fgetc(myfile);
		if (ch == '\n')
			number_of_lines++;
	} while (ch != EOF);

	if (ch != '\n' && number_of_lines != 0)
		number_of_lines++;

	fclose(myfile);

	//printf("number of lines in test.txt = %d", number_of_lines);


	return number_of_lines;
}
void Schedule()
{
	FILE *ptr_file;
	int OperationCode = operationSecond[2], OrderCode = operationSecond[0], AmountOfWork = operationSecond[1], SetupOverhead = 0;
	if (OrderCode == 0)
		return;//5. ve 6. satýrdan sonra sadece 0 okuyor onun için
	ptr_file = fopen("Schedule.txt", "a");

	if (lineNumber == 0)
	{
		fprintf(ptr_file, "%d;%d;%d;%d;%d\n", ScheduleTime, OperationCode, OrderCode, AmountOfWork, SetupOverhead);
		float operationDuration = OperationDuration(OperationCode);
		FirstOperationTime = ceil(AmountOfWork / operationDuration);
		 
	}
	else
	{

		SetupOverhead = SetupDuration(operationFirst[2], operationSecond[2]);
		ScheduleTime += FirstOperationTime + operationTime + SetupOverhead;
		fprintf(ptr_file, "%d;%d;%d;%d;%d\n", ScheduleTime, OperationCode, OrderCode, AmountOfWork, SetupOverhead);
		FirstOperationTime = 0;
		float operationDuration = OperationDuration(OperationCode);
		operationTime = ceil(AmountOfWork / operationDuration);
	}

	fclose(ptr_file);
}
void OrdersOperation() {

	static char filename[] = "Orders.txt";
	int operationsSize = 4 * OperationCount(filename);
	int *ordersArray = (int *)malloc(sizeof(int)*operationsSize);
	
	FILE *file = fopen(filename, "r");
	if (file != NULL)
	{
		char *p;
		int i = 0;
		char oprcode[100]; /* or other suitable maximum line size */
		while (fgets(oprcode, sizeof oprcode, file) != NULL) /* read a line */
		{
			//printf("Alinan %s mesajini parcaliyoruz:\n ", oprcode);
			p = strtok(oprcode, ";\n");
			while (p != NULL)
			{
				//printf("%s\n", p);
				int y;
				sscanf(p, "%d", &y);
				*(ordersArray + i) = y;
				i++;
				p = strtok(NULL, ";");

			}
		}
		//fclose(file);
	}
	else
	{
		perror(filename); /* why didn't the file open? */
	}
	for (int i = 0; i < operationsSize; i++)
	{
		int a = *(ordersArray + i);
		printf("%d;", a);

		if (i % 4 == 3)
		{
			printf("\n");
		}
	}


	int   c, d, swap, firstFourth, secondFourth,firstDiv,secondDiv, firstDeadLine, secondDeadLine;
	int ilk, ilk2, ikinci, ikinci2, ucuncu, ucuncu2, dorduncu, dorduncu2;

	for (c = 0; c < (operationsSize - 1); c++)
	{
		for (d = 0; d < operationsSize - c - 1; d++)
		{
			
			int firsOrc = *(ordersArray + d); int secOrc = *(ordersArray + d + 4);
			firstFourth = *(ordersArray + d + 3); secondFourth = *(ordersArray + d + 7);
			if (firsOrc>0 )//&& firsOrc<operationsSize / 4
			{
				/*firstDeadLine = DeadLineControl(firsOrc);*/
				if (deadLineArray != NULL)
				{
					firstDeadLine = *(deadLineArray + (2 * firsOrc - 1));;
				}
				else
				{
					firstDeadLine = DeadLineControl(firsOrc);
				}
				
			}
			if (secOrc>0 && secOrc<operationsSize / 4)
			{
				/*secondDeadLine = DeadLineControl(secOrc);*/
				if (deadLineArray != NULL)
				{
					secondDeadLine = *(deadLineArray + (2 * secOrc - 1));;
				}
				else
				{
					secondDeadLine = DeadLineControl(secOrc);
				}
				
			}
			   
			if (firstFourth > secondFourth&&firstFourth > 0 && secondFourth>0 && firstDeadLine >= secondDeadLine) /* For decreasing order use < */
			{
				 ilk = *(ordersArray + d);  ilk2 = *(ordersArray + d + 4);
				swap = ilk;
				*(ordersArray + d) = *(ordersArray + d + 4);
				ilk = *(ordersArray + d);
				*(ordersArray + d + 4) = swap;
				ilk2 = *(ordersArray + d + 4);
				//printf("%d\n", ilk);
				 ikinci = *(ordersArray + d + 1);  ikinci2 = *(ordersArray + d + 5);
				swap = ikinci;
				*(ordersArray + d + 1) = *(ordersArray + d + 5);
				*(ordersArray + d + 5) = swap;

				 ucuncu = *(ordersArray + d + 2);  ucuncu2 = *(ordersArray + d + 6);
				swap = ucuncu;
				*(ordersArray + d + 2) = *(ordersArray + d + 6);
				*(ordersArray + d + 6) = swap;

				 dorduncu = *(ordersArray + d + 3);  dorduncu2 = *(ordersArray + d + 7);
				swap = dorduncu;
				*(ordersArray + d + 3) = *(ordersArray + d + 7);
				*(ordersArray + d + 7) = swap;

				/*for (int i = 0; i < 4; i++)
				{
					ilk = *(ordersArray + d + i );
					ikinci = *(ordersArray + d + i + 4);
					swap = ilk;
					*(ordersArray + d + i) = *(ordersArray + d + i+1);
					*(ordersArray + d + i+1) = swap;
				}*/
			}
			d += 3;
		}
	}
	
	for (int i = 0; i < operationsSize; i++)
	{
		int a = *(ordersArray + i);
		//printf("%d;", a);
		
		*(ordersArray2 + i) = a;

		/*if (i % 4 == 3)
		{
			printf("\n");
		}*/
	}
	
	
	free(ordersArray);

		for (int i = 0; i < operationsSize; i++)
	{
		int a = *(ordersArray2 + i);
		int b = *(ordersArray2 + i + 1);
		int c = *(ordersArray2 + i + 2);
		int d = *(ordersArray2 + i + 3);
		printf( "%d;%d;%d;%d\n", a, b, c, d);
		i += 3;
	}
}