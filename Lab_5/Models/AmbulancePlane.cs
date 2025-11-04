namespace Lab_5.Models;

public class AmbulancePlane : SpecialPlaneBase
{
    public AmbulancePlane(string model, int numberOfCrew,
        string typeTakeOfAndLanding, double flightRange, double fuelConsumption, int passengerCapacity,
        double payload) : base(model, typeOfPlane: "санитарный",
        purpose: "эвакуация раненых, транспортировка врачей и оборудования", numberOfCrew, typeTakeOfAndLanding,
        flightRange,
        fuelConsumption, passengerCapacity, payload)
    {
        Equipment = new List<string>
            { "медицинский салон", "носилки", "кислород", "аппараты жизнеобеспечения", "санитарная герметизация" };
    }

    public AmbulancePlane() : base() { }
    
    public override void EditInfo()
    {
        Console.WriteLine($"===== МЕНЮ РЕДАКТИРОВАНИЯ ИНФОРМАЦИИ О САМОЛЁТЕ {Model.ToUpper()}");

        while (true)
        {
            Console.WriteLine("1. Модель.");
            Console.WriteLine("2. Экипаж.");
            Console.WriteLine("3. Тип взлёта/посадки.");
            Console.WriteLine("4. Дальность полёта.");
            Console.WriteLine("5. Потребление горючего.");
            Console.WriteLine("6. Грузоподъёмность.");
            Console.WriteLine("7. Пассажировместимость.");
            Console.WriteLine("8. Наличие оборудования.");
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
                        "данные о количестве членов экипажа", 2, 5);
                    NumberOfCrew = numberOfCrew;
                    break;
                case "3":
                    string typeTakeOfAndLanding = PlaneHandler.IsTypeTakeOfAndLanding();
                    TypeTakeOfAndLanding = typeTakeOfAndLanding;
                    break;
                case "4":
                    double flightRange = PlaneHandler.ReadDouble("Введите дальность полёта в км",
                        "данные о дальности полёта.", 500, 5000);
                    FlightRange = flightRange;
                    break;
                case "5":
                    double fuelConsumption = PlaneHandler.ReadDouble("Введите расход топлива л/ч",
                        "данные о расходе топлива", 300, 2000);
                    FuelConsumption = fuelConsumption;
                    break;
                case "6":
                    double payload = PlaneHandler.ReadDouble("Введите грузоподъёмность", "данные о грузоподъёмности",
                        500, 5000);
                    Payload = payload;
                    break;
                case "7":
                    int passengerCapacity = PlaneHandler.ReadInt("Введите пассажировместимость",
                        "данные о количестве посадочных мест", 1, 12);
                    PassengerCapacity = passengerCapacity;
                    break;
                case "8":
                    while (true)
                    {
                        Console.WriteLine("\n1. Добавить оборудование.");
                        Console.WriteLine("2. Удалить оборудование.");
                        Console.WriteLine($"0. Вернуться в общее меню редактирования информации самолёта {Model}.");

                        var ch = Console.ReadLine();
                        switch (ch)
                        {
                            case "1":
                                AddSpecialEquipment();
                                break;
                            case "2":
                                RemoveSpecialEquipment();

                                if (Equipment.Count == 0)
                                {
                                    Console.WriteLine("Оборудование не найдено на данном самолёте.");
                                    return;
                                }

                                Console.WriteLine(
                                    $"Текущий список оборудования на самолёте: {string.Join(", ", Equipment)}.");
                                break;
                            case "0":
                                return;
                            default:
                                Console.WriteLine("Неверный ввод.");
                                break;
                        }
                    }
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.WriteLine("Информация обновлена.");
        }
    }
}