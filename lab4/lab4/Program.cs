using System;
using System.Collections.Generic;

namespace Program 
{
    // Интерфейс с методом DoClone() 
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

        // Одноимённый метод с интерфейсом
        public abstract bool DoClone();

        public override string ToString()
        {
            return $"{GetType().Name}: {Name}";
        }
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
    class Continent : Land, ICloneableObj
    {
        public List<Country> Countries { get; set; } = new List<Country>();

        public Continent(string name, double area) : base(name, area) { }

        // Реализация интерфейса (другая логика)
        bool ICloneableObj.DoClone()
        {
            Console.WriteLine("Клонирование континента по интерфейсу.");
            return true;
        }

        public override void Describe()
        {
            Console.WriteLine($"Континент {Name} содержит {Countries.Count} государств.");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, стран: {Countries.Count}";
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

        public override bool Equals(object obj)
        {
            if (obj is Country other)
                return Name == other.Name && Capital == other.Capital && Area == other.Area;
            return false;
        }

        public override int GetHashCode() => Name.GetHashCode() ^ Capital.GetHashCode();
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

    // Вода (абстрактный)
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

    //  Sealed Море
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

    //  Printer
    class Printer
    {
        public void IAmPrinting(NatureObject obj)
        {
            Console.WriteLine($"Тип: {obj.GetType().Name}");
            Console.WriteLine(obj.ToString());
        }
    }

    
    class Program
    {
        static void Main()
        {
            Continent europe = new Continent("Евразия", 54000000);
            Country france = new Country("Франция", "Париж", 643801);
            Island madagascar = new Island("Мадагаскар", 587041, true);
            Sea baltic = new Sea("Балтийское море", 21000, 470);

            europe.Countries.Add(france);

            NatureObject[] objects = { europe, france, madagascar, baltic };

            Printer printer = new Printer();
            foreach (var obj in objects)
            {
                printer.IAmPrinting(obj);
                obj.Describe();
                Console.WriteLine();
            }

            // Демонстрация работы через интерфейс и абстрактный класс
            ICloneableObj cloneable = france as ICloneableObj;
            cloneable?.DoClone();

            NatureObject abstractRef = france;
            abstractRef.DoClone();

            // Проверка типов
            if (baltic is Water)
                Console.WriteLine($"{baltic.Name} — водный объект.");
        }
    }
}