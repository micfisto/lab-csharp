using Lab_5.Interfaces;

namespace Lab_5.Models;

public class CargoPlane : Plane, ITransoptCargo
{
    public bool PresenceOfRamp { get; set; }
    public double WeightCurrentCargo { get; set; }

    public CargoPlane
    (string model, int numberOfCrew, string typeTakeOfAndLanding, double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, string purpose, bool presenceOfRamp, double weightCurrentCargo) : base(
        model, numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload, purpose)
    {
        PresenceOfRamp = presenceOfRamp;
        WeightCurrentCargo = weightCurrentCargo;
    }

    public override string GetInfo()
    {
        return
            $"Это грузовой самолёт. Модель: {Model}, грузоподъёмность: {Payload}, дальность полёта: {FlightRange}, наличие рампы: {PresenceOfRamp}.";
    }

    public override string ToString()
    {
        return GetInfo();
    }

    public void LoadCargo(double loadWeightCargo)
    {
        if (loadWeightCargo + WeightCurrentCargo > Payload)
        {
            Console.WriteLine($"Данная модель не может перевезти груз весом {loadWeightCargo} кг.");
            Console.WriteLine($"Имеются ограничение по весу {Payload} кг.");
            return;
        }

        WeightCurrentCargo += loadWeightCargo;
        Console.WriteLine($"Груз успешно загружен. Сейчас на борту {WeightCurrentCargo} кг.");
    }

    public void IsPresenceOfRamp(bool presenceOfRamp)
    {
        Console.WriteLine(PresenceOfRamp ? "У данной модели есть рампа." : "Рампа на данной модели отсутствует.");
    }

    public void UnloadCargo()
    {
        if (WeightCurrentCargo == 0)
        {
            Console.WriteLine("На данном самолёте отсутствует груз. Выгрузка не требуется.");
            return;
        }

        Console.WriteLine($"Выгружено {WeightCurrentCargo} кг груза.");
        WeightCurrentCargo = 0;
    }
}