using System;
using System.Linq;

// Дополнительный класс для демонстрации Join
public class Station
{
    public string City { get; set; }
    public string Region { get; set; }
    public int DistanceFromMoscow { get; set; }

    public Station(string city, string region, int distance)
    {
        City = city;
        Region = region;
        DistanceFromMoscow = distance;
    }
}

public class JoinQueryDemo
{
    public static void DemonstrateJoinQuery(List<Train> trains)
    {
        Console.WriteLine("\n=== ЗАПРОС С OPERATOR JOIN ===\n");

        // Создаем коллекцию станций
        var stations = new[]
        {
            new Station("Москва", "Центральный", 0),
            new Station("Санкт-Петербург", "Северо-Западный", 650),
            new Station("Казань", "Приволжский", 800),
            new Station("Екатеринбург", "Уральский", 1800),
            new Station("Новосибирск", "Сибирский", 3300),
            new Station("Ростов", "Южный", 1200),
            new Station("Сочи", "Южный", 1600)
        };

        // Запрос Join: соединение поездов и станций по городу назначения
        var joinQuery = trains
            .Join(stations,
                  train => train.Destination,
                  station => station.City,
                  (train, station) => new
                  {
                      TrainNumber = train.TrainNumber,
                      Destination = train.Destination,
                      DepartureTime = train.DepartureTime,
                      TotalSeats = train.TotalSeats,
                      Region = station.Region,
                      Distance = station.DistanceFromMoscow
                  })
            .OrderBy(x => x.Distance);

        Console.WriteLine("Поезда с информацией о станциях назначения:");
        Console.WriteLine("№\tНаправление\tРегион\t\tРасстояние\tВремя\tМеста");
        Console.WriteLine("------------------------------------------------------------");

        foreach (var item in joinQuery)
        {
            Console.WriteLine($"{item.TrainNumber}\t{item.Destination}\t\t{item.Region}\t{item.Distance} км\t\t{item.DepartureTime:HH:mm}\t{item.TotalSeats}");
        }

        // Группированный Join
        var groupJoinQuery = stations
            .GroupJoin(trains,
                      station => station.City,
                      train => train.Destination,
                      (station, trainGroup) => new
                      {
                          Station = station.City,
                          Region = station.Region,
                          TrainCount = trainGroup.Count(),
                          Trains = trainGroup.OrderBy(t => t.DepartureTime)
                      })
            .Where(x => x.TrainCount > 0)
            .OrderByDescending(x => x.TrainCount);

        Console.WriteLine("\n\nСтатистика по станциям:");
        foreach (var station in groupJoinQuery)
        {
            Console.WriteLine($"\n{station.Station} ({station.Region}): {station.TrainCount} поездов");
            foreach (var train in station.Trains)
            {
                Console.WriteLine($"   - {train.TrainNumber} в {train.DepartureTime:HH:mm}");
            }
        }
    }
}