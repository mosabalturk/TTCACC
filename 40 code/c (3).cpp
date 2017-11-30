// 1306140109 FURKAN YALÇIN Visual Studio 2015
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <string.h>
#include <math.h>

int opc;
FILE *di;
int ordersayac = 0;
int *operations = NULL; int **mat = NULL; int **mat2 = NULL; int **mat3 = NULL;

void opcbul() {
	opc = 0;
	char temp[100];
	if ((di = fopen("Operations.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(di)) {
		fgets(temp, 100, di);
		opc++;
	}
	fclose(di);
	//operasyon sayýsý kontrol
	/*printf("opc=%d\n", opc);*/
	operations = (int*)malloc(sizeof(int)*opc);
}
void operasyon() {
	int i, x;
	i = 0;
	if ((di = fopen("Operations.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(di)) {
		fscanf(di, "%d;%d", &x, &i);
		operations[x - 1] = i;
	}
	fclose(di);
}

void main() {

	int a;
	opcbul();
	operasyon();
	int o1, o2, s;

	mat3 = (int**)malloc(ordersayac * sizeof(int*));
	for (int i = 0; i<ordersayac; i++) {
		mat3[i] = (int*)malloc(6 * sizeof(int));
		if (NULL == mat3[i]) {
			free(mat3[i]);
			printf("mat3[i][] için hafýza ayýrýrken hata oluþtu.n");
			exit(-1);
		}
	}

	//operasyon dizisi kontrol
	/*for (int j = 0; j < 4; j++) {
		printf("\n%d", operations[j]);
	}*/

	mat = (int**)malloc(opc * sizeof(int*));
	for (int i = 0; i<opc; i++) {
		mat[i] = (int*)malloc(opc * sizeof(int));
		if (NULL == mat[i]) {
			free(mat[i]);
			printf("mat[i][] için hafýza ayýrýrken hata oluþtu.n");
			exit(-1);
		}
	}
	for (int t = 0; t < opc; t++) {
		mat[t][t] = 0;
	}

	if ((di = fopen("SetupDuration.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	//else { printf("acildi"); system("PAUSE"); }
	while (!feof(di)) {

		fscanf(di, "%d;%d;%d", &o1, &o2, &s);
		mat[o1 - 1][o2 - 1] = s;
		mat[o2 - 1][o1 - 1] = s;
	}
	fclose(di);

	// gecis süreleri kontrol
	/*for (int k = 0; k < 4; k++) {
		for (int l = k + 1; l < 4; l++) {
			printf("\nbasla %d'den %d'e=%d", k + 1, l + 1, mat[k][l]);
		}
	}*/

	///////////////////////////////////////////////////////////////////
	char temp2[100];

	if ((di = fopen("Orders.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(di)) {
		fgets(temp2, 100, di);
		ordersayac++;
	}
	fclose(di);


	mat2 = (int**)malloc(ordersayac * sizeof(int*));
	for (int i = 0; i<ordersayac; i++) {
		mat2[i] = (int*)malloc(6 * sizeof(int));
		if (NULL == mat2[i]) {
			free(mat2[i]);
			printf("mat2[i][] için hafýza ayýrýrken hata oluþtu.n");
			exit(-1);
		}
	}

	if ((di = fopen("Orders.txt", "r")) == NULL) {
		printf("Dosya acma hatasi!");
		system("PAUSE");
		exit(1);
	}
	while (!feof(di)) {
		int no, adet, tur, zaman;
		/*int co = 0;*/
		fscanf(di, "%d;%d;%d;%d", &no, &adet, &tur, &zaman);
		mat2[no - 1][0] = adet;
		mat2[no - 1][1] = tur;
		mat2[no - 1][2] = zaman;
		mat2[no - 1][3] = 0;
		mat2[no - 1][4] = ceil(float(adet) / operations[tur - 1]);
		mat2[no - 1][5] = no;
	}

	for (int i = 0; i < ordersayac-1; i++)
	{
		if (mat2[i][2] > mat2[i + 1][2]) {
			mat3[i] = mat2[i + 1];
			mat2[i + 1] = mat2[i];
			mat2[i] = mat3[i];
		}
	}
	//siparis sýralamasý kontrol
	/*for (int fy = 0; fy < ordersayac; fy++) {
		printf("\n%d. siparis bilgileri ", fy + 1);
		for (int yf = 0; yf < 5; yf++) {
			printf("%d  ", mat2[fy][yf]);
		}
	}
*/
	printf("\n");
	fclose(di);

	//burada olacak kodlar
	int zeski, z, ztut;
	zeski = z = ztut = 0;
	int joke = mat2[0][1]-1;

	if ((di = fopen("Schedule.txt", "w")) != NULL)
	{
		for (int i = 0; i < ordersayac; i++)
		{
			ztut = mat[joke][mat2[i][1] - 1];
			z = z + ztut + mat2[i][4];
			printf("\n%d baslangic suresiyle %d. siparis %d adet %d.operasyonda operasyon gecis suresi %d olarak %d dakikada tamamlandi", zeski,mat2[i][5], mat2[i][0], mat2[i][1], ztut, z);
			fprintf(di, "\n%d,%d;%d;%d;%d;%d", zeski,mat2[i][5], mat2[i][0], mat2[i][1], ztut, z);
			joke = mat2[i][1] - 1;
			zeski = z;
		}
	}
	else
	{
		printf("sonuc dosyasý acilamadi");
		system("PAUSE");
		exit(1);
	}
	printf("\n");

	fclose(di);

	system("PAUSE");
}