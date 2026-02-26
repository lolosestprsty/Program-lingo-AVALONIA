# Complete questions.json generator - adds Level 1 and 2, sorts levels

$existingJson = Get-Content "AvaloniaApplication1\AvaloniaApplication1\Assets\questions_generated.json" -Raw | ConvertFrom-Json

# Add Level 1
$level1 = @{
    levelNumber = 1
    name = "Uvod do C#"
    difficulty = "easy"
    questions = @(
        @{
            type = "ABCD"
            text = "Ktora spolocnost vyvinula jazyk C#?"
            explanation = "C# bol vytvoreny spolocnostou Microsoft ako sucast platformy .NET."
            options = @(
                @{ id = 1; question_id = 1; option_text = "Apple"; option_index = 0; is_correct = 0 }
                @{ id = 2; question_id = 1; option_text = "Google"; option_index = 1; is_correct = 0 }
                @{ id = 3; question_id = 1; option_text = "Microsoft"; option_index = 2; is_correct = 1 }
                @{ id = 4; question_id = 1; option_text = "IBM"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "V ktorom roku bol jazyk C# predstaveny spolu s .NET Framework?"
            explanation = "C# bol predstaveny v roku 2002."
            options = @(
                @{ id = 5; question_id = 2; option_text = "1998"; option_index = 0; is_correct = 0 }
                @{ id = 6; question_id = 2; option_text = "2002"; option_index = 1; is_correct = 1 }
                @{ id = 7; question_id = 2; option_text = "2005"; option_index = 2; is_correct = 0 }
                @{ id = 8; question_id = 2; option_text = "2010"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Aku priponu maju zdrojove subory jazyka C#?"
            explanation = "Zdrojove subory C# maju priponu .cs"
            options = @(
                @{ id = 9; question_id = 3; option_text = ".cpp"; option_index = 0; is_correct = 0 }
                @{ id = 10; question_id = 3; option_text = ".java"; option_index = 1; is_correct = 0 }
                @{ id = 11; question_id = 3; option_text = ".cs"; option_index = 2; is_correct = 1 }
                @{ id = 12; question_id = 3; option_text = ".csharp"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Je jazyk C# case sensitive?"
            explanation = "C# rozlisuje velke a male pismena (case sensitive)."
            options = @(
                @{ id = 13; question_id = 4; option_text = "ANO"; option_index = 0; is_correct = 1 }
                @{ id = 14; question_id = 4; option_text = "NIE"; option_index = 1; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Ktora z moznosti NIE JE uvedena ako vyuzitie C#?"
            explanation = "C# sa pouziva na desktopove, mobilne aplikacie a hry, ale nie na programovanie mikrovlniek."
            options = @(
                @{ id = 15; question_id = 5; option_text = "Desktopove aplikacie"; option_index = 0; is_correct = 0 }
                @{ id = 16; question_id = 5; option_text = "Mobilne aplikacie"; option_index = 1; is_correct = 0 }
                @{ id = 17; question_id = 5; option_text = "Programovanie mikrovlniek"; option_index = 2; is_correct = 1 }
                @{ id = 18; question_id = 5; option_text = "Pocitacove hry"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Ako sa nazyva automaticka sprava pamate v C#?"
            explanation = "Garbage Collector v C# automaticky uvolnuje pamat."
            options = @(
                @{ id = 19; question_id = 6; option_text = "Memory Cleaner"; option_index = 0; is_correct = 0 }
                @{ id = 20; question_id = 6; option_text = "Garbage Collector"; option_index = 1; is_correct = 1 }
                @{ id = 21; question_id = 6; option_text = "Memory Manager"; option_index = 2; is_correct = 0 }
                @{ id = 22; question_id = 6; option_text = "AutoDelete"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Ktore vyvojove prostredie sa odporuca na zaciatok s C#?"
            explanation = "Microsoft Visual Studio je najpouzivanejsie IDE pre C#."
            options = @(
                @{ id = 23; question_id = 7; option_text = "PyCharm"; option_index = 0; is_correct = 0 }
                @{ id = 24; question_id = 7; option_text = "Eclipse"; option_index = 1; is_correct = 0 }
                @{ id = 25; question_id = 7; option_text = "Microsoft Visual Studio"; option_index = 2; is_correct = 1 }
                @{ id = 26; question_id = 7; option_text = "NetBeans"; option_index = 3; is_correct = 0 }
            )
        }
    )
}

# Add Level 2
$level2 = @{
    levelNumber = 2
    name = "Zaklady syntaxe"
    difficulty = "easy"
    questions = @(
        @{
            type = "ABCD"
            text = "Cim sa ukoncuje kazdy riadok kodu v jazyku C#?"
            explanation = "Kazdy prikaz v C# sa ukoncuje bodkociarkou."
            options = @(
                @{ id = 27; question_id = 8; option_text = "Bodkou"; option_index = 0; is_correct = 0 }
                @{ id = 28; question_id = 8; option_text = "Dvojbodkou"; option_index = 1; is_correct = 0 }
                @{ id = 29; question_id = 8; option_text = "Bodkociarkou"; option_index = 2; is_correct = 1 }
                @{ id = 30; question_id = 8; option_text = "Ciarkou"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Na co sluzia zlozene zatvorky {} v jazyku C#?"
            explanation = "Zlozene zatvorky oznacuju blok kodu."
            options = @(
                @{ id = 31; question_id = 9; option_text = "Na oddelenie parametrov"; option_index = 0; is_correct = 0 }
                @{ id = 32; question_id = 9; option_text = "Na oznacenie bloku kodu"; option_index = 1; is_correct = 1 }
                @{ id = 33; question_id = 9; option_text = "Na ukoncenie programu"; option_index = 2; is_correct = 0 }
                @{ id = 34; question_id = 9; option_text = "Na zapisovanie komentarov"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Co znamena zapis Console.WriteLine(\"Hello\");"
            explanation = "WriteLine vypise text do konzoly a presunie kurzor na novy riadok."
            options = @(
                @{ id = 35; question_id = 10; option_text = "Vytvorenie premennej"; option_index = 0; is_correct = 0 }
                @{ id = 36; question_id = 10; option_text = "Vypis textu do konzoly a prechod na novy riadok"; option_index = 1; is_correct = 1 }
                @{ id = 37; question_id = 10; option_text = "Nacitanie vstupu"; option_index = 2; is_correct = 0 }
                @{ id = 38; question_id = 10; option_text = "Ukoncenie programu"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Co robi bodka medzi Console a WriteLine?"
            explanation = "Bodka umoznuje pristup k metode triedy."
            options = @(
                @{ id = 39; question_id = 11; option_text = "Oddeluje dva riadky kodu"; option_index = 0; is_correct = 0 }
                @{ id = 40; question_id = 11; option_text = "Spaja dva texty"; option_index = 1; is_correct = 0 }
                @{ id = 41; question_id = 11; option_text = "Umoznuje pristup k metode triedy"; option_index = 2; is_correct = 1 }
                @{ id = 42; question_id = 11; option_text = "Ukoncuje blok kodu"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Aky je rozdiel medzi Write() a WriteLine()?"
            explanation = "Write nevytvori novy riadok, WriteLine ano."
            options = @(
                @{ id = 43; question_id = 12; option_text = "Nie je ziadny rozdiel"; option_index = 0; is_correct = 0 }
                @{ id = 44; question_id = 12; option_text = "WriteLine vypise text bez odriadkovania"; option_index = 1; is_correct = 0 }
                @{ id = 45; question_id = 12; option_text = "Write nevytvori novy riadok, WriteLine ano"; option_index = 2; is_correct = 1 }
                @{ id = 46; question_id = 12; option_text = "Write sluzi na vstup"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Ako zapisujeme jednoriadkovy komentar?"
            explanation = "Jednoriadkovy komentar zacina //"
            options = @(
                @{ id = 47; question_id = 13; option_text = "/* komentar */"; option_index = 0; is_correct = 0 }
                @{ id = 48; question_id = 13; option_text = "// komentar"; option_index = 1; is_correct = 1 }
                @{ id = 49; question_id = 13; option_text = "# komentar"; option_index = 2; is_correct = 0 }
                @{ id = 50; question_id = 13; option_text = "-- komentar"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Co je premenna?"
            explanation = "Premenna je pomenovane miesto v pamati."
            options = @(
                @{ id = 51; question_id = 14; option_text = "Funkcia na vypis"; option_index = 0; is_correct = 0 }
                @{ id = 52; question_id = 14; option_text = "Pomenovane miesto v pamati"; option_index = 1; is_correct = 1 }
                @{ id = 53; question_id = 14; option_text = "Typ komentara"; option_index = 2; is_correct = 0 }
                @{ id = 54; question_id = 14; option_text = "Operator"; option_index = 3; is_correct = 0 }
            )
        }
        @{
            type = "ABCD"
            text = "Ktory datovy typ sluzi na ulozenie textu?"
            explanation = "String sa pouziva na ulozenie textovych retazcov."
            options = @(
                @{ id = 55; question_id = 15; option_text = "int"; option_index = 0; is_correct = 0 }
                @{ id = 56; question_id = 15; option_text = "double"; option_index = 1; is_correct = 0 }
                @{ id = 57; question_id = 15; option_text = "string"; option_index = 2; is_correct = 1 }
                @{ id = 58; question_id = 15; option_text = "char"; option_index = 3; is_correct = 0 }
            )
        }
    )
}

# Combine all levels and sort
$allLevels = @($level1, $level2) + $existingJson.levels | Sort-Object levelNumber

# Create final JSON
$finalJson = @{
    levels = $allLevels
} | ConvertTo-Json -Depth 10

# Save
$finalJson | Out-File -FilePath "AvaloniaApplication1\AvaloniaApplication1\Assets\questions.json" -Encoding UTF8 -Force

Write-Host "Complete questions.json created with all 13 levels!"
