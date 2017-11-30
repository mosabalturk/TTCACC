//            Muhammed Bahaeddin Aydemir         
//            1306150096          
//            Date : 08.05.2016           
//            Development Enviorement : Visual Studio2015 

#include<stdio.h>
#include<stdlib.h>

int opspeedfinder=1, p, k, n, m;
int *opspeed, *opcode;
int *firstop, *secondop, *seduration;
int *ordercode, *orderamount, *operationcode, *orderdeadline, *worktime, *setupduration;
int *fordercode, *forderamount, *foperationcode, *forderdeadline, *fworktime, *fsetupduration;
int *starttime, *endtime, *laststart;
int *fstarttime, *fendtime, *flaststart;

void duplicate_orders(int a) {
	if (a == 1) {
		for (int i = 0; i < n; i++) {
			fordercode[i] = ordercode[i];
			forderamount[i] = orderamount[i];
			foperationcode[i] = operationcode[i];
			forderdeadline[i] = orderdeadline[i];
		}
	}
	else if (a == 2) {
		for (int i = 0; i < n; i++) {
			ordercode[i] = fordercode[i];
			orderamount[i] = forderamount[i];
			operationcode[i] = foperationcode[i];
			orderdeadline[i] = forderdeadline[i];
			worktime[i] = fworktime[i];
			setupduration[i] = fsetupduration[i];
			laststart[i] = flaststart[i];
			starttime[i] = fstarttime[i];
			endtime[i] = fendtime[i];
		}
	}
}


void swap(int v[], int i, int j) {
	int	t;

	t = v[i];
	v[i] = v[j];
	v[j] = t;
}

int GetSetupDuration(int *seduration, int *first, int *second, int op2, int op1) {
	for (int i = 0; i < m; i++) {
		if ((op1 == first[i]) && (op2 == second[i])) return seduration[i];
		else if ((op2 == first[i]) && (op1 == second[i])) return seduration[i];
	}
}

int getopspeed(int code, int length) {
	for (int i = 0; i < length; i++) {
		if (code == opcode[i]) return opspeed[i];
	}
}


void perm(int n, int i) {
	int	j, control;
	if (i == n) {
		control = 0;
		for (j = 0; j < n; j++) {
			opspeedfinder = getopspeed(foperationcode[j], k);
			if (forderamount[j] % opspeedfinder) fworktime[j] = (forderamount[j] / opspeedfinder) + 1;
			else fworktime[j] = (forderamount[j] / opspeedfinder);
			if (j == 0) fsetupduration[j] = 0;
			else if (foperationcode[j] == foperationcode[j - 1]) fsetupduration[j] = 0;
			else fsetupduration[j] = GetSetupDuration(seduration, firstop, secondop, foperationcode[j], foperationcode[j - 1]);
			flaststart[j] = forderdeadline[j] - fworktime[j];
			if (j != 0) fstarttime[j] = fendtime[j - 1] + fsetupduration[j], fendtime[j] = fstarttime[j] + fworktime[j];
			else fstarttime[j] = 0, fendtime[j] = fworktime[j];
			if (fstarttime[j] > flaststart[j]) {
				control++;
			}

		}
		if (endtime[n - 1] > fendtime[n - 1] && control<1) {
			duplicate_orders(2);
		}
	}
	else
		for (j = i; j<n; j++) {
			swap(fordercode, i, j);
			swap(forderamount, i, j);
			swap(foperationcode, i, j);
			swap(forderdeadline, i, j);
			perm(n, i + 1);
		}
}


void sýrala(int *ptr, int i) {
	int temp;
	temp = ptr[i + 1];
	ptr[i + 1] = ptr[i];
	ptr[i] = temp;
}


int satýrsayisi(FILE *ptr)
{
	char ch;
	int a=1;
	while (!feof(ptr))
	{
		ch = getc(ptr);
		if (ch == '\n')
			a++;
	}
	rewind(ptr);
	return a;
}

void main() {
	char enter;
	printf("\n\n\tWELLCOME\n\nThis Program shows the fastest way for your machine to complete the orders.\n");
	printf("First it will read the values from the files, then it will find the fastest way.");
	printf("After that, It will create a schedule.txt file where you can see the solution.\nPress only Enter to continue....\n");

	scanf("%c", &enter);
	switch (enter) {
	case '\n': break;
	default:
		exit(1);
	}

	FILE *dosya;
	
	//Operasyonlar tanýmlanýyor.

	dosya = fopen("Operations.txt", "r");

	if (dosya == NULL)
		printf("Dosya acilamadi.");

	int i;

	k = satýrsayisi(dosya);
	printf(" Number of Operations = %d \n", k);

	opspeed = (int*)calloc(k, sizeof(int));
	opcode = (int*)calloc(k, sizeof(int));
	
	printf("\n\nOperation \t Work Amount Per Minute(m)\n\n");

	for (i = 0; i < k; i++)
	{
		fscanf(dosya, "%d;%d", &opcode[i], &opspeed[i]);
		printf("   %d.\t\t\t   %d\n", opcode[i], opspeed[i]);
	}
	
	fclose(dosya);

	//Operasyonlar arasý geçiþ süresi okunuyor.

	dosya = fopen("SetupDuration.txt", "r");
	if (dosya == NULL)
		printf("Dosya acilamadi.");

	m = satýrsayisi(dosya);
	printf("\n\n Number of Setup Durations:%d \n", m);

	firstop = (int*)calloc(m, sizeof(int));
	secondop = (int*)calloc(m, sizeof(int));
	seduration = (int*)calloc(m, sizeof(int));

	printf("\n\n------Setup Durations Between Operations------- \n\n");
	printf("\tOperations \t Duration(min)\n");

	for (i = 0; i < m; i++) {
		fscanf(dosya, "%d;%d;%d", &firstop[i], &secondop[i], &seduration[i]);
		printf("\t   %d-%d \t\t      %d\n", firstop[i], secondop[i], seduration[i]);
	}

	fclose(dosya);


	//Sipariþler okunuyor.
	

	dosya = fopen("Orders.txt", "r");
	if (dosya == NULL)
		printf("Dosya acilamadi.");

	n = satýrsayisi(dosya);
	printf("\n\n Number of Orders:%d \n", n);
	
	worktime = (int*)calloc(n, sizeof(int));
	ordercode = (int*)calloc(n, sizeof(int));
	orderamount = (int*)calloc(n, sizeof(int));
	operationcode = (int*)calloc(n, sizeof(int));
	orderdeadline = (int*)calloc(n, sizeof(int));
	setupduration = (int*)calloc(n, sizeof(int));

	printf("\n\n---------------------------- ORDERS -----------------------------\n\n");
	printf(" OrderCode  Amount(m)  OperationCode  Deadline(min)  WorkTime\n");

	for (i = 0; i < n; i++) {
		fscanf(dosya, "%d;%d;%d;%d", &ordercode[i], &orderamount[i], &operationcode[i], &orderdeadline[i]);

		opspeedfinder = getopspeed(operationcode[i], k);
		if (orderamount[i]%1+opspeedfinder) worktime[i] = (orderamount[i] / opspeedfinder) + 1;
		else worktime[i] = (orderamount[i] / opspeedfinder);

		printf("    %2d         %d            %d           %d    \t %d\n", ordercode[i], orderamount[i], operationcode[i], orderdeadline[i], worktime[i]);
	}

	printf("\n-----------------------------------------------------------------\n");

	fclose(dosya);

	//-------------------------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------------------
	//--------------------------------------------Optimizasyon yapýlýyor.------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------------------

	//SetupDurations sýralanýyor.
	int b, temp;
	printf("\n\n\n");
	for (b = 0; b < m-1; b++) {
		for (i = 0; i < m; i++)
		{
			if (seduration[i+1]>seduration[i]) {
				sýrala(seduration, i);
				sýrala(firstop, i);
				sýrala(secondop, i);
			}
		}
	}
	printf("\tAfter Sorting Data:\n\n\n\n");
	printf("\tOperations \t Duration(min)\n");
	for (i = 0; i < m ; i++) {
		printf("\t   %d-%d \t\t      %d\n", firstop[i], secondop[i], seduration[i]);
	}


	//Deadline'a göre sýralýyor.
	//for (b = 0; b < n-1; b++) {
	//	for (i = 0; i < n-1; i++)
	//	{
	//		if (orderdeadline[i+1]<orderdeadline[i]) {
	//			sýrala(ordercode, i);
	//			sýrala(orderamount, i);
	//			sýrala(operationcode, i);
	//			sýrala(orderdeadline, i);
	//		}
	//	}

	//}


	starttime = (int*)calloc(n, sizeof(int));
	endtime = (int*)calloc(n, sizeof(int));
	laststart = (int*)calloc(n, sizeof(int));



	for (i = 0; i < n; i++) {
		opspeedfinder = getopspeed(operationcode[i], k);
		if (orderamount[i] % opspeedfinder) worktime[i] = (orderamount[i] / opspeedfinder) + 1;
		else worktime[i] = (orderamount[i] / opspeedfinder);
		if (i == 0) setupduration[i] = 0;
		else if (operationcode[i] == operationcode[i - 1]) setupduration[i] = 0;
		else setupduration[i] = GetSetupDuration(seduration, firstop, secondop, operationcode[i], operationcode[i - 1]);
		laststart[i] = orderdeadline[i] - worktime[i];
		if (i != 0) starttime[i] = endtime[i - 1] + setupduration[i], endtime[i] = starttime[i] + worktime[i];
		else starttime[i] = 0, endtime[i] = worktime[i];
	}

	fordercode = (int*)calloc(n, sizeof(int));
	forderamount = (int*)calloc(n, sizeof(int));
	foperationcode = (int*)calloc(n, sizeof(int));
	forderdeadline = (int*)calloc(n, sizeof(int));
	fsetupduration = (int*)calloc(n, sizeof(int));
	fstarttime = (int*)calloc(n, sizeof(int));
	fendtime = (int*)calloc(n, sizeof(int));
	flaststart = (int*)calloc(n, sizeof(int));
	fworktime = (int*)calloc(n, sizeof(int));

	duplicate_orders(1);
	perm(n, 0);

	free(fordercode);
	free(forderamount);
	free(foperationcode);
	free(forderdeadline);
	free(fworktime);
	free(fstarttime);
	free(fendtime);
	free(flaststart);
	
	printf("\n\n-------------------------------- SCHEDULE ------------------------------------\n\n");
	printf(" (OrderCode,Amount(m),OperationCode,Deadline(min),WorkTime,SetupDuration,\n  LastScheduleTime, StartTime, EndTime\n\n");
	printf(" Or.C  Amount  Op.C   Dead.L   W.T   S.D   L.S.T    S.T    E.T\n-----------------------------------------------------------------\n");

	for (i = 0; i < n; i++) {
		printf("  %2d,   %3d,    %d,    %4d,    %2d,", ordercode[i], orderamount[i], operationcode[i], orderdeadline[i], worktime[i]);
		printf("   %2d,   %4d,    %3d,   %3d\n", setupduration[i], laststart[i], starttime[i], endtime[i]);
	}
	
	free(opcode);
	free(opspeed);
	free(firstop);
	free(secondop);
	free(seduration);
	free(worktime);
	free(orderdeadline);
	free(endtime);
	free(laststart);

	//Schedule.txt dosyasý oluþturuluyor.
	
	dosya = fopen("Schedule.txt", "w+");
	if (dosya == NULL)
		printf("Dosya oluþturulamadý hatasý.");

	for (i = 0; i < n; i++) {
		fprintf(dosya, "%3d;%2d;%3d;%3d;%3d\n", starttime[i], operationcode[i], ordercode[i], orderamount[i], setupduration[i]);
	}


	fclose(dosya);
	
	free(ordercode);
	free(orderamount);
	free(operationcode);
	free(setupduration);
	free(starttime);

}