using System.Xml.Serialization;

namespace Lab_3;

[Serializable]
public class Word : Token
{
   
    [XmlText]
    public string Text
    {
        get => Value;
        set => Value = value;
    }
    
    public Word()
    {
    }

    public Word(string? value)
    {
        Value = value?.Trim() ?? "";
    }
}