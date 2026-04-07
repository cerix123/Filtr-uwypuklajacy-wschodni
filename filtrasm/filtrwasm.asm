.code
MyProc1 proc
    ; Argumenty:
    ; rsi - wskaŸnik na bufor wejœciowy (oryginalny obraz)
    ; rdi - wskaŸnik na bufor wyjœciowy (przetworzony obraz)
    ; r8  - wysokoœæ
    ; r9  - szerokoœæ
    ; stride - na stosie, przeniesiony do r11

    mov r11, [rsp+40]        ; Przenieœ stride do r11
    sub r8, 2                ; Pomijanie brzegów (wysokoœæ - 2)
    sub r9, 2                ; Pomijanie brzegów (szerokoœæ - 2)

    mov rbx, 1               ; Licznik wierszy (zaczynamy od wiersza 1)
    mov r12, rdi             ; WskaŸnik bie¿¹cej pozycji w buforze wyjœciowym

rows_loop:
    cmp rbx, r8              ; Czy osi¹gnêliœmy ostatni wiersz?
    jge koniec

    ; Bie¿¹cy wiersz
    mov rax, rbx
    imul rax, r11            ; rax = rbx * stride
    add rax, rsi             ; Dodaj wskaŸnik wejœciowy
    add rax, 3               ; Pomijamy pierwszy piksel (brzeg)
    mov r13, rax             ; r13 = wskaŸnik bie¿¹cego wiersza

    ; Wiersz powy¿ej
    mov rax, rbx
    dec rax
    imul rax, r11
    add rax, rsi
    add rax, 3
    mov r14, rax             ; r14 = wskaŸnik wiersza powy¿ej

    ; Wiersz poni¿ej
    mov rax, rbx
    inc rax
    imul rax, r11
    add rax, rsi
    add rax, 3
    mov r15, rax             ; r15 = wskaŸnik wiersza poni¿ej

    ; Licznik kolumn
  ;  xor rcx, rcx             ; Ustaw licznik kolumn na 0
    xor r10, r10            ;licznik bajtow
columns_loop:
    cmp r10, r11             ; Czy osi¹gnêliœmy ostatni¹ kolumnê?
    jge next_row

    mov rax, r11
    sub rax, r10             ; SprawdŸ, ile bajtów zosta³o do koñca wiersza
    cmp rax, 8
    jl scalar_processing     ; Jeœli mniej ni¿ 8, przejdŸ do trybu skalarnego

    ; Przetwarzaj 8 bajtów (2 piksele RGB) na raz

    pmovzxbw xmm0, [r14 + r10 - 3] ; Górny-lewy
    pmovzxbw xmm1, [r14 + r10 + 3] ; Górny-prawy
    pmovzxbw xmm2, [r13 + r10 - 3] ; Œrodkowy-lewy
    pmovzxbw xmm3, [r13 + r10]     ; Œrodkowy (centralny pixel)
    pmovzxbw xmm4, [r13 + r10 + 3] ; Œrodkowy-prawy
    pmovzxbw xmm5, [r15 + r10 - 3] ; Dolny-lewy
    pmovzxbw xmm6, [r15 + r10 + 3] ; Dolny-prawy

    psubw xmm3, xmm0               ; -1 * górny-lewy
    paddw xmm3, xmm1               ; +1 * górny-prawy
    psubw xmm3, xmm2               ; -1 * œrodkowy-lewy
    paddw xmm3, xmm4               ; +1 * œrodkowy-prawy
    psubw xmm3, xmm5               ; -1 * dolny-lewy
    paddw xmm3, xmm6               ; +1 * dolny-prawy

    packuswb xmm3, xmm3            ; Clampowanie wyników do zakresu 0-255

    movq QWORD PTR [r12 + r10], xmm3

    add r10, 8
    jmp columns_loop

scalar_processing:
    movzx ax, BYTE PTR [r14 + r10 - 3]  ; Górny-lewy 
    sub ax, WORD PTR [r13 + r10]        ; -1 * centralny piksel
    add ax, WORD PTR [r14 + r10 + 3]    ; +1 * górny-prawy
    sub ax, WORD PTR [r13 + r10 - 3]    ; -1 * œrodkowy-lewy
    add ax, WORD PTR [r13 + r10 + 3]    ; +1 * œrodkowy-prawy
    sub ax, WORD PTR [r15 + r10 - 3]    ; -1 * dolny-lewy
    add ax, WORD PTR [r15 + r10 + 3]    ; +1 * dolny-prawy

    ; Ograniczenie wyniku do zakresu 0-255
    cmp ax, 255
    jle no_clamp
    mov ax, 255
no_clamp:
    cmp ax, 0
    jge store_value
    mov ax, 0
store_value:
    mov BYTE PTR [r12 + r10], al   ; Zapisz wynik

    inc r10
    cmp r10, r11
    jl scalar_processing

    jmp next_row

next_row:
    add r12, r11                   ; Przesuñ wskaŸnik wyjœciowy do nastêpnego wiersza
    inc rbx                        ; PrzejdŸ do nastêpnego wiersza
    jmp rows_loop

koniec:
    ret
MyProc1 endp
end
