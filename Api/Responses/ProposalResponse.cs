using PersuadeMate.Data;

namespace PersuadeMate.Api.Responses;

/// <summary>
/// APIから返却されるひとこと提言を保持するレスポンスを表現するクラスです
/// </summary>
public class ProposalResponse
{
    /// <summary>
    /// ひとこと提言です。候補をいくつか配列に格納されます
    /// </summary>
    public List<Candidate> Proposals { get; set; } = [];
}
