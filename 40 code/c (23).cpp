#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

//Süleyman Acar 1306140112
void main() {

	int operasyonsayisi;
	FILE *fp;
	int siparissayisi = 0;
	int *operasyonsureleri = NULL; int **opgecissureleri = NULL; int **siparisbilgileri = NULL;
	operasyonsayisi = 0;
	int toplamzmn, gecissuresi;





	char temp[100];
	if ((fp = fopen("Operations.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		exit(1);
	}
	while (!feof(fp)) {
		fgets(temp, 100, fp);
		operasyonsayisi++;
	}
	fclose(fp);

	operasyonsureleri = (int*)malloc(operasyonsayisi * sizeof(int));
	opgecissureleri = (int**)malloc(operasyonsayisi * sizeof(int*));
	for (int i = 0; i<operasyonsayisi; i++) {
		opgecissureleri[i] = (int*)malloc(operasyonsayisi * sizeof(int));
	}

	for (int t = 0; t < operasyonsayisi; t++) {
		opgecissureleri[t][t] = 0;
	}

	int a, b;

	if ((fp = fopen("Operations.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(fp)) {
		fscanf(fp, "%d;%d", &b, &a);
		operasyonsureleri[b - 1] = a;
	}
	fclose(fp);



	int x, y, z;

	if ((fp = fopen("SetupDuration.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}

	while (!feof(fp)) {

		fscanf(fp, "%d;%d;%d", &x, &y, &z);
		opgecissureleri[x - 1][y - 1] = z;
		opgecissureleri[y - 1][x - 1] = z;
	}
	fclose(fp);

	char temp2[100];

	if ((fp = fopen("Orders.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		exit(1);
	}
	while (!feof(fp)) {
		fgets(temp2, 100, fp);
		siparissayisi++;
	}
	fclose(fp);

	siparisbilgileri = (int**)malloc(siparissayisi * sizeof(int*));
	for (int i = 0; i<siparissayisi; i++) {
		siparisbilgileri[i] = (int*)malloc(4 * sizeof(int));
	}

	if ((fp = fopen("Orders.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(fp)) {
		int num, sayi, oper, zmn;
		/*int co = 0;*/
		fscanf(fp, "%d;%d;%d;%d", &num, &sayi, &oper, &zmn);
		siparisbilgileri[num - 1][0] = sayi;
		siparisbilgileri[num - 1][1] = oper;
		siparisbilgileri[num - 1][2] = zmn;
		siparisbilgileri[num - 1][3] = ceil(float(sayi) / operasyonsureleri[oper - 1]);
	}

	printf("\n");
	fclose(fp);

	toplamzmn = gecissuresi = 0;
	int lastsip = siparisbilgileri[0][1] - 1;

	if ((fp = fopen("Schedule.txt", "w")) != NULL)
	{
		for (int i = 0; i < siparissayisi; i++)
		{
			gecissuresi = opgecissureleri[lastsip][siparisbilgileri[i][1] - 1];
			toplamzmn += +gecissuresi + siparisbilgileri[i][3];
			fprintf(fp, "\n%d;%d;%d;%d", siparisbilgileri[i][0], siparisbilgileri[i][1], gecissuresi, toplamzmn);
			lastsip = siparisbilgileri[i][1] - 1;
		}
	}
	else
	{
		printf("schedule dosyasi acilamadi");
		system("PAUSE");
		exit(1);
	}
	printf("Dosya olusturuldu.\n");
	fclose(fp);


	system("PAUSE");
}