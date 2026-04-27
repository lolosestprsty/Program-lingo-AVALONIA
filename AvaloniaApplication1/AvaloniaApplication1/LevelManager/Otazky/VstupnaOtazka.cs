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
}
