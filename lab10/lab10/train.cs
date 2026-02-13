using System;
using System.Collections.Generic;
using System.Linq;

public class Train
{
    public string Destination { get; set; }
    public string TrainNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    public int TotalSeats { get; set; }
    public string TrainType { get; set; }

    public Train(string destination, string trainNumber, DateTime departureTime, int totalSeats, string trainType)
    {
        Destination = destination;
        TrainNumber = trainNumber;
        DepartureTime = departureTime;
        TotalSeats = totalSeats;
        TrainType = trainType;
    }

    public override string ToString()
    {
        return $"{TrainNumber} -> {Destination} | {DepartureTime:HH:mm} | Мест: {TotalSeats} | Тип: {TrainType}";
    }
}

public class TrainCollection
{
    private List<Train> trains;

    public TrainCollection()
    {
        trains = new List<Train>();
        InitializeTrains();
    }

    private void InitializeTrains()
    {
        // Заполнение коллекции 10+ поездами
        trains.AddRange(new[]
        {
            new Train("Москва", "001А", new DateTime(2024, 1, 15, 8, 30, 0), 350, "Скорый"),
            new Train("Санкт-Петербург", "002Б", new DateTime(2024, 1, 15, 14, 20, 0), 280, "Пассажирский"),
            new Train("Москва", "003В", new DateTime(2024, 1, 15, 20, 45, 0), 420, "Фирменный"),
            new Train("Казань", "004Г", new DateTime(2024, 1, 16, 6, 15, 0), 320, "Скорый"),
            new Train("Екатеринбург", "005Д", new DateTime(2024, 1, 16, 12, 0, 0), 380, "Скорый"),
            new Train("Москва", "006Е", new DateTime(2024, 1, 16, 18, 30, 0), 290, "Пассажирский"),
            new Train("Новосибирск", "007Ж", new DateTime(2024, 1, 17, 9, 0, 0), 450, "Фирменный"),
            new Train("Москва", "008З", new DateTime(2024, 1, 17, 16, 45, 0), 310, "Скорый"),
            new Train("Ростов", "009И", new DateTime(2024, 1, 18, 7, 20, 0), 270, "Пассажирский"),
            new Train("Сочи", "010К", new DateTime(2024, 1, 18, 22, 10, 0), 400, "Фирменный"),
            new Train("Москва", "011Л", new DateTime(2024, 1, 19, 10, 30, 0), 330, "Скорый")
        });
    }

    public List<Train> GetTrains()
    {
        return trains;
    }

    public void DisplayAllTrains()
    {
        Console.WriteLine("\n=== ВСЕ ПОЕЗДА ===");
        foreach (var train in trains)
        {
            Console.WriteLine(train);
        }
    }
}