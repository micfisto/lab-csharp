namespace Lab_5.Models;

public class AgriculturalPlane : SpecialPlaneBase
{
    public AgriculturalPlane
    (string model, string typeOfPlane, string purpose, int numberOfCrew, string typeTakeOfAndLanding,
        double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, string[] equipment) : base(
        model, typeOfPlane, purpose, numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload)
    {
        Equipment = new List<string>(equipment);

        TypeOfPlane = "сельскохозяйственный";
        Purpose = "распыление удобрений и пестицидов, посев, тушение травы";
    }
}