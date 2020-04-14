#pragma once

#define EXTERN_DLL_EXPORT extern "C" __declspec(dllexport)

EXTERN_DLL_EXPORT const char* __stdcall generatePassword(int passwordLength);
EXTERN_DLL_EXPORT int __cdecl generateRandomNumberFromRange(long long min, long long max);


