# Testovanie otázok z databázy (JSON)

## ?o bolo zmenené?

Do?asne som **deaktivoval hardcoded fallback** vo všetkých level modeloch (Level1Model až Level13Model). Teraz aplikácia používa **iba otázky z questions.json** súboru.

## Zmeny v kóde:

### Všetky LevelXModel.cs súbory (Level 1-13):
- V metóde `NacitajOtazky()` som zakomentoval podmienku, ktorá kontrolovala ?i JSON obsahuje otázky
- Hardcoded otázky (fallback) sú stále v kóde, ale sú nedostupné
- Ak JSON neobsahuje otázky pre daný level, level bude prázdny

### QuestionConverter.cs:
- Pridal som komentár, ktorý upozor?uje na testovací režim

## Ako testova?:

1. **Spus? aplikáciu** (Desktop verzia)
2. **Prejdi cez všetky levely** (1-13)
3. **Sleduj, ktoré levely majú otázky a ktoré sú prázdne**

## O?akávané výsledky:

Pod?a aktuálneho stavu `questions.json`:
- ? **Level 1**: Má 7 otázok v JSON ? Mal by fungova? normálne
- ? **Level 2-13**: Nemajú otázky v JSON ? Budú prázdne

## Ako zisti? chýbajúce otázky:

Ke? spustíš aplikáciu a level je prázdny, znamená to že:
- Otázky pre tento level ešte **nie sú v questions.json**
- Potrebuješ ich **prida? do databázy** pomocou `ExtractQuestionsToJson.ps1` alebo manuálne

## Vrátenie spä?:

Ke? chceš **obnovi? hardcoded fallback**, jednoducho **odkomentuj** tieto riadky vo všetkých LevelXModel.cs:

```csharp
// Zmeni? z:
// TEMPORARILY DISABLED: Fallback to hardcoded questions
//if (otazkyZJson.Count > 0)
//{
    foreach (var otazka in otazkyZJson)
    {
        Otazky.Add(otazka);
    }
    return;
//}

// Spä? na:
if (otazkyZJson.Count > 0)
{
    foreach (var otazka in otazkyZJson)
    {
        Otazky.Add(otazka);
    }
    return;
}
```

## PowerShell príkaz na kontrolu questions.json:

```powershell
$json = Get-Content "AvaloniaApplication1\AvaloniaApplication1\Assets\questions.json" -Raw | ConvertFrom-Json
"Total levels in JSON: $($json.levels.Count)"
$json.levels | ForEach-Object { 
    "Level $($_.levelNumber) ($($_.name)): $($_.questions.Count) questions" 
}
```

## Aktuálny stav databázy:

- **Total levels v JSON: 1**
- **Level 1 (Úvod do C#): 7 otázok** ?
- **Level 2-13: Chybajú** ?

---

**Poznámka:** Build warnings "CS0162: Unreachable code detected" sú normálne - upozor?ujú na nedostupný hardcoded kód, ?o je presne to, ?o chceme po?as testovania.
