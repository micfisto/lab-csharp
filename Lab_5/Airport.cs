using System.Xml.Serialization;
using Lab_5.Models;

namespace Lab_5;

public class Airport
{
    public string Name { get; set; } = String.Empty;

    [XmlArray("Planes")]
    [XmlArrayItem("PassengerPlane", typeof(PassengerPlane))]
    [XmlArrayItem("CargoPlane", typeof(CargoPlane))]
    [XmlArrayItem("AmbulancePlane", typeof(AmbulancePlane))]
    [XmlArrayItem("AgriculturalPlane", typeof(AgriculturalPlane))]
    public List<Plane> Planes { get; set; } = new List<Plane>();

    public event Action? DataChanged;

    public Airport()
    {
    }

    public Airport(string name)
    {
        Name = name;
    }

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("1.Добавить самолёт в базу аэропорта.");
            Console.WriteLine("2.Удалить самолёт из базы аэропорта.");
            Console.WriteLine("3.Показать список самолётов.");
            Console.WriteLine("4.Вывести информацию по выбранному самолёту.");
            Console.WriteLine("5.Рассчитать общую пассажировместимость.");
            Console.WriteLine("6.Рассчитать общую грузоподъёмность.");
            Console.WriteLine("7.Найти самолёты в заданном диапазоне потребления топлива.");
            Console.WriteLine("8.Отсортировать по дальности полёта.");
            Console.WriteLine("0.Выйти в меню управления аэропортом.");
            Console.WriteLine();

            Console.WriteLine("Введите число:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddPlane();
                    break;
                case "2":
                    RemovePlane();
                    break;
                case "3":
                    ShowPlane(Planes);
                    break;
                case "4":
                    InformationAboutThePlane();
                    break;
                case "5":
                    TotalPassengerCapacity();
                    break;
                case "6":
                    TotalPayload();
                    break;
                case "7":
                    FindByConsumption();
                    break;
                case "8":
                    CompareByFlightRange();
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
                case "0":
                    Console.WriteLine("Выход из приложения...");
                    return;
            }
        }
    }

    private void AddPlane()
    {
        while (true)
        {
            Console.WriteLine("1.Создать свой самолёт.");
            Console.WriteLine("2.Выбрать из существующих.");
            Console.WriteLine("0.Выйти в меню редактирования информации об аэропорте.");
            Console.WriteLine();

            Console.WriteLine("Введите число:");
            var ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    Console.WriteLine("1. пассажирский");
                    Console.WriteLine("2. грузовой");
                    Console.WriteLine("3. санитарный");
                    Console.WriteLine("4. сельскохозяйственный");
                    Console.WriteLine();

                    Console.WriteLine("Выберите тип самолёта:");
                    var choice = Console.ReadLine();

                    Plane plane = null!;

                    switch (choice)
                    {
                        case "1":
                            plane = PlaneHandler.CreatePassengerPlaneFromConsole();
                            break;
                        case "2":
                            plane = PlaneHandler.CreateCargoPlaneFromConsole();
                            break;
                        case "3":
                            plane = PlaneHandler.CreateAmbulancePlaneFromConsole();
                            break;
                        case "4":
                            plane = PlaneHandler.CreateAgriculturalPlaneFromConsole();
                            break;
                        default:
                            Console.WriteLine("Неверный ввод.");
                            break;
                    }

                    Planes.Add(plane);
                    Console.WriteLine($"Самолёт {plane.Model} добавлен в базу авиакомпании.");
                    DataChanged?.Invoke();
                    break;
                case "2":
                    List<Plane> defaultPlanes = new List<Plane>();
                    defaultPlanes.AddRange(PlaneFactory.DefaultPlanes1());
                    defaultPlanes.AddRange(PlaneFactory.DefaultPlanes2());
                    ShowPlane(defaultPlanes);
                    
                    int index =
                        PlaneHandler.ReadInt("Выберите самолёт из списка", "число", 1, defaultPlanes.Count);
                    Planes.Add(defaultPlanes[index - 1]);
                    
                    Console.WriteLine($"Самолёт {defaultPlanes[index-1].Model} добавлен в базу авиакомпании.");
                    DataChanged?.Invoke();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private void InformationAboutThePlane()
    {
        ShowPlane(Planes);
        int index = PlaneHandler.ReadInt(
            "Выберите номер самолёта из списка, чтобы получить более подробную информацию о нём", "число", 1,
            Planes.Count);
        Console.WriteLine(Planes[index - 1].GetInfo());
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("1.Редактировать информацию о самолёте.");
            Console.WriteLine("2.Удалить данный самолёт.");
            Console.WriteLine("0.Выйти в меню редактирования информации об аэропорте.");
            Console.WriteLine();

            Console.WriteLine("Введите число:");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Planes[index - 1].EditInfo();
                    DataChanged?.Invoke();
                    break;
                case "2":
                    Planes.RemoveAt(index - 1);
                    DataChanged?.Invoke();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private void RemovePlane()
    {
        ShowPlane(Planes);
        int index = PlaneHandler.ReadInt("Введите номер самолёта для удаления", "число", 1, Planes.Count);
        Planes.RemoveAt(index - 1);
        DataChanged?.Invoke();
    }

    private void ShowPlane(List<Plane> planes)
    {
        if (planes.Count == 0)
        {
            Console.WriteLine($"В аэропорте {Name} пока нет самолётов.");
            return;
        }

        Console.WriteLine($"====== СПИСОК САМОЛЁТОВ АЭРОПОРТА {Name.ToUpper()} ======");
        for (int i = 0; i < planes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {planes[i].Model}, {planes[i].TypeOfPlane}.");
        }

        Console.WriteLine();
    }

    private void TotalPassengerCapacity()
    {
        int total = 0;
        foreach (var plane in Planes)
        {
            total += plane.PassengerCapacity;
        }

        Console.WriteLine($"Общая пассажировместимость: {total}");
    }

    private void TotalPayload()
    {
        double total = 0;
        foreach (var plane in Planes)
        {
            total += plane.Payload;
        }

        Console.WriteLine($"Общая грузоподъёмность: {total}");
    }

    private void FindByConsumption()
    {
        Console.WriteLine("Требуется задать диапазон значений потребления топлива.");
        double min, max;
        while (true)
        {
            Console.WriteLine("Введите минимальное значение:");
            while (!double.TryParse(Console.ReadLine(), out min))
            {
                Console.WriteLine("Неверный ввод. Введите число:");
            }

            Console.WriteLine("Введите максимальное значение:");
            while (!double.TryParse(Console.ReadLine(), out max))
            {
                Console.WriteLine("Неверный ввод. Введите число:");
            }

            if (min <= max)
                break;
            Console.WriteLine("Ошибка: минимальное значение больше максимального. Повторите ввод данных.");
        }

        List<Plane> findPlanes = new();

        foreach (var plane in Planes)
        {
            if (plane.FuelConsumption >= min && plane.FuelConsumption <= max)
                findPlanes.Add(plane);
        }

        if (findPlanes.Count == 0)
            Console.WriteLine("Не найдено самолётов в данном диапазоне.");

        Console.WriteLine("Найденные самолёты:");
        for (int i = 0; i < findPlanes.Count; i++)
        {
            var plane = findPlanes[i];
            Console.WriteLine($"{i + 1}.{plane.Model} — {plane.FuelConsumption}");
        }
    }

    private void CompareByFlightRange()
    {
        Console.WriteLine("1.Отсортировать в порядке возрастания.");
        Console.WriteLine("2.Отсортировать в порядке убывания.");
        Console.WriteLine();

        Console.WriteLine("Ваш выбор:");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Planes = Planes.OrderBy(plane => plane.FlightRange).ToList();
                break;
            case "2":
                Planes = Planes.OrderByDescending(plane => plane.FlightRange).ToList();
                break;
            default:
                Console.WriteLine("Неверный ввод. Сортировка отменена");
                return;
        }

        Console.WriteLine("Самолёты отсортированы.");
        for (int i = 0;
             i < Planes
                 .Count;
             i++)
        {
            var plane = Planes[i];
            Console.WriteLine($"{i + 1}.{plane.Model} — {plane.FlightRange}");
        }
    }

    public override string ToString()
    {
        return Name;
    }
}