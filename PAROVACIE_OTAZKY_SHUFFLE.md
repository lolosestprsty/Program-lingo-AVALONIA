# Premiešanie párovacích otázok

## Implementované zmeny

? **Pravý st?pec párovacích otázok sa teraz náhodne premiešava** pri každom na?ítaní otázky.

### ?o bolo zmenené:

**Súbor**: `QuestionConverter.cs`

1. **Pridaný Random generátor**:
   ```csharp
   private static Random _random = new Random();
   ```

2. **Premiešanie pravého st?pca** (nový formát s `pairs`):
   ```csharp
   // Premiešaj pravý st?pec, aby odpovede neboli v rovnakom poradí
   var shuffledRightItems = rightItems.OrderBy(x => _random.Next()).ToList();
   ```

3. **Premiešanie pravého st?pca** (starý formát s `leftColumn` a `rightColumn`):
   ```csharp
   // Premiešaj pravý st?pec, aby odpovede neboli v rovnakom poradí
   var shuffledRightColumn = questionData.RightColumn.OrderBy(x => _random.Next()).ToList();
   ```

## Ako to funguje

### Pred zmenou:
```
?avý st?pec          Pravý st?pec
?????????????        ????????????????
int            ?     celé ?íslo
double         ?     desatinné ?íslo
char           ?     jeden znak
string         ?     text
```
? Študent môže len klika? zhora nadol v rovnakom poradí.

### Po zmene:
```
?avý st?pec          Pravý st?pec (premiešaný)
?????????????        ????????????????
int            ?     jeden znak
double         ?     text
char           ?     celé ?íslo
string         ?     desatinné ?íslo
```
? Študent musí skuto?ne vedie?, ktorá odpove? patrí ku ktorej položke.

## Príklad použitia

Pri otvorení Level 3 s párovacou otázkou:

**Otázka**: "Spoj dvojice - Dátové typy a ich významy"

**?avý st?pec** (vždy v rovnakom poradí):
- int
- double
- char
- string

**Pravý st?pec** (náhodne premiešaný pri každom na?ítaní):
- Môže by?: "text", "celé ?íslo", "jeden znak", "desatinné ?íslo"
- Alebo: "jeden znak", "desatinné ?íslo", "text", "celé ?íslo"
- Alebo akéko?vek iné náhodné poradie

## Výhody

1. ? **Vyššia náro?nos?** - študent nemôže len mechanicky klika?
2. ? **Lepšie u?enie** - musí skuto?ne rozumie? materiálu
3. ? **Variabilita** - pri opakovaní levelu sú otázky inak usporiadané
4. ? **Zachovaná správnos?** - validácia stále funguje správne (kontroluje sa text, nie pozícia)

## Testovanie

1. Spustite aplikáciu
2. Otvorte Level 3 (alebo ktorýko?vek level s párovacou otázkou)
3. Overte, že pravý st?pec je v inom poradí ako ?avý
4. Reštartujte level - pravý st?pec by mal by? opä? premiešaný (inak)
5. Skontrolujte, že validácia funguje správne

## Dotknuté levely

Táto zmena ovplyv?uje všetky levely s párovaciami otázkami:
- ? Level 3 (Dátové typy)
- ? Level 4 (Konverzie)
- ? Level 5 (Podmienky)
- ? Level 6 (Switch - 2 párovacíe otázky)
- ? Level 7 (Cyklus for)
- ? Level 8 (Cykly while)
- ? Level 9 (Náhodné ?ísla)
- ? Level 10 (Polia)
- ? Level 11 (2D polia - 2 párovacíe otázky)
- ? Level 12 (Štruktúry - 2 párovacíe otázky)
- ? Level 13 (Súbory - 2 párovacíe otázky)

**Celkovo**: 16 párovacích otázok vo všetkých leveloch

## Technická poznámka

Použitie `OrderBy(x => _random.Next())` je jednoduchá a efektívna metóda náhodného premiešania (Fisher-Yates shuffle by bol o nie?o efektívnejší, ale pre malé kolekcie 4-5 položiek je tento prístup úplne posta?ujúci).
