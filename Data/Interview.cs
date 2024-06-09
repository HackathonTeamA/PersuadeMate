namespace PersuadeMate.Data;

/// <summary>
/// クライアントから送られてくる質問とその回答です
/// </summary>
/// <param name="Question"></param>
/// <param name="Answer"></param>
public record Interview(string Question, string? Answer);
