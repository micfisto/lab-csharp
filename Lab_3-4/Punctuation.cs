using System.Xml.Serialization;

namespace Lab_3;

[Serializable]
public class Punctuation : Token
{
    
    [XmlText]
    public string Text
    {
        get => Value;
        set => Value = value;
    }
    
    public Punctuation()
    {
    }

    public Punctuation(char symbol)
    {
        Value = symbol.ToString();
    }

    public Punctuation(string? value)
    {
        Value = value ?? "";
    }
}