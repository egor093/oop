using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace Program
{
    // Пользовательские исключения (иерархия) 
    // Базовое пользовательское исключение
    class PlanetException : Exception
    {
        public PlanetException(string message) : base(message) { }
        public PlanetException(string message, Exception inner) : base(message, inner) { }
    }

    // Некорректные данные (наследуем от ArgumentOutOfRangeException)
    class InvalidAreaException : ArgumentOutOfRangeException
    {
        public InvalidAreaException(string paramName, string message) : base(paramName, message) { }
    }

    // Ошибка при поиске страны/индекса
    class CountryNotFoundException : PlanetException
    {
        public CountryNotFoundException(string message) : base(message) { }
        public CountryNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

    // Ошибка файловой операции (наследуем от IOException)
    class FileOperationException : IOException
    {
        public FileOperationException(string message) : base(message) { }
        public FileOperationException(string message, Exception inner) : base(message, inner) { }
    }

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

        // если area <= 0 — бросаем InvalidAreaException 
        protected Land(string name, double area) : base(name)
        {
            // Debug.Assert 
            Debug.Assert(area > 0, "Площадь должна быть больше 0");

            if (area <= 0)
                throw new InvalidAreaException(nameof(area), $"Недопустимая площадь: {area}. Ожидалось > 0.");

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

    // partial-класс Континент (основная часть)
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
        public int Population { get; set; }

        public Country(string name, string capital, double area, int population = 0) : base(name, area)
        {
            // Assert: name не null и население >=0
            Debug.Assert(!string.IsNullOrEmpty(name), "Название страны должно быть указано");
            if (population < 0) throw new ArgumentOutOfRangeException(nameof(population), "Население не может быть орицательным");

            Capital = capital;
            Population = population;
        }

        public override void Describe()
        {
            Console.WriteLine($"Государство {Name}, столица {Capital}, площадь {Area} км^2, население {Population}.");
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
            return $"{base.ToString()}, столица: {Capital}, население: {Population}";
        }

        // Метод демонстрации возможного деления на ноль
        public double PeoplePerSquareKm()
        {
            if (Area == 0) throw new DivideByZeroException("Площадь равна 0");
            return Population / Area;
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
            Console.WriteLine("=== Список объектов планеты ===");
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

        // все государства на заданном континенте
        public IEnumerable<Country> GetCountriesByContinent(string continentName)
        {
            return container.GetAll()
                .OfType<Continent>()
                .Where(c => c.Name.Equals(continentName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(c => c.Countries);
        }

        // количество морей
        public int CountSeas() => container.GetAll().OfType<Sea>().Count();

        // острова по алфавиту
        public IEnumerable<Island> GetIslandsSorted()
        {
            return container.GetAll().OfType<Island>().OrderBy(i => i.Name);
        }

        // стандартное исключение ArgumentOutOfRangeException
        //  и повторный проброс (rethrow).
        public Country GetCountryAt(string continentName, int index)
        {
            var continent = container.GetAll().OfType<Continent>()
                .FirstOrDefault(c => c.Name.Equals(continentName, StringComparison.OrdinalIgnoreCase));

            if (continent == null)
                throw new CountryNotFoundException($"Континент '{continentName}' не найден.");

            try
            {
                return continent.Countries[index];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"[PlanetController] Неверный индекс {index} для континента {continentName}. Логируем и пробрасываем.");
                throw new CountryNotFoundException($"Нет страны с индексом {index} на континенте '{continentName}'.", ex);
            }
        }
    }

    class Program
    {
        // Метод, симулирующий ошибку файловой операции (демонстрация FileOperationException)
        static void SimulateFileOperation(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new FileOperationException("Передан пустой путь файла для чтения.");

            if (!path.EndsWith(".txt"))
                throw new FileOperationException($"Неподдерживаемый формат файла: {path}");
        }

        // Метод, демонстрирующий деление на ноль
        static double ComputeRatio(int numerator, int denominator)
        {
            // Возможен DivideByZeroException автоматически
            return (double)numerator / denominator;
        }

        static void Main()
        {
            PlanetContainer planet = new PlanetContainer();
            Continent eurasia = null;
            Country france = null;
            Country germany = null;
            Island madagascar = null;
            Island greenland = null;
            Sea baltic = null;
            Sea mediterranean = null;

            // основной блок обработки исключений 
            try
            {
                eurasia = new Continent("Евразия", 54000000, ContinentType.Eurasia, new Coordinates(54.5, 37.6));
                france = new Country("Франция", "Париж", 643801, 67000000);
                germany = new Country("Германия", "Берлин", 357588, 83000000);
                eurasia.AddCountry(france);
                eurasia.AddCountry(germany);

                madagascar = new Island("Мадагаскар", 587041, true);
                greenland = new Island("Гренландия", 2166000, false);
                baltic = new Sea("Балтийское море", 21000, 470);
                mediterranean = new Sea("Средиземное море", 3830000, 1500);

                planet.Add(eurasia);
                planet.Add(france);
                planet.Add(germany);
                planet.Add(madagascar);
                planet.Add(greenland);
                planet.Add(baltic);
                planet.Add(mediterranean);

                planet.PrintAll();

                PlanetController controller = new PlanetController(planet);

                // 2) Исключительная ситуация: попытка создать объект с неверной площадью (<=0)
                Console.WriteLine("\n--- Демонстрация InvalidAreaException ---");
                try
                {
                    var badContinent = new Continent("Брак", 0, ContinentType.Africa, new Coordinates(0, 0));
                }
                catch (InvalidAreaException iae)
                {
                    // Локальная обработка: выводим подробности, потом продолжаем
                    Console.WriteLine("[Local handler] Пойман InvalidAreaException при инициализации континента:");
                    Console.WriteLine($"  Type: {iae.GetType().Name}");
                    Console.WriteLine($"  Message: {iae.Message}");
                }

                // 3) Исключительная ситуация: попытка добавить null в контейнер -> ArgumentNullException
                Console.WriteLine("\n--- Демонстрация ArgumentNullException (Add null) ---");
                try
                {
                    planet.Add(null);
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("[Local handler] Пойман ArgumentNullException при попытке Add(null):");
                    Console.WriteLine($"  ParamName: {ane.ParamName}, Message: {ane.Message}");
                    throw; // пробрасываем тот же ArgumentNullException наверх
                }

                // 4) Демонстрация Index/ArgumentOutOfRange и повторного проброса / оборачивания
                Console.WriteLine("\n--- Демонстрация IndexOutOfRange / CountryNotFoundException ---");
                try
                {
                    var country = controller.GetCountryAt("Евразия", 10); 
                    Console.WriteLine(country);
                }
                catch (CountryNotFoundException cnfe)
                {
                    Console.WriteLine("[Main] Пойман CountryNotFoundException:");
                    Console.WriteLine($"  Message: {cnfe.Message}");
                    if (cnfe.InnerException != null)
                        Console.WriteLine($"  Inner: {cnfe.InnerException.GetType().Name} - {cnfe.InnerException.Message}");
                }

                // 5) Демонстрация деления на ноль (DivideByZeroException)
                Console.WriteLine("\n--- Демонстрация DivideByZeroException ---");
                try
                {
                    double r = ComputeRatio(10, 0);
                    Console.WriteLine($"Результат: {r}");
                }
                catch (DivideByZeroException dbz)
                {
                    Console.WriteLine("[Main] Пойман DivideByZeroException:");
                    Console.WriteLine($"  Message: {dbz.Message}");
                }

                // 6) Демонстрация файловой операции (пользовательское FileOperationException)
                Console.WriteLine("\n--- Демонстрация FileOperationException ---");
                try
                {
                    SimulateFileOperation("data.bin"); 
                }
                catch (FileOperationException fox)
                {
                    Console.WriteLine("[Main] Пойман FileOperationException:");
                    Console.WriteLine($"  Message: {fox.Message}");
                }

                // 7) Ещё одна демонстрация: ручное бросание стандартного NullReferenceException (симуляция)
                Console.WriteLine("\n--- Демонстрация NullReferenceException ---");
                try
                {
                    Country maybeNull = null;
                    Console.WriteLine(maybeNull.Name);
                }
                catch (NullReferenceException nre)
                {
                    Console.WriteLine("[Main] Пойман NullReferenceException:");
                    Console.WriteLine($"  Message: {nre.Message}");
                }

                Console.WriteLine("\n--- Демонстрация завершена успешно (если не было rethrow) ---");
            }
            // блоки catch внешнего уровня (обработка общего набора ошибок)
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("\n[Outer handler] ArgumentNullException пойман в Main (повторная обработка):");
                Console.WriteLine($"  ParamName: {ane.ParamName}");
                Console.WriteLine($"  Message: {ane.Message}");
            }
            catch (CountryNotFoundException cnfe)
            {
                Console.WriteLine("\n[Outer handler] CountryNotFoundException пойман в Main:");
                Console.WriteLine($"  Message: {cnfe.Message}");
                if (cnfe.InnerException != null)
                    Console.WriteLine($"  Inner: {cnfe.InnerException.GetType().Name} - {cnfe.InnerException.Message}");
            }
            catch (InvalidAreaException iae)
            {
                Console.WriteLine("\n[Outer handler] InvalidAreaException пойман в Main:");
                Console.WriteLine($"  Message: {iae.Message}");
            }
            catch (DivideByZeroException dbz)
            {
                Console.WriteLine("\n[Outer handler] DivideByZeroException пойман в Main:");
                Console.WriteLine($"  Message: {dbz.Message}");
            }
            catch (FileOperationException fox)
            {
                Console.WriteLine("\n[Outer handler] FileOperationException пойман в Main:");
                Console.WriteLine($"  Message: {fox.Message}"); 
            }
            // Универсальный обработчик
            catch (Exception ex)
            {
                Console.WriteLine("\n[Universal handler] Поймано исключение (неперечисленное выше):");
                Console.WriteLine($"  Type: {ex.GetType().Name}");
                Console.WriteLine($"  Message: {ex.Message}");
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("\n[Finally] Очистка и завершение.");
            }

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}
