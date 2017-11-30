//Ahmet Furkan Arvas
//1306150079
//Visual Studio 2015
//
//Kodu önce CodeBlocks'ta proje türünü c seçerek oluþturdum
//Sonra debugging aþamasý Visual Studio kadar iyi olmadýðý için köklü deðiþiklikler yaparak kodu VS'ye taþýdým.
//
//Bu taþýma aþamasýný 3-4 gün kala yaptýðým için; ve nerede hata yaptýðýmý bulmak için koyduðum breakpointlerde VS hata verdiði için yaptýðým hatalarý düzeltemedim.
//
//
//
//Ödevin son teslim tarihine 2 saat kala "cannot open consoleapplication.exe for writing" hatasýný aldým, isyan ettim ve yarým yamalak, bolca hatalý, derlenen ama çalýþmayan bu kodu, hiçbir þey göndermemiþ olmak için gönderiyorum.



#include <stdio.h>
#include <stdlib.h>
#define MAX(x, y) (((x) > (y)) ? (x) : (y)) //SetupDuration'da çift yönlü okuma yapabilmek için
#define MIN(x, y) (((x) < (y)) ? (x) : (y)) //SetupDuration'da çift yönlü okuma yapabilmek için

int main(void) {
	//Operations.txt yi oku
	int forcounter = 0, operaCount;
	FILE *dosya = fopen("Operations.txt", "r");
	if (!dosya) {
		perror("Operations.txt");
		system("pause");
		return 1;
	}

	int d1, d2, rows = 0;
	while (2 == fscanf(dosya, "%d;%d\n\r", &d1, &d2))
		++rows;
	int **operations = (int**)malloc(rows * sizeof(*operations));

	rewind(dosya);
	operaCount = rows;
	rows = 0;
	while (2 == fscanf(dosya, "%d;%d\n\r", &d1, &d2)) {
		operations[rows] = (int *)malloc(2 * sizeof(**operations));
		operations[rows][0] = d1;
		operations[rows][1] = d2;
		rows++;
	}
	fclose(dosya);
	rows = 0;


	//SetupDuration.txt yi oku
	dosya = fopen("SetupDuration.txt", "r");
	if (!dosya) {
		perror("SetupDuration.txt");
		system("pause");
		return 1;
	}
	int **setupDuration = (int**)malloc(sizeof(*setupDuration)*(((operaCount)*(operaCount - 1)) / 2));
	int d3;
	while (3 == fscanf(dosya, "%d;%d;%d\n\r", &d1, &d2, &d3)) {
		setupDuration[rows] = (int*)malloc(3 * sizeof(*setupDuration));
		setupDuration[rows][0] = d1;
		setupDuration[rows][1] = d2;
		setupDuration[rows][2] = d3;
		rows++;
	}
	fclose(dosya);
	


	//Orders.txt yi oku
	dosya = fopen("Orders.txt", "r");
	if (!dosya) {
		perror("Orders.txt");
		system("pause");
		return 1;
	}


	int d4;
	rows = 0;
	while (4 == fscanf(dosya, "%d;%d;%d;%d\n\r", &d1, &d2, &d3, &d4))
		++rows;
	rewind(dosya);
	int orderCount = rows;
	
	int **orders = (int**)malloc(sizeof(*orders)*orderCount);
	rows = 0;
	while (4 == fscanf(dosya, "%d;%d;%d;%d\n\r", &d1, &d2, &d3, &d4)) {
		orders[rows] = (int*)malloc(4 * sizeof(**orders));
		orders[rows][0] = d1;
		orders[rows][1] = d2;
		orders[rows][2] = d3;
		orders[rows][3] = d4;
		rows++;
	}
	fclose(dosya);
	
	for (forcounter = 0; forcounter < orderCount; forcounter++)
		printf("%d;%d;%d;%d\n", orders[forcounter][0], orders[forcounter][1], orders[forcounter][2], orders[forcounter][3]);








	//schedule oluþtur
	//
	//


	int **schedule = (int**)malloc(sizeof(*schedule)*orderCount);



	//Orderlarý deadline lara göre küçükten büyüðe sýrala
	int i, j, k;
	for (i = 0; i < orderCount; ++i)
	{
		for (j = i + 1; j < orderCount; ++j)
		{
			if (orders[i][4] > orders[j][4])
			{
				k = orders[i][4];
				orders[i][4] = orders[j][4];
				orders[j][4] = k;
			}
		}
	}


	//schedule a orders tan verileri kopyala
	for (forcounter = 0; forcounter < orderCount; forcounter++) {
		schedule[forcounter] = (int*)malloc(5 * sizeof(**schedule));
		schedule[forcounter][2] = orders[forcounter][3];
		schedule[forcounter][3] = orders[forcounter][1];
		schedule[forcounter][4] = orders[forcounter][2];

	}

	
	
	/*Yarým býraktýðým kýsým
	
	//Eðer bir dizinin OrderCode u ile iki eleman sonraki dizinin OrderCode u ayný ise
	for (forcounter = 0; forcounter < (orderCount - 2); forcounter++) {
		if (schedule[forcounter][3] == schedule[forcounter + 2][3]) {
			//Ve 2 uzaktaki diziyi bir uzaktakiyle yer deðiþtirince toplam deadline lar ilk halinden daha kýsa oluyorsa

		}
	}
	
	//2 uzaktaki diziyle 1 uzaktaki diziyi yer deðiþtir




	*/













	/*
	//Schedule.txt yi oluþtur
	dosya = fopen("Schedule.txt", "w");
	if (!dosya) {
		perror("Schedule.txt");
		system("pause");
		return 1;
	}


	for (forcounter = 0; forcounter < orderCount; forcounter++)
	{
		fprintf(dosya, "%d;%d;%d;%d;%d\n\r", schedule[forcounter][0], schedule[forcounter][1], schedule[forcounter][2], schedule[forcounter][3], schedule[forcounter][4]);
	}
	system("pause");
	return 0;
	*/
}