namespace Lab_5.Models;

public abstract class Plane(
    string model,
    double flightRange,
    double totalCapacity,
    double payload,
    double fuelConsumption)
{
    //модель
    public string Model { get; set; } = model;

    //дальность полёта
    public double FlightRange { get; set; } = flightRange;

    //общая вместимость
    public double TotalCapacity { get; set; } = totalCapacity;

    //грузоподъёмность
    public double Payload { get; set; } = payload;

    //потребление горючего
    public double FuelConsumption {get; set;} = fuelConsumption;

    public abstract string GetInfo();
}