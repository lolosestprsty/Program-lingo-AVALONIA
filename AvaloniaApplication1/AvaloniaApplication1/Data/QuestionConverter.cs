using AvaloniaApplication1.LevelManager.Otazky;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaApplication1.Data
{
    public static class QuestionConverter
    {
        public static ObservableCollection<OtazkaBase> ConvertToOtazky(int levelNumber)
        {
            var otazky = new ObservableCollection<OtazkaBase>();
            
            // Na?ítaj dáta z JSON
            var levels = QuestionsLoader.LoadFromJson();
            var levelData = levels.FirstOrDefault(l => l.LevelNumber == levelNumber);

            if (levelData == null || levelData.Questions.Count == 0)
            {
                return otazky; // Vráti prázdnu kolekciu, volajúca metóda môže použi? fallback
            }

            // Konvertuj QuestionData na OtazkaBase objekty
            foreach (var questionData in levelData.Questions)
            {
                if (questionData.Type == "ABCD" && questionData.Options != null)
                {
                    var abcdOtazka = new ABCDOtazka
                    {
                        OtazkaText = questionData.Text,
                        Moznosti = new List<ABCDMoznost>()
                    };

                    // Pridaj možnosti
                    foreach (var option in questionData.Options.OrderBy(o => o.OptionIndex))
                    {
                        abcdOtazka.Moznosti.Add(new ABCDMoznost
                        {
                            Text = option.OptionText,
                            Index = option.OptionIndex
                        });
                    }

                    // Nastav správnu odpove?
                    var correctOption = questionData.Options.FirstOrDefault(o => o.IsCorrect == 1);
                    if (correctOption != null)
                    {
                        abcdOtazka.SpravnaMoznostIndex = correctOption.OptionIndex;
                    }

                    otazky.Add(abcdOtazka);
                }
                else if (questionData.Type == "Input" && !string.IsNullOrEmpty(questionData.CorrectAnswer))
                {
                    var vstupnaOtazka = new VstupnaOtazka
                    {
                        OtazkaText = questionData.Text,
                        SpravnaOdpoved = questionData.CorrectAnswer
                    };

                    otazky.Add(vstupnaOtazka);
                }
                else if (questionData.Type == "Pairing" && 
                         questionData.LeftColumn != null && 
                         questionData.RightColumn != null && 
                         questionData.CorrectPairs != null)
                {
                    var parovaciaOtazka = new ParovaciaOtazka
                    {
                        OtazkaText = questionData.Text
                    };

                    // Pridaj ?avý st?pec
                    for (int i = 0; i < questionData.LeftColumn.Count; i++)
                    {
                        parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka
                        {
                            Text = questionData.LeftColumn[i],
                            Index = i,
                            IsLeft = true
                        });
                    }

                    // Pridaj pravý st?pec
                    for (int i = 0; i < questionData.RightColumn.Count; i++)
                    {
                        parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka
                        {
                            Text = questionData.RightColumn[i],
                            Index = i,
                            IsLeft = false
                        });
                    }

                    // Pridaj správne páry
                    foreach (var pair in questionData.CorrectPairs)
                    {
                        parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka
                        {
                            Lava = pair.Left,
                            Prava = pair.Right
                        });
                    }

                    otazky.Add(parovaciaOtazka);
                }
            }

            return otazky;
        }
    }
}
