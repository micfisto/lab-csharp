using Lab_5.Interfaces;

namespace Lab_5.Models;

public class PassengerPlane : Plane, IPassenger
{
    public int NumberOfEngines { get;  set; }
    public string[] ComfortClass { get; set; } 

    public static Dictionary<int, string> AllowedComfortClasses = new()
    {
        { 1, "эконом" },
        { 2, "комфорт" },
        { 3, "бизнес" }
    };

    public PassengerPlane
    (
        string model, int numberOfCrew,  
        double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, int numberOfEngines, string[] comfortClass) : base(
        model, typeOfPlane: "пассажирский", purpose: "перевозка пассажиров и их багажа", numberOfCrew: numberOfCrew,
        flightRange: flightRange, fuelConsumption: fuelConsumption, passengerCapacity: passengerCapacity,
        payload: payload)
    {
        NumberOfEngines = numberOfEngines;
        ComfortClass = comfortClass;
    }

    public PassengerPlane() : base() { }
    
    public override string GetInfo()
    {
        return
            $"Модель: {Model}\n" +
            $"Тип: {TypeOfPlane}\n" +
            $"Назначение: {Purpose}\n" +
            $"Экипаж: {NumberOfCrew} чел.\n" +
            $"Дальность полёта: {FlightRange} км\n" +
            $"Потребление горючего: {FuelConsumption} л/ч\n" +
            $"Пассажировместимость: {PassengerCapacity} чел.\n" +
            $"Грузоподъёмность: {Payload} кг\n" +
            $"Количество двигателей: {NumberOfEngines}\n" +
            $"Категория: {PlaneCategory()}\n" +
            $"Классы обслуживания: {string.Join(", ", ComfortClass)}\n";
    }

    public override void EditInfo()
    {
        Console.WriteLine($"===== МЕНЮ РЕДАКТИРОВАНИЯ ИНФОРМАЦИИ О САМОЛЁТЕ {Model.ToUpper()}");

        while (true)
        {
            Console.WriteLine("1. Модель.");
            Console.WriteLine("2. Экипаж.");
            Console.WriteLine("3. Дальность полёта.");
            Console.WriteLine("4. Потребление горючего.");
            Console.WriteLine("5. Пассажировместимость.");
            Console.WriteLine("6. Грузоподъёмность.");
            Console.WriteLine("7. Количество двигателей.");
            Console.WriteLine("8. Классы обслуживания.");
            Console.WriteLine("0. Выход из меню редактирования.");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string model = PlaneHandler.ReadString("Введите модель самолёта", "\"модель\"");
                    Model = model;
                    break;
                case "2":
                    int numberOfCrew = PlaneHandler.ReadInt("Введите количество членов экипажа",
                        "данные о количестве членов экипажа", 2, 18);
                    NumberOfCrew = numberOfCrew;
                    break;
                case "3":
                    double flightRange = PlaneHandler.ReadDouble("Введите дальность полёта в км",
                        "данные о дальности полёта.", 100, 15000);
                    FlightRange = flightRange;
                    break;
                case "4":
                    double fuelConsumption = PlaneHandler.ReadDouble("Введите расход топлива л/ч",
                        "данные о расходе топлива", 200, 8000);
                    FuelConsumption = fuelConsumption;
                    break;
                case "5":
                    int passengerCapacity = PlaneHandler.ReadInt("Введите пассажировместимость",
                        "данные о количестве посадочных мест", 2, 850);
                    PassengerCapacity = passengerCapacity;
                    break;
                case "6":
                    double payload = PlaneHandler.ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности",
                        100, 200000);
                    Payload = payload;
                    break;
                case "7":
                    int numberOfEngines = PlaneHandler.ReadInt("Введите количество двигателей",
                        "данные о количестве двигателей", 1, 4);
                    NumberOfEngines = numberOfEngines;
                    break;
                case "8":
                    while (true)
                    {
                        Console.WriteLine("\n1. Добавить класс обслуживания.");
                        Console.WriteLine("2. Удалить класс обслуживания.");
                        Console.WriteLine($"0. Вернуться в общее меню редактирования информации самолёта {Model}.");

                        var ch = Console.ReadLine();

                        switch (ch)
                        {
                            case "1":
                                AddComfortClasses();
                                Console.WriteLine(
                                    $"\nТекущий список доступных классов обслуживания в данном самолёте: {string.Join(", ", ComfortClass)}");
                                break;
                            case "2":
                                RemoveComfortClass();
                                Console.WriteLine(
                                    $"\nТекущий список доступных классов обслуживания в данном самолёте: {string.Join(", ", ComfortClass)}");
                                break;
                            case "0":
                                return;
                            default:
                                Console.WriteLine("Неверный ввод.");
                                break;
                        }
                    }
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.WriteLine("Информация обновлена.");
        }
    }


    public string PlaneCategory()
    {
        if (NumberOfEngines <= 2 && FlightRange < 3000)
            return "региональный";
        if (NumberOfEngines >= 2 && FlightRange > 3000)
            return "магистральный лайнер";
        return "промежуточный тип";
    }

    public void AddComfortClasses()
    {
        var existingClasses = ComfortClass;
        var availableClasses = AllowedComfortClasses
            .Where(keyValuePair => !existingClasses.Contains(keyValuePair.Value))
            .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);

        if (availableClasses.Count == 0)
        {
            Console.WriteLine("Все доступные классы уже добавлены.");
            return;
        }

        Console.WriteLine("Доступные для добавления классы обслуживания:");
        foreach (var keyValuePair in availableClasses)
        {
            Console.WriteLine($"{keyValuePair.Key}.{keyValuePair.Value}");
        }

        Console.WriteLine("Введите номера классов для добавления через запятую:");

        string input = Console.ReadLine() ?? "";

        var chosenKeys = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim())
            .Where(str => int.TryParse(str, out _)).Select(int.Parse).Distinct().ToList();

        var invalidKeys = chosenKeys.Where(key => !availableClasses.ContainsKey(key)).ToList();
        if (invalidKeys.Count > 0)
            Console.WriteLine($"Следующие номера не были допустимыми для добавления: {string.Join(", ", invalidKeys)}");

        var newComfortClasses = chosenKeys.Where(key => availableClasses.ContainsKey(key))
            .Select(key => availableClasses[key]);

        ComfortClass = existingClasses.Concat(newComfortClasses).ToArray();

        Console.WriteLine("Добавлено.");
    }

    public void RemoveComfortClass()
    {
        if (ComfortClass.Length == 0)
        {
            Console.WriteLine("Список классов обслуживания пуст.");
            return;
        }

        Console.WriteLine("\nСписок классов обслуживания:");
        for (int i = 0; i < ComfortClass.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {ComfortClass[i]}");
        }

        int choice = PlaneHandler.ReadInt("Введите число", "число", 1, ComfortClass.Length);
        ComfortClass = ComfortClass.Where((_, index) => index != choice - 1).ToArray();
        Console.WriteLine("Удалено.");
    }
}