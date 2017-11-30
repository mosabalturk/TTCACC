//Alpcan ÝÞCAN
//1306130093
//date:12.05.2016
//enviorement:codeblocks



#include <iostream>
#include <fstream>
using namespace std;

int main()

{
    int holder;
    int execute;
    int setupt;
    int order;
    int timer;
    int number;
    int total=0;
    string operation;
    string setup;
    ifstream ope;
    ifstream setu;
    cout << "enter how many times you will order an execute"<<endl;
    cin >>number;


         for(int i=1;i<=number;i++){

    ope.open("Operations.txt");
    setu.open("SetupDuration.txt");
    cout << "\n--Operation numbers and their speed --"<<endl;
    while (!ope.eof())
    {

        getline(ope,operation);
        cout << operation<<"    ";

    }
    cout << endl;
        ope.close();
        cout << "which operation speed would you like to select?"<<endl;

        cin >>execute;
        if(!execute<5 && !execute>0)
        {
            cout << "enter correct operation number"<<endl;
            return 0;
        }
    cout << "-- Those are setup timers between operations."<<endl;
    cout << "\nit takes preparation before going to another operation.--"<<endl;
    cout << "\nthere is no setup time for doing  same operation back to back.   --\n"<<endl;
        while (!setu.eof())
        {
            getline(setu,setup);
            cout << setup<<"     ";

        }
    setu.close();

    switch(execute)
    {
    case 1:
        setupt=12;
        break;
    case 2:
        setupt=10;
        break;
    case 3:
        setupt=20;
        break;
    case 4:
        setupt=20;
        break;
    }
    cout <<endl;

cout << "\nEnter your order in meters: "<<endl;
cin >>order;

if(execute==1)
{
    timer=order /10.0;

}
else if(execute==2)
{
    timer =order /15.0;
}
else if(execute==3)
{
    timer =order /100.0;
}
else if(execute==4)
{
    timer=order /50.0;
}


    if(i>1&&execute==1)
    {
        total=total+timer+setupt;
    }
    else if(i>1&&execute==2)
{
    total=total+timer+setupt;
}
    else if(i>1&&execute==3)
{
    total=total+timer+setupt;
}
    else if (i>1&&execute==4)
{
     total=total+timer+setupt;
}

  else  if(i==1) {

        total=total+timer;
}


    cout <<"orders execution time by doing it with "<<execute<<" is "<<timer<<endl;
    cout << "total timer for all operations is "<<total<<endl;



    }






    return 0;
}
