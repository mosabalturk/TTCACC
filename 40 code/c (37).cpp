#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>


typedef struct {
	int siparis_no;
	int islem_uzunlugu;
	int islem_kodu;
	int teslimat_suresi;
	int islem_suresi;
} siparisler;

typedef struct {
	int islem_kodu;
	int hizi;
} operasyonlar;

void main ()	{
///order dosyasından siparişler alınır structa kaydedilir
 FILE* dosya = NULL;
dosya = fopen("Orders.txt", "r");

if(dosya == NULL)
	printf("dosya acilamadi");
char ch;
int satir_sayisi = 1 ;
while (!feof(dosya)){
	ch=fgetc(dosya);
	if(ch=='\n'){
        satir_sayisi ++;
	}
}

    siparisler * siparislerim;
siparislerim = (siparisler *)malloc(satir_sayisi * sizeof(siparisler));

	char **kelimedizisi;
    int op = 0;
	kelimedizisi = (char **) malloc(sizeof (char*) * satir_sayisi);
	for (op = 0; op < satir_sayisi; op++)
        kelimedizisi[op] = (char *) malloc(sizeof (char) * 15);

    rewind(dosya);
	int kt = 0;
	while (kt<satir_sayisi){
		fgets(kelimedizisi[kt], 15, dosya);

		int a;
		char *sinirlayici = ";";
		char* p = strtok ( kelimedizisi[kt], sinirlayici);
            int j;
			for( j = 0 ; j<4 && p != NULL; j++){

			a = atoi(p);

                switch(j) {
				case 0:
					(siparislerim+ kt*sizeof(siparisler))->siparis_no = a ;
					break;
				case 1:
					(siparislerim+ kt*sizeof(siparisler))->islem_uzunlugu = a ;
					break;
				case 2:
					(siparislerim + kt*sizeof(siparisler))->islem_kodu = a ;
					break;
				case 3:
					(siparislerim+ kt*sizeof(siparisler))->teslimat_suresi = a ;
					break;
						}

			p = strtok(NULL, sinirlayici);
           			}

            kt++;
	}

        int v;
    for ( v = 0; v < satir_sayisi; v++){
                    free(kelimedizisi[v]);
        }
	free(kelimedizisi);

fclose(dosya);

///operation dosyasından operasyonlar alınıp structa kaydedilir
    FILE* dosya1 = NULL;
dosya1 = fopen("Operations.txt", "r");

if(dosya1 == NULL)
	printf("dosya acilamadi");
char ch1;
int operasyon_sayisi = 1 ;
while (!feof(dosya1)){
	ch1=fgetc(dosya1);
	if(ch1=='\n'){
        operasyon_sayisi ++;
	}
}

operasyonlar * operasyonlarim;
operasyonlarim = (operasyonlar *)malloc(operasyon_sayisi * sizeof(operasyonlar));

	char **operasyondizisi;
    int i;

	operasyondizisi = (char **) malloc(sizeof (char*) * operasyon_sayisi);
	for (i = 0; i < operasyon_sayisi; i++)
        operasyondizisi[i] = (char *) malloc(sizeof (char) * 50);

    int k = 0;
    rewind(dosya1);
	while (k<operasyon_sayisi && !feof(dosya1)){
		fgets(operasyondizisi[k], 50, dosya1);

		int a;
		char *sinirlayici = ";";
		char* p1 = strtok ( operasyondizisi[k], sinirlayici);
            int j;
			for( j = 0 ; j<2 && p1 != NULL; j++){

			a = atoi(p1);

                switch(j) {
				case 0:
					(operasyonlarim+ i*sizeof(siparisler))->islem_kodu = a ;
					break;
				case 1:
					(operasyonlarim+ i*sizeof(siparisler))->hizi = a ;
					break;
						}


			p1 = strtok(NULL, sinirlayici);

			}
		k++;

	}



    for ( v = 0; v < operasyon_sayisi; v++){
                    free(operasyondizisi[v]);
        }
	free(operasyondizisi);


fclose(dosya1);


/// kaydedilen operasyonlar ve siparişler structundan yararlanılıp işlem süresi bulunur

int kut, yuvarlanmis_hal;
double x;
for ( i = 0; i<satir_sayisi; i++)
{
for ( kut = 0; kut<operasyon_sayisi; kut++){

if((siparislerim +i*sizeof(siparisler))->islem_kodu == (operasyonlarim+kut*sizeof(operasyonlar))->islem_kodu){
	x = (double)(siparislerim +i*sizeof(siparisler))->islem_uzunlugu / (operasyonlarim+kut*sizeof(operasyonlar))->hizi;
	 yuvarlanmis_hal = ceil(x);

	(siparislerim +i*sizeof(siparisler))->islem_suresi = yuvarlanmis_hal;
}
}
}




///beklemeler okunup bir dizide tutulur

FILE* dosya3 = NULL;
dosya3 = fopen("SetupDuration.txt", "r");
	if(dosya3 == NULL)
	printf("dosya acilamadi");

	int bekleme_sayisi = 1;
	char ch2;
	while (!feof(dosya3)){
	ch2=fgetc(dosya3);
	if(ch2=='\n'){
        bekleme_sayisi ++;
	}
}

		///beklemelerin tutulacaðý dizi
	int **beklemedizisi;
	beklemedizisi = (int **) malloc(sizeof (int*) *bekleme_sayisi );
	for (i = 0; i < bekleme_sayisi; i++)
       		 beklemedizisi[i] = (int *) malloc(sizeof (int) * 3);


    int mt = 0;
    int sayi0, sayi2, bekeleme1;
    rewind(dosya3);
while (mt<bekleme_sayisi){

		fscanf(dosya3,"%d;%d;%d",&sayi0, &sayi2, &bekeleme1);

		beklemedizisi[mt][0]=sayi0;
		beklemedizisi[mt][1]=sayi2;
		beklemedizisi[mt][2]=bekeleme1;

		mt++;

	}

/// baştaki siparişler dizisinden yaralanırarak bir matris oluşturur.

	FILE* dosya4 = NULL;
dosya4 = fopen("Schedule.txt", "w");

int **deadlinedizisi;
	deadlinedizisi = (int **) malloc(sizeof (int*) *satir_sayisi );
	for (i = 0; i < satir_sayisi; i++)
       		 deadlinedizisi[i] = (int *) malloc(sizeof (int) * 5);

for ( i = 0; i<satir_sayisi; i++)
{
	deadlinedizisi[i][0] = (siparislerim+i*sizeof(siparisler))->islem_kodu;
	deadlinedizisi[i][1] = (siparislerim+i*sizeof(siparisler))->siparis_no;
	deadlinedizisi[i][2] = (siparislerim+i*sizeof(siparisler))->islem_uzunlugu;
	deadlinedizisi[i][3] = (siparislerim+i*sizeof(siparisler))->islem_suresi;
	deadlinedizisi[i][4] = (siparislerim+i*sizeof(siparisler))->islem_kodu;

}

/// oluşturulan matrisin 4. sütununa göre kabarcık sıralaması yapılır deadlinelarına göre sıralanır
int kd, j, gecici;
	for(kd=0; kd<satir_sayisi-1; kd++){
   for(j=0; j<satir_sayisi-1; j++){
      if( deadlinedizisi[j][4]<deadlinedizisi[j+1][4] ){
         gecici = deadlinedizisi[j][4];
           deadlinedizisi[j][4] = deadlinedizisi[j+1][4];
         deadlinedizisi[j+1][4] = gecici;
      }
   }
	}

/// kabarcık sıralamasıyla sıralanan matrisler Schedule dosyası oluştururup içine sıralama yazılır.

	  	int te;
        int start_time = 0;
        int time = 0;
        int sayi1;


	for(sayi1=0; sayi1<satir_sayisi; sayi1++)
	{
		if ( sayi1 != 0){
                    for( te = 0; te<bekleme_sayisi; te++){
                            if(((beklemedizisi[te][0] = deadlinedizisi[sayi1-1][3]) || (beklemedizisi[te][1] = deadlinedizisi[sayi1-1][3])) && ((beklemedizisi[te][0] = deadlinedizisi[sayi1-1][3]) || (beklemedizisi[te][1] = deadlinedizisi[sayi1-1][3])))
	{
                            time = beklemedizisi[te][2];
                                break;
			}
	}

	start_time = deadlinedizisi[sayi1-1][3] + time;
        fprintf(dosya4,"%d;%d;%d;%d;%d\n",start_time,deadlinedizisi[sayi1][0], deadlinedizisi[sayi1][1], deadlinedizisi[sayi1][2], time);

		}
        else
	fprintf(dosya4,"0;%d;%d;%d;0\n",deadlinedizisi[sayi1][0], deadlinedizisi[sayi1][1], deadlinedizisi[sayi1][2]);


}
fclose(dosya3);
fclose(dosya4);


return ;

}

/// Ömer Kutay Atik
/// 1306140058
/// date : 20.05.2016
/// Codeblock 16.01


///bir yerinde bi hata var hocam anlayamadığım ve kime sorsam da bir cevap bulamadığım bir nedenden dolayı atamalarda bir tane atamayı yanlış
///yapıyor nedeninin bir türlü anlayamadım, oradan da zaten kodun geri kalanı istemsizce bozulmuş oluyor.
///ama en azından deadlinelara göre sıralamada algoritmamın doğru olduğuna inanıyorum.


///aslinda ilk bastaki denedigim seyler daha farkliydi 3 boyutlu bir dizi oluşturup onda ağaç yapıp en kısa yolu bulmaktı lakin o zaman da
///en küçük yolu bulsamda geçtiğim yolları bulamadım o algoritmayı kuramadım yani öyle olunca bende buna yöneldim.
///int *** siparis_duraklama;
///siparis_duraklama = (int***) malloc(sizeof(int**) * satir_sayisi);
///for ( q = 0; q<satir_sayisi; q++){
	///for ( w = 0; w<OPERASYON SAYIISI KADAR ; w+++){
		//siparis_duraklama[q][w]= (int *) malloc(sizeof(int) * OPERASYON SAYISI KADAR);
		//}
///	}

//// siparis_kodu[][][] 3lü dizi
/// yaptýk bunlardan ilki siprasler
////2.si siprasi kodu 3.sü öteki stopdurationlar
