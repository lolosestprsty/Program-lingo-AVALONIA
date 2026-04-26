# Script to update comments in Level models
$files = @(
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level5Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level6Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level7Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level8Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level9Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level10Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level11Model.cs",
    "AvaloniaApplication1\AvaloniaApplication1\LevelManager\LevelModels\Level13Model.cs"
)

$replacements = @{
    '// define command: evaluate answer but wait for user to click "Dalej" to continue' = ''
    '// Validate empty input for VstupnaOtazka' = '// check if input empty'
    '// count every answered question' = ''
    '// mark progress color red for wrong answer' = ''
    '// update progress \(based on how many questions were answered so far\)' = ''
    "// ensure 'Dalej' button only when incorrect \(but not for ParovaciaOtazka\)" = '// dalej button len pre zle odpovede (nie parovacie)'
    '// if correct -> auto-advance to next question' = '// spravna odpoved = dalsia otazka hned'
    '// advance immediately' = ''
    '// finished - show summary' = ''
    "// move to next without showing 'Dalej'" = ''
    "// hide 'Dalej' because next is displayed" = ''
    "// wait for user to press 'Dalej' before advancing \(except ParovaciaOtazka\)" = '// cakat na dalej (okrem parovacie)'
    '// for ParovaciaOtazka, auto-advance even on wrong answer' = '// parovacie pokracuju aj po zle'
    "// advance to next question after user clicked 'Dalej'" = '// ked klikne dalej'
    '// clear answer visibility flags when moving to a next question' = '// vycisti flags pred dalsou otazkou'
    '// reset the displayed correct answer text' = ''
    '// reset parovacia items' = '// reset parovacich poloziek'
    '// unlock next level when 75% or more questions were answered correctly' = '// odomkni dalsi level ak >75%'
}

foreach ($file in $files) {
    if (Test-Path $file) {
        $content = Get-Content $file -Raw -Encoding UTF8

        foreach ($old in $replacements.Keys) {
            $new = $replacements[$old]
            $content = $content -replace [regex]::Escape($old), $new
        }

        # Fix nacitaj otazky comments with unicode chars
        $content = $content -replace '// Nac.*taj d.*ta z JSON', '// nahraj z JSON'
        $content = $content -replace '// Ak sa nenac.*', ''
        $content = $content -creplace 'žiadne otázky neboli na?ítané z databázy!', 'ziadne otazky!'

        Set-Content $file -Value $content -Encoding UTF8 -NoNewline
        Write-Host "Updated: $file"
    } else {
        Write-Host "Not found: $file"
    }
}
Write-Host "Done!"
