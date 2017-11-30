// Anýl Furkan Ökçün 
// 1306140077 
// Date : 20.05.2016 
// Development Enviorement : Visual Studio2013
#include <stdio.h>
#include <conio.h>
#include <stdlib.h>

#ifdef _MSC_VER
#define getch() _getch()
#endif

struct orders {
	char dizi_order[200];
	int orderNo;
	int amount;
	int opNo;
	int deadline;
	int is_suresi;
	int gecis;
	int zaman;
};

struct operation {
	char dizi_op[200];
	char opNo[5];
	int duration;
};

struct setup {
	char dizi_sd[200];
	int opNo1;
	int opNo2;
	int setupDuration;
};

int main() {

	//Orders txt dosyanin okuma modunda acilmasi	
	FILE * order;
	order = fopen("orders.txt", "r");

	int satir = 0;
	char tmpdizi[100];
	//Bu dongu malloc ile satir sayisi(siparis sayisi) kadar yer ayirmak icin. Gereksiz yer ayirmayi engellemek icin.
	while (fgets(tmpdizi, 100, order) != NULL) {
		satir++;
	}
	//printf("order dosyasinin satir sayisi = %d\n", satir);
	orders *siparisler = (orders *)malloc(satir * sizeof(orders));//Dosyadan okunacak satirlari tutmasi icin dizi
	fseek(order, 0, SEEK_SET);//Dosyanin satir sayisini saydiktan sonra dosyanin sonuna ulasmis olduk. Tekrar basina gelmek icin bu kod gerekli. 

	char c = NULL;//okunacak karakterleri ifade etmesi icin dizi
	satir = 1;//satir sayisi icin sayac
	int s = 0;//kacinci sipariste oldugumuzu tutuyor


	while (fgets(siparisler[s].dizi_order, 100, order) != NULL) {//fgets() fonksiyonu dosyadan bir satir okur

		//printf("%d. satir = %s\n", satir, siparisler[s].dizi_order);// Okunan satirin numarasi ve okunan satirdaki ifadelerin yazdirilmasi
		satir++;//satir okunduktan sonra satir sayaci 1 artýrýlýr

		int i = 0;
		int j = 0;
		char dizi2[5];
		c = siparisler[s].dizi_order[i];
		i++;
		//Order no alma
		while (c != ';') {
			dizi2[j] = c;
			j++;
			c = siparisler[s].dizi_order[i];
			i++;
		}
		dizi2[j] = '\0';
		siparisler[s].orderNo = atoi(dizi2);
		//printf("\tOrderNo = %d\n", siparisler[s].orderNo);

		//Amount alma
		char dizi6[6];
		c = siparisler[s].dizi_order[i];
		i++;
		j = 0;
		while (c != ';') {
			dizi6[j] = c;
			j++;
			c = siparisler[s].dizi_order[i];
			i++;
		}
		dizi6[j] = '\0';
		siparisler[s].amount = atoi(dizi6);
		//printf("\tAmount = %d\n", siparisler[s].amount);

		//Operation no alma
		char dizi3[5];
		c = siparisler[s].dizi_order[i];
		i++;
		j = 0;
		while (c != ';') {
			dizi3[j] = c;
			j++;
			c = siparisler[s].dizi_order[i];
			i++;
		}
		dizi3[j] = '\0';
		siparisler[s].opNo = atoi(dizi3);
		//printf("\tOpNo = %d\n", siparisler[s].opNo);

		//Deadline alma
		char dizi[5];
		c = siparisler[s].dizi_order[i];
		i++;
		j = 0;
		while (c != '\n' && c != '\0') {
			dizi[j] = c;
			j++;
			c = siparisler[s].dizi_order[i];
			i++;
		}
		dizi[j] = '\0';
		siparisler[s].deadline = atoi(dizi);
		//printf("\tDeadline = %d\n\n", siparisler[s].deadline);

		s++;
	}
	int sip_sayac = s;

	FILE * operations;
	operations = fopen("Operations.txt", "r");
	satir = 0;
	//Bu dongu malloc ile satir sayisi(operasyon sayisi) kadar yer ayirmak icin. Gereksiz yer ayirmayi engellemek icin.
	while (fgets(tmpdizi, 100, operations) != NULL) {
		satir++;
	}
	//printf("operations dosyasinin satir sayisi = %d\n", satir);
	operation *islemler = (operation *)malloc(satir * sizeof(operation));//Dosyadan okunacak satirlari tutmasi icin dizi
	fseek(operations, 0, SEEK_SET);

	satir = 1;//satir sayisi icin sayac
	s = 0;//kacinci islemde oldugumuzu tutuyor

	while (fgets(islemler[s].dizi_op, 100, operations) != NULL) {//fgets() fonksiyonu dosyadan bir satir okur

		//printf("%d. satir = %s\n", satir, islemler[s].dizi_op);// Okunan satirin numarasi ve okunan satirdaki ifadelerin yazdirilmasi
		satir++;//satir okunduktan sonra satir sayaci 1 artýrýlýr

		int i = 0;

		//opNo alma
		c = islemler[s].dizi_op[i];
		i++;
		int j = 0;
		while (c != ';') {
			islemler[s].opNo[j] = c;
			j++;
			c = islemler[s].dizi_op[i];
			i++;
		}
		islemler[s].opNo[j] = '\0';
		//printf("\tOperation No = %s\n", islemler[s].opNo);

		//Duration alma
		char dizi5[5];
		c = islemler[s].dizi_op[i];
		i++;
		j = 0;
		while (c != '\n' && c != '\0') {
			dizi5[j] = c;
			j++;
			c = islemler[s].dizi_op[i];
			i++;
		}
		dizi5[j] = '\0';
		islemler[s].duration = atoi(dizi5);
		//printf("\tDuration = %d\n", islemler[s].duration);

		s++;
	}
	int is_sayac = s;

	FILE * setups;
	setups = fopen("setupDuration.txt", "r");
	satir = 0;
	while (fgets(tmpdizi, 100, setups) != NULL) {
		satir++;
	}
	//printf("\n\nSetupDuration dosyasinin satir sayisi = %d\n", satir);
	setup *gecisler = (setup *)malloc(satir * sizeof(setup));
	fseek(setups, 0, SEEK_SET);
	satir = 1;
	s = 0;

	while (fgets(gecisler[s].dizi_sd, 100, setups) != NULL) {

		//printf("%d. satir = %s\n", satir, gecisler[s].dizi_sd);
		satir++;

		int i = 0;
		//opNo1 alma
		char dizi7[6];
		c = gecisler[s].dizi_sd[i];
		i++;
		int j = 0;
		while (c != ';') {
			dizi7[j] = c;
			j++;
			c = gecisler[s].dizi_sd[i];
			i++;
		}
		dizi7[j] = '\0';
		gecisler[s].opNo1 = atoi(dizi7);
		//printf("\tOperation No 1 = %d\n", gecisler[s].opNo1);

		//opNo2 alma
		char dizi8[6];
		c = gecisler[s].dizi_sd[i];
		i++;
		j = 0;
		while (c != ';') {
			dizi8[j] = c;
			j++;
			c = gecisler[s].dizi_sd[i];
			i++;
		}
		dizi8[j] = '\0';
		gecisler[s].opNo2 = atoi(dizi8);
		//printf("\tOperation No 2 = %d\n", gecisler[s].opNo2);

		//setupDuration alma
		char dizi4[5];
		c = gecisler[s].dizi_sd[i];
		i++;
		j = 0;
		while (c != '\n' && c != '\0') {
			dizi4[j] = c;
			j++;
			c = gecisler[s].dizi_sd[i];
			i++;
		}
		dizi4[j] = '\0';
		gecisler[s].setupDuration = atoi(dizi4);
		//printf("\tDuration = %d\n", gecisler[s].setupDuration);

		s++;
	}
	int set_sayac = s;

	fclose(order);
	fclose(operations);
	fclose(setups);



	orders tmp;
	//deadline a gore siralama
	for (int i = 1; i <= sip_sayac; i++) {
		for (int j = 0; j < sip_sayac - 1; j++) {
			if (siparisler[j].deadline > siparisler[j + 1].deadline) {
				tmp = siparisler[j];
				siparisler[j] = siparisler[j + 1];
				siparisler[j + 1] = tmp;
			}
		}
	}
	
	//printf("\n-------------------------------------------\n");
	/*for (int i = 0; i < sip_sayac; i++) {
		printf("%02d ; %d ; %d ; %d \n", siparisler[i].orderNo, siparisler[i].amount, siparisler[i].opNo, siparisler[i].deadline);
	}
	*/
	//OpNo a gore siralama
	for (int i = 1; i <= sip_sayac; i++) {
		for (int j = 0; j < sip_sayac - 1; j++) {
			if ((siparisler[j].opNo > siparisler[j + 1].opNo) && (siparisler[j].deadline == siparisler[j + 1].deadline)) {
				tmp = siparisler[j];
				siparisler[j] = siparisler[j + 1];
				siparisler[j + 1] = tmp;
			}
		}
	}
	
	//printf("\n---------------------------------------------------\n");
	/*
	for (int i = 0; i < sip_sayac; i++) {
		printf("%02d ; %d ; %d ; %d \n", siparisler[i].orderNo, siparisler[i].amount, siparisler[i].opNo, siparisler[i].deadline);
	}
	*/
	//is suresi hesaplama
	for (int i = 0; i < sip_sayac; i++) {
		siparisler[i].is_suresi = siparisler[i].amount / islemler[(siparisler[i].opNo) - 1].duration;
	}
	//Ýs Sürelerine gore siralama
	for (int i = 1; i <= sip_sayac; i++) {
		for (int j = 0; j < sip_sayac - 1; j++) {
			if ((siparisler[j].is_suresi > siparisler[j + 1].is_suresi) && (siparisler[j].deadline == siparisler[j + 1].deadline) && (siparisler[j].opNo == siparisler[j + 1].opNo)) {

				tmp = siparisler[j];
				siparisler[j] = siparisler[j + 1];
				siparisler[j + 1] = tmp;
			}
		}
	}
	/*
	printf("\n\n\n--------------------------------------------------------\n\n");
	for (int i = 0; i < sip_sayac; i++) {
		printf("%02d ; %d ; %d ; %d \n", siparisler[i].orderNo, siparisler[i].amount, siparisler[i].opNo, siparisler[i].deadline);
	}
	*/
	//gecis hesaplama
	siparisler[0].gecis = 0;
	for (int i = 0; i < sip_sayac - 1; i++) {
		for (int j = 0; j < set_sayac; j++) {
			if (((gecisler[j].opNo1 == siparisler[i].opNo) && (gecisler[j].opNo2 == siparisler[i + 1].opNo)) || (gecisler[j].opNo2 == siparisler[i].opNo) && (gecisler[j].opNo1 == siparisler[i + 1].opNo)) {
				siparisler[i + 1].gecis = gecisler[j].setupDuration;
				break;
			}
			else {
				siparisler[i + 1].gecis = 0;
			}
		}

	}
	/*
	printf("\n\n\n--------------------------------------------------------\n\n");
	for (int i = 0; i < sip_sayac; i++) {
		printf("%02d ; %d ; %d ; %d ; gecis = %02d\n", siparisler[i].orderNo, siparisler[i].amount, siparisler[i].opNo, siparisler[i].deadline, siparisler[i].gecis);
	}
	*/
	//zaman hesaplama
	siparisler[0].zaman = 0;
	for (int i = 1; i < sip_sayac; i++) {
		siparisler[i].zaman = siparisler[i - 1].zaman + siparisler[i].is_suresi + siparisler[i].gecis;
	}
	//schedule.txt oluþturma
	FILE * schedule;
	schedule = fopen("Schedule.txt", "w+");
	//schedule.txt dosyasýna son sýralamayý yazdýrma
	for (int i = 0; i < sip_sayac; i++) {
		fprintf(schedule, "%d;%d;%d;%d;%d \n", siparisler[i].zaman, siparisler[i].opNo, siparisler[i].orderNo, siparisler[i].amount, siparisler[i].gecis);

	}

	//printf("\n--------------------------------------------------------\n\n");

	for (int i = 0; i < sip_sayac; i++) {
		printf("orderNo = %003d ;amount = %003d ;opNo = %003d ;deadline = %0004d ; gecis = %02d; is suresi = %d\n", siparisler[i].orderNo, siparisler[i].amount, siparisler[i].opNo, siparisler[i].deadline, siparisler[i].gecis, siparisler[i].is_suresi);
	}

	fclose(schedule);
	char x = getch();
	return 0;
}

