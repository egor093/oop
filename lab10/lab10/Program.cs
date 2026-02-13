using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("LINQ TO OBJECTS\n");

        // 1. Демонстрация запросов для месяцев
        MonthsLINQ.DemonstrateMonthsQueries();

        // 2. Создание и заполнение коллекции поездов
        TrainCollection trainCollection = new TrainCollection();
        var trains = trainCollection.GetTrains();
        trainCollection.DisplayAllTrains();

        // 3. LINQ запросы для поездов
        TrainLINQQueries.DemonstrateTrainQueries(trains);

        // 4. Собственный сложный запрос
        CustomComplexQuery.DemonstrateComplexQuery(trains);

        // 5. Запрос с оператором Join
        JoinQueryDemo.DemonstrateJoinQuery(trains);
    }
}