using Lab_5.Models;

namespace Lab_5;

public static class PlaneFactory
{
    public static PassengerPlane CreatePassengerPlane(string model,
        int numberOfCrew,
        double flightRange,
        double fuelConsumption,
        int passengerCapacity,
        double payload,
        int numberOfEngines,
        string[] comfortClass)
    {
        return new PassengerPlane(
            model,
            numberOfCrew,
            flightRange,
            fuelConsumption,
            passengerCapacity,
            payload,
            numberOfEngines,
            comfortClass
        );
    }

    public static CargoPlane CreateCargoPlane(string model,
        int numberOfCrew,
        double flightRange,
        double fuelConsumption,
        double payload,
        bool presenceOfRamp,
        double weightCurrentCargo)
    {
        return new CargoPlane(
            model, numberOfCrew, flightRange, fuelConsumption, payload, presenceOfRamp)
        {
            WeightCurrentCargo = weightCurrentCargo
        };
    }

    public static AmbulancePlane CreateAmbulancePlane(string model,
        int numberOfCrew,
        string typeTakeOfAndLanding,
        double flightRange,
        double fuelConsumption,
        int passengerCapacity,
        double payload)
    {
        return new AmbulancePlane(
            model,
            numberOfCrew,
            typeTakeOfAndLanding,
            flightRange,
            fuelConsumption,
            passengerCapacity,
            payload);
    }
    
    public static AgriculturalPlane CreateAgriculturalPlane(string model,
        int numberOfCrew,
        string typeTakeOfAndLanding,
        double flightRange,
        double fuelConsumption,
        double payload)
    {
        return new AgriculturalPlane(
            model,
            numberOfCrew,
            typeTakeOfAndLanding,
            flightRange,
            fuelConsumption,
            payload);
    }
}