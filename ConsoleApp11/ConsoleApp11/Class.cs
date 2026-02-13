public class ExampleClass
{
    public int Number { get; set; }
    public string Text { get; set; }

    public ExampleClass()
    {
        Number = 1;
        Text = "Egor";
    }

    public ExampleClass(int number, string text)
    {
        Number = number;
        Text = text;
    }

    public void PrintInfo(string message)
    {
        Console.WriteLine($"Number: {Number}, Text: {Text}, Message: {message}");
    }
}

public class AnotherClass
{
    public void TestMethod(int value)
    {
        Console.WriteLine($"Value: {value}");
    }
}
