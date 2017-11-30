#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
#include <algorithm>

// Damla EREK
// 1306150090 
// Date : 20.05.2016 
// Development Enviorement : Visual Studio2013
using namespace std;

// Define file structures for data IMPORT
struct Operations
{
	int code;
	int productrate;
};
struct SetupDuration
{
	int opcode1;
	int opcode2;
	int sduration;
};
struct Orders
{
	int ordercode;
	int work;
	int opcode;
	int deadline;
};

struct Schedule
{
	int scheduletime;
	int opcode;
	int amountwork;
	int setupoverhead;
};


// Sort Container by opcode function
bool sortByopcode(const Orders &lhs, const Orders &rhs) { return lhs.opcode < rhs.opcode; }


// Sort Container by deadline function
bool sortBydeadline(const Orders &lhs, const Orders &rhs) { return lhs.deadline < rhs.deadline; }


int sduration_calc(struct SetupDuration sss[], int x, int y, int count2){
	int sduration_calc = 0;
	for (int i = 0; i < count2; i++)
	{

		if ((x == sss[i].opcode1 && y == sss[i].opcode2) || (y == sss[i].opcode1 && x == sss[i].opcode2)){

			return (sss[i].sduration);
		    }
		
	}
	
}

int productrate_calc(struct Operations ooo[], int codex, int count1){

	for (int i = 0; i < count1; i++)
	{

		if (codex == ooo[i].code){

			return (ooo[i].productrate);
		}

	}



}

void split(const std::string &s, char delim, std::string elems[])
{
	std::stringstream ss(s);
	std::string item;

	int i = 0;

	while (std::getline(ss, item, delim))
	{
		elems[i++] = item;
	}
}

int main()
{



	std::string line;
	std::ifstream myfile;
	myfile.open("Orders.txt");

	int count3 = 0;						// Total data in SetupDuration File
	while (getline(myfile, line))
	{

		count3++;
	}

	myfile.close();

	// Define vectors & Dimensions
	Operations operations[4];
	SetupDuration duration[10];
	vector<Orders> orders(count3);
	vector<Schedule> schedule(count3);
	// IMPORT Operations File
	std::string line1;
	std::ifstream myfile1;
	myfile1.open("Operations.txt");
	std::string columns1[2];
	int count1 = 0;						// Total data in Operation File

	while (getline(myfile1, line1))
	{
		split(line1, ';', columns1);

		operations[count1].code = std::stoi(columns1[0]);
		operations[count1].productrate = std::stoi(columns1[1]);
		count1++;
	}

	myfile1.close();

	for (int i = 0; i < count1; i++)
	{
		std::cout << operations[i].code << " " << operations[i].productrate << std::endl;
	}

	// IMPORT SetupDuration File
	std::string line2;
	std::ifstream myfile2;
	myfile2.open("SetupDuration.txt");
	std::string columns2[3];
	int count2 = 0;						// Total data in SetupDuration File
	while (getline(myfile2, line2))
	{
		split(line2, ';', columns2);

		duration[count2].opcode1 = std::stoi(columns2[0]);
		duration[count2].opcode2 = std::stoi(columns2[1]);
		duration[count2].sduration = std::stoi(columns2[2]);
		count2++;
	}

	myfile2.close();

	for (int i = 0; i < count2; i++)
	{
		std::cout << duration[i].opcode1 << " " << duration[i].opcode2 << " " << duration[i].sduration << std::endl;
	}

	// IMPORT Orders File
	std::string line3;
	std::ifstream myfile3;
	myfile3.open("Orders.txt");
	std::string columns3[4];
	count3 = 0;						// Total data in SetupDuration File
	while (getline(myfile3, line3))
	{
		split(line3, ';', columns3);

		orders[count3].ordercode = std::stoi(columns3[0]);
		orders[count3].work = std::stoi(columns3[1]);
		orders[count3].opcode = std::stoi(columns3[2]);
		orders[count3].deadline = std::stoi(columns3[3]);
		count3++;
	}

	myfile3.close();

	for (int i = 0; i < count3; i++)
	{
		//std::cout << orders[i].ordercode << " " << orders[i].work << " " << orders[i].opcode << " " << orders[i].deadline << std::endl;
	}



	sort(orders.begin(), orders.end(), sortBydeadline); // orders list will be ordered by deadline



	std::string line4;
	std::ofstream myfile4;
	myfile4.open("Schedule.txt");




	for (int i = 0; i < count3; i++)
	{
		//std::cout << i+1 << "   " << orders[i].ordercode << "   " << orders[i].work << "   " << orders[i].opcode << " " << orders[i].deadline << std::endl;
		if (i != 0) {
			schedule[i].setupoverhead = sduration_calc(duration, orders[i - 1].opcode, orders[i].opcode, count2);
			if (schedule[i].setupoverhead == 6) {
				schedule[i].setupoverhead = 0;
			}
			schedule[i].scheduletime = schedule[i - 1].scheduletime + schedule[i - 1].setupoverhead + ceil(orders[i - 1].work / productrate_calc(operations, orders[i].opcode, count1));

		}
		myfile4 << std::to_string(schedule[i].scheduletime) << ";" << std::to_string(orders[i].opcode) << ";" << std::to_string(orders[i].ordercode) << ";" << std::to_string(orders[i].work) << ";" << std::to_string(schedule[i].setupoverhead) << std::endl;
	}

	myfile4.close();
	std::cout << "schedule list have prepared ...... " << std::endl;
}


