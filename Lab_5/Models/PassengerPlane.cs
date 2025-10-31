using Lab_5.Interfaces;

namespace Lab_5.Models;

public class PassengerPlane : Plane, IPassenger
{
    public int NumberOfEngines { get; private set; }
    public List<string> ComfortClass { get; private set; }

    public PassengerPlane
    (
        string model, int numberOfCrew,
        double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, int numberOfEngines, string[] comfortClass) : base(
        model, typeOfPlane: "пассажирский", purpose: "перевозка пассажиров и их багажа", numberOfCrew: numberOfCrew,flightRange: flightRange, fuelConsumption: fuelConsumption, passengerCapacity: passengerCapacity, payload: payload)
    {
        NumberOfEngines = numberOfEngines;
        ComfortClass = new List<string>(comfortClass);
    }

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

    public override string ToString()
    {
        return GetInfo();
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
        Console.WriteLine("Введите название класса обслуживания(эконом, комфорт, бизнес):");
        string? addComfortClass = Console.ReadLine();
        if (addComfortClass != null)
        {
            ComfortClass.Add(addComfortClass);
        }

        Console.WriteLine(
            $"Обновлено.\nСписок доступных классов обслуживания в данном самолёте: {string.Join(", ", ComfortClass)}");
    }

    public void RemoveComfortClass()
    {
        Console.WriteLine("Введите название класса для удаления: ");
        string? removeComfortClass = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(removeComfortClass))
        {
            removeComfortClass = removeComfortClass.ToLower();
            string? found = ComfortClass.Find(comfortClass => comfortClass.ToLower() == removeComfortClass);
            if (found != null)
            {
                ComfortClass.Remove(found);
                Console.WriteLine(
                    $"Обновлено.\nСписок доступных классов обслуживания в данном самолёте: {string.Join(", ", ComfortClass)}\"");
            }
            else
            {
                Console.WriteLine("Данного класса нет в списке доступных классов обслуживания на данном самолёте.");
            }
        }
        else
            Console.WriteLine("Некорректный ввод.");
    }
}