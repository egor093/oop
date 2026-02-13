using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;

namespace OOP_lab13
{
    public static class CustomSerializer
    {
        public static void SerializeToBinary<T>(T obj) where T : class
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\Binary.bin", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, obj);
            }
        }

        public static void DeserializeFromBinary<T>(ref T container) where T : class
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\Binary.bin", FileMode.OpenOrCreate))
            {
                container = binaryFormatter.Deserialize(fs) as T;
            }
        }

        public static void SerializeToXml<T>(T obj) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T), new Type[] { typeof(Circle), typeof(Rectangle) });

            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\XML.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, obj);
            }
        }

        public static void DeserializefromXml<T>(ref T container) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T), new Type[] { typeof(Circle), typeof(Rectangle) });

            using (var fs = new FileStream(@"E:\study\oop\ConsoleApp13\ConsoleApp13\XML.xml", FileMode.Open))
            {
                container = xmlSerializer.Deserialize(fs) as T;
            }
        }

        public static void SerializeToJson<T>(T obj, string path) where T : class
        {
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public static T DeserializeFromJson<T>(string path) where T : class
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
