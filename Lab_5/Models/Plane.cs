namespace Lab_5.Models;

public abstract class Plane
{
    //модель
    public string Model { get; set; }

    //тип самолёта
    public string TypeOfPlane { get; protected set; }

    //назначение
    public string Purpose { get; protected set; }

    //количество членов экипажа
    public int NumberOfCrew { get; set; }

    //дальность полёта
    public double FlightRange { get; set; }

    //потребление горючего
    public double FuelConsumption { get; set; }

    //пассажировместимость
    public int PassengerCapacity { get; set; }

    //грузоподъёмность
    public double Payload { get; set; }

    protected Plane(string model, string typeOfPlane, string purpose, int numberOfCrew,
        double flightRange, double fuelConsumption, int passengerCapacity, double payload)
    {
        Model = model;
        TypeOfPlane = typeOfPlane;
        Purpose = purpose;
        NumberOfCrew = numberOfCrew;
        FlightRange = flightRange;
        FuelConsumption = fuelConsumption;
        PassengerCapacity = passengerCapacity;
        Payload = payload;
    }
    public abstract string GetInfo();

    public abstract void EditInfo();

    public override string ToString()
    {
        return GetInfo();
    }
}