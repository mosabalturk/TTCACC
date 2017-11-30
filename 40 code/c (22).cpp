// Bilge KAMBEROÐLU 
// 1306150092 
// 21.05.2016 
// Visual Studio 2015 (Microsoft Visual Studio 14.0) 

#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<math.h>

typedef struct {
	int operationCode;
	int duration;
}Operations;

typedef struct {
	int operationCode1, operationCode2;
	int setupDurations;
}SetupDuration;

typedef struct {
	int orderCode;
	int amountOfWork;
	int operationCode;
	int deadline;
}Orders;

typedef struct {
	int scheduleTime;
	int operationCode;
	int orderCode;
	int amountOfWork;
	int setupOverhead;//setupDuration
}Schedule;

int main()
{
	FILE *fp = NULL;
	char line[100];
	int counter1 = 0, oprCount;

	Operations * operList;
	SetupDuration * setupList;
	Orders * orderList, *sortedOrderList, *temp;
	int *durationOfWorkList, *setupDurationList, *scheduleTimeList, *setupOverheadList;
	Schedule * scheduleList;


	//file1 
	fp=fopen("Operations.txt", "r");
	if (fp == NULL) {
		printf("Operations.txt can not open!\n");
		exit(1);
	}
	while (!feof(fp)) {
		fgets(line, 100, fp);
		counter1++;
	}
	fclose(fp);
	oprCount = counter1;
	operList=(Operations*)malloc(sizeof(Operations)*counter1);


	fp = fopen("Operations.txt", "r");
	if (fp == NULL) {
		printf("Operations.txt can not open!\n");
		exit(1);
	}
	counter1 = 0;
	while (!feof(fp)) {
		fscanf(fp, "%d;%d", &operList[counter1].operationCode, &operList[counter1].duration);
		counter1++;
	}
	fclose(fp);
	
	////assigned data control 
	//for (int i = 0; i < counter1; i++) {
	//printf("%d;%d\n", operList[i].operationCode, operList[i].duration);
	//}

	for (int d = 0; d < counter1; d++) {
		line[d] = NULL;
	}
	
	////empty array control 
	//for (int j = 0; j < counter1; j++) {
	//	printf("%d\tend\n", line[j]);
	//}



	//file2 
	setupList = (SetupDuration*)malloc(sizeof(SetupDuration)*counter1*counter1);


	fp = fopen("SetupDuration.txt", "r");
	if (fp == NULL) {
		printf("SetupDuration.txt can not open!\n");
		exit(1);
	}
	counter1 = 0;
	while (!feof(fp)) {
		int opCode1, opCode2, dur;
		fscanf(fp, "%d;%d;%d", &opCode1, &opCode2, &dur);
		setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].operationCode1 = opCode1;
		setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].operationCode2 = opCode2;
		setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].setupDurations = dur;
		setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].operationCode1 = opCode2;
		setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].operationCode2 = opCode1;
		setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].setupDurations = dur;

		////assigned data control
		//printf("  %d;%d;%d\n", setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].operationCode1,
		//		setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].operationCode2,
		//		setupList[(oprCount*(opCode2 - 1) + (opCode1 - 1))].setupDurations);
		//printf(". %d;%d;%d\n", setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].operationCode1,
		//		setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].operationCode2,
		//		setupList[(oprCount*(opCode1 - 1) + (opCode2 - 1))].setupDurations);
	}
	fclose(fp);



	//file3
	fp = fopen("Orders.txt", "r");
	if (fp == NULL) {
		printf("Orders.txt can not open!\n");
		exit(1);
	}
	while (!feof(fp)) {
		fgets(line, 100, fp);
		counter1++;
	}
	fclose(fp);
	orderList = (Orders*)malloc(sizeof(Orders)*counter1);

	fp = fopen("Orders.txt", "r");
	if (fp == NULL) {
		printf("Orders.txt can not open!\n");
		exit(1);
	}
	counter1 = 0;
	while (!feof(fp)) {
		fscanf(fp, "%d;%d;%d;%d", &orderList[counter1].orderCode, &orderList[counter1].amountOfWork,
			&orderList[counter1].operationCode, &orderList[counter1].deadline);
		counter1++;
	}
	fclose(fp);

	////assigned data control 
	//for (int k = 0; k < counter1; k++) {
	//	printf("%d;%d;%d;%d\n", orderList[k].orderCode, orderList[k].amountOfWork, orderList[k].operationCode,
	//		orderList[k].deadline);
	//}


	//calculations 
	sortedOrderList = (Orders*)malloc(sizeof(Orders)*counter1);
	temp = (Orders*)malloc(sizeof(Orders));
	sortedOrderList = orderList;
	for (int a = 0; a < counter1 - 1; a++)
		for (int b = 0; b < counter1 - 1;b++)
			if (sortedOrderList[b].deadline>sortedOrderList[b + 1].deadline) {
				temp[0].amountOfWork = sortedOrderList[b].amountOfWork;
				temp[0].deadline = sortedOrderList[b].deadline;
				temp[0].operationCode = sortedOrderList[b].operationCode;
				temp[0].orderCode = sortedOrderList[b].orderCode;
				sortedOrderList[b].amountOfWork = sortedOrderList[b + 1].amountOfWork;
				sortedOrderList[b].deadline = sortedOrderList[b + 1].deadline;
				sortedOrderList[b].operationCode = sortedOrderList[b + 1].operationCode;
				sortedOrderList[b].orderCode = sortedOrderList[b + 1].orderCode;
				sortedOrderList[b + 1].amountOfWork = temp[0].amountOfWork;
				sortedOrderList[b + 1].deadline = temp[0].deadline;
				sortedOrderList[b + 1].operationCode = temp[0].operationCode;
				sortedOrderList[b + 1].orderCode = temp[0].orderCode;
			}
	free(temp);
	
	////sorted data control 
	//for (int a = 0; a < counter1; a++)
	//	printf("%d;%d;%d;%d\n", sortedOrderList[a].orderCode, sortedOrderList[a].amountOfWork,
	//		sortedOrderList[a].operationCode, sortedOrderList[a].deadline);


	durationOfWorkList = (int*)malloc(sizeof(int)*counter1);
	for (int d = 0; d < counter1; d++) {
		int x = sortedOrderList[d].operationCode;
		int y;
		for (y = 0; x != operList[y].operationCode; y++);
		float floatNum = (float)sortedOrderList[d].amountOfWork / operList[y].duration;
		durationOfWorkList[d] = ceil(floatNum);
	}

	////after calculation control 
	//for (int d = 0; d < counter1; d++)
	//	printf("%d\n", durationOfWorkList[d]);


	setupDurationList = (int*)malloc(sizeof(int)*counter1);
	for (int s = 0; s < counter1 - 1; s++) {
		if (sortedOrderList[s].operationCode == sortedOrderList[s + 1].operationCode)
			setupDurationList[s] = 0;
		else {
			int p = 0;
			while (sortedOrderList[s].operationCode != setupList[p].operationCode1) {
				p++;
				if (sortedOrderList[s + 1].operationCode == setupList[p].operationCode2)
					setupDurationList[s] = setupList[p].setupDurations;
				else {
					while (sortedOrderList[s + 1].operationCode != setupList[p].operationCode2)
						p++;
					setupDurationList[s] = setupList[p].setupDurations;
				}
			}
		}
	}

	////assigned data control 
	//for (int s = 0; s < counter1 - 1; s++)
	//	printf("%d\n", setupDurationList[s]);


	scheduleTimeList = (int*)malloc(sizeof(int)*counter1);
	scheduleTimeList[0] = 0;
	for (int t = 1; t < counter1; t++)
		scheduleTimeList[t] = scheduleTimeList[t-1] + setupDurationList[t-1] + durationOfWorkList[t-1];

	////assigned data control 
	//printf("%d\n", scheduleTimeList[0]);
	//for (int t = 1; t < counter1; t++)
	//	printf("%d=%d+%d+%d\n", scheduleTimeList[t],scheduleTimeList[t - 1],setupDurationList[t-1],
	//		durationOfWorkList[t-1]);


	setupOverheadList = (int*)malloc(sizeof(int)*counter1);
	setupOverheadList[0] = 0;
	for (int f = 1; f < counter1; f++)
		setupOverheadList[f] = setupDurationList[f - 1];
	
	////assigned data control
	//for (int f = 0; f < counter1; f++)
	//	printf("%d\n", setupOverheadList[f]);


	//file4
	scheduleList = (Schedule*)malloc(sizeof(Schedule)*counter1);
	for (int as = 0; as < counter1; as++) {
		scheduleList[as].scheduleTime = scheduleTimeList[as];
		scheduleList[as].operationCode = sortedOrderList[as].operationCode;
		scheduleList[as].orderCode = sortedOrderList[as].orderCode;
		scheduleList[as].amountOfWork = sortedOrderList[as].amountOfWork;
		scheduleList[as].setupOverhead = setupOverheadList[as];
	}

	////assigned data control 
	//for (int as = 0; as < counter1; as++) {
	//	printf("%d;%d;%d;%d;%d\n", scheduleTimeList[as], sortedOrderList[as].operationCode,
	//		sortedOrderList[as].orderCode, sortedOrderList[as].amountOfWork, setupOverheadList[as]);
	//}


	fp = fopen("Schedule.txt", "w");
	if (fp == NULL) {
		printf("Schedule.txt can not open!\n");
		exit(1);
	}
	for (int ass = 0; ass < counter1; ass++) {
		fprintf(fp, "%d;%d;%d;%d;%d\n", scheduleList[ass].scheduleTime, scheduleList[ass].operationCode,
			scheduleList[ass].orderCode, scheduleList[ass].amountOfWork, scheduleList[ass].setupOverhead);
	}
	fclose(fp);

	printf("\n%d\n\n", scheduleTimeList[counter1-1]);


	system("pause");
	return 0;
}
