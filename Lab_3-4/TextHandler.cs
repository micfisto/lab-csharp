using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lab_3;

public class TextHandler
{
    public List<Sentence> SortByWordCount(Text text)
    {
        return text.Sentences.OrderBy(sentence => sentence.WordCount()).ToList();
    }

    public List<Sentence> SortByLength(Text text)
    {
        return text.Sentences.OrderBy(sentence => sentence.Tokens
                .Where(token => token is Word or UnknownToken)
                .Sum(token => token.Value.Length))
            .ToList();
    }

    public List<string> FindWordInQuestions(Text text, int length)
    {
        var wordToSentences = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        var wordOriginals = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var sentence in text.Sentences)
        {
            bool isQuestion = sentence.Tokens.Any(token => token is Punctuation && token.Value.Contains('?'));
            if (!isQuestion)
                continue;

            foreach (var token in sentence.Tokens)
            {
                if (token is Word or UnknownToken && token.Value.Length == length)
                {
                    string wordLower = token.Value.ToLower();
                    string original = token.Value;

                    if (!wordToSentences.ContainsKey(wordLower))
                    {
                        wordToSentences[wordLower] = new List<string>();
                        wordOriginals[wordLower] = new HashSet<string>();
                    }

                    string sentenceString = sentence.ToString();
                    if (!wordToSentences[wordLower].Contains(sentenceString))
                        wordToSentences[wordLower].Add(sentenceString);

                    wordOriginals[wordLower].Add(original);
                }
            }
        }

        var result = new List<string>();
        foreach (var keyValuePair in wordToSentences)
        {
            string wordKey = keyValuePair.Key;
            var sentences = keyValuePair.Value;
            var variants = wordOriginals[wordKey];

            string displayWord = string.Join("/", variants);

            if (sentences.Count == 0)
            {
                result.Add($"Не найдено слов длинной {length} в вопросительных предложений.");
            }
            else if (sentences.Count == 1)
            {
                result.Add($"Найдено слово \"{displayWord}\" в предложении \"{sentences[0]}\".");
            }
            else
            {
                string sentenceList = string.Join("; ", sentences.Select(sentence => $"\"{sentence}\""));
                result.Add($"Найдено слово \"{displayWord}\" в предложениях: {sentenceList}.");
            }
        }

        return result;
    }

    public int RemoveWords(Text text, int length)
    {
        string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";
        int removeCount = 0;

        foreach (var sentence in text.Sentences)
        {
            int before = sentence.Tokens.Count;
            
            sentence.Tokens.RemoveAll(token =>
                (token is Word or UnknownToken) &&
                token.Value.Length == length &&
                consonants.Contains(char.ToLower(token.Value[0])));
            
            removeCount += before - sentence.Tokens.Count;
        }

        return removeCount;
    }

    public int ReplaceWithSubstring(Text text, int length, string replacement)
    {
        int replacedCount = 0;

        foreach (var sentence in text.Sentences)
        {
            foreach (var token in sentence.Tokens)
            {
                if (token is Word or UnknownToken && token.Value.Length == length)
                {
                    token.Value = replacement;
                    replacedCount++;
                }
            }
        }

        return replacedCount;
    }

    public void RemoveStopWords(Text text, HashSet<string> stopWords)
    {
        foreach (var sentence in text.Sentences)
        {
            sentence.Tokens.RemoveAll(token =>
                token is Word or UnknownToken && stopWords.Contains(token.Value.ToLower()));
        }
    }

    public void ExportToXml(Text text, string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Text));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                NewLineOnAttributes = false
            };

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = XmlWriter.Create(filePath, settings))
            {
                serializer.Serialize(writer, text, namespaces);
            }

            Console.WriteLine($"Текст успешно экспортирован в XML: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при экспорте XML: {ex.Message}");
        }
    }
}