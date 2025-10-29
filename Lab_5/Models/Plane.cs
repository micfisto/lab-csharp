namespace Lab_5.Models;

public abstract class Plane
{
    //модель
    public string Model { get; set; }
    
    //колчество членов экипажа
    public int NumberOfCrew { get; set; }
     
    //тип взлёта и посадки
    public string TypeTakeOfAndLanding { get; set; }

    //дальность полёта
    public double FlightRange { get; set; }
    
    //потребление горючего
    public double FuelConsumption {get; set;}

    //пассажировместимость
    public int PassengerCapacity { get; set; }

    //грузоподъёмность
    public double Payload { get; set; }
   
    //назначение
    public string Purpose { get; set; }
 
    protected Plane(string model, int numberOfCrew, string typeTakeOfAndLanding, double flightRange, double fuelConsumption, int passengerCapacity, double payload, string purpose)
    {
        Model = model;
        NumberOfCrew = numberOfCrew;
        TypeTakeOfAndLanding = typeTakeOfAndLanding;
        FlightRange =  flightRange;
        FuelConsumption = fuelConsumption;
        PassengerCapacity = passengerCapacity;
        Payload = payload;
        Purpose = purpose;
    }
    public abstract string GetInfo();

    public override string ToString()
    {
        return GetInfo();
    }
}