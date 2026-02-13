using System;
using System.Linq;

public class MonthsLINQ
{
    public static void DemonstrateMonthsQueries()
    {
        string[] months = {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        Console.WriteLine("LINQ ЗАПРОСЫ ДЛЯ МЕСЯЦЕВ\n");

        // Месяцы с длиной строки равной n
        int n = 5;
        var lengthQuery = months.Where(m => m.Length == n);
        Console.WriteLine($"a. Месяцы с длиной {n}: {string.Join(", ", lengthQuery)}");

        // Летние и зимние месяцы
        var seasonQuery = months.Where(m =>
            m == "June" || m == "July" || m == "August" ||
            m == "December" || m == "January" || m == "February");
        Console.WriteLine($"b. Летние и зимние месяцы: {string.Join(", ", seasonQuery)}");

        // Месяцы в алфавитном порядке
        var alphabeticalQuery = months.OrderBy(m => m);
        Console.WriteLine($"c. Месяцы в алфавитном порядке: {string.Join(", ", alphabeticalQuery)}");

        // Месяцы содержащие букву 'u' и длиной не менее 4-х
        var letterQuery = months.Where(m => m.Contains('u') && m.Length >= 4);
        Console.WriteLine($"d. Месяцы с 'u' и длиной ≥4: {string.Join(", ", letterQuery)}");
        Console.WriteLine($"   Количество: {letterQuery.Count()}");
    }
}