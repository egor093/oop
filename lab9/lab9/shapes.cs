using System;
using System.Collections;
using System.Collections.Generic;

// Класс Геометрическая фигура
public class GeometricShape : IEnumerator
{
    public string Name { get; set; }
    public double Area { get; set; }
    public string Color { get; set; }

    private int position = -1;
    private static GeometricShape[] shapes;

    public GeometricShape() { }

    public GeometricShape(string name, double area, string color)
    {
        Name = name;
        Area = area;
        Color = color;
    }

    // Реализация IEnumerator
    public object Current
    {
        get
        {
            try
            {
                return shapes[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public bool MoveNext()
    {
        position++;
        return position < shapes.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    public override string ToString()
    {
        return $"{Name} (Площадь: {Area}, Цвет: {Color})";
    }
}

// Класс для управления коллекцией
public class ShapeCollection
{
    private Stack<GeometricShape> shapesStack;

    public ShapeCollection()
    {
        shapesStack = new Stack<GeometricShape>();
    }

    // Добавление фигуры
    public void AddShape(GeometricShape shape)
    {
        shapesStack.Push(shape);
        Console.WriteLine($"Добавлена фигура: {shape}");
    }

    // Удаление фигуры
    public void RemoveShape()
    {
        if (shapesStack.Count > 0)
        {
            var shape = shapesStack.Pop();
            Console.WriteLine($"Удалена фигура: {shape}");
        }
        else
        {
            Console.WriteLine("Стек пуст!");
        }
    }

    // Поиск фигуры по имени
    public void FindShapeByName(string name)
    {
        bool found = false;
        foreach (var shape in shapesStack)
        {
            if (shape.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Найдена фигура: {shape}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Фигура с именем '{name}' не найдена");
        }
    }

    // Вывод всех фигур
    public void DisplayAllShapes()
    {
        Console.WriteLine("\nВсе фигуры в стеке:");
        if (shapesStack.Count == 0)
        {
            Console.WriteLine("Стек пуст!");
            return;
        }

        int index = 1;
        foreach (var shape in shapesStack)
        {
            Console.WriteLine($"{index}. {shape}");
            index++;
        }
    }

    public Stack<GeometricShape> GetStack()
    {
        return shapesStack;
    }
}