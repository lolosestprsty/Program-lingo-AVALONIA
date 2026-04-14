using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.LevelManager.Otazky;
public partial class VstupnaOtazka : OtazkaBase
{
    public string SpravnaOdpoved { get; set; } = "";
    public IRelayCommand<object>? OdpovedCommand { get; set; }

    [ObservableProperty]
    private string userInput = string.Empty;

    private bool _showCorrectAnswerFlag;
    public bool ShowCorrectAnswerFlag { get => _showCorrectAnswerFlag; set => SetProperty(ref _showCorrectAnswerFlag, value); }

    public string SpravnaOdpovedNoDiacritics => RemoveDiacritics(SpravnaOdpoved ?? string.Empty);

    public override bool SkontrolujOdpoved(object odpoved)
    {
        var result = odpoved is string text &&
                     text.Trim().Equals(SpravnaOdpoved, StringComparison.OrdinalIgnoreCase);

        // show correct-answer view only when incorrect
        ShowCorrectAnswerFlag = !result;

        // Vymaž text po odpovedi
        UserInput = string.Empty;

        return result;
    }

    private static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder();
        foreach (var ch in normalized)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch) != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(ch);
        }
        return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }
}
