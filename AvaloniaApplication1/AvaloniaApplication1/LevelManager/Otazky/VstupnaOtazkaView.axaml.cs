using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.LevelManager.Otazky;

namespace AvaloniaApplication1.Views.Questions;

public partial class VstupnaOtazkaView : UserControl
{
    private TextBox? _answerTextBox;
    private Button? _submitButton;

    public VstupnaOtazkaView()
    {
        InitializeComponent();
        _answerTextBox = this.FindControl<TextBox>("AnswerTextBox");
        _submitButton = this.FindControl<Button>("SubmitButton");

        if (_answerTextBox != null)
        {
            _answerTextBox.KeyDown += AnswerTextBox_KeyDown;
        }
    }

    private void AnswerTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && _submitButton != null)
        {
            if (DataContext is VstupnaOtazka otazka)
            {
                if (string.IsNullOrWhiteSpace(otazka.UserInput))
                {
                    return;
                }
                
                if (otazka.OdpovedCommand?.CanExecute(otazka.UserInput) == true)
                {
                    otazka.OdpovedCommand.Execute(otazka.UserInput);
                }
            }
        }
    }
}