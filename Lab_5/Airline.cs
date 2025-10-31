using System.Collections.Generic;
using Lab_5.Models;

namespace Lab_5;

public class Airline
{
    private List<Plane> planes = new List<Plane>();

    public void AddPlane(Plane plane)
    {
        Console.WriteLine("Выберите тип самолёта:");
        Console.WriteLine("1. пассажирский");
        Console.WriteLine("2. грузовой");
        Console.WriteLine("3. санитарный");
        Console.WriteLine("4. сельскохозяйственный");

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        plane = PlaneHandler.CreatePessengerPlane();
                        break;
                    case 2:
                        plane = PlaneHandler.CreateCargoPlane();
                        break;
                }
            }
            else
                Console.WriteLine("Некорректный ввод. Введите число из списка.");
        }

        planes.Add(plane);
        Console.WriteLine($"Самолёт {plane.Model} добавлен в базу авиакомпании.");
    }

    public void RemovePlane(string model)
    {
        Plane? removePlane = planes.Find(plane => plane.Model == model);
        if (removePlane != null)
        {
            planes.Remove(removePlane);
            Console.WriteLine($"Самолёт {removePlane} удалён из базы авиакомпании.");
        }
        else
        {
            Console.WriteLine($"Самолёт {removePlane} не найден в базе авиакомпании.");
        }
    }

    public void ShowPlane()
    {
        if (planes.Count == 0)
        {
            Console.WriteLine("В авиакомпании пока нет самолётов.");
            return;
        }

        Console.WriteLine("====== СПИСОК САМОЛЁТОВ АВИКОМПАНИИ ======");
        foreach (var plane in planes)
        {
            Console.WriteLine($"{plane.Model}, {plane.TypeOfPlane}.");
        }
    }

    public List<Plane> GetPlane()
    {
        return planes;
    }
}