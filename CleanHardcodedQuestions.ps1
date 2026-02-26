# Properly remove hardcoded questions by replacing entire NacitajOtazky method

$template = @'
        private void NacitajOtazky()
        {
            // Na?ítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky({0});

            // Pridaj otázky do kolekcie
            foreach (var otazka in otazkyZJson)
            {
                Otazky.Add(otazka);
            }
        }
'@

for ($i = 3; $i -le 13; $i++) {
    $file = "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level${i}Model.cs"
    
    if (Test-Path $file) {
        Write-Host "Processing Level $i..."
        
        $content = Get-Content $file -Raw -Encoding UTF8
        
        # Find and replace the NacitajOtazky method
        # Match from "private void NacitajOtazky()" to the closing brace of that method
        $pattern = '(?s)private void NacitajOtazky\(\).*?\r?\n\s*\}\r?\n(?=\s*\}\r?\n\})'
        
        $replacement = $template -f $i
        
        $newContent = $content -replace $pattern, $replacement
        
        if ($newContent -ne $content) {
            $newContent | Out-File $file -Encoding UTF8 -NoNewline
            Write-Host "  Cleaned Level $i"
        } else {
            Write-Host "  No match for Level $i - trying alternative pattern"
        }
    }
}

Write-Host "`nDone!"
