using AvaloniaApplication1.LevelManager.Otazky;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication1.LevelManager.Otazky;

namespace AvaloniaApplication1.Data
{
    // TEMPORARY TESTING MODE: Hardcoded fallbacks are disabled in all Level models
    // to verify which questions are properly stored in the JSON database.
    // If a level has no questions, it will appear empty in the app.
    // This helps identify which levels need their questions added to questions.json
    public static class QuestionConverter
    {
        private static Random _random = new Random();

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
                // Podporuj obe verzie názvov typov (anglické aj slovenské)
                var questionType = questionData.Type.ToLower();
                
                if (questionType == "abcd" && questionData.Options != null)
                {
                    var abcdOtazka = new ABCDOtazka
                    {
                        OtazkaText = questionData.Text,
                        Explanation = questionData.Explanation ?? string.Empty,
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
                else if ((questionType == "input" || questionType == "vstupna") && 
                         !string.IsNullOrEmpty(questionData.CorrectAnswer))
                {
                    var vstupnaOtazka = new VstupnaOtazka
                    {
                        OtazkaText = questionData.Text,
                        SpravnaOdpoved = questionData.CorrectAnswer,
                        Explanation = questionData.Explanation ?? string.Empty
                    };

                    otazky.Add(vstupnaOtazka);
                }
                else if ((questionType == "pairing" || questionType == "parovacia") && 
                         questionData.Pairs != null)
                {
                    var parovaciaOtazka = new ParovaciaOtazka
                    {
                        OtazkaText = questionData.Text,
                        Explanation = questionData.Explanation ?? string.Empty
                    };

                    // Extrahuj ?avý a pravý st?pec z párov
                    var leftItems = new List<string>();
                    var rightItems = new List<string>();
                    
                    foreach (var pair in questionData.Pairs)
                    {
                        if (!leftItems.Contains(pair.Left))
                            leftItems.Add(pair.Left);
                        if (!rightItems.Contains(pair.Right))
                            rightItems.Add(pair.Right);
                    }

                    // Pridaj ?avý st?pec
                    for (int i = 0; i < leftItems.Count; i++)
                    {
                        parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka
                        {
                            Text = leftItems[i],
                            Index = i,
                            IsLeft = true
                        });
                    }

                    // Premiešaj pravý st?pec, aby odpovede neboli v rovnakom poradí
                    var shuffledRightItems = rightItems.OrderBy(x => _random.Next()).ToList();

                    // Pridaj pravý st?pec (premiešaný)
                    for (int i = 0; i < shuffledRightItems.Count; i++)
                    {
                        parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka
                        {
                            Text = shuffledRightItems[i],
                            Index = i,
                            IsLeft = false
                        });
                    }

                    // Pridaj správne páry
                    foreach (var pair in questionData.Pairs)
                    {
                        parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka
                        {
                            Lava = pair.Left,
                            Prava = pair.Right
                        });
                    }

                    otazky.Add(parovaciaOtazka);
                }
                // Starý formát s explicitnými st?pcami (pre spätnú kompatibilitu)
                else if ((questionType == "pairing" || questionType == "parovacia") && 
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

                    // Premiešaj pravý st?pec, aby odpovede neboli v rovnakom poradí
                    var shuffledRightColumn = questionData.RightColumn.OrderBy(x => _random.Next()).ToList();

                    // Pridaj pravý st?pec (premiešaný)
                    for (int i = 0; i < shuffledRightColumn.Count; i++)
                    {
                        parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka
                        {
                            Text = shuffledRightColumn[i],
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
