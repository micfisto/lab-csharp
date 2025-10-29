using Lab_5.Interfaces;

namespace Lab_5.Models;

public class PassengerPlane : Plane, IPassenger
{
    //количество двигателей(1-2 для региональных, 2-4 у лайнеров)
    public int NumberOfEngines { get; private set; }

    //класс комфорта в самолёте
    public List<string> ComfortClass { get; private set; }

    public PassengerPlane
    (
        string model, string typeOfPlane, string purpose, int numberOfCrew, string typeTakeOfAndLanding, double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, int numberOfEngines, string[] comfortClass) : base(
        model, typeOfPlane, purpose, numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload)
    {
        NumberOfEngines = numberOfEngines;
        ComfortClass = new List<string>(comfortClass);

        TypeOfPlane = "пассажирский";
        Purpose = "перевозка пассажиров и их багажа";
    }

    public override string GetInfo()
    {
        return
            $"Модель: {Model}\n" +
            $"Тип: {TypeOfPlane}\n" +
            $"Назначение: {Purpose}\n" +
            $"Экипаж: {NumberOfCrew} чел.\n" +
            $"Тип взлёта и посадки: {TypeTakeOfAndLanding}\n" +
            $"Дальность полёта: {FlightRange} км\n" +
            $"Потребление горючего: {FuelConsumption} л/ч\n" +
            $"Пассажировместимость: {PassengerCapacity} чел.\n" +
            $"Грузоподъёмность: {Payload} кг\n" +
            $"Количество двигателей: {NumberOfEngines}\n" +
            $"Категория: {PlaneCategory()}\n" +
            $"Классы комфорта: \n";
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

    public void ShowComfortClasses()
    {
        Console.WriteLine($"Классы комфорта в данном самолёте: {string.Join(", ", ComfortClass)}.");
    }
}