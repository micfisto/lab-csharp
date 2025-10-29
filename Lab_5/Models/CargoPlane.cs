using Lab_5.Interfaces;

namespace Lab_5.Models;

public class CargoPlane : Plane, ITransoptCargo
{
    public bool PresenceOfRamp { get; set; }
    public double WeightCurrentCargo { get; set; }

    public CargoPlane
    (string model, string typeOfPlane, string purpose, int numberOfCrew, string typeTakeOfAndLanding,
        double flightRange, double fuelConsumption,
        int passengerCapacity, double payload, bool presenceOfRamp, double weightCurrentCargo) : base(
        model, typeOfPlane, purpose, numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload)
    {
        PresenceOfRamp = presenceOfRamp;
        WeightCurrentCargo = weightCurrentCargo;

        TypeOfPlane = "грузовой";
        Purpose = "перевозка грузов";
    }

    public override string GetInfo()
    {
        return
            $"Модель: {Model}\n" +
            $"Тип: {TypeOfPlane}\n" +
            $"Назначение: {Purpose}\n" +
            $"Экипаж: {NumberOfCrew} чел.\n" +
            $"Тип взлёта и посадки: {TypeTakeOfAndLanding}\n" +
            $"Дальность полёта: {FlightRange} км\n" +
            $"Потребление горючего: {FuelConsumption} л/ч\n" +
            $"Пассажировместимость: {PassengerCapacity} чел.\n" +
            $"Грузоподъёмность: {Payload} кг\n" +
            $"Наличие рампы: {(PresenceOfRamp ? "есть" : "отсутствует")}\n" +
            $"Текущий объём груза на самолёте: {WeightCurrentCargo} кг\n";
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