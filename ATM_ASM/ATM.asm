PUBLIC VerifyPIN

.code

VerifyPIN PROC inputPin:DWORD, storedPin:DWORD

    mov eax, inputPin
    cmp eax, storedPin
    je Correct

    mov eax, 0
    ret

Correct:
    mov eax, 1
    ret

VerifyPIN ENDP

END
