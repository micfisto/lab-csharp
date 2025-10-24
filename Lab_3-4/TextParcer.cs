using System.Text;
using System.Text.RegularExpressions;

namespace Lab_3;

public static class TextParcer
{
    private static readonly HashSet<char> PunctuationMarks = new HashSet<char>
    {
        '–', '«', '»', '"', ',', '.', '!', '?', '(', ')', '[', ']', '{', '}'
    };

    private static readonly HashSet<char> SentenceEndings = new HashSet<char>() { '.', '!', '?' };

    private static bool IsValidWord(string value)
    {
        return Regex.IsMatch(value, @"^['’]?([A-Za-zА-Яа-яЁё])+(?:[-'’][A-Za-zА-Яа-яЁё]+)*$");
    }

    public static Text ParceFile(string filePath)
    {
        Text text = new Text();
        Sentence currentSentence = new Sentence();
        StringBuilder currentWord = new StringBuilder();

        string content = File.ReadAllText(filePath, Encoding.UTF8);

        for (int i = 0; i < content.Length; i++)
        {
            char symbol = content[i];

            if (char.IsWhiteSpace(symbol)&&symbol!='\n')
            {
                AddWordIfExist(currentSentence, currentWord);
                continue;
            }

            if (symbol == '\n')
            {
                AddWordIfExist(currentSentence, currentWord);

                if (currentSentence.Tokens.Count > 0)
                {
                    text.AddSentence(currentSentence);
                    currentSentence = new Sentence();
                }

                continue;
            }

            if (symbol == '–')
            {
                char? prevChar = i > 0 ? content[i - 1] : null;
                char? nextChar = i + 1 < content.Length ? content[i + 1] : null;

                if (prevChar == ' ' && nextChar == ' ')
                {
                    currentSentence.AddPunctuation(new Punctuation(" – "));
                    i++;
                    continue;
                }
            }

            if (PunctuationMarks.Contains(symbol))
            {
                AddWordIfExist(currentSentence, currentWord);

                StringBuilder punctBuilder = new StringBuilder();
                while (i < content.Length && PunctuationMarks.Contains(content[i]))
                {
                    punctBuilder.Append(content[i]);
                    i++;
                }

                i--;

                string punctStr = punctBuilder.ToString();
                currentSentence.AddPunctuation(new Punctuation(punctStr));

                if (punctStr.Any(ch => SentenceEndings.Contains(ch)) && currentSentence.Tokens.Count > 0)
                {
                    text.AddSentence(currentSentence);
                    currentSentence = new Sentence();
                }

                continue;
            }

            currentWord.Append(symbol);
        }

        AddWordIfExist(currentSentence, currentWord);

        if (currentSentence.Tokens.Count > 0)
            text.AddSentence(currentSentence);
        return text;
    }

    private static void AddWordIfExist(Sentence sentence, StringBuilder currentWord)
    {
        if (currentWord.Length == 0)
            return;

        string value = currentWord.ToString();
        currentWord.Clear();

        if (IsValidWord(value))
            sentence.AddWord(new Word(value));
        else
            sentence.AddUnknownToken(new UnknownToken(value));
    }
}