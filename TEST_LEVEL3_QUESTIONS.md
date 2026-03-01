# Test na?ítania otázok z databázy

## Test Level 3

Spustite aplikáciu a otvorte Level 3. Mali by ste vidie? nasledujúce otázky:

### O?akávané otázky v Level 3:

1. **Vstupná otázka**: "Každý riadok kódu ukon?ujeme znakom _______"
   - Správna odpove?: `;`

2. **Vstupná otázka**: "Na na?ítanie vstupu z klávesnice používame metódu __________"
   - Správna odpove?: `Console.ReadLine()`

3. **Vstupná otázka**: "Prevod vstupu na celé ?íslo urobíme pomocou __________"
   - Správna odpove?: `Convert.ToInt32`

4. **Vstupná otázka**: "Dátový typ pre jedno písmeno alebo znak je __________"
   - Správna odpove?: `char`

5. **Párovacia otázka**: "Spoj dvojice - Dátové typy a ich významy"
   - Páry:
     - int ? celé ?íslo
     - double ? desatinné ?íslo
     - char ? jeden znak
     - string ? text

## ?o bolo opravené:

1. **QuestionConverter.cs**:
   - Pridaná podpora pre slovenské názvy typov: `"Vstupna"` a `"Parovacia"`
   - Pridaná podpora pre nový formát párovacích otázok s `"pairs"` namiesto `"leftColumn"` a `"rightColumn"`
   - Zachovaná spätná kompatibilita so starým formátom

2. **QuestionsLoader.cs**:
   - Pridané pole `Pairs` pre nový formát párovacích otázok

## Ladenie:

Ak sa otázky stále nezobrazujú:

1. Skontrolujte konzolu aplikácie - mali by sa tam zobrazova? prípadné chyby pri na?ítaní JSON
2. Overte, že súbor `questions.json` je správne zahrnutý v projekte ako `AvaloniaResource`
3. Pridajte breakpoint do `QuestionConverter.ConvertToOtazky()` a sledujte, ?i sa otázky správne parsujú

## Overenie v kóde:

V `QuestionConverter.cs` sa teraz kontroluje typ otázky takto:

```csharp
var questionType = questionData.Type.ToLower();

if (questionType == "vstupna" || questionType == "input") {
    // Vytvorí VstupnaOtazka
}

if (questionType == "parovacia" || questionType == "pairing") {
    // Vytvorí ParovaciaOtazka
}
```

## Testovanie:

1. Spustite aplikáciu
2. Prejdite na Level 3
3. Mali by sa zobrazi? **5 otázok** (4 vstupné + 1 párovacia)
4. Skúste odpoveda? na každú otázku a overte, že validácia funguje
