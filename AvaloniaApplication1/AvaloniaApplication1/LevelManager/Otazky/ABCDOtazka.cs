using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.LevelManager.Otazky;

public partial class ABCDOtazka : OtazkaBase
{
    public List<ABCDMoznost> Moznosti { get; set; } = new List<ABCDMoznost>();
    public int SpravnaMoznostIndex { get; set; }
    public IRelayCommand<object>? OdpovedCommand { get; set; }
    private bool _showCorrectAnswerFlag;
    public bool ShowCorrectAnswerFlag { get => _showCorrectAnswerFlag; set => SetProperty(ref _showCorrectAnswerFlag, value); }

    private string _spravnaMoznostText = string.Empty;
    public string SpravnaMoznostText { get => _spravnaMoznostText; set => SetProperty(ref _spravnaMoznostText, value); }

    public string SpravnaMoznostLabel => string.IsNullOrWhiteSpace(SpravnaMoznostText) ? string.Empty : $"Správna odpoveď: {SpravnaMoznostText}";

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
}
