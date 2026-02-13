using System;
using System.Net.Sockets;
using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace OOP_lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            Task1();
            //Task2();
            //Task3();
            //Task4();
        }

        public static void Task1()
        {
            Console.WriteLine("\n--- Task 1: Serialization and Deserialization ---");

            Circle circle = new Circle(5);

            Circle binaryCircle = null;
            CustomSerializer.SerializeToBinary(circle);
            CustomSerializer.DeserializeFromBinary(ref binaryCircle);
            Console.WriteLine($"Binary Deserialization: {binaryCircle}\n");

            Circle jsonCircle = null;
            CustomSerializer.SerializeToJson(circle, @"E:\study\oop\ConsoleApp13\ConsoleApp13\JSON.json");
            jsonCircle = CustomSerializer.DeserializeFromJson<Circle>(@"E:\Figures.json");
            Console.WriteLine($"JSON Deserialization: {jsonCircle}\n");
        }

        public static void Task2()
        {
            Console.WriteLine("\n--- Task 2: Collection Serialization and Deserialization ---");

            var figures = new List<Figure>
            {
             new Circle(3),
             new Rectangle(5, 8),
             new Circle(7)
             };

            var deserializedFigures = new List<Figure>();


            var xmlSerializer = new XmlSerializer(typeof(List<Figure>), new Type[] { typeof(Circle), typeof(Rectangle) });

            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\XML.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, figures);
            }

            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\XML.xml", FileMode.Open))
            {
                deserializedFigures = xmlSerializer.Deserialize(fs) as List<Figure>;
            }

            Console.WriteLine("Deserialized Collection:");
            foreach (var figure in deserializedFigures)
            {
                Console.WriteLine(figure);
            }
        }


        public static void Task3()
        {
            Console.WriteLine("\n--- Task 3: XPath Queries ---");

            var xDoc = new XmlDocument();
            xDoc.Load(@"E:\study\oop\ConsoleApp13\ConsoleApp13\XML.xml");

            var namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            var root = xDoc.DocumentElement;

            Console.WriteLine("\nAll Circles:");
            var circles = root.SelectNodes("//Figure[@xsi:type='Circle']", namespaceManager);

            foreach (XmlNode circle in circles)
            {
                Console.WriteLine(circle.InnerXml);
            }

            Console.WriteLine("\nAll Rectangles:");
            var rectangles = root.SelectNodes("//Figure[@xsi:type='Rectangle']", namespaceManager);

            foreach (XmlNode rectangle in rectangles)
            {
                Console.WriteLine(rectangle.InnerXml);
            }
        }



        public static void Task4()
        {
            Console.WriteLine("\n--- Task 4: Linq to JSON ---");

            var jsonArray = new JsonArray(
                new JsonObject
                {
                    ["type"] = "Circle",
                    ["radius"] = 5,
                    ["area"] = Math.PI * 5 * 5
                },
                new JsonObject
                {
                    ["type"] = "Rectangle",
                    ["width"] = 4,
                    ["height"] = 6,
                    ["area"] = 4 * 6
                }
            );

            string path = @"E:\study\oop\ConsoleApp13\ConsoleApp13\JSON.json";
            File.WriteAllText(path, jsonArray.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            string jsonContent = File.ReadAllText(path);
            var loadedJsonArray = JsonNode.Parse(jsonContent)!.AsArray();

            Console.WriteLine("Введите тип фигуры (Circle или Rectangle):");
            string type = Console.ReadLine();
            var filteredByType = loadedJsonArray.Where(node => node["type"]?.ToString() == type);
            foreach (var item in filteredByType)
            {
                Console.WriteLine(item);
            }
        }  
    }
}
