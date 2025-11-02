using Lab_5.Models;

namespace Lab_5;

public class Airline
{
    public static List<Plane> Planes = PlaneFactory.DefaultPlanes();

    public static void AddPlane()
    {
        Console.WriteLine("\nВыберите тип самолёта:");
        Console.WriteLine("1. пассажирский");
        Console.WriteLine("2. грузовой");
        Console.WriteLine("3. санитарный");
        Console.WriteLine("4. сельскохозяйственный");

        int choice = PlaneHandler.ReadInt("Введите число:", "число", 1, 4);

        Plane plane = null!;

        switch (choice)
        {
            case 1:
                plane = PlaneHandler.CreatePassengerPlaneFromConsole();
                break;
            case 2:
                plane = PlaneHandler.CreateCargoPlaneFromConsole();
                break;
            case 3:
                plane = PlaneHandler.CreateAmbulancePlaneFromConsole();
                break;
            case 4:
                plane = PlaneHandler.CreateAgriculturalPlaneFromConsole();
                break;
        }

        Planes.Add(plane);
        Console.WriteLine($"Самолёт {plane.Model} добавлен в базу авиакомпании.");
    }

    public static void InformationAboutThePlane()
    {
        ShowPlane();
        int index = PlaneHandler.ReadInt(
            "Выберите номер самолёта из списка, чтобы получить более подробную информацию о нём", "число", 1,
            Planes.Count);
        Console.WriteLine(Planes[index - 1].GetInfo());
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("1.Редактировать информацию о самолёте.");
            Console.WriteLine("2.Удалить данный самолёт.");
            Console.WriteLine("0.Выйти в главное меню.");

            int choice = PlaneHandler.ReadInt("Введите число", "число", 0, 2);
            switch (choice)
            {
                case 1:
                    Planes[index].EditInfo();
                    break;
                case 2:
                    Planes.RemoveAt(index);
                    break;
                case 3:
                    return;
            }
        }
    }
    
    public static void RemovePlane()
    {
        ShowPlane();
        int index = PlaneHandler.ReadInt("Введите номер самолёта для удаления", "число", 1, Planes.Count);
        Planes.RemoveAt(index-1);
    }

    public static void ShowPlane()
    {
        if (Planes.Count == 0)
        {
            Console.WriteLine("В авиакомпании пока нет самолётов.");
            return;
        }

        Console.WriteLine("====== СПИСОК САМОЛЁТОВ АВИКОМПАНИИ ======");
        for (int i = 0; i < Planes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Planes[i].Model}, {Planes[i].TypeOfPlane}.");
        }

        Console.WriteLine();
    }
}