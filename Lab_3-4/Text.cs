using System.Xml.Serialization;
using System.Text;

namespace Lab_3;

[Serializable]
[XmlRoot("text")]
public class Text
{
    [XmlElement("sentence")] public List<Sentence> Sentences { get; set; } = new();

    public Text()
    {
    }

    public void AddSentence(Sentence sentence)
    {
        Sentences.Add(sentence);
    }

    public List<string> BuildConcordance()
    {
        Dictionary<string, List<int>> concordance = new(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < Sentences.Count; i++)
        {
            var sentence = Sentences[i];
            int sentenceIndex = i + 1;

            foreach (var token in sentence.Tokens)
            {
                if (token is not Word word)
                    continue;

                string value = word.Value.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(value))
                    continue;

                if (!concordance.ContainsKey(value))
                    concordance[value] = new List<int>();

                concordance[value].Add(sentenceIndex);
            }
        }

        if (concordance.Count == 0)
            return new List<string> { "Текст пуст или не содержит слов." };

        int maxWordLength = concordance.Keys.Max(word => word.Length);

        List<string> result = new();

        foreach (var keyValuePair in concordance.OrderBy(key => key.Key))
        {
            string word = keyValuePair.Key;
            int totalCount = keyValuePair.Value.Count;
            var distinctSentences = keyValuePair.Value.Distinct().OrderBy(num => num);
            string lines = string.Join(" ", distinctSentences);

            int dotCount = Math.Max(1, (maxWordLength + 5) - word.Length);
            string dots = new string('.', dotCount);

            result.Add($"{word}{dots}{totalCount}: {lines}");
        }

        return result;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < Sentences.Count; i++)
        {
            string sentenceStr = Sentences[i].ToString();

            if (builder.Length > 0)
            {
                if (!builder.ToString().EndsWith("\n") && !sentenceStr.StartsWith("\n"))
                    builder.Append(' ');
            }

            builder.Append(sentenceStr);
        }

        return builder.ToString();
    }
}