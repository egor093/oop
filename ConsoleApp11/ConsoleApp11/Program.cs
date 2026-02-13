class Program
{
    static void Main()
    {
        string className = "ExampleClass";

        Console.WriteLine("---------------------a. Имя сборки---------------------------");

        Console.WriteLine($"Assembly Name: {Reflector.GetAssemblyName(className)}");

        Console.WriteLine("---------------------b. Публичные конструкторы---------------------------");

        Console.WriteLine($"Has Public Constructors: {Reflector.HasPublicConstructors(className)}");

        Console.WriteLine("----------------------c. Публичные методы--------------------------");

        Console.WriteLine("Public Methods:");
        foreach (var method in Reflector.GetPublicMethods(className))
        {
            Console.WriteLine(method);
        }

        Console.WriteLine("----------------------d. Поля и свойства--------------------------");

        Console.WriteLine("Fields and Properties:");
        foreach (var member in Reflector.GetFieldsAndProperties(className))
        {
            Console.WriteLine(member);
        }

        Console.WriteLine("Implemented Interfaces:");
        foreach (var iface in Reflector.GetImplementedInterfaces(className))
        {
            Console.WriteLine(iface);
        }

        Console.WriteLine("---------------------f. Методы с заданным типом параметра---------------------------");

        Console.WriteLine("Methods with string parameter:");
        foreach (var method in Reflector.GetMethodsByParameterType(className, typeof(string)))
        {
            Console.WriteLine(method);
        }

        Console.WriteLine("--------------------g. InvokeMethod----------------------------");

        ExampleClass example = new ExampleClass(5, "Hello");
        Reflector.InvokeMethod(example, "PrintInfo", new object[] { "Test message" });

        Console.WriteLine("---------------------Invoke с чтением параметров из файла---------------------------");

        string filePath = "E:\\study\\oop\\ConsoleApp11\\ConsoleApp11\\params.txt";
        object result = Reflector.InvokeMethodFromFile("ExampleClass", "PrintInfo", filePath);

        Console.WriteLine("--------------------Создание объекта через обобщённый метод----------------------------");

        ExampleClass newExample = Reflector.Create<ExampleClass>();
        Console.WriteLine($"Created Object: Number={newExample.Number}, Text={newExample.Text}");
    }
}
