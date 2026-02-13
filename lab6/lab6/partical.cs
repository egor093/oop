using System;

namespace Program
{
    partial class Continent
    {
        public void AddCountry(Country c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c), "Country cannot be null when adding to continent.");
            Countries.Add(c);
        }

        public void ShowCountries()
        {
            Console.WriteLine($"Список стран континента {Name}:");
            if (Countries.Count == 0)
            {
                Console.WriteLine("(нет стран)");
                return;
            }

            foreach (var c in Countries)
                Console.WriteLine($"  → {c.Name} ({c.Capital})");
        }
    }
}
