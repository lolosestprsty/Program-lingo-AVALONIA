# Add Level 1 and 2 to questions.json

$json = Get-Content "AvaloniaApplication1\AvaloniaApplication1\Assets\questions.json" -Raw | ConvertFrom-Json

$level1Questions = @'
[
    {"type": "ABCD", "text": "Ktora spolocnost vyvinula jazyk C#?", "explanation": "", "options": [
        {"id": 1, "question_id": 1, "option_text": "Apple", "option_index": 0, "is_correct": 0},
        {"id": 2, "question_id": 1, "option_text": "Google", "option_index": 1, "is_correct": 0},
        {"id": 3, "question_id": 1, "option_text": "Microsoft", "option_index": 2, "is_correct": 1},
        {"id": 4, "question_id": 1, "option_text": "IBM", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "V ktorom roku bol jazyk C# predstaveny?", "explanation": "", "options": [
        {"id": 5, "question_id": 2, "option_text": "1998", "option_index": 0, "is_correct": 0},
        {"id": 6, "question_id": 2, "option_text": "2002", "option_index": 1, "is_correct": 1},
        {"id": 7, "question_id": 2, "option_text": "2005", "option_index": 2, "is_correct": 0},
        {"id": 8, "question_id": 2, "option_text": "2010", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Aku priponu maju zdrojove subory C#?", "explanation": "", "options": [
        {"id": 9, "question_id": 3, "option_text": ".cpp", "option_index": 0, "is_correct": 0},
        {"id": 10, "question_id": 3, "option_text": ".java", "option_index": 1, "is_correct": 0},
        {"id": 11, "question_id": 3, "option_text": ".cs", "option_index": 2, "is_correct": 1},
        {"id": 12, "question_id": 3, "option_text": ".csharp", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Je jazyk C# case sensitive?", "explanation": "", "options": [
        {"id": 13, "question_id": 4, "option_text": "ANO", "option_index": 0, "is_correct": 1},
        {"id": 14, "question_id": 4, "option_text": "NIE", "option_index": 1, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Ktora moznost NIE JE vyuzitie C#?", "explanation": "", "options": [
        {"id": 15, "question_id": 5, "option_text": "Desktopove aplikacie", "option_index": 0, "is_correct": 0},
        {"id": 16, "question_id": 5, "option_text": "Mobilne aplikacie", "option_index": 1, "is_correct": 0},
        {"id": 17, "question_id": 5, "option_text": "Programovanie mikrovlniek", "option_index": 2, "is_correct": 1},
        {"id": 18, "question_id": 5, "option_text": "Pocitacove hry", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Ako sa nazyva automaticka sprava pamate?", "explanation": "", "options": [
        {"id": 19, "question_id": 6, "option_text": "Memory Cleaner", "option_index": 0, "is_correct": 0},
        {"id": 20, "question_id": 6, "option_text": "Garbage Collector", "option_index": 1, "is_correct": 1},
        {"id": 21, "question_id": 6, "option_text": "Memory Manager", "option_index": 2, "is_correct": 0},
        {"id": 22, "question_id": 6, "option_text": "AutoDelete", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Ktore IDE sa odporuca pre C#?", "explanation": "", "options": [
        {"id": 23, "question_id": 7, "option_text": "PyCharm", "option_index": 0, "is_correct": 0},
        {"id": 24, "question_id": 7, "option_text": "Eclipse", "option_index": 1, "is_correct": 0},
        {"id": 25, "question_id": 7, "option_text": "Microsoft Visual Studio", "option_index": 2, "is_correct": 1},
        {"id": 26, "question_id": 7, "option_text": "NetBeans", "option_index": 3, "is_correct": 0}
    ]}
]
'@ | ConvertFrom-Json

$level2Questions = @'
[
    {"type": "ABCD", "text": "Cim sa ukoncuje riadok kodu?", "explanation": "", "options": [
        {"id": 27, "question_id": 8, "option_text": "Bodkou", "option_index": 0, "is_correct": 0},
        {"id": 28, "question_id": 8, "option_text": "Dvojbodkou", "option_index": 1, "is_correct": 0},
        {"id": 29, "question_id": 8, "option_text": "Bodkociarkou", "option_index": 2, "is_correct": 1},
        {"id": 30, "question_id": 8, "option_text": "Ciarkou", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Na co sluzia zatvorky {}?", "explanation": "", "options": [
        {"id": 31, "question_id": 9, "option_text": "Oddelenie parametrov", "option_index": 0, "is_correct": 0},
        {"id": 32, "question_id": 9, "option_text": "Oznacenie bloku kodu", "option_index": 1, "is_correct": 1},
        {"id": 33, "question_id": 9, "option_text": "Ukoncenie programu", "option_index": 2, "is_correct": 0},
        {"id": 34, "question_id": 9, "option_text": "Zapisovanie komentarov", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Co robi WriteLine?", "explanation": "", "options": [
        {"id": 35, "question_id": 10, "option_text": "Vytvorenie premennej", "option_index": 0, "is_correct": 0},
        {"id": 36, "question_id": 10, "option_text": "Vypis do konzoly", "option_index": 1, "is_correct": 1},
        {"id": 37, "question_id": 10, "option_text": "Nacitanie vstupu", "option_index": 2, "is_correct": 0},
        {"id": 38, "question_id": 10, "option_text": "Ukoncenie programu", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Co robi bodka v Console.WriteLine?", "explanation": "", "options": [
        {"id": 39, "question_id": 11, "option_text": "Oddeluje riadky", "option_index": 0, "is_correct": 0},
        {"id": 40, "question_id": 11, "option_text": "Spaja texty", "option_index": 1, "is_correct": 0},
        {"id": 41, "question_id": 11, "option_text": "Pristup k metode", "option_index": 2, "is_correct": 1},
        {"id": 42, "question_id": 11, "option_text": "Ukoncuje blok", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Rozdiel medzi Write a WriteLine?", "explanation": "", "options": [
        {"id": 43, "question_id": 12, "option_text": "Nie je rozdiel", "option_index": 0, "is_correct": 0},
        {"id": 44, "question_id": 12, "option_text": "WriteLine bez odriadkovania", "option_index": 1, "is_correct": 0},
        {"id": 45, "question_id": 12, "option_text": "Write nevytvori novy riadok", "option_index": 2, "is_correct": 1},
        {"id": 46, "question_id": 12, "option_text": "Write pre vstup", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Ako sa zapisuje komentar?", "explanation": "", "options": [
        {"id": 47, "question_id": 13, "option_text": "/* */", "option_index": 0, "is_correct": 0},
        {"id": 48, "question_id": 13, "option_text": "//", "option_index": 1, "is_correct": 1},
        {"id": 49, "question_id": 13, "option_text": "#", "option_index": 2, "is_correct": 0},
        {"id": 50, "question_id": 13, "option_text": "--", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Co je premenna?", "explanation": "", "options": [
        {"id": 51, "question_id": 14, "option_text": "Funkcia", "option_index": 0, "is_correct": 0},
        {"id": 52, "question_id": 14, "option_text": "Miesto v pamati", "option_index": 1, "is_correct": 1},
        {"id": 53, "question_id": 14, "option_text": "Typ komentara", "option_index": 2, "is_correct": 0},
        {"id": 54, "question_id": 14, "option_text": "Operator", "option_index": 3, "is_correct": 0}
    ]},
    {"type": "ABCD", "text": "Datovy typ pre text?", "explanation": "", "options": [
        {"id": 55, "question_id": 15, "option_text": "int", "option_index": 0, "is_correct": 0},
        {"id": 56, "question_id": 15, "option_text": "double", "option_index": 1, "is_correct": 0},
        {"id": 57, "question_id": 15, "option_text": "string", "option_index": 2, "is_correct": 1},
        {"id": 58, "question_id": 15, "option_text": "char", "option_index": 3, "is_correct": 0}
    ]}
]
'@ | ConvertFrom-Json

$level1 = [PSCustomObject]@{
    levelNumber = 1
    name = "Uvod do C#"
    difficulty = "easy"
    questions = $level1Questions
}

$level2 = [PSCustomObject]@{
    levelNumber = 2
    name = "Zaklady syntaxe"
    difficulty = "easy"
    questions = $level2Questions
}

$allLevels = @($level1, $level2) + $json.levels | Sort-Object levelNumber
$final = [PSCustomObject]@{ levels = $allLevels }
$final | ConvertTo-Json -Depth 10 | Out-File "AvaloniaApplication1\AvaloniaApplication1\Assets\questions.json" -Encoding UTF8 -Force

Write-Host "Done! Total levels: $($allLevels.Count)"
