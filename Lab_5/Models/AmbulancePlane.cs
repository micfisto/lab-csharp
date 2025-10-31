using Lab_5.Interfaces;

namespace Lab_5.Models;

public class AmbulancePlane : SpecialPlaneBase
{
    public AmbulancePlane(string model, int numberOfCrew,
        string typeTakeOfAndLanding, double flightRange, double fuelConsumption, int passengerCapacity,
        double payload) : base(model, typeOfPlane:"санитарный", purpose:"эвакуация раненых, транспортировка врачей и оборудования", numberOfCrew, typeTakeOfAndLanding, flightRange,
        fuelConsumption, passengerCapacity, payload)
    {
        Equipment = new List<string>
            { "медицинский салон", "носилки", "кислород", "аппараты жизнеобеспечения", "санитарная герметизация" };
    }
}