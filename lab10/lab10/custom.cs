using System;
using System.Linq;

public class CustomComplexQuery
{
    public static void DemonstrateComplexQuery(List<Train> trains)
    {
        Console.WriteLine("\n=== СОБСТВЕННЫЙ СЛОЖНЫЙ ЗАПРОС ===\n");

        // Запрос с 5+ операторами: условия, проекции, упорядочивания, группировки, агрегирования
        var complexQuery = trains
            .Where(t => t.TotalSeats > 300)
            .GroupBy(t => t.TrainType)
            .Select(g => new
            {
                // тип поезда
                TrainType = g.Key,
                // подсчет поездов в группе
                TrainCount = g.Count(),
                // среднее количество мест
                AverageSeats = g.Average(t => t.TotalSeats),
                // максимальное количество мест
                MaxSeats = g.Max(t => t.TotalSeats),
                // минимальное количество мест
                MinSeats = g.Min(t => t.TotalSeats),
                // Список поездов в группе
                Trains = g.OrderByDescending(t => t.TotalSeats).ToList()
            })
            // Упорядочивание по количеству поездов в группе
            .OrderByDescending(x => x.TrainCount)
            .Take(2);

        Console.WriteLine("Статистика по типам поездов (мест > 300):");
        foreach (var group in complexQuery)
        {
            Console.WriteLine($"\nТип: {group.TrainType}");
            Console.WriteLine($"  Количество поездов: {group.TrainCount}");
            Console.WriteLine($"  Среднее количество мест: {group.AverageSeats:F0}");
            Console.WriteLine($"  Максимальное количество мест: {group.MaxSeats}");
            Console.WriteLine($"  Минимальное количество мест: {group.MinSeats}");

            Console.WriteLine("  Поезда:");
            foreach (var train in group.Trains)
            {
                Console.WriteLine($"    - {train.TrainNumber}: {train.TotalSeats} мест");
            }
        }
    }
}