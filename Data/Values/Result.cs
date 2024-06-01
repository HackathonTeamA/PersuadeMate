namespace PersuadeMate.Data.Values;

/// <summary>
/// 正常データ、または異常が発生した場合はそのエラーメッセージを保持するデータクラスです
/// </summary>
/// <typeparam name="TOk">正常データの型です</typeparam>
/// <typeparam name="TErr">エラー情報を表す型です</typeparam>
public readonly struct Result<TOk, TErr>
    : IEquatable<Result<TOk, TErr>>, IComparable<Result<TOk, TErr>>
    where TOk : notnull
{
    private readonly bool _isOk;
    private readonly TOk _value;
    private readonly TErr _error;

    /// <summary>
    /// 正常値からなる Result 型を生成するコンストラクタです
    /// </summary>
    /// <param name="value">正常値です</param>
    public Result(TOk value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _isOk = true;
        _value = value;
        _error = default!;
    }

    /// <summary>
    /// エラー情報からなる Result 型を生成するコンストラクタです
    /// </summary>
    /// <param name="error">エラー情報です</param>
    public Result(TErr error)
    {
        ArgumentNullException.ThrowIfNull(error);
        _isOk = false;
        _value = default!;
        _error = error;
    }

    /// <summary>
    /// 正常値かどうかを判断して、正常値であればその内容を取り出します
    /// </summary>
    /// <param name="value">[Out]正常値を受け取ります</param>
    /// <returns>正常値である場合に true を返却します</returns>
    public bool IsOk(out TOk value)
    {
        value = _value;
        return _isOk;
    }

    /// <summary>
    /// 異常値かどうかを判断して、異常値であればエラー情報を取り出します
    /// </summary>
    /// <param name="error">[Out]エラー情報を受け取ります</param>
    /// <returns>異常値である場合に true を返却します</returns>
    public bool IsError(out TErr error)
    {
        error = _error;
        return !_isOk;
    }

    /// <summary>
    /// 等値生を比較します
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Result<TOk, TErr> other)
    {
        return (_isOk, other._isOk) switch
        {
            (true, true) => EqualityComparer<TOk>.Default.Equals(_value, other._value),
            (false, false) => EqualityComparer<TErr>.Default.Equals(_error, other._error),
            _ => false,
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Result<TOk, TErr> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        if (_isOk)
        {
            return _value.GetHashCode();
        }

        var errorHashCode = _error?.GetHashCode();
        return errorHashCode ?? 0;
    }

    /// <summary>
    /// 大小を比較します。正常値は異常値よりも大であるとします
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Result<TOk, TErr> other)
    {
        return (_isOk, other._isOk) switch
        {
            (true, true) => Comparer<TOk>.Default.Compare(_value, other._value),
            (false, false) => Comparer<TErr>.Default.Compare(_error, other._error),
            (true, false) => -1,
            (false, true) => 1,
        };
    }
}
