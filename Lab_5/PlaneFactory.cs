using Lab_5.Models;

namespace Lab_5;

public static class PlaneFactory
{
        public static List<Plane> DefaultPlanes()
    {
        return new List<Plane>
        {
            new PassengerPlane(
                model: "Boeing 737",
                numberOfCrew: 6,
                flightRange: 3500,
                fuelConsumption: 2600,
                passengerCapacity: 160,
                payload: 20000,
                numberOfEngines: 2,
                comfortClass: new[] { "эконом, бизнес" }
            ),
            new PassengerPlane(
                model: "Embraer E195",
                numberOfCrew: 4,
                flightRange: 2700,
                fuelConsumption: 1800,
                passengerCapacity: 120,
                payload: 15000,
                numberOfEngines: 2,
                comfortClass: new[] { "эконом", "комфорт" }
            ),
            new CargoPlane(
                model: "C-130 Hercules",
                numberOfCrew: 4,
                flightRange: 3800,
                fuelConsumption: 5000,
                payload: 20000,
                presenceOfRamp: true
            ),
            new CargoPlane(
                model: "An-124 Ruslan",
                numberOfCrew: 5,
                flightRange: 4800,
                fuelConsumption: 8000,
                payload: 120000,
                presenceOfRamp: true
            ),
            new AmbulancePlane(
                model: "Learjet 45XR MedEvac",
                numberOfCrew: 3,
                typeTakeOfAndLanding: "обычные(наземные)",
                flightRange: 3600,
                fuelConsumption: 1500,
                passengerCapacity: 4,
                payload: 2000
            ),

            new AmbulancePlane(
                model: "Piaggio P.180 Avanti MedEvac",
                numberOfCrew: 3,
                typeTakeOfAndLanding: "stol",
                flightRange: 2800,
                fuelConsumption: 1300,
                passengerCapacity: 6,
                payload: 2500
            ),

            new AmbulancePlane(
                model: "Air Tractor AT-502B",
                numberOfCrew: 1,
                typeTakeOfAndLanding: "stol",
                flightRange: 700,
                fuelConsumption: 500,
                passengerCapacity: 0,
                payload: 2800
            ),
            new AgriculturalPlane(
                model: "Thrush 510G",
                numberOfCrew: 1,
                typeTakeOfAndLanding: "stol",
                flightRange: 950,
                fuelConsumption: 650,
                payload: 3200
            )
        };
    }
    
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