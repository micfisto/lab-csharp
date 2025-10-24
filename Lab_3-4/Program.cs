using System.Text;

namespace Lab_3;

static class Program
{
    static void Main()
    {
        TextHandler handler = new TextHandler();

        while (true)
        {
            Console.WriteLine("Требуется выбрать текст для его дальнейшей обработки.");
            Console.WriteLine("1. Выбрать язык(rus/eng).");
            Console.WriteLine("2. Ввести название файла.");
            Console.WriteLine("-1. Завершить работу программы.");

            int choiceFile = InputChoice();

            Text text;
            string inputFile;

            switch (choiceFile)
            {
                case 1:
                    Console.WriteLine("Выберите язык: ");
                    Console.WriteLine("1.Русский.");
                    Console.WriteLine("2.Английский.");

                    int choiceLang = InputChoice();

                    switch (choiceLang)
                    {
                        case 1:
                            inputFile = "input_rus.txt";
                            break;
                        case 2:
                            inputFile = "input_eng.txt";
                            break;
                        default:
                            Console.WriteLine("Неверный выбор пункта.");
                            continue;
                    }

                    if (!CheckFile(inputFile))
                        continue;

                    Console.WriteLine($"Файл был выбран по умолчанию. Название файла: {inputFile}.");
                    text = TextParcer.ParceFile(inputFile);

                    if (text.Sentences.Count == 0)
                    {
                        Console.WriteLine("Ошибка: текст не загружен. Повторите выбор файла.");
                        continue;
                    }

                    Run(inputFile, text, handler);

                    break;

                case 2:
                    Console.WriteLine("Введите название файла без расширения: ");
                    string? input = Console.ReadLine();
                    inputFile = $"{input}.txt";
                    if (!CheckFile(inputFile))
                        continue;


                    text = TextParcer.ParceFile(inputFile);

                    if (text.Sentences.Count == 0)
                    {
                        Console.WriteLine("Ошибка: текст не загружен. Повторите выбор файла.");
                        continue;
                    }

                    Run(inputFile, text, handler);

                    break;
                case -1:
                    Console.WriteLine("Выход из программы...");
                    Environment.Exit(0);
                    break;
            }
        }
    }

    static void Run(string inputFile, Text text, TextHandler handler)
    {
        while (true)
        {
            PrintMenu();
            var choice = InputChoice();

            switch (choice)
            {
                case 1:
                    var sortedByWords = handler.SortByWordCount(text);
                    SaveText($"SORTED BY WORD COUNT {Path.GetFileNameWithoutExtension(inputFile)}",
                        sortedByWords);
                    Console.WriteLine("Предложения отсортированы в порядке возрастания количества слов.");
                    break;
                case 2:
                    var sortedByLength = handler.SortByLength(text);
                    SaveText($"SORTED BY SENTENCE LENGTH {Path.GetFileNameWithoutExtension(inputFile)}",
                        sortedByLength);
                    Console.WriteLine("Предложения отсортированы в порядке возрастания длин предложений.");
                    break;
                case 3:
                    Console.WriteLine("Введите длину слова для поиска в вопросах.");
                    int searchLength = InputChoice();
                    var foundWords = handler.FindWordInQuestions(text, searchLength);

                    if (foundWords.Count == 0)
                    {
                        Console.WriteLine("Слова указанной длины отсутствуют в вопросительных предложениях.");
                    }
                    else
                    {
                        SaveTextStrings(
                            $"FOUND WORD IN QUESTION SENTENCE {Path.GetFileNameWithoutExtension(inputFile)}",
                            foundWords);
                        Console.WriteLine(
                            $"Найдено {foundWords.Count} слов длинной {searchLength} в вопросительных предложениях.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Введите длину слова для удаления.");
                    int deletedLength = InputChoice();

                    int removedWords = handler.RemoveWords(text, deletedLength);

                    if (removedWords == 0)
                        Console.WriteLine($"Слова длинной {deletedLength} отсутствуют в тексте.");
                    else
                    {
                        SaveText($"REMOVE WORD {Path.GetFileNameWithoutExtension(inputFile)}", text.Sentences);
                        Console.WriteLine($"Удалено слов: {removedWords} длиной {deletedLength}.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Введите длину слова для замены.");
                    int replacementLength = InputChoice();

                    Console.WriteLine("Введите подстроку для замены: ");
                    string replacement = Console.ReadLine() ?? "";

                    int replaced = handler.ReplaceWithSubstring(text, replacementLength, replacement);

                    if (replaced == 0)
                        Console.WriteLine(
                            $"Слова длиной {replacementLength} отсутствуют в тексте. Замены не произведены.");
                    else
                        SaveText($"REPLACED BY A SUBSTRING {Path.GetFileNameWithoutExtension(inputFile)}",
                            text.Sentences);
                    
                    Console.WriteLine($"Заменено слов: {replaced}, длиной {replacementLength} на \"{replacement}\".");
                    break;
                case 6:
                    string stopWordsFile = GetStopWord();
                    
                    if (string.IsNullOrEmpty(stopWordsFile))
                        break;

                    HashSet<string> stopWords = new HashSet<string>();
                    
                    foreach (var line in File.ReadAllLines(stopWordsFile))
                        if (!string.IsNullOrWhiteSpace(line))
                            stopWords.Add(line.Trim().ToLower());

                    handler.RemoveStopWords(text, stopWords);
                    SaveText($"REMOVED STOP-WORD {Path.GetFileNameWithoutExtension(inputFile)}", text.Sentences);
                    Console.WriteLine("Стоп-слова удалены из текста.");
                    break;
                case 7:
                    string xmlFileName = $"{Path.GetFileNameWithoutExtension(inputFile)}.xml";
                    handler.ExportToXml(text, xmlFileName);
                    break;
                case 8:
                    var concordanceLines = text.BuildConcordance();
                    SaveTextStrings($"CONCORDANCE {Path.GetFileNameWithoutExtension(inputFile)}", concordanceLines);
                    Console.WriteLine("Конкорданс построен и сохранён в файл.");
                    break;
                case 9:
                    SaveText($"PARCED {Path.GetFileNameWithoutExtension(inputFile)}", text.Sentences);
                    break;
                case 10:
                    return;
                case -1:
                    Console.WriteLine("Выход из программы...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Выберите из предложенных пунктов.");
                    break;
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("===== МЕНЮ =====");
        Console.WriteLine("1. Сортировать по количеству слов.");
        Console.WriteLine("2. Сортировать по размеру слов.");
        Console.WriteLine("3. Найти слово в вопросе.");
        Console.WriteLine("4. Удалить слова.");
        Console.WriteLine("5. Заменить слово на подстроку.");
        Console.WriteLine("6. Удалить стоп-слова.");
        Console.WriteLine("7. Экспорт в XML-файл.");
        Console.WriteLine("8. Сортировка текста по наиболее встречающимся словам.");
        Console.WriteLine("9. ОТЛАДКА: проверка работы парсера.");
        Console.WriteLine("10. Выход в меню выбора текста.");
        Console.WriteLine("-1. Завершить работу программы.");
    }

    static int InputChoice()
    {
        Console.WriteLine("Ваш выбор: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
            Console.WriteLine("Неверный ввод! Введите число: ");
        return choice;
    }

    static string GetStopWord()
    {
        Console.WriteLine("Выберите файл со стоп-словами для удаления.");
        Console.WriteLine("1. stopwords_ru.");
        Console.WriteLine("2. stopwords_en.");

        string stopWordsFile = "";
        int choice = InputChoice();

        switch (choice)
        {
            case 1:
                stopWordsFile = "stopwords_ru.txt";
                break;
            case 2:
                stopWordsFile = "stopwords_en.txt";
                break;
            default:
                Console.WriteLine("Неверный выбор. Выберите из предложенных пунктов.");
                break;
        }

        if (!CheckFile(stopWordsFile))
        {
            Console.WriteLine("Файл со стоп-словами отсутствует.");
            Console.WriteLine("Операция отменена.");
            throw new FileNotFoundException($"Файл {stopWordsFile} не найден или пуст.");
        }

        return stopWordsFile;
    }

    static void SaveText(string fileName, IEnumerable<Sentence> sentences)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(fileName + ".txt", false, Encoding.UTF8);
            foreach (var sentence in sentences)
                writer.WriteLine(sentence.ToString());
            Console.WriteLine($"Текст успешно записан в {fileName}.txt.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка записи файла {e.Message}.");
            throw;
        }
    }

    static void SaveTextStrings(string fileName, IEnumerable<string> lines)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(fileName + ".txt", false, Encoding.UTF8);
            foreach (var line in lines)
                writer.WriteLine(line);
            Console.WriteLine($"Текст успешно записан в {fileName}.txt.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка записи файла {e.Message}.");
            throw;
        }
    }

    static bool CheckFile(string? checkingFile)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(checkingFile))
                throw new ArgumentException("имя входного файла пустое или состоит из пробелов");

            if (!File.Exists(checkingFile))
                throw new FileNotFoundException("файл не найден");

            if (new FileInfo(checkingFile).Length == 0)
                throw new InvalidDataException("файл пуст");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка в обработке файла {checkingFile}: {e.Message}.");
            return false;
        }

        return true;
    }
}