using System.Xml.Serialization;

namespace Lab_3;

[XmlInclude(typeof(Word))]
[XmlInclude(typeof(Punctuation))]
[XmlInclude(typeof(UnknownToken))]
[Serializable]
public abstract class Token
{
    [XmlIgnore] public string Value { get; set; } = "";

    public Token()
    {
    }

    public override string ToString() => Value;
}