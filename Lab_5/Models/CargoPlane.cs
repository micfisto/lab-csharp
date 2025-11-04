using Lab_5.Interfaces;

namespace Lab_5.Models;

public class CargoPlane : Plane, ITransportCargo
{
    public bool PresenceOfRamp { get; set; }
    public double WeightCurrentCargo { get; set; }

    public CargoPlane
    (string model, int numberOfCrew,
        double flightRange, double fuelConsumption, double payload, bool presenceOfRamp) : base(
        model, typeOfPlane: "грузовой", purpose: "перевозка грузов", numberOfCrew: numberOfCrew,
        flightRange: flightRange, fuelConsumption: fuelConsumption,
        passengerCapacity: 0, payload: payload)
    {
        PresenceOfRamp = presenceOfRamp;
        WeightCurrentCargo = 0;
    }

    public CargoPlane() : base() { }
    
    public override string GetInfo()
    {
        return
            $"Модель: {Model}\n" +
            $"Тип: {TypeOfPlane}\n" +
            $"Назначение: {Purpose}\n" +
            $"Экипаж: {NumberOfCrew} чел.\n" +
            $"Дальность полёта: {FlightRange} км\n" +
            $"Потребление горючего: {FuelConsumption} л/ч\n" +
            $"Пассажировместимость: {PassengerCapacity} чел.\n" +
            $"Грузоподъёмность: {Payload} кг\n" +
            $"Наличие рампы: {(PresenceOfRamp ? "есть" : "отсутствует")}\n" +
            $"Текущий объём груза на самолёте: {WeightCurrentCargo} кг\n";
    }

    public override void EditInfo()
    {
        Console.WriteLine($"===== МЕНЮ РЕДАКТИРОВАНИЯ ИНФОРМАЦИИ О САМОЛЁТЕ {Model.ToUpper()}");

        while (true)
        {
            Console.WriteLine("1. Модель.");
            Console.WriteLine("2. Экипаж.");
            Console.WriteLine("3. Дальность полёта.");
            Console.WriteLine("4. Потребление горючего.");
            Console.WriteLine("5. Грузоподъёмность.");
            Console.WriteLine("6. Наличие рампы.");
            Console.WriteLine("7. Текущий вес груза на самолёте.");
            Console.WriteLine("0. Выход из меню редактирования.");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string model = PlaneHandler.ReadString("Введите модель самолёта", "\"модель\"");
                    Model = model;
                    break;
                case "2":
                    int numberOfCrew = PlaneHandler.ReadInt("Введите количество членов экипажа",
                        "данные о количестве членов экипажа", 2, 10);
                    NumberOfCrew = numberOfCrew;
                    break;
                case "3":
                    double flightRange = PlaneHandler.ReadDouble("Введите дальность полёта в км",
                        "данные о дальности полёта.", 300, 12000);
                    FlightRange = flightRange;
                    break;
                case "4":
                    double fuelConsumption = PlaneHandler.ReadDouble("Введите расход топлива л/ч",
                        "данные о расходе топлива", 500, 10000);
                    FuelConsumption = fuelConsumption;
                    break;
                case "5":
                    double payload = PlaneHandler.ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности",
                        1000, 150000);
                    Payload = payload;
                    break;
                case "6":
                    bool presenceOfRamp = PlaneHandler.ReadBool("Имеет ли данная модель рампу (да/нет)?");
                    PresenceOfRamp = presenceOfRamp;
                    break;
                case "7":
                    double weightCurrentCargo = PlaneHandler.ReadDouble(
                        "Введите вес груза, который сейчас имеется на данном самолёте",
                        "данные о текущем весе груза на самолёте", 0, Payload);
                    WeightCurrentCargo = weightCurrentCargo;
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.WriteLine("Информация обновлена.");
        }
    }

    public void IsPresenceOfRamp(bool presenceOfRamp)
    {
        Console.WriteLine(PresenceOfRamp ? "У данной модели есть рампа." : "Рампа на данной модели отсутствует.");
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