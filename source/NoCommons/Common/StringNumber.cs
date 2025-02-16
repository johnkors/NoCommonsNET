namespace NoCommons.Common;

public abstract record StringNumber(string value)
{
    public string GetValue()
    {
        return value;
    }

    public int GetAt(int i)
    {
        return GetValue()[i] - '0';
    }

    public override string ToString()
    {
        return GetValue();
    }

    public int GetLength()
    {
        return GetValue().Length;
    }

    public int GetChecksumDigit()
    {
        return GetAt(GetLength() - 1);
    }
}
