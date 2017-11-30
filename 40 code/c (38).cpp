/*AYL�N YAZICI
1306140057
20.05.2016
V�SUAL STUD�O 2013*/





#include <stdio.h>
#include <stdlib.h>

struct Operation {
	int code;
	int velocity;
};

struct SetupDuration {
	int operationCode1;
	int operationCode2;
	int setupDuration;
};

struct Order {
	int orderCode;
	int amountOfWork;
	int operationCode;
	int deadline;
};

struct Schedule {
	int scheduleTime;
	int operationCode;
	int orderCode;
	int amountOfWork;
	int setupOverHead;
};

// 6 = SetupDuration'�n boyutu
int findSetupDuration(int operationCode1, int operationCode2, struct SetupDuration setupDurations[]) {
	int i;
	for (i = 0; i < 6; ++i) {
		if (operationCode1 == setupDurations[i].operationCode1 && operationCode2 == setupDurations[i].operationCode2) {
			return setupDurations[i].setupDuration;
		}
	}
}

// 4 = Operations'�n boyutu
int findOperationVelocity(int operationCode, struct Operation operations[]) {
	int i;
	for (i = 0; i < 4; ++i) {
		if (operationCode == operations[i].code) {
			return operations[i].velocity;
		}
	}
}

void main(void) {

	// Operations.txt dosyas� a��ld�, okundu ve operations struct dizisine dolduruldu
	FILE * operationFile;
	char satirArrayForOperationFile[5];
	char * operationPtr;

	if ((operationFile = fopen("Operations.txt", "r")) == NULL) {
		printf("Operations.txt a��lamad�.");
		exit(1);
	}

	struct Operation operations[4];
	int i;
	for (i = 0; fgets(satirArrayForOperationFile, 5, operationFile) != NULL; ++i) {
		operationPtr = strtok(satirArrayForOperationFile, ";");

		operations[i].code = operationPtr; // dosyadan gelen operasyon kodu
		operations[i].velocity = strtok(NULL, ";"); // dosyadan gelen s�re
	}


	// SetupDuration.txt dosyas� a��ld�, okundu ve setupDurations struct dizisine dolduruldu
	FILE * setupDurationFile;
	char satirArrayForSetupDuration[5];
	char * setupDurationPtr;

	if ((setupDurationFile = fopen("SetupDuration.txt", "r")) == NULL) {
		printf("SetupDuration.txt a��lamad�.");
		exit(1);
	}

	struct SetupDuration setupDurations[6];
	char * setupDurasionPtr;
	for (i = 0; fgets(satirArrayForSetupDuration, 5, setupDurationFile) != NULL; ++i) {
		setupDurationPtr = strtok(satirArrayForSetupDuration, ";");

		setupDurations[i].operationCode1 = setupDurationPtr; // dosyadan gelen operasyon operationCode1
		setupDurations[i].operationCode2 = strtok(NULL, ";"); // dosyadan gelen operationCode2
		setupDurations[i].setupDuration = strtok(NULL, ";"); // dosyadan gelen setupDuration

	}



	// Orders.txt dosyas� a��ld�, okundu ve order struct dizisine dolduruldu
	FILE *orderFile;
	char satirArrayForOrder[5];
	char * orderPtr;

	if ((orderFile = fopen("Orders.txt", "r")) == NULL) {
		printf("Orders.txt a��lamad�.");
		exit(1);
	}

	struct Order orders[7];
	for (i = 0; fgets(satirArrayForOrder, 5, orderFile) != NULL; ++i) {
		orderPtr = strtok(satirArrayForOrder, ";");

		orders[i].orderCode = orderPtr; // dosyadan gelen  orderCode
		orders[i].amountOfWork = strtok(NULL, ";"); // dosyadan gelen AmountOfWork
		orders[i].operationCode = strtok(NULL, ";"); // dosyadan gelen OperationCode
		orders[i].deadline = strtok(NULL, ";");//dosyadan gelen deadline
	}


	// 7 = Orders'�n boyutu, 6'da kar��la�t�r�lan eleman� kendisiyle kar��la�t�rmamak i�in
	int a, b, setupDuration1, setupDuration2, operationHizi, amountOfWork, timeForOrder, amountOfWorkDividedByOperationHizi;
	struct Order gecici;
	for (a = 0; a < 7; ++a) {
		for (b = a + 1; b <= 6; ++b) {
			setupDuration1 = findSetupDuration(orders[a].orderCode, orders[a + 1].orderCode, setupDurations);
			setupDuration2 = findSetupDuration(orders[b].orderCode, orders[b + 1].orderCode, setupDurations);

			if (setupDuration2 < setupDuration1) { // �s�nma s�resi az olan i�i orders dizisinde yukar� ��kar�yoruz
				gecici = orders[b]; // kabarc�k s�ralamas�
				orders[b] = orders[a];
				orders[a] = gecici;
			}
		}
	}



	//Schedule.txt dosyas� a��ld�, okundu ve schedule dizisine dolduruldu.
	FILE * scheduleFile;
	char * satirArrayForScheduleFile[5];
	char * schedulePtr;


	if ((scheduleFile = fopen("Schedule.txt", "r")) == NULL){
		printf("schedule.txt a��lamad�.");
		exit(1);
	}

	struct Schedule schedule[2];
	int satirArrayForSchedule;
	char * schedulePtr;
	for (i = 0; fgets(satirArrayForSchedule, 5, scheduleFile) != NULL; ++i){
		schedulePtr = strtok(satirArrayForSchedule, ";");

		schedule[i].scheduleTime = schedulePtr;//dosyadan gelen schedule kodu
		schedule[i].operationCode = strtok(NULL, ";");
		schedule[i].orderCode = strtok(NULL, ";");
		schedule[i].amountOfWork = strtok(NULL, ";");
		schedule[i].setupOverHead = strtok(NULL, ";");
	}
}
