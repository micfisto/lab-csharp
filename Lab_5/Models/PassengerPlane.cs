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
        string model, int numberOfCrew, string typeTakeOfAndLanding, double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, string purpose, int numberOfEngines, string[] comfortClass) : base(model,
        numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload, purpose)
    {
        NumberOfEngines = numberOfEngines;
        ComfortClass = new List<string>(comfortClass);
    }

    public override string GetInfo()
    {
        return
            $"Это пассажирский самолёт. Модель: {Model}, вместимость: {PassengerCapacity}, дальность полёта: {FlightRange}.";
    }

    public override string ToString()
    {
        return GetInfo();
    }

    public string TypoOfPlane()
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