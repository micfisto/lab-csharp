using Lab_5.Interfaces;

namespace Lab_5.Models;

public class AgriculturalPlane : Plane, ISpecialPlane
{
    public List<string> Equipment { get; private set; }

    public AgriculturalPlane
    (string model, string typeOfPlane, string purpose, int numberOfCrew, string typeTakeOfAndLanding, double flightRange, double fuelConsumption,
        int passengerCapacity, double payload,  string[] equipment) : base(
        model, typeOfPlane, purpose, numberOfCrew,
        typeTakeOfAndLanding, flightRange, fuelConsumption, passengerCapacity, payload)
    {
        Equipment = new List<string>(equipment);

        TypeOfPlane = "сельскохозяйственный";
        Purpose = "распыление удобрений и пестицидов, посев, тушение травы";
    }

    public override string GetInfo()
    {
       return $"Это сельскохозяйственный самолёт. Модель: {Model}, оснащение: {Equipment}, тип посадки {TypeTakeOfAndLanding}.";
    }

    public override string ToString()
    {
        return GetInfo();
    }

    public bool CanLandOnUnpreparedSurface()
    {
        switch (TypeTakeOfAndLanding)
        {
            case "vtol":
                Console.WriteLine("Модель способна взлетать и садиться вертикально.");
                return true;
            case "stol":
                Console.WriteLine("Модель требует минимальную длину взлётно-посадочной полосы.");
                return true;
            case "обычные":
                Console.WriteLine("Модель типа взлетает и садится только на аэродроме");
                return false;
            case "корабельные":
                Console.WriteLine("Модель способна взлетать и садиться на авианосцах.");
                return false;
            default:
                Console.WriteLine(
                    "Модель неизвестного типа посадки. Вероятно не может сесть на неподготовленное поле.");
                return false;
        }
    }

    public void SpecialEquipment()
    {
        Console.WriteLine($"Модель имеет специальное оборудование: {Equipment}.");
    }

    public void AddSpecialEquipment()
    {
        Console.WriteLine("Добавить на модель специальное оснащение.");
        string? newEquipment = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEquipment))
        {
            Equipment.Add(newEquipment);
            Console.WriteLine($"Новое оборудование {newEquipment} добавлено.");
            Console.WriteLine($"Полный перечень оборудования на самолёте: {string.Join(", ", Equipment)}.");
        }
    }

    public void RemoveSpecialEquipment()
    {
        if (Equipment.Count == 0)
        {
            Console.WriteLine("Оборудование не найдено на данном самолёте.");
            return;
        }
        Equipment.Clear();
        Console.WriteLine("Было удалено всё оборудование.");
    }
}