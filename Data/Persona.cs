namespace PersuadeMate.Data;

/// <summary>
/// 提案する対象の人を表現するクラスです
/// </summary>
public record class Persona
{
    /// <summary>
    /// 性別です
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 年代です
    /// </summary>
    public Age? Age { get; set; }

    /// <summary>
    /// 趣味・嗜好です
    /// </summary>
    public List<string> Preferences { get; set; } = [];

    /// <summary>
    /// 提案する対象者の人となりを表現する言葉です
    /// </summary>
    public string Personality
    {
        get
        {
            var gender = Gender switch
            {
                Data.Gender.Male => "男性",
                Data.Gender.Female => "女性",
                _ => "性別は不明",
            };
            var age = Age is null ? "年齢層は不明" : Age.Name;

            var preferences = string.Join("で、", Preferences);

            return $"{preferences}。{age}の{gender}です。";
        }
    }
}
