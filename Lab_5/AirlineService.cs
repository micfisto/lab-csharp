using Lab_5.Models;

namespace Lab_5;

public class AirlineService
{
    private List<Airport> _airports = new List<Airport>();
    private readonly StorageService _storage = new();
    public event Action? DataChanged;

    private void UpdateData()
    {
        DataChanged?.Invoke();
    }

    public AirlineService()
    {
        _airports = _storage.LoadPlanesXml();
        
        if (_airports.Count == 0)
        {
            var grodno = new Airport("Гродно")
            {
                Planes = PlaneFactory.DefaultPlanes1()
            };
            _airports.Add(grodno);

            var minsk = new Airport("Минск")
            {
                Planes = PlaneFactory.DefaultPlanes2()
            };
            _airports.Add(minsk);

            var moscow = new Airport("Москва")
            {
                Planes = []
            };
            _airports.Add(moscow);
        }

        foreach (var airport in _airports)
        {
            airport.DataChanged += UpdateData;
        }

        DataChanged += () => _storage.SavePlaneXml(_airports);
    }

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("1.Показать все аэропорты.");
            Console.WriteLine("2.Общая сводка по аэропорту.");
            Console.WriteLine("3.Добавить аэропорт.");
            Console.WriteLine("4.Удалить аэропорт.");
            Console.WriteLine("5.Сохранить данные в XML.");
            Console.WriteLine("6.Загрузить данные из XML.");
            Console.WriteLine("0.Выход из приложения.");
            Console.WriteLine();

            Console.WriteLine("Введите число:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAirports();
                    break;
                case "2":
                    InformationAboutAirport();
                    break;
                case "3":
                    AddAirport();
                    break;
                case "4":
                    RemoveAirport();
                    break;
                case "5":
                    _storage.SavePlaneXml(_airports);
                    break;
                case "6":
                    _storage.LoadPlanesXml();
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

    private void ShowAirports()
    {
        if (_airports.Count == 0)
        {
            Console.WriteLine("Аэропорты отсутствуют.");
            return;
        }

        Console.WriteLine("===== СПИСОК АЭРОПОРТОВ =====");

        for (int i = 0; i < _airports.Count; i++)
        {
            Console.WriteLine($"{i + 1}.{_airports[i].Name}.");
        }

        Console.WriteLine();
    }

    private void InformationAboutAirport()
    {
        ShowAirports();

        int index = PlaneHandler.ReadInt("Выберите аэропорт из списка", "число", 1, _airports.Count);
        var selectedAirport = _airports[index - 1];

        if (selectedAirport.Planes.Count == 0)
        {
            Console.WriteLine($"Самолёты в аэропорту \"{selectedAirport.Name}\" отсутствуют.");
            Console.WriteLine();
        }

        if (selectedAirport.Planes.Count > 0)
        {
            Console.WriteLine("===== ОБЩАЯ СВОДКА ПО АЭРОПОРТУ =====");
            Console.WriteLine();
            Console.WriteLine($"Аэропорт: {selectedAirport.Name}");
            Console.WriteLine($"Всего самолётов: {selectedAirport.Planes.Count}");

            int passengerCount = selectedAirport.Planes.Count(plane => plane is PassengerPlane);
            int cargoCount = selectedAirport.Planes.Count(plane => plane is CargoPlane);
            int ambulanceCount = selectedAirport.Planes.Count(plane => plane is AmbulancePlane);
            int agriculturalCount = selectedAirport.Planes.Count(plane => plane is AgriculturalPlane);

            Console.WriteLine("Из них:");
            Console.WriteLine($"    пассажирских: {passengerCount}");
            Console.WriteLine($"    грузовых: {cargoCount}");
            Console.WriteLine($"    санитарных: {ambulanceCount}");
            Console.WriteLine($"    сельскохозяйственных: {agriculturalCount}");
            Console.WriteLine();
        }

        while (true)
        {
            Console.WriteLine("1.Показать подробную информацию.");
            Console.WriteLine("0.Выйти в меню управления аэропортами.");

            Console.WriteLine("Введите число:");
            var ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    selectedAirport.Menu();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private void AddAirport()
    {
        string name = PlaneHandler.ReadString("Введите название аэропорта", "\"поле\"");
        var airport = new Airport(name);
        airport.DataChanged += UpdateData;
        _airports.Add(airport);
        Console.WriteLine($"Аэропорт \"{name}\" успешно добавлен.");
        UpdateData();
    }

    private void RemoveAirport()
    {
        ShowAirports();

        int index = PlaneHandler.ReadInt("Выберите аэропорт из списка", "число", 1, _airports.Count);
        var removeAirport = _airports[index - 1];
        _airports.Remove(removeAirport);
        Console.WriteLine($"Аэропорт \"{removeAirport.Name}\" успешно удалён.");
        UpdateData();
    }
}