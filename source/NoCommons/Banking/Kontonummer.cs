﻿using System.Text;
using NoCommons.Common;

namespace NoCommons.Banking;

public record Kontonummer(string value) : StringNumber(value)
{
    public string GetRegisternummer()
    {
        return GetValue().Substring(0, 4);
    }

    public string GetAccountType()
    {
        return GetValue().Substring(4, 2);
    }

    public string GetKonto()
    {
        return GetValue().Substring(4, 6);
    }

    public string GetGroupedValue()
    {
        StringBuilder sb = new();
        sb.Append(GetRegisternummer()).Append(Constants.DOT);
        sb.Append(GetAccountType()).Append(Constants.DOT);
        sb.Append(GetPartAfterAccountType());
        return sb.ToString();
    }

    private string GetPartAfterAccountType()
    {
        return GetValue().Substring(6);
    }

    public override string ToString()
    {
        return GetValue();
    }
}
