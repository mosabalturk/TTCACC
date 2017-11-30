// Alaa Alnemer 
// 1306140118 
// Date : 22.05.2016 
// Development Enviorement : Visual Studio 2015
// Note: sorry it's not finished, I run out of time becuase of the exams
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <fstream> 
#include <string>   

using namespace std;

int main()
{
	int ch = 0;
	FILE *Operations;
	FILE *Orders;
	FILE *SetupDuration;
	FILE *Schedule;
	  
	 Operations = fopen("Operations.txt", "r");
	 Orders = fopen("Orders.txt", "r");
	 SetupDuration = fopen("SetupDuration.txt", "r");
	 Schedule = fopen("Schedule.txt", "r");
	string ss; 
	// Format: (OperationCode; Duration\n\r)
	// Format: (OperationCode1; OperationCode2; SetupDuration\n\r)
	// Format: (OrderCode; AmountOfWork; OperationCode; Deadline\n\r)
	// Format: (ScheduleTime; OperationCode; OrderCode; AmountOfWork; SetupOverhead\n\r)
	char line[100];

	int OrderCode = 0;
	int AmountOfWork = 0;
	int OperationCode = 0;
	int Deadline = 0;
	int Duration = 0;
	int OperationCode1 = 0;
	int OperationCode2 = 0;
	int SetupDurationField = 0;
	int ScheduleTime = 0;
	int SetupOverhead = 0; 
	ifstream OperationsF("Operations.txt");
	ifstream OrdersF("Orders.txt");
	ifstream SetupDurationF("SetupDuration.txt");
	ifstream ScheduleF("Schedule.txt");


//	ch = getc(Operations);
	//putc(ch, Operations);
	//while (getline(OrdersF, ss))
	while (getline(OrdersF, ss))
	{
		cout << ss << endl;;
	}
	while (getline(OperationsF, ss))
	{
		cout << ss << endl;;
	}
	while (getline(SetupDurationF, ss))
	{
		cout << ss << endl;;
	}
	while (getline(ScheduleF, ss))
	{
		cout << ss << endl;;
	}

	//	while (fgets(line,100,Operations) != NULL)
	//   {  
	//	  
	//   	cout << getc(Operations) << endl;;
	//   } 
	//  char buff[255];
	//   
	//  fscanf(Operations, "%s", buff);
	//  printf("1 : %s\n", buff);
	//  
	//  fgets(buff, 255, (FILE*)Operations);
	//  printf("2: %s\n", buff);
	//  
	//  fgets(buff, 255, (FILE*)Operations);
	//  printf("3: %s\n", buff);
	//  
	//  fgets(buff, 255, (FILE*)Operations);
	//  printf("3: %s\n", buff);
	//  
	//  fgets(buff, 255, (FILE*)Operations);
	//  printf("3: %s\n", buff); 
	// cout << ch;


	int x;
	cin >> x; 

	fclose(Operations);
	fclose(Orders);
	fclose(SetupDuration);
	fclose(Schedule);

	return 0;
}

