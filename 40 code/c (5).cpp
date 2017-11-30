#include <iostream>
#define SEEK_SET 0
#define SEEK_CUR  1
#define SEEK_END 2
using namespace
std;
int main() {
    FİLE *dosya1 = fopen("C:\\operations.txt", "r");
    FİLE *dosya2 = fopen("C:\\setupDuration.txt", "r");
    FİLE *dosya3 = fopen("C:\\Orders.txt", "r");
    FİLE *dosya1 = fopen("C:\\schedule.txt", "r");
    if (dosya != NULL){
        fseek(dosya,0,SEEK_SET);
        fseek(dosya,0,SEET_END);

    }
    return 0;
}