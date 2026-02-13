using System;
using System.Collections.Generic;
using System.Linq;

public class TrainLINQQueries
{
    public static void DemonstrateTrainQueries(List<Train> trains)
    {
        Console.WriteLine("\n=== LINQ ЗАПРОСЫ ДЛЯ ПОЕЗДОВ ===\n");

        // a. Список поездов, следующих до заданного пункта назначения
        string targetDestination = "Москва";
        var destinationQuery = trains.Where(t => t.Destination == targetDestination);
        Console.WriteLine($"a. Поезда до {targetDestination}:");
        foreach (var train in destinationQuery)
        {
            Console.WriteLine($"   {train}");
        }

        // b. Список поездов до заданного пункта назначения и отправляющихся после заданного часа
        int targetHour = 12;
        var destinationAndTimeQuery = trains
            .Where(t => t.Destination == targetDestination && t.DepartureTime.Hour >= targetHour);
        Console.WriteLine($"\nb. Поезда до {targetDestination} после {targetHour}:00:");
        foreach (var train in destinationAndTimeQuery)
        {
            Console.WriteLine($"   {train}");
        }

        // c. Максимальный поезд по количеству мест
        var maxSeatsTrain = trains.OrderByDescending(t => t.TotalSeats).First();
        Console.WriteLine($"\nc. Максимальный поезд по количеству мест:");
        Console.WriteLine($"   {maxSeatsTrain}");

        // d. Последние пять поездов по времени отправления
        var lastFiveTrains = trains.OrderByDescending(t => t.DepartureTime).Take(5);
        Console.WriteLine($"\nd. Последние пять поездов по времени отправления:");
        foreach (var train in lastFiveTrains)
        {
            Console.WriteLine($"   {train}");
        }

        // e. Упорядоченный список поездов по пункту назначения в алфавитном порядке
        var orderedByDestination = trains.OrderBy(t => t.Destination);
        Console.WriteLine($"\ne. Поезда, упорядоченные по пункту назначения:");
        foreach (var train in orderedByDestination)
        {
            Console.WriteLine($"   {train}");
        }
    }

}