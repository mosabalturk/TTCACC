 // Furkan NEHÝR
 // 1306110024
 // Date : 20.05.2016
 // Development Enviorement : Visual Studio2013

#include <stdio.h>
#include <stdlib.h>
#include <conio.h>


strlen(text);
{
	int total = 0;
	int digit = 0;
	for (int i = 0; i < len; i++)
	{
		digit = convertchartonumber(text[i]);
		int powerofdigit = 1;
		for (int j = 1; j < len - i; j++)
		{
			powerofdigit *= 10;
		}
		total += (digit * powerofdigit);
	}
	return total;
}


struct Order
{
	int OrderCode;
	int AmountOfWork;
	int OperationCode;
	int Deadline;
};

char* ProcessOrder(char* ordertxt)
{
	char schedule[4096];
	struct Order orders[50];
	int lastlineposition = 0;
	char line[256];
	int orderCount = 0;
	while(1 == readuntilcharacter(ordertxt, line, lastlineposition, '\n'))
	{ 
		lastlineposition += strlen(line) + 1;
		int lastnumberposition = 0;
		char numbertext[10];
		struct Order order;
		if(readuntilcharacter(line, numbertext, lastnumberposition, ';'))
		{
			order.OrderCode = converttexttonumber(numbertext);
			lastnumberposition += strlen(numbertext) + 1;
		}
		if (readuntilcharacter(line, numbertext, lastnumberposition, ';'))
		{
			order.AmountOfWork = converttexttonumber(numbertext);
			lastnumberposition += strlen(numbertext) + 1;
		}
		if (readuntilcharacter(line, numbertext, lastnumberposition, ';'))
		{
			order.OperationCode = converttexttonumber(numbertext);
			lastnumberposition += strlen(numbertext) + 1;
		}
		if (readuntilcharacter(line, numbertext, lastnumberposition, ';'))
		{
			order.Deadline = converttexttonumber(numbertext);
			lastnumberposition += strlen(numbertext) + 1;
		}
		orders[orderCount] = order;
		orderCount++;
	}
	printf("order count: %d", orderCount);
	return ordertxt;
}

int readuntilcharacter(char * from, char* to, int startposition, char stopchar)
{
	char result[256];
	int i = 0;
	while (from[startposition + i] != stopchar && from[startposition + i] != '\0')
	{
		to[i] = from[startposition + i];
		i++;
	}
	if (i == 0)
	{
		return 0;
	}
	to[i] = '\0';
	//strcpy(result, to);
	return 1;
}

int converttexttonumber(char * text)
{
	int len = 
int convertchartonumber(char character)
{
	if (character == '0')
	{
		return 0;
	}
	if (character == '1')
	{
		return 1;
	}
	if (character == '2')
	{
		return 2;
	}
	if (character == '3')
	{
		return 3;
	}
	if (character == '4')
	{
		return 4;
	}
	if (character == '5')
	{
		return 5;
	}
	if (character == '6')
	{
		return 6;
	}
	if (character == '7')
	{
		return 7;
	}
	if (character == '8')
	{
		return 8;
	}
	if (character == '9')
	{
		return 9;
	}
}

void ReadFromFile(char order[])
{
	int a, i = 0;
	FILE * file;
	file = fopen("d:\\orders.txt", "r");
	if (file == NULL)
	{
		printf("Dosyayý okuyamadýk");
		return -1;
	}

	while ((a = getc(file)) != EOF)
	{
		order[i] = a;
		i = i + 1;
	}
	order[i] = '\0';
	fclose(file);
	printf("Siparis okuma: tamam\r\n");
}

int WritetoFile(char* ordertxt)
{
	FILE *fp;
	fp = fopen("d:\\schedule.txt", "wb");
	if (fp == NULL)
		return;
	fwrite(ordertxt, 1, strlen(ordertxt), fp);
	fclose(fp);
	printf("Takvim yazma: tamam\r\n");
}

int converttexttonumber(char * text)
{
	int len = strlen(text);
	int total = 0;
	int digit = 0;
	for (int i = 0; i < len; i++)
	{
		digit = convertchartonumber(text[i]);
		int powerofdigit = 1;
		for (int j = 1; j < len - i; j++)
		{
			powerofdigit *= 10;
		}
		total += (digit * powerofdigit);
	}
	return total;
}


void printmyarray(char* chararray)
{
	printf("%s", chararray);
}

int main()
{
	char ordertxt[4096];
	ReadFromFile(ordertxt);
	char* schedule = ProcessOrder(ordertxt);
	WritetoFile(schedule);
	printf("Yazma tamam");
	// wait for a response
	char aux[64];
	int a = scanf("%s", &aux);


}


