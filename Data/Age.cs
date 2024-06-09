namespace PersuadeMate.Data;

/// <summary>
/// 年代を表現するレコードタイプです
/// </summary>
/// <param name="Key">年代を識別するキーです</param>
/// <param name="Name">年代の日本語表現です</param>
/// <param name="Order">年代を若い順に並べる時のソートキーです</param>
public record Age(string Key, string Name, int Order)
{
    /// <summary>
    /// 年代の等値性をキーの等値性で判断します
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Age? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Key == other.Key;
    }

    /// <summary>
    /// 年代のハッシュコードをキーから計算します
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Key.GetHashCode();
}
