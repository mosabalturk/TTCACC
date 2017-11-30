// Muhammed Melih YURT 
// 1306150053 
// Date : 20.05.2016 
// Development Enviorenment : Visual Studio 2015

#include <stdio.h>
#include <stdlib.h>

int round_Up(float, float);

struct Operations {
	int* OperationCode, *Duration;
};

struct SetupDuration {
	int* OperationCode1, *OperationCode2, *SetupDurations;
};

struct Orders {
	int* OrderCode, *AmountOfWork, *MakingOperationCode, *Deadline;
};

struct Schedule {
	int* ScheduleTime, *OperationCode, *OrderCode, *AmountOfWork, *SetupOverhead;
};

int main()
{
	struct Operations Operations;
	struct SetupDuration SetupDuration;
	struct Orders Orders, OrdersArtan, Temp;
	struct Schedule Schedule;
	FILE *fp1 = NULL;
	int i = 0;
	if ((fp1 = fopen("Operations.txt", "r")) == NULL){
		printf("Cannot open file");
		exit(1);										//fp1=Operations fp2=SetupDuration fp3=Orders
	}
	int count1 = 0;
	char ch1;
	while (!feof(fp1)) {
		
		ch1 = fgetc(fp1);
		if (ch1 == '\n')
			count1++;
	}

	

	Operations.OperationCode = (int *)malloc((count1 + 1) * sizeof(int));
	Operations.Duration = (int *)malloc((count1 + 1) * sizeof(int));

	rewind(fp1);
	while(!feof(fp1)){
		fscanf(fp1, "%d;%d\n", &Operations.OperationCode[i], &Operations.Duration[i]);
		i++;
		
	}
	
	FILE *fp2 = NULL;
	
	if ((fp2 = fopen("SetupDuration.txt", "r")) == NULL) {
		printf("Cannot open file");
		exit(1);
	}
	
	int count2 = 0;
	char ch2;
	while (!feof(fp2)) {

		ch2 = fgetc(fp2);
		if (ch2 == '\n')
			count2++;
	}
	SetupDuration.OperationCode1 = (int *)malloc((count2 + 1) * sizeof(int));
	SetupDuration.SetupDurations = (int *)malloc((count2 + 1) * sizeof(int));
	SetupDuration.OperationCode2 = (int *)malloc((count2 + 1) * sizeof(int));

	rewind(fp2);
	i = 0;
	while (!feof(fp2)) {
		fscanf(fp2, "%d;%d;%d\n", &SetupDuration.OperationCode1[i], &SetupDuration.OperationCode2[i], &SetupDuration.SetupDurations[i]);
		i++;
	}

	FILE *fp3 = NULL;
	
	if ((fp3 = fopen("Orders.txt", "r")) == NULL) {
		printf("Cannot open file");
		exit(1);
	}

	int count3 = 0;
	char ch3;
	while (!feof(fp3)) {

		ch3 = fgetc(fp3);
		if (ch3 == '\n')
			count3++;
	}

	Orders.OrderCode = (int *)malloc((count3 + 1) * sizeof(int));
	Orders.AmountOfWork = (int *)malloc((count3 + 1) * sizeof(int));
	Orders.MakingOperationCode = (int *)malloc((count3 + 1) * sizeof(int));
	Orders.Deadline = (int *)malloc((count3 + 1) * sizeof(int));

	rewind(fp3);
	i = 0;
	while (!feof(fp3))
	{
		fscanf(fp3, "%d;%d;%d;%d\n", &Orders.OrderCode[i], &Orders.AmountOfWork[i], &Orders.MakingOperationCode[i], &Orders.Deadline[i]);
		i++;
	}
	
	int csort, csort1;

	Temp.OrderCode = (int *)malloc((count3 + 1) * sizeof(int));
	Temp.AmountOfWork = (int *)malloc((count3 + 1) * sizeof(int));
	Temp.MakingOperationCode = (int *)malloc((count3 + 1) * sizeof(int));
	Temp.Deadline = (int *)malloc((count3 + 1) * sizeof(int));

	for (csort = 0; csort <= count3; csort++) {
		for (csort1 = 0; csort1 < (count3) - csort; csort1++) {
			if (Orders.Deadline[csort1] > Orders.Deadline[csort1 + 1]) {

				Temp.AmountOfWork[csort1] = Orders.AmountOfWork[csort1];
				Temp.Deadline[csort1] = Orders.Deadline[csort1];
				Temp.MakingOperationCode[csort1] = Orders.MakingOperationCode[csort1];
				Temp.OrderCode[csort1] = Orders.OrderCode[csort1];

				Orders.AmountOfWork[csort1]= Orders.AmountOfWork[csort1 + 1];
				Orders.Deadline[csort1] = Orders.Deadline[csort1 + 1];
				Orders.MakingOperationCode[csort1] = Orders.MakingOperationCode[csort1 + 1];
				Orders.OrderCode[csort1] = Orders.OrderCode[csort1 + 1];

				Orders.AmountOfWork[csort1 + 1] = Temp.AmountOfWork[csort1];
				Orders.Deadline[csort1 + 1] = Temp.Deadline[csort1];
				Orders.MakingOperationCode[csort1 + 1] = Temp.MakingOperationCode[csort1];
				Orders.OrderCode[csort1 + 1] = Temp.OrderCode[csort1];



			}
		}
	}
	
	
	
	int u1 = 0, u2 = 0, u3 = 0, control;
	
	

	for (control = 0; control < count3; control++) {
		while (Orders.Deadline[u1] == Orders.Deadline[u1 + 1]) {
			if (Orders.MakingOperationCode[u2] == Orders.MakingOperationCode[u1 + 1]) {

				Temp.AmountOfWork[u2 + 1] = Orders.AmountOfWork[u1 + 1];
				Temp.Deadline[u2 + 1] = Orders.Deadline[u1 + 1];
				Temp.MakingOperationCode[u2 + 1] = Orders.MakingOperationCode[u1 + 1];
				Temp.OrderCode[u2 + 1] = Orders.OrderCode[u1 + 1];

				Orders.AmountOfWork[u1 + 1] = Orders.AmountOfWork[u2 + 1];
				Orders.Deadline[u1 + 1] = Orders.Deadline[u2 + 1];
				Orders.MakingOperationCode[u1 + 1] = Orders.MakingOperationCode[u2 + 1];
				Orders.OrderCode[u1 + 1] = Orders.OrderCode[u2 + 1];

				Orders.AmountOfWork[u2 + 1] = Temp.AmountOfWork[u2 + 1];
				Orders.Deadline[u2 + 1] = Temp.Deadline[u2 + 1];
				Orders.MakingOperationCode[u2 + 1] = Temp.MakingOperationCode[u2 + 1];
				Orders.OrderCode[u2 + 1] = Temp.OrderCode[u2 + 1];
				u2++;
			}
			u1++;
		}
		do
		{
			u3++;
		} while (Orders.MakingOperationCode[u3] == Orders.MakingOperationCode[u3 + 1]);
		if (u3 > count3)
			goto end;
		u3 += 1;
		u2 = u3;
		u1 = u3;

	}
	end:
	

	FILE *fp4;
	if ((fp4 = fopen("Schedule.txt", "w")) == NULL) {
		printf("Cannot open file");
		exit(1);
	}

	Schedule.ScheduleTime = (int *)malloc((count3 + 1) * sizeof(long int));
	Schedule.OperationCode = (int *)malloc((count3 + 1) * sizeof(int));
	Schedule.OrderCode = (int *)malloc((count3 + 1) * sizeof(int));
	Schedule.AmountOfWork = (int *)malloc((count3 + 1) * sizeof(int));
	Schedule.SetupOverhead = (int *)malloc((count3 + 1) * sizeof(int));


	int s = 0;
	while (s <= count3) {
		Schedule.AmountOfWork[s] = Orders.AmountOfWork[s];
		Schedule.OrderCode[s] = Orders.OrderCode[s];
		Schedule.OperationCode[s] = Orders.MakingOperationCode[s];
		Schedule.SetupOverhead[0] = 0;
		s++;
	}

	int so_cntrl = 0, so1 = 0, so2 = 0;
	while (so_cntrl <= count3) {
		so1 = Orders.MakingOperationCode[so_cntrl];
		so2 = Orders.MakingOperationCode[so_cntrl + 1];
		int opr = 0, opr1 = 0;
		if (Orders.MakingOperationCode[so_cntrl] == Orders.MakingOperationCode[so_cntrl + 1]) {
			Schedule.SetupOverhead[so_cntrl + 1] = 0;
		}
		else{
		while (opr <= count2){
			if (((so1 == SetupDuration.OperationCode1[opr]) || (so2 == SetupDuration.OperationCode1[opr])) && ((so1 == SetupDuration.OperationCode2[opr]) || (so2 == SetupDuration.OperationCode2[opr]))) {
				Schedule.SetupOverhead[so_cntrl + 1] = SetupDuration.SetupDurations[opr];
			}
			
				opr++;
			}
		}
		so_cntrl++;
	}

	Schedule.ScheduleTime[0] = 0;
	int sch_control = 0;
	do
	{
		int opr_dur = 0, opr_cntrl = 0, amnt_Work = 0;
		while (opr_cntrl <= count1) {
			if (Schedule.OperationCode[sch_control] == Operations.OperationCode[opr_cntrl]) {
				opr_dur = Operations.Duration[opr_cntrl];
			}
			opr_cntrl++;
		}
		amnt_Work = round_Up((float)Schedule.AmountOfWork[sch_control], float(opr_dur));



		Schedule.ScheduleTime[sch_control + 1] = Schedule.SetupOverhead[sch_control + 1] + Schedule.ScheduleTime[sch_control] + amnt_Work;
		sch_control++;
	} while (sch_control < count3);

	for (int i_i = 0; i_i <= count3; i_i++)
		fprintf(fp4, "%d;%d;%d;%d;%d\n", Schedule.ScheduleTime[i_i], Schedule.OperationCode[i_i], Schedule.OrderCode[i_i], Schedule.AmountOfWork[i_i], Schedule.SetupOverhead[i_i]);
	


	printf("Total duration : %d min.\n\n", Schedule.ScheduleTime[count3]);
	

	system("PAUSE");
	

}

int round_Up(float a, float b)
{
	float c;
	c = a / b;
		int intpart = (int)c;
		double decpart = c - intpart;
		if (decpart == 0.0f)
		{
			return intpart;
		}
		else
		{
			return (int)(c + (1.0 - decpart));
		}
}
