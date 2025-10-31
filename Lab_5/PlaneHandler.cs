using Lab_5.Models;

namespace Lab_5;

public class PlaneHandler
{
    public static List<Plane> DefaultPlanes()
    {
        return new List<Plane>
        {
            new PassengerPlane(
                model: "Boeing 737",
                numberOfCrew: 6,
                flightRange: 3500,
                fuelConsumption: 2600,
                passengerCapacity: 160,
                payload: 20000,
                numberOfEngines: 2,
                comfortClass: new[] { "эконом, бизнес" }
            ),
            new PassengerPlane(
                model: "Embraer E195",
                numberOfCrew: 4,
                flightRange: 2700,
                fuelConsumption: 1800,
                passengerCapacity: 120,
                payload: 15000,
                numberOfEngines: 2,
                comfortClass: new[] { "эконом", "комфорт" }
            ),
            new CargoPlane(
                model: "C-130 Hercules",
                numberOfCrew: 4,
                flightRange: 3800,
                fuelConsumption: 5000,
                payload: 20000,
                presenceOfRamp: true
                ),
            new CargoPlane(
                model: "An-124 Ruslan",
                numberOfCrew: 5,
                flightRange: 4800,
                fuelConsumption: 8000,
                payload: 120000,
                presenceOfRamp: true
            ),
            new AmbulancePlane(
                model: "Learjet 45XR MedEvac",
                numberOfCrew: 3,
                typeTakeOfAndLanding: "обычные(наземные)",
                flightRange: 3600,
                fuelConsumption: 1500,
                passengerCapacity: 4,
                payload: 2000
            ),

            new AmbulancePlane(
                model: "Piaggio P.180 Avanti MedEvac",
                numberOfCrew: 3,
                typeTakeOfAndLanding: "stol",
                flightRange: 2800,
                fuelConsumption: 1300,
                passengerCapacity: 6,
                payload: 2500
            ),

            new AmbulancePlane(
                model: "Air Tractor AT-502B",
                numberOfCrew: 1,
                typeTakeOfAndLanding: "stol",
                flightRange: 700,
                fuelConsumption: 500,
                passengerCapacity: 0,
                payload: 2800
            ),
            new AgriculturalPlane(
                model: "Thrush 510G",
                numberOfCrew: 1,
                typeTakeOfAndLanding: "stol",
                flightRange: 950,
                fuelConsumption: 650,
                payload: 3200
            )
        };
    }

    public static PassengerPlane CreatePessengerPlane()
    {
        Console.WriteLine("=====СОЗДАНИЕ ПАССАЖИРСКОГО САМОЛЁТА=====");

        string? model;
        while (true)
        {
            Console.WriteLine("Введите модель самолёта:");
            model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model))
                break;
            Console.WriteLine("Ошибка: поле \"модель\" не может быть пустым.");
        }

        int numberOfCrew;
        while (true)
        {
            Console.WriteLine("Введите количество членов экипажа(2-18):");
            if (int.TryParse(Console.ReadLine(), out numberOfCrew) && numberOfCrew is >= 2 and <= 18)
                break;
            Console.WriteLine("Ошибка: количество членов экипажа должно быть в диапазоне от 2 до 18.");
        }

        double flightRange;
        while (true)
        {
            Console.WriteLine("Введите дальность полёта в км (100-15000):");
            if (double.TryParse(Console.ReadLine(), out flightRange) && flightRange is >= 100 and <= 15000)
                break;
            Console.WriteLine("Ошибка: введите данные о дальности полёта в корректных значениях.");
        }

        double fuelConsumption;
        while (true)
        {
            Console.WriteLine("Введите расход топлива л/ч (200-8000):");
            if (double.TryParse(Console.ReadLine(), out fuelConsumption) && fuelConsumption is >= 200 and <= 8000)
                break;
            Console.WriteLine("Ошибка: введите данные о расходе топлива в корректных значениях.");
        }

        int passengerCapacity;
        while (true)
        {
            Console.WriteLine("Введите пассажировместимость (2-850):");
            if (int.TryParse(Console.ReadLine(), out passengerCapacity) && passengerCapacity is >= 2 and <= 850)
                break;
            Console.WriteLine("Ошибка: количество посадочных мест должно быть в диапазоне от 2 до 850.");
        }

        double payload;
        while (true)
        {
            Console.WriteLine("Введите грузоподъёмность (100-200000):");
            if (double.TryParse(Console.ReadLine(), out payload) && payload is >= 100 and <= 200000)
                break;
            Console.WriteLine("Ошибка: введите данные о грузоподъёмности в корректных значениях.");
        }

        int numberOfEngines;
        while (true)
        {
            Console.WriteLine("Введите количество двигателей (1-4):");
            if (int.TryParse(Console.ReadLine(), out numberOfEngines) && numberOfEngines is >= 1 and <= 4)
                break;
            Console.WriteLine("Ошибка: количество двигателей должно быть в диапазоне от 2 до 18.");
        }

        string? comfortClassInput;
        while (true)
        {
            Console.WriteLine("Введите классы обслуживания через запятую(эконом,комфорт,бизнес):");
            comfortClassInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(comfortClassInput))
                break;
            Console.WriteLine("Ошибка: поле \"класс обслуживания\" не может быть пустым.");
        }

        string[] comfortClass = comfortClassInput.Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(str => str.Trim()).ToArray();

        PassengerPlane plane = new PassengerPlane(
            model,
            numberOfCrew,
            flightRange,
            fuelConsumption,
            passengerCapacity,
            payload,
            numberOfEngines,
            comfortClass);

        Console.WriteLine($"Пассажирский самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static CargoPlane CreateCargoPlane()
    {
        Console.WriteLine("=====СОЗДАНИЕ ГРУЗОВОГО САМОЛЁТА=====");

        string? model;
        while (true)
        {
            Console.WriteLine("Введите модель самолёта:");
            model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model))
                break;
            Console.WriteLine("Ошибка: поле \"модель\" не может быть пустым.");
        }

        int numberOfCrew;
        while (true)
        {
            Console.WriteLine("Введите количество членов экипажа(2-10):");
            if (int.TryParse(Console.ReadLine(), out numberOfCrew) && numberOfCrew is >= 2 and <= 10)
                break;
            Console.WriteLine("Ошибка: количество членов экипажа должно быть в диапазоне от 2 до 10.");
        }

        double flightRange;
        while (true)
        {
            Console.WriteLine("Введите дальность полёта в км (300-12000):");
            if (double.TryParse(Console.ReadLine(), out flightRange) && flightRange is >= 300 and <= 12000)
                break;
            Console.WriteLine("Ошибка: введите данные о дальности полёта в корректных значениях.");
        }

        double fuelConsumption;
        while (true)
        {
            Console.WriteLine("Введите расход топлива л/ч (500-10000):");
            if (double.TryParse(Console.ReadLine(), out fuelConsumption) && fuelConsumption is >= 500 and <= 10000)
                break;
            Console.WriteLine("Ошибка: введите данные о расходе топлива в корректных значениях.");
        }

        double payload;
        while (true)
        {
            Console.WriteLine("Введите грузоподъёмность (1000-150000):");
            if (double.TryParse(Console.ReadLine(), out payload) && payload is >= 1000 and <= 150000)
                break;
            Console.WriteLine("Ошибка: введите данные о грузоподъёмности в корректных значениях.");
        }

        bool presenceOfRamp;
        while (true)
        {
            Console.WriteLine("Имеет ли данная модель рампу (да/нет)?");
            string choice = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (choice == "да" || choice == "д" || choice == "yes")
            {
                presenceOfRamp = true;
                break;
            }

            if (choice == "нет" || choice == "н" || choice == "no")
            {
                presenceOfRamp = false;
                break;
            }

            Console.WriteLine("Ошибка: требуется ввести \"да\" или \"нет\".");
        }

        double weightCurrentCargo;
        while (true)
        {
            Console.WriteLine($"Введите вес груза, который сейчас имеется на данном самолёте (0-{payload}):");
            if (double.TryParse(Console.ReadLine(), out weightCurrentCargo) && weightCurrentCargo >= 0 &&
                weightCurrentCargo <= payload)
            {
                Console.WriteLine("Значение текущего груза успешно добавлено.");
                break;
            }

            Console.WriteLine($"Значение текущего груза не должно превышать {payload}.");
        }

        CargoPlane plane = new CargoPlane(
            model,
            numberOfCrew,
            flightRange,
            fuelConsumption,
            payload,
            presenceOfRamp)
        {
            WeightCurrentCargo = weightCurrentCargo
        };

        Console.WriteLine($"Грузовой самолёт {plane.Model} успешно создан и добавлен в базу авиакомпании.");
        return plane;
    }

    public static AmbulancePlane CreateAmbulancePlane()
    {
        Console.WriteLine("=====СОЗДАНИЕ САНИТАРНОГО САМОЛЁТА=====");

        string? model;
        while (true)
        {
            Console.WriteLine("Введите модель самолёта:");
            model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model))
                break;
            Console.WriteLine("Ошибка: поле \"модель\" не может быть пустым.");
        }

        int numberOfCrew;
        while (true)
        {
            Console.WriteLine("Введите количество членов экипажа(2-5):");
            if (int.TryParse(Console.ReadLine(), out numberOfCrew) && numberOfCrew is >= 2 and <= 5)
                break;
            Console.WriteLine("Ошибка: количество членов экипажа должно быть в диапазоне от 2 до 5.");
        }

        string typeTakeOfAndLanding = IsTypeTakeOfAndLanding();

        double flightRange;
        while (true)
        {
            Console.WriteLine("Введите дальность полёта в км (500-5000):");
            if (double.TryParse(Console.ReadLine(), out flightRange) && flightRange is >= 500 and <= 5000)
                break;
            Console.WriteLine("Ошибка: введите данные о дальности полёта в корректных значениях.");
        }

        double fuelConsumption;
        while (true)
        {
            Console.WriteLine("Введите расход топлива л/ч (300-2000):");
            if (double.TryParse(Console.ReadLine(), out fuelConsumption) && fuelConsumption is >= 300 and <= 2000)
                break;
            Console.WriteLine("Ошибка: введите данные о расходе топлива в корректных значениях.");
        }

        int passengerCapacity;
        while (true)
        {
            Console.WriteLine("Введите пассажировместимость (1-12):");
            if (int.TryParse(Console.ReadLine(), out passengerCapacity) && passengerCapacity is >= 1 and <= 12)
                break;
            Console.WriteLine("Ошибка: количество посадочных мест должно быть в диапазоне от 1 до 12.");
        }

        double payload;
        while (true)
        {
            Console.WriteLine("Введите грузоподъёмность (500-5000):");
            if (double.TryParse(Console.ReadLine(), out payload) && payload is >= 500 and <= 5000)
                break;
            Console.WriteLine("Ошибка: введите данные о грузоподъёмности в корректных значениях.");
        }

        AmbulancePlane plane = new AmbulancePlane(
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

    public static AgriculturalPlane CreateAgriculturalPlane()
    {
        Console.WriteLine("=====СОЗДАНИЕ СЕЛЬСКОХОЗЯЙСТВЕННОГО САМОЛЁТА=====");

        string? model;
        while (true)
        {
            Console.WriteLine("Введите модель самолёта:");
            model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model))
                break;
            Console.WriteLine("Ошибка: поле \"модель\" не может быть пустым.");
        }

        int numberOfCrew;
        while (true)
        {
            Console.WriteLine("Введите количество членов экипажа(1-2):");
            if (int.TryParse(Console.ReadLine(), out numberOfCrew) && numberOfCrew is >= 1 and <= 2)
                break;
            Console.WriteLine("Ошибка: количество членов экипажа должно быть в диапазоне от 1 до 2.");
        }

        string typeTakeOfAndLanding = IsTypeTakeOfAndLanding();

        double flightRange;
        while (true)
        {
            Console.WriteLine("Введите дальность полёта в км (200-1200):");
            if (double.TryParse(Console.ReadLine(), out flightRange) && flightRange is >= 200 and <= 1200)
                break;
            Console.WriteLine("Ошибка: введите данные о дальности полёта в корректных значениях.");
        }

        double fuelConsumption;
        while (true)
        {
            Console.WriteLine("Введите расход топлива л/ч (150-800):");
            if (double.TryParse(Console.ReadLine(), out fuelConsumption) && fuelConsumption is >= 150 and <= 800)
                break;
            Console.WriteLine("Ошибка: введите данные о расходе топлива в корректных значениях.");
        }

        double payload;
        while (true)
        {
            Console.WriteLine("Введите грузоподъёмность (500-5000):");
            if (double.TryParse(Console.ReadLine(), out payload) && payload is >= 500 and <= 5000)
                break;
            Console.WriteLine("Ошибка: введите данные о грузоподъёмности в корректных значениях.");
        }

        AgriculturalPlane plane = new AgriculturalPlane(
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
            Console.WriteLine("1.vtol");
            Console.WriteLine("2.stol");
            Console.WriteLine("3.обычные(наземные)");
            Console.WriteLine("4.другой");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        return "vtol";
                    case 2:
                        return "stol";
                    case 3:
                        return "обычные(наземные)";
                    case 4:
                        return "другой";
                    default:
                        Console.WriteLine("Некорректный ввод. Введите число.");
                        continue;
                }
            }
        }
    }
}