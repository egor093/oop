using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    // Тип континента
    enum ContinentType
    {
        Eurasia,
        Africa,
        NorthAmerica,
        SouthAmerica,
        Australia,
        Antarctica
    }

    struct Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public override string ToString() => $"({Latitude}°, {Longitude}°)";
    }

    // Интерфейс
    interface ICloneableObj
    {
        bool DoClone();
    }

    // Абстрактный базовый класс
    abstract class NatureObject
    {
        public string Name { get; set; }

        protected NatureObject(string name)
        {
            Name = name;
        }

        public abstract void Describe();
        public abstract bool DoClone();

        public override string ToString() => $"{GetType().Name}: {Name}";
    }

    // Суша
    abstract class Land : NatureObject
    {
        protected double Area;

        protected Land(string name, double area) : base(name)
        {
            Area = area;
        }

        public override void Describe()
        {
            Console.WriteLine($"{Name} — объект суши площадью {Area} км^2.");
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование суши выполнено успешно.");
            return true;
        }
    }

    // Континент
    partial class Continent : Land, ICloneableObj
    {
        public List<Country> Countries { get; set; } = new List<Country>();
        public ContinentType Type { get; set; }
        public Coordinates Location { get; set; }

        public Continent(string name, double area, ContinentType type, Coordinates location)
            : base(name, area)
        {
            Type = type;
            Location = location;
        }

        public override void Describe()
        {
            Console.WriteLine($"Континент {Name} ({Type}) содержит {Countries.Count} государств.");
        }

        bool ICloneableObj.DoClone()
        {
            Console.WriteLine("Клонирование континента по интерфейсу.");
            return true;
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование континента выполнено успешно.");
            return true;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, тип: {Type}, стран: {Countries.Count}, координаты: {Location}";
        }
    }

    // Государство
    class Country : Land, ICloneableObj
    {
        public string Capital { get; set; }

        public Country(string name, string capital, double area) : base(name, area)
        {
            Capital = capital;
        }

        public override void Describe()
        {
            Console.WriteLine($"Государство {Name}, столица {Capital}, площадь {Area} км^2.");
        }

        public override bool DoClone()
        {
            Console.WriteLine($"Создан клон государства {Name}.");
            return true;
        }

        bool ICloneableObj.DoClone()
        {
            Console.WriteLine($"Интерфейсное клонирование государства {Name}.");
            return true;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, столица: {Capital}";
        }
    }

    // Остров
    class Island : Land
    {
        public bool IsInhabited { get; set; }

        public Island(string name, double area, bool inhabited) : base(name, area)
        {
            IsInhabited = inhabited;
        }

        public override void Describe()
        {
            string inhabit = IsInhabited ? "обитаемый" : "необитаемый";
            Console.WriteLine($"{Name} — {inhabit} остров площадью {Area} км^2.");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, обитаемость: {IsInhabited}";
        }
    }

    // Вода
    abstract class Water : NatureObject
    {
        protected double Volume;

        protected Water(string name, double volume) : base(name)
        {
            Volume = volume;
        }

        public override void Describe()
        {
            Console.WriteLine($"{Name} — водный объект объёмом {Volume} км^3.");
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование водного объекта.");
            return true;
        }
    }

    // Море
    sealed class Sea : Water, ICloneableObj
    {
        public double Depth { get; set; }

        public Sea(string name, double volume, double depth) : base(name, volume)
        {
            Depth = depth;
        }

        public override void Describe()
        {
            Console.WriteLine($"Море {Name} (глубина {Depth} м, объём {Volume} км^3).");
        }

        bool ICloneableObj.DoClone()
        {
            Console.WriteLine($"Интерфейсное клонирование моря {Name}.");
            return true;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, глубина: {Depth}";
        }
    }

    // Printer
    class Printer
    {
        public void IAmPrinting(NatureObject obj)
        {
            Console.WriteLine($"Тип: {obj.GetType().Name}");
            Console.WriteLine(obj.ToString());
        }
    }

    // Класс-контейнер
    class PlanetContainer
    {
        private IList<NatureObject> objects;

        public IList<NatureObject> Objects
        {
            get => objects;
            set => objects = value ?? new List<NatureObject>();
        }

        public PlanetContainer()
        {
            objects = new List<NatureObject>();
        }

        public PlanetContainer(IList<NatureObject> initial)
        {
            objects = initial ?? new List<NatureObject>();
        }

        public void Add(NatureObject obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            objects.Add(obj);
        }

        public bool Remove(NatureObject obj)
        {
            if (obj == null) return false;
            return objects.Remove(obj);
        }

        public IEnumerable<NatureObject> GetAll() => objects.AsEnumerable();

        public void PrintAll()
        {
            Console.WriteLine("Список объектов планеты ");
            if (objects.Count == 0)
            {
                Console.WriteLine("(пусто)");
                return;
            }

            foreach (var obj in objects)
                Console.WriteLine(obj.ToString());
        }
    }

    // Контроллер
    class PlanetController
    {
        private PlanetContainer container;

        public PlanetController(PlanetContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IEnumerable<Country> GetCountriesByContinent(string continentName)
        {
            return container.GetAll()
                .OfType<Continent>()
                .Where(c => c.Name.Equals(continentName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(c => c.Countries);
        }

        public int CountSeas() => container.GetAll().OfType<Sea>().Count();

        public IEnumerable<Island> GetIslandsSorted()
        {
            return container.GetAll().OfType<Island>().OrderBy(i => i.Name);
        }
    }

    class Program
    {
        static void Main()
        {
            Continent eurasia = new Continent("Евразия", 54000000, ContinentType.Eurasia, new Coordinates(54.5, 37.6));
            Country france = new Country("Франция", "Париж", 643801);
            Country germany = new Country("Германия", "Берлин", 357588);
            eurasia.AddCountry(france);
            eurasia.AddCountry(germany);

            Island madagascar = new Island("Мадагаскар", 587041, true);
            Island greenland = new Island("Гренландия", 2166000, false);
            Sea baltic = new Sea("Балтийское море", 21000, 470);
            Sea mediterranean = new Sea("Средиземное море", 3830000, 1500);

            PlanetContainer planet = new PlanetContainer();
            planet.Add(eurasia);
            planet.Add(france);
            planet.Add(germany);
            planet.Add(madagascar);
            planet.Add(greenland);
            planet.Add(baltic);
            planet.Add(mediterranean);

            planet.PrintAll();

            PlanetController controller = new PlanetController(planet);

            Console.WriteLine("\nГосударства на континенте Евразия:");
            foreach (var c in controller.GetCountriesByContinent("Евразия"))
                Console.WriteLine(c);

            Console.WriteLine($"\nКоличество морей: {controller.CountSeas()}");

            Console.WriteLine("\nОстрова по алфавиту:");
            foreach (var i in controller.GetIslandsSorted())
                Console.WriteLine(i);
        }
    }
}
