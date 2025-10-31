namespace Lab_5.Models;

public class AgriculturalPlane : SpecialPlaneBase
{
    public AgriculturalPlane
    (string model, int numberOfCrew, string typeTakeOfAndLanding,
        double flightRange, double fuelConsumption, double payload) : base(
        model, typeOfPlane: "сельскохозяйственный", purpose: "распыление удобрений и пестицидов, посев, тушение травы",
        numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity: 0, payload)
    {
        Equipment = new List<string> { "баки для химикатов", "распылители под крыльями", "защита кабины" };
    }
}