# Script to remove all hardcoded questions from Level Models 3-13

$levelFiles = 3..13 | ForEach-Object { "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level${_}Model.cs" }

foreach ($file in $levelFiles) {
    if (Test-Path $file) {
        Write-Host "Processing $file..."
        
        $content = Get-Content $file -Raw -Encoding UTF8
        
        # Pattern to match the fallback section - from "if (otazkyZJson.Count > 0)" to end of method
        # Replace with simple direct loop
        
        $pattern = '(?s)if \(otazkyZJson\.Count > 0\)\s*\{.*?return;\s*\}.*?(?=\s+\})'
        
        $replacement = "// Pridaj otázky do kolekcie`r`n            foreach (var otazka in otazkyZJson)`r`n            {`r`n                Otazky.Add(otazka);`r`n            }`r`n        "
        
        $newContent = $content -replace $pattern, $replacement
        
        if ($newContent -ne $content) {
            $newContent | Out-File $file -Encoding UTF8 -Force
            Write-Host "  Updated $file"
        } else {
            Write-Host "  No changes needed for $file"
        }
    }
}

Write-Host "`nDone!"
