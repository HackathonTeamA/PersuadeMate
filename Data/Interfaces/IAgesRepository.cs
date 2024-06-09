namespace PersuadeMate.Data.Interfaces;

/// <summary>
/// システムで利用可能な年代の情報を提供するインターフェイスです
/// </summary>
public interface IAgesRepository
{
    /// <summary>
    /// 利用可能なすべての年代を取得します
    /// </summary>
    /// <returns></returns>
    IEnumerable<Age> GetAllAges();

    /// <summary>
    /// 年代のキー文字列に対応する年代を取得します。
    /// </summary>
    /// <param name="key">年代のキー文字列です</param>
    /// <returns></returns>
    Age? GetAgeByKey(string key);
}
