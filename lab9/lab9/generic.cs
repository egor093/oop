using System;
using System.Collections.Generic;
using System.Linq;

public class GenericCollectionsDemo
{
    public static void DemonstrateGenericCollections()
    {
        Console.WriteLine(" ДЕМОНСТРАЦИЯ УНИВЕРСАЛЬНЫХ КОЛЛЕКЦИЙ \n");

        // a. Создание и заполнение Stack<int>
        Stack<int> intStack = new Stack<int>();
        for (int i = 1; i <= 10; i++)
        {
            intStack.Push(i * 10);
        }

        Console.WriteLine("a. Исходная коллекция Stack<int>:");
        PrintStack(intStack);

        // b. Удаление n последовательных элементов
        int n = 3;
        Console.WriteLine($"\nb. Удаление {n} элементов:");
        for (int i = 0; i < n && intStack.Count > 0; i++)
        {
            int removed = intStack.Pop();
            Console.WriteLine($"Удален элемент: {removed}");
        }

        Console.WriteLine("Стек после удаления:");
        PrintStack(intStack);

        // c. Добавление других элементов
        Console.WriteLine("\nc. Добавление новых элементов:");
        int[] newElements = { 300, 350, 400 };
        foreach (int element in newElements)
        {
            intStack.Push(element);
        }

        Console.WriteLine("Стек после добавления:");
        PrintStack(intStack);

        // d. Создание второй коллекции (Dictionary) из первой
        Console.WriteLine("\nd. Создание Dictionary из Stack:");
        Dictionary<int, int> intDictionary = new Dictionary<int, int>();
        int key = 1;

        foreach (int value in intStack.Reverse()) 
        {
            intDictionary.Add(key++, value);
        }

        // e. Вывод второй коллекции
        Console.WriteLine("\ne. Вторая коллекция Dictionary<int, int>:");
        foreach (var kvp in intDictionary)
        {
            Console.WriteLine($"Ключ: {kvp.Key}, Значение:{kvp.Value}");
        }

        // f. Поиск значения во второй коллекции
        int searchValue = 200;
        Console.WriteLine($"\nf. Поиск значения {searchValue} в Dictionary:");
        var foundItem = intDictionary.FirstOrDefault(x => x.Value == searchValue);
        if (foundItem.Value == searchValue)
        {
            Console.WriteLine($"Значение {searchValue} найдено! Ключ: {foundItem.Key}");
        }
        else
        {
            Console.WriteLine($"Значение {searchValue} не найдено!");
        }
    }

    private static void PrintStack(Stack<int> stack)
    {
        if (stack.Count == 0)
        {
            Console.WriteLine("Стек пуст!");
            return;
        }

        Console.WriteLine(string.Join(" <- ", stack.Reverse()));
        Console.WriteLine($"Вершина стека: {stack.Peek()}");
    }
}