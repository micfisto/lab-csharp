using System.Text;
using System.Xml.Serialization;

namespace Lab_3;

[Serializable]
public class Sentence
{
    [XmlElement("word", typeof (Word))]
    [XmlElement("unknown", typeof (UnknownToken))]
    [XmlElement("punctuation", typeof (Punctuation))]

    public List<Token> Tokens { get; set; } = new();

    public Sentence()
    {
    }

    [XmlIgnore]
    public IEnumerable<Word> Words => Tokens.OfType<Word>();

    public void AddWord(Word? word)
    {
        if (word != null)
            Tokens.Add(word);
    }

    public void AddPunctuation(Punctuation? punctuation)
    {
        if (punctuation != null)
            Tokens.Add(punctuation);
    }

    public void AddUnknownToken(UnknownToken? unknownToken)
    {
        if (unknownToken != null)
            Tokens.Add(unknownToken);
    }

    public int WordCount()
    {
        return Tokens.Count(tokens => tokens is Word || tokens is UnknownToken);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        Token previous = null;

        foreach (var token in Tokens)
        {
            switch (token)
            {
                case Word or UnknownToken:
                    if (previous is Word or UnknownToken)
                        stringBuilder.Append(' ');
                    stringBuilder.Append(token.Value);
                    break;
                case Punctuation punctuation:
                    stringBuilder.Append(punctuation.Value);
                    if (!string.IsNullOrEmpty(punctuation.Value) && "–.,!?;:".Contains(punctuation.Value[0]))
                        stringBuilder.Append(' ');
                    break;
            }

            previous = token;
        }

        return stringBuilder.ToString().Trim();
    }
}