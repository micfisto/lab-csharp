using System.Text;
using System.Xml;
using Lab_5.Models;
using  System.Xml.Serialization;

namespace Lab_5;

public class Airline
{
    private List<Plane> _planes = PlaneFactory.DefaultPlanes();
    private const string FilePath = "planes.xml";

    public Airline Menu()
    {
        while (true)
        {
            Console.WriteLine("1.Добавить самолёт в авиабазу.");
            Console.WriteLine("2.Удалить самолёт из авиабазы.");
            Console.WriteLine("3.Показать список самолётов.");
            Console.WriteLine("4.Вывести информацию по конкретному самолёту.");
            Console.WriteLine("5.Рассчитать общую пассажировместимость.");
            Console.WriteLine("6.Рассчитать общую грузоподъёмность.");
            Console.WriteLine("7.Найти самолёты в заданном диапазоне потребления топлива.");
            Console.WriteLine("8.Отсортировать по дальности полёта.");
            Console.WriteLine("9.Сохранить в XML-файл.");
            Console.WriteLine("10.Загрузить из XML-файла.");
            Console.WriteLine("0.Выйти из приложения.");

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
                    ShowPlane();
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
                case "9":
                    SavePlaneXml();
                    break;
                case "10":
                    LoadPlanesXml();
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
                case "0":
                    Console.WriteLine("Выход из приложения...");
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void AddPlane()
    {
        Console.WriteLine("\nВыберите тип самолёта:");
        Console.WriteLine("1. пассажирский");
        Console.WriteLine("2. грузовой");
        Console.WriteLine("3. санитарный");
        Console.WriteLine("4. сельскохозяйственный");

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

        _planes.Add(plane);
        Console.WriteLine($"Самолёт {plane.Model} добавлен в базу авиакомпании.");
        SavePlaneXml();
    }

    private void InformationAboutThePlane()
    {
        ShowPlane();
        int index = PlaneHandler.ReadInt(
            "Выберите номер самолёта из списка, чтобы получить более подробную информацию о нём", "число", 1,
            _planes.Count);
        Console.WriteLine(_planes[index - 1].GetInfo());
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("1.Редактировать информацию о самолёте.");
            Console.WriteLine("2.Удалить данный самолёт.");
            Console.WriteLine("0.Выйти в главное меню.");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _planes[index - 1].EditInfo();
                    SavePlaneXml();
                    break;
                case "2":
                    _planes.RemoveAt(index - 1);
                    SavePlaneXml();
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
        ShowPlane();
        int index = PlaneHandler.ReadInt("Введите номер самолёта для удаления", "число", 1, _planes.Count);
        _planes.RemoveAt(index - 1);
        SavePlaneXml();
    }

    private void ShowPlane()
    {
        if (_planes.Count == 0)
        {
            Console.WriteLine("В авиакомпании пока нет самолётов.");
            return;
        }

        Console.WriteLine("====== СПИСОК САМОЛЁТОВ АВИКОМПАНИИ ======");
        for (int i = 0; i < _planes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_planes[i].Model}, {_planes[i].TypeOfPlane}.");
        }

        Console.WriteLine();
    }

    private void TotalPassengerCapacity()
    {
        int total = 0;
        foreach (var plane in _planes)
        {
            total += plane.PassengerCapacity;
        }

        Console.WriteLine($"Общая пассажировместимость: {total}");
    }

    private void TotalPayload()
    {
        double total = 0;
        foreach (var plane in _planes)
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

        foreach (var plane in _planes)
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
        Console.WriteLine("Ваш выбор:");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _planes = _planes.OrderBy(plane => plane.FlightRange).ToList();
                break;
            case "2":
                _planes = _planes.OrderByDescending(plane => plane.FlightRange).ToList();
                break;
            default:
                Console.WriteLine("Неверный ввод. Сортировка отменена");
                return;
        }

        Console.WriteLine("Самолёты отсортированы.");
        for (int i = 0; i < _planes.Count; i++)
        {
            var plane = _planes[i];
            Console.WriteLine($"{i + 1}.{plane.Model} — {plane.FlightRange}");
        }
    }


    private void SavePlaneXml()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plane>));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                NewLineOnAttributes = false
            };

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = XmlWriter.Create(FilePath, settings))
            {
                serializer.Serialize(writer, _planes, namespaces);
            }

            Console.WriteLine($"Текст успешно экспортирован в XML: {FilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при экспорте XML: {ex.Message}");
        }
    }

    private void LoadPlanesXml()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Файл не существует.");
            return;
        }
        
        XmlSerializer serializer = new XmlSerializer(typeof(List<Plane>));
        using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
        {
            try
            {
                _planes = (List<Plane>)serializer.Deserialize(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при загрузке XML: {e.Message}");
                _planes = new List<Plane>();
            }
        }
    }
}