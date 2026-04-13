using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Globalization;

namespace AvaloniaApplication1.LevelManager.Otazky;

public partial class ABCDOtazka : OtazkaBase
{
    public List<ABCDMoznost> Moznosti { get; set; } = new List<ABCDMoznost>();
    public int SpravnaMoznostIndex { get; set; }
    public IRelayCommand<object>? OdpovedCommand { get; set; }
    private bool _showCorrectAnswerFlag;
    public bool ShowCorrectAnswerFlag { get => _showCorrectAnswerFlag; set => SetProperty(ref _showCorrectAnswerFlag, value); }

    private string _spravnaMoznostText = string.Empty;
    public string SpravnaMoznostText
    {
        get => _spravnaMoznostText;
        set
        {
            if (SetProperty(ref _spravnaMoznostText, value))
            {
                // notify dependent computed property
                OnPropertyChanged(nameof(SpravnaMoznostTextNoDiacritics));
            }
        }
    }

    // computed property: correct answer without diacritics
    public string SpravnaMoznostTextNoDiacritics => RemoveDiacritics(SpravnaMoznostText ?? string.Empty);

    public override bool SkontrolujOdpoved(object odpoved)
    {
        if (!(odpoved is int index))
            return false;

        bool correct = index == SpravnaMoznostIndex;

        // show correct-answer view only when incorrect
        ShowCorrectAnswerFlag = !correct;

        // update the displayed correct answer text (raw text only)
        var correctText = Moznosti?.FirstOrDefault(m => m.Index == SpravnaMoznostIndex)?.Text ?? string.Empty;
        SpravnaMoznostText = correctText;

        return correct;
    }

    private static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var ch in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                sb.Append(ch);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
