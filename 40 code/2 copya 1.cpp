#include <stdio.h>
#define MAX 1000000
struct Operations
{
	int OperationCode[MAX];
	int OperationSpeed[MAX];

}Operations;
//Bu kýsýmda Operations.txt içindeki bilgileri eþleþtirdik
struct SetupDuration
{

	int OperationCode1[MAX];
	int OperationCode2[MAX];
	int SetupDuration[MAX];

}SetupDuration;
//Bu kýsýmda SetupDuration.txt içindeki bilgileri eþleþtirdik
struct Orders
{
	int OrderCode[MAX];
	int AmountOfWork[MAX];
	int OperationCode[MAX];
	int Deadline[MAX];
	int DurationOfWork[MAX];

}Orders;
//Bu kýsýmda Orders.txt yi içindeki bilgileri eþleþtirdik
int Time = 0;
int LastOperation = 0;

main()
{
	ReadtoFile();
	OrderByDeadline();
	Schedule();
	getch();

}

ReadtoFile()
{
	FILE *stream;
	stream = fopen("C:\\Users\\YunusKiraz\\Desktop\\Proje\\Operations.txt", "r");
	for (int i = 0; !feof(stream); i++)
	{
		fscanf(stream, "%d;%d", &Operations.OperationCode[i], &Operations.OperationSpeed[i]);
	}
	fclose(stream);

	stream = fopen("C:\\Users\\YunusKiraz\\Desktop\\Proje\\SetupDuration.txt", "r");
	for (int j = 0; !feof(stream); j++)
	{
		fscanf(stream, "%d;%d;%d", &SetupDuration.OperationCode1[j], &SetupDuration.OperationCode2[j], &SetupDuration.SetupDuration[j]);
	}
	fclose(stream);

	stream = fopen("C:\\Users\\YunusKiraz\\Desktop\\Proje\\Orders.txt", "r");
	for (int k = 0; !feof(stream); k++)
	{
		fscanf(stream, "%d;%d;%d;%d", &Orders.OrderCode[k], &Orders.AmountOfWork[k], &Orders.OperationCode[k], &Orders.Deadline[k]);
		Orders.DurationOfWork[k] = getDurationTime(Orders.OperationCode[k], Orders.AmountOfWork[k]);
	}
	fclose(stream);
}
//Bu kýsýmda txt dosyalarýmýzýn hepsini programýmýza tarattýk
int getDurationTime(int Operation, int Amount)
{
	int i = 0, Speed = 0;
	while (Operations.OperationCode[i] != NULL)
	{
		if (Operations.OperationCode[i] == Operation)
		{
			Speed = Operations.OperationSpeed[i];
			break;
		}
		i++;
	}
	if (Amount%Speed == 0)
	{
		return Amount / Speed;
	}
	else
	{
		return (Amount / Speed) + 1;
	}
}
//Bu kýsýmda iþlerin yapýlma sürelerini hesapladýk
int changeOperation(int LastOperation, int NextOperation)
{
	int i = 0;
	if (LastOperation == NextOperation || LastOperation == 0)
	{
		return 0;
	}
	else {
		while (SetupDuration.OperationCode1[i] != NULL)
		{

			if ((SetupDuration.OperationCode1[i] == LastOperation && SetupDuration.OperationCode2[i] == NextOperation) || (SetupDuration.OperationCode1[i] == NextOperation && SetupDuration.OperationCode2[i] == LastOperation))
			{
				Time += SetupDuration.SetupDuration[i];
				return SetupDuration.SetupDuration[i];

			}

			i++;

		}
	}
}
//Bu kýsýmda operasyon geçiþlerindeki bekleme sürelerini hesapladýk
Schedule()
{
	FILE *Writer;
	Writer = fopen("C:\\Users\\YunusKiraz\\Desktop\\Proje\\Shedule.txt", "a+");
	int i = 0;
	while (Orders.OrderCode[i] != NULL)
	{

		fprintf(Writer, "%d;%d;%d;%d;%d\n", Time, Orders.OperationCode[i], Orders.OrderCode[i], Orders.Deadline[i], changeOperation(LastOperation, Orders.OperationCode[i]));
		LastOperation = Orders.OperationCode[i];
		Time += Orders.DurationOfWork[i];
		i++;
	}
	fcloseall();
}
//Bu kýsýmda operasyonlarýn yapýlýþ sürelerini operasyonlar arasýndaki geçiþ süreleri ve tüm operasyonlarýn bitiþ sürelerini schedule dosyamýza yazdýrdýk
OrderByDeadline()
{
	int i = 0;
	while (Orders.Deadline[i] != NULL)
	{
	
		i++;
	}

	int c, d;
	int sAmount, sDeadline, sOperation, sOrder, sDuration;
	for (c = 0; c < (i- 1)  ; c++)
	{
	
		for (d = 0; d < i- c - 1  ; d++)
		{
		
			if (Orders.Deadline[d] > Orders.Deadline[d + 1] )
			{
				sAmount = Orders.AmountOfWork[d];
				sDeadline = Orders.Deadline[d];
				sOperation = Orders.OperationCode[d];
				sOrder = Orders.OrderCode[d];
				sDuration = Orders.DurationOfWork[d];

				Orders.AmountOfWork[d] = Orders.AmountOfWork[d + 1];
				Orders.Deadline[d] = Orders.Deadline[d + 1];
				Orders.OperationCode[d] = Orders.OperationCode[d + 1];
				Orders.OrderCode[d] = Orders.OrderCode[d + 1];
				Orders.DurationOfWork[d] = Orders.DurationOfWork[d + 1];

				Orders.AmountOfWork[d + 1] = sAmount;
				Orders.Deadline[d + 1] = sDeadline;
				Orders.OperationCode[d + 1] = sOperation;
				Orders.OrderCode[d + 1] = sOrder;
				Orders.DurationOfWork[d + 1] = sDuration;

			}
		}
	}
}
//Bu kýsýmda da Schedule dosyamýzýn içindeki bilgileri Deadline a göre sýraladýk