#include "pch.h"

#include "ATM_Bridge.h"

#include "ATM.h"

public ref class ATMWrapper
{
public:
    static int CheckPIN(int inputPin, int storedPin)
    {
        return VerifyPIN(inputPin, storedPin);
    }
};
