using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GenericsLab
{
    public interface ICollectionOperations<T>
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> ViewAll();
    }

    public class CollectionType<T> : ICollectionOperations<T> where T : class
    {
        private List<T> _items;

        public CollectionType()
        {
            _items = new List<T>();
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "element can't be null.");
            _items.Add(item);
        }

        public void Remove(T item)
        {
            if (!_items.Remove(item))
                throw new KeyNotFoundException("element not found in collection");
        }

        public IEnumerable<T> ViewAll()
        {
            return _items;
        }

        public IEnumerable<T> FindByPredicate(Predicate<T> predicate)
        {
            return _items.FindAll(predicate);
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                string json = JsonSerializer.Serialize(_items);
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving to file: " + ex.Message);
            }
        }

        public void LoadFromFile(string fileName)
        {
            try
            {
                string json = File.ReadAllText(fileName);
                _items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading from file: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Finally");
            }
        }

        public override string ToString()
        {
            return string.Join(", ", _items);
        }
    }

    public class Ship
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"Ship: {Name}, Capacity: {Capacity}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CollectionType<Ship> ships = new CollectionType<Ship>();
                ships.Add(new Ship { Name = "111", Capacity = 3000 });
                ships.Add(new Ship { Name = "222", Capacity = 2000 });

                Console.WriteLine("Ships:");
                foreach (var ship in ships.ViewAll())
                {
                    Console.WriteLine(ship);
                }

                Console.WriteLine("saved");

                CollectionType<Ship> loadedShips = new CollectionType<Ship>();
                loadedShips.LoadFromFile("ships.json");
                Console.WriteLine("loaded");
                foreach (var ship in loadedShips.ViewAll())
                {
                    Console.WriteLine(ship);
                }

                CollectionType<string> names = new CollectionType<string>();
                names.Add("mmm");
                names.Add("nnn");

                Console.WriteLine("Names: ");
                foreach (var name in names.ViewAll())
                {
                    Console.WriteLine(name);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
