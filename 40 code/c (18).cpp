// Ali Fahri Yusein
// 1306080085
// Date : 22.05.2016
// Development Enviorement : Visual Studio 2015
#include <stdio.h>
#include <stdlib.h>
//-----------------Defining structs ----------------------
typedef struct operation {
	int workod;
	int workspd;
}work;
typedef struct order1 {
	int ordnum, worktodo, workkod, due;
}ord;
typedef struct setup1 {
	int work1, work2, time;
}set;


int main() {
	int ch = 1, workcount = 1;
	int count = 0, fo = 0;
	work *worklist;
	ord *ordlist;
	set *setlist;
	//-----------------------------Opr list ----------------------------------
	FILE *fp;
	fp = fopen("Operations.txt", "r");
	while (EOF != (ch = fgetc(fp)))
	{
		if (ch == '\n')
			workcount++;
	}

	fclose(fp);
	worklist = (work*)malloc(sizeof(work)*(workcount));
	fp = fopen("Operations.txt", "r");
	int wc = 0;
	while (!feof(fp)) {

		fscanf(fp, "%d;%d", &worklist[wc].workod, &worklist[wc].workspd);
		wc++;
	}
	fclose(fp);
	//--------------------------------   SET LÝST  ---------------------------------------
	setlist = (set*)malloc(sizeof(set)*(workcount*workcount)); //Creating memory for setlist
	fp = fopen("SetupDuration.txt", "r");
	if (fp == NULL)
	{
		perror("Error opening file");
		return(-1);
	}
	int worka, workb, settime;
	fp = fopen("SetupDuration.txt", "r");
	while (fscanf(fp, "%d;%d;%d", &worka, &workb, &settime) != EOF) {
		setlist[workcount*(workb - 1) + (worka - 1)].work1 = worka;
		setlist[workcount*(workb - 1) + (worka - 1)].work2 = workb;
		setlist[workcount*(workb - 1) + (worka - 1)].time = settime;
		setlist[workcount*(worka - 1) + (workb - 1)].work1 = workb;
		setlist[workcount*(worka - 1) + (workb - 1)].work2 = worka;
		setlist[workcount*(worka - 1) + (workb - 1)].time = settime;
	}
	fclose(fp);
	//------------------------------------Ordlist-------------------------------------------------
	fp = fopen("Orders.txt", "r");
	if (fp == NULL)
	{
		perror("Error opening file");
		return(-1);
	}
	int  oc = 1, c = 0;
	while (EOF != (ch = fgetc(fp)))
	{
		if (ch == '\n')
			oc++;
	}
	fclose(fp);
	ordlist = (ord*)malloc(sizeof(ord)*oc); // Creating memory for ordlist
	fp = fopen("Orders.txt", "r");
	int h = 1, o;
	while (fscanf(fp, "%d;%d;%d;%d", &ordlist[c].ordnum, &ordlist[c].worktodo, &ordlist[c].workkod, &ordlist[c].due) != EOF) {
		if (feof(fp))
		{
			/*	printf("End of file \n");*/
		}
		else
			c++;
	}
	fclose(fp);
	// -------------------------------- lists are created --------------
	int i = 0, j = 0;
	////--------Sorting ALG ------------


	for (i = 0; i < oc; ++i) {
		for (j = i + 1; j < oc; ++j) {
			if (ordlist[i].due > ordlist[j].due) {
				ord temp;
				temp = ordlist[i];
				ordlist[i] = ordlist[j];
				ordlist[j] = temp;
			}
		}

	}
	//	//---------------Same workkod = 0 -------------------
		for (j = 0; j < oc - 1; ++j)
		{
			for (i = 1; i < oc - 1; ++i) {	
				if (ordlist[i].workkod == ordlist[j].workkod) {
					setlist[workcount*(ordlist[i].workkod - 1) + (ordlist[j].workkod - 1)].time = 0;
				}
			}
		}
	//------------------Shedule.txt-------------------------------------
	int shedtime = 0;
	int worktime1 = 0;
	int settime1 = 0;
	fp = fopen("Shedule.txt", "w+");
	//printf("ScheduleTime;OperationCode;OrderCode;AmountOfWork;SetupOverhead\n\n)");
	printf("%d;%d;%d;%d;%d\n", shedtime, ordlist[i].ordnum, ordlist[i].workkod, ordlist[i].worktodo, settime1);
	fprintf(fp, "%d;%d;%d;%d;%d\n", shedtime, ordlist[i].ordnum, ordlist[i].workkod, ordlist[i].worktodo, settime1);
	for (i = 0; i < oc-1; ++i)
	{

		worktime1 = (ordlist[i].worktodo) / worklist[ordlist[i].workkod - 1].workspd;
        settime1 = setlist[workcount*(ordlist[i + 1].workkod - 1) + (ordlist[i].workkod - 1)].time;
		shedtime = shedtime + (worktime1 + settime1);
		printf("%d;%d;%d;%d;%d\n", shedtime, ordlist[i].ordnum, ordlist[i].workkod, ordlist[i].worktodo, settime1);
		fprintf(fp, "%d;%d;%d;%d;%d\n", shedtime, ordlist[i].ordnum, ordlist[i].workkod, ordlist[i].worktodo, settime1);		
	}
	printf("\n\n    SHEDULE TIME : %d \n\n", shedtime);
	fclose(fp);
	free(worklist);
	free(setlist);
	free(ordlist);


	system("pause");
}