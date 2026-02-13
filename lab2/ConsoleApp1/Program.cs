using System;

public partial class Airline
{
    // Свойства
    public string Destination
    {
        get => destination;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                destination = value;
            else
                throw new ArgumentException("Пункт назначения не может быть пустым.");
        }
    }

    public int FlightNumber
    {
        get => flightNumber;
        set
        {
            if (value > 0)
                flightNumber = value;
            else
                throw new ArgumentException("Номер рейса должен быть положительным.");
        }
    }

    public string AirplaneType
    {
        get => airplaneType;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                airplaneType = value;
            else
                throw new ArgumentException("Тип самолета не может быть пустым.");
        }
    }

    public TimeSpan DepartureTime
    {
        get => departureTime;
        set => departureTime = value;
    }

    public string[] DaysOfWeek
    {
        get => daysOfWeek;
        set
        {
            if (value.Length > 0)
                daysOfWeek = value;
            else
                throw new ArgumentException("Должен быть указан хотя бы один день недели.");
        }
    }

    // Метод для работы с ref и out параметрами
    public void UpdateFlightInfo(ref string newDestination, out int newFlightNumber)
    {
        newFlightNumber = new Random().Next(1, 1000);  
        Destination = newDestination; 
    }


    public static void PrintClassInfo()
    {
        Console.WriteLine($"Общее количество созданных объектов авиакомпании: {objectCount}");
    }

    // Переопределение методов Object
    public override bool Equals(object? obj) 
    {
        if (obj is Airline other)
        {
            return flightNumber == other.flightNumber && destination == other.destination;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return flightNumber.GetHashCode() ^ destination.GetHashCode();
    }

    public override string ToString()
    {
        return $"Рейс {FlightNumber}, Пункт назначения: {Destination}, Тип самолета: {AirplaneType}, Время вылета: {DepartureTime}";
    }

    public static void FindFlightsByDestination(Airline[] airlines, string destination)
    {
        var flights = airlines.Where(f => f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase)).ToArray();
        if (flights.Length == 0)
        {
            Console.WriteLine($"Рейсы в пункт назначения {destination} не найдены.");
        }
        else
        {
            Console.WriteLine($"Рейсы в пункт назначения {destination}:");
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }

    public static void FindFlightsByDayOfWeek(Airline[] airlines, string dayOfWeek)
    {
        var flights = airlines.Where(f => f.DaysOfWeek.Any(day => day.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase))).ToArray();
        if (flights.Length == 0)
        {
            Console.WriteLine($"Рейсы на {dayOfWeek} не найдены.");
        }
        else
        {
            Console.WriteLine($"Рейсы на {dayOfWeek}:");
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }

}

public class Program
{
    public static void Main(string[] args)
    {
        // Создание объектов
        Airline flight1 = new Airline("Минск", 111, "Embraer 195", new TimeSpan(16, 15, 0), new string[] { "Понедельник", "Среда", "Пятница", "Суббота" });
        Airline flight2 = new Airline("Лондон", 202);
        Airline flight3 = new Airline("Москва", 404, "Embraer 195-E2", new TimeSpan(19, 0, 0), new string[] { "Пятница", "Суббота" });

        Airline[] airlines = new Airline[]
        {
            new Airline("Москва", 101, "Boeing 737", new TimeSpan(14, 30, 0), new string[] { "Понедельник", "Среда", "Пятница" }),
            new Airline("Минск", 202, "Airbus A320", new TimeSpan(16, 45, 0), new string[] { "Вторник", "Четверг" }),
            new Airline("Лондон", 303, "Boeing 777", new TimeSpan(10, 15, 0), new string[] { "Понедельник", "Четверг" }),
            new Airline("Москва", 404, "Embraer 195-E2", new TimeSpan(19, 0, 0), new string[] { "Пятница", "Суббота" }),
            new Airline("Минск", 133, "Embraer 7", new TimeSpan(16, 45, 0), new string[] { "Понедельник", "Пятница", "Суббота" })
        };

        // a) Запрос пункта назначения у пользователя
        Console.WriteLine("Введите пункт назначения:");
        string destination = Console.ReadLine();
        Airline.FindFlightsByDestination(airlines, destination);

        // b) Запрос дня недели у пользователя
        Console.WriteLine("\nВведите день недели:");
        string dayOfWeek = Console.ReadLine();
        Airline.FindFlightsByDayOfWeek(airlines, dayOfWeek);

        Console.WriteLine("______________________________________\n");

        // Вызов методов и свойств
        Console.WriteLine(flight1); 
        Console.WriteLine(flight2);
        Console.WriteLine(flight3);

        Console.WriteLine("______________________________________\n");

        // Сравнение объектов
        Console.WriteLine(flight1.Equals(flight2));  // False
        Console.WriteLine(flight1.GetHashCode());

        Console.WriteLine("______________________________________\n");

        // Использование ref и out параметров
        string newDestination = "Париж";
        flight1.UpdateFlightInfo(ref newDestination, out int newFlightNumber);
        Console.WriteLine($"Новый номер рейса: {newFlightNumber}, Город: {flight1.Destination}");

        Console.WriteLine("______________________________________\n");


        Airline.PrintClassInfo();  // Вывод количества созданных объектов
    }
}
