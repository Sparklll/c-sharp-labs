#include <string>
#include <random>
#include <combaseapi.h>
#include "generator.h"


const char* __stdcall generatePassword(int passwordLength) {
	std::random_device rd;
	std::mt19937 gen(rd());

	std::string passwordAlphabet
		= "|my!;Q_^]@:tjUgZEI/hlOrJFq(#ep{fCcx~+iPvAD=.Vn,<SbNo'RHw>K$s`LBMGuY)W*[-kza%}dT&X?~";
	int alphabetLength = passwordAlphabet.length();
	std::uniform_int_distribution<> dis(0, alphabetLength - 1);
	std::string result;

	for (int i = 0; i < passwordLength; i++) {
		result += passwordAlphabet.at(dis(gen));
	}

	long stringSize = result.length() + sizeof(char);
	char* returnString = (char*)::CoTaskMemAlloc(stringSize);
	strcpy_s(returnString, stringSize, result.c_str());

	return returnString;
}

int __cdecl generateRandomNumberFromRange(long long min, long long max) {
	std::random_device rd;
	std::mt19937 gen(rd());
	if (min > max) {
		std::swap(min, max);
	}
	std::uniform_int_distribution<> dis(min, max);

	return dis(gen);
}


