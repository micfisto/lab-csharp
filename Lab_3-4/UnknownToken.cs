using System.Xml.Serialization;

namespace Lab_3;

[Serializable]
public class UnknownToken : Token
{
    [XmlText]
    public string Text
    {
        get => Value;
        set => Value = value;
    }
    
    public UnknownToken()
    {
    }

    public UnknownToken(string? value)
    {
        Value = value?.Trim() ?? "";
    }
}