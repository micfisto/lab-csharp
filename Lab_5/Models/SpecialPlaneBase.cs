using Lab_5.Interfaces;

namespace Lab_5.Models;

public abstract class SpecialPlaneBase : Plane, ISpecialPlane
{
    protected List<string> Equipment;
    protected string TypeTakeOfAndLanding { get; set; }

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
                return "может. Модель способна взлетать и садиться вертикально.";
            case "stol":
                return
                    "может. Модель требует минимальную длину взлётно-посадочной полосы и способна сесть на грунтовое покрытие.";
            case "обычные(наземные)":
                return "не может. Модель типа взлетает и садится только на аэродроме";
            default:
                return "модель неизвестного типа посадки. Вероятно не может сесть на неподготовленное поле.";
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

        Equipment.Clear();
        Console.WriteLine("Было удалено всё оборудование.");
    }
}