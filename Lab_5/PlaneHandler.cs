using Lab_5.Models;

namespace Lab_5;

public class PlaneHandler
{
    public static PassengerPlane CreatePassengerPlaneFromConsole()
    {
        Console.WriteLine("===== СОЗДАНИЕ ПАССАЖИРСКОГО САМОЛЁТА =====");

        string model = ReadString("Введите модель самолёта", "\"модель\"");
        int numberOfCrew = ReadInt("Введите количество членов экипажа",
            "данные о количестве членов экипажа", 2, 18);
        double flightRange = ReadDouble("Введите дальность полёта в км",
            "данные о дальности полёта.", 100, 15000);
        double fuelConsumption = ReadDouble("Введите расход топлива л/ч",
            "данные о расходе топлива", 200, 8000);
        int passengerCapacity = ReadInt("Введите пассажировместимость",
            "данные о количестве посадочных мест", 2, 850);
        double payload = ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности", 100, 200000);
        int numberOfEngines = ReadInt("Введите количество двигателей", "данные о количестве двигателей", 1, 4);
        string[] comfortClass = Array.Empty<string>();

        PassengerPlane plane = PlaneFactory.CreatePassengerPlane(
            model,
            numberOfCrew,
            flightRange,
            fuelConsumption,
            passengerCapacity,
            payload,
            numberOfEngines,
            comfortClass);

        plane.AddComfortClasses();
        
        Console.WriteLine($"Пассажирский самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static CargoPlane CreateCargoPlaneFromConsole()
    {
        Console.WriteLine("===== СОЗДАНИЕ ГРУЗОВОГО САМОЛЁТА =====");

        string model = ReadString("Введите модель самолёта", "\"модель\"");
        int numberOfCrew = ReadInt("Введите количество членов экипажа",
            "данные о количестве членов экипажа", 2, 10);
        double flightRange = ReadDouble("Введите дальность полёта в км",
            "данные о дальности полёта.", 300, 12000);
        double fuelConsumption = ReadDouble("Введите расход топлива л/ч",
            "данные о расходе топлива", 500, 10000);
        double payload = ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности", 1000, 150000);
        bool presenceOfRamp = ReadBool("Имеет ли данная модель рампу (да/нет)?");
        double weightCurrentCargo = ReadDouble("Введите вес груза, который сейчас имеется на данном самолёте",
            "данные о текущем весе груза на самолёте", 0, payload);

        CargoPlane plane = PlaneFactory.CreateCargoPlane(model, numberOfCrew, flightRange, fuelConsumption, payload,
            presenceOfRamp,
            weightCurrentCargo);

        Console.WriteLine($"Грузовой самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static AmbulancePlane CreateAmbulancePlaneFromConsole()
    {
        Console.WriteLine("===== СОЗДАНИЕ САНИТАРНОГО САМОЛЁТА =====");

        string model = ReadString("Введите модель самолёта", "\"модель\"");
        int numberOfCrew = ReadInt("Введите количество членов экипажа",
            "данные о количестве членов экипажа", 2, 5);
        string typeTakeOfAndLanding = IsTypeTakeOfAndLanding();
        double flightRange = ReadDouble("Введите дальность полёта в км",
            "данные о дальности полёта.", 500, 5000);
        double fuelConsumption = ReadDouble("Введите расход топлива л/ч",
            "данные о расходе топлива", 300, 2000);
        int passengerCapacity = ReadInt("Введите пассажировместимость",
            "данные о количестве посадочных мест", 1, 12);
        double payload = ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности", 500, 5000);

        AmbulancePlane plane = PlaneFactory.CreateAmbulancePlane(
            model,
            numberOfCrew,
            typeTakeOfAndLanding,
            flightRange,
            fuelConsumption,
            passengerCapacity,
            payload
        );

        Console.WriteLine($"Санитарный самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static AgriculturalPlane CreateAgriculturalPlaneFromConsole()
    {
        Console.WriteLine("===== СОЗДАНИЕ СЕЛЬСКОХОЗЯЙСТВЕННОГО САМОЛЁТА =====");

        string model = ReadString("Введите модель самолёта", "\"модель\"");
        int numberOfCrew = ReadInt("Введите количество членов экипажа",
            "данные о количестве членов экипажа", 2, 5);
        string typeTakeOfAndLanding = IsTypeTakeOfAndLanding();
        double flightRange = ReadDouble("Введите дальность полёта в км",
            "данные о дальности полёта.", 500, 5000);
        double fuelConsumption = ReadDouble("Введите расход топлива л/ч",
            "данные о расходе топлива", 300, 2000);
        double payload = ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности", 500, 5000);


        AgriculturalPlane plane = PlaneFactory.CreateAgriculturalPlane(
            model,
            numberOfCrew,
            typeTakeOfAndLanding,
            flightRange,
            fuelConsumption,
            payload
        );

        Console.WriteLine($"Сельскохозяйственный самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static string IsTypeTakeOfAndLanding()
    {
        while (true)
        {
            Console.WriteLine("Выберите тип взлёта и посадки:");
            foreach (var type in SpecialPlaneBase.TakeOffTypes)
                Console.WriteLine($"{type.Key}. {type.Value}");

            if (int.TryParse(Console.ReadLine(), out int choice)&&SpecialPlaneBase.TakeOffTypes.TryGetValue(choice, out var typeTakeOfAndLanding))
            {
                return typeTakeOfAndLanding;
            }
            
            Console.WriteLine("Ошибка: введите целое число от 1 до 4.");
        }
    }

    public static string ReadString(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine($"{prompt}:");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine($"Ошибка: поле {errorMessage} не может быть пустым.");
            }
        }

        public static string[] ReadStringArray(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine($"{prompt}:");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(str => str.Trim().ToLower()).Distinct().ToArray();
                Console.WriteLine($"Ошибка: поле {errorMessage} не может быть пустым.");
            }
        }

        public static int ReadInt(string prompt, string errorMessage, int min, int max)
        {
            while (true)
            {
                Console.WriteLine($"{prompt} в диапазоне от {min} до {max}:");
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine(
                    $"Ошибка: неверный ввод. Введите {errorMessage} в диапазоне от {min} до {max}.");
            }
        }

        public static double ReadDouble(string prompt, string errorMessage, double min, double max)
        {
            while (true)
            {
                Console.WriteLine($"{prompt} в диапазоне от {min} до {max}:");
                if (double.TryParse(Console.ReadLine(), out double value) && value >= min && value <= max)
                    return value;
                Console.WriteLine(
                    $"Ошибка: неверный ввод. Введите {errorMessage} в диапазоне от {min} до {max}.");
            }
        }

        public static bool ReadBool(string prompt)
        {
            bool value;

            while (true)
            {
                Console.WriteLine($"{prompt}");
                string choice = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (choice == "да" || choice == "д" || choice == "yes")
                {
                    value = true;
                    break;
                }

                if (choice == "нет" || choice == "н" || choice == "no")
                {
                    value = false;
                    break;
                }

                Console.WriteLine("Ошибка: требуется ввести \"да\" или \"нет\".");
            }

            return value;
        }
    }