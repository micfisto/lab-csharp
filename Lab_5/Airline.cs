using System.Collections.Generic;
using Lab_5.Models;

namespace Lab_5;

public class Airline
{
    private List<Plane> planes = new List<Plane>();

    public void AddPlane(Plane plane)
    {
        planes.Add(plane);
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