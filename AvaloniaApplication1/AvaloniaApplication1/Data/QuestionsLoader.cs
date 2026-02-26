using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class QuestionsLoader
{
    public static List<LevelData> LoadFromJson()
    {
        var uri = new Uri("avares://AvaloniaApplication1/Assets/questions.json");
        using var stream = AssetLoader.Open(uri);
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<QuestionsData>(json, options);
        return data?.Levels ?? new List<LevelData>();
    }
}

public class QuestionsData
{
    [JsonPropertyName("levels")]
    public List<LevelData> Levels { get; set; } = new();
}

public class LevelData
{
    [JsonPropertyName("levelNumber")]
    public int LevelNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [JsonPropertyName("questions")]
    public List<QuestionData> Questions { get; set; } = new();
}

public class QuestionData
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("explanation")]
    public string? Explanation { get; set; }

    [JsonPropertyName("options")]
    public List<OptionData>? Options { get; set; }

    [JsonPropertyName("correctAnswer")]
    public string? CorrectAnswer { get; set; }
}

public class OptionData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("question_id")]
    public int QuestionId { get; set; }

    [JsonPropertyName("option_text")]
    public string OptionText { get; set; } = string.Empty;

    [JsonPropertyName("option_index")]
    public int OptionIndex { get; set; }

    [JsonPropertyName("is_correct")]
    public int IsCorrect { get; set; }
}