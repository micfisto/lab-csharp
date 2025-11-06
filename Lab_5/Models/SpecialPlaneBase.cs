using System.Xml.Serialization;
using Lab_5.Interfaces;

namespace Lab_5.Models;

public abstract class SpecialPlaneBase : Plane, ISpecialPlane
{
    public List<string> Equipment { get; set; } = new List<string>();
    public string TypeTakeOfAndLanding { get; set; } = string.Empty;

    [XmlIgnore]
    public static readonly Dictionary<int, string> TakeOffTypes = new()
    {
        { 1, "stol" },
        { 2, "vtol" },
        { 3, "обычные(наземные)" },
        { 4, "другой" }
    };

    public SpecialPlaneBase(string model, string typeOfPlane, string purpose, int numberOfCrew,
        string typeTakeOfAndLanding, double flightRange, double fuelConsumption, int passengerCapacity,
        double payload) : base(model, typeOfPlane, purpose, numberOfCrew,
        flightRange,
        fuelConsumption, passengerCapacity, payload)
    {
        TypeTakeOfAndLanding = typeTakeOfAndLanding;
        Equipment = new List<string>();
    }
    
    public SpecialPlaneBase() : base() { }
    
    public override string GetInfo()
    {
        return
            $"Модель: {Model}\n" +
            $"Тип: {TypeOfPlane}\n" +
            $"Назначение: {Purpose}\n" +
            $"Экипаж: {NumberOfCrew} чел.\n" +
            $"Тип взлёта и посадки: {TypeTakeOfAndLanding}\n" +
            $"Возможность сесть на неподготовленное поле: {CanLandOnUnpreparedSurface()}\n" +
            $"Дальность полёта: {FlightRange} км\n" +
            $"Потребление горючего: {FuelConsumption} л/ч\n" +
            $"Пассажировместимость: {PassengerCapacity} чел.\n" +
            $"Грузоподъёмность: {Payload} кг\n" +
            $"Оборудование: {string.Join(", ", Equipment)}";
    }    

    public virtual string CanLandOnUnpreparedSurface()
    {
        switch (TypeTakeOfAndLanding)
        {
            case "vtol":
                return "есть. Модель способна взлетать и садиться вертикально.";
            case "stol":
                return
                    "есть. Модель требует минимальную длину взлётно-посадочной полосы и способна сесть на грунтовое покрытие.";
            case "обычные(наземные)":
                return "отсутствует. Модель типа взлетает и садится только на аэродроме";
            default:
                return "модель неизвестного типа посадки. Вероятно отсутствует возможность сесть на неподготовленное поле.";
        }
    }

    public string SpecialEquipment()
    {
        if (Equipment.Count > 0)
        {
            return string.Join(", ", Equipment);
        }

        return "отсутствует";
    }

    public void AddSpecialEquipment()
    {
        Console.WriteLine("Введите название оборудования:");
        string? newEquipment = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEquipment))
        {
            newEquipment = newEquipment.ToLower();
            if (Equipment.Contains(newEquipment))
            {
                Console.WriteLine("Такое оборудование уже установлено.");
                return;
            }

            Equipment.Add(newEquipment);
            Console.WriteLine($"Новое оборудование {newEquipment} добавлено.");
            Console.WriteLine($"Полный перечень оборудования на самолёте: {string.Join(", ", Equipment)}.");
        }
        else
        {
            Console.WriteLine("Некорректный ввод.");
        }
    }

    public void RemoveSpecialEquipment()
    {
        if (Equipment.Count == 0)
        {
            Console.WriteLine("Оборудование не найдено на данном самолёте.");
            return;
        }

        Console.WriteLine("1. Удалить всё оборудование.");
        Console.WriteLine("2. Удалить позицию по номеру.");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Equipment.Clear();
                Console.WriteLine("Было удалено всё оборудование.");
                break;
            case "2":
                Console.WriteLine("Текущий список оборудования на самолёте:");
                for (int i = 0; i < Equipment.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Equipment[i]}");
                }

                int index = PlaneHandler.ReadInt("Введите номер позиции для удаления:", "число", 1, Equipment.Count);
                Equipment.RemoveAt(index - 1);
                break;
            default:
                Console.WriteLine("Неверный ввод.");
                break;
        }
    }
}