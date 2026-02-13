using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class ObservableCollectionDemo
{
    private static ObservableCollection<GeometricShape> observableShapes;

    public static void DemonstrateObservableCollection()
    {
        Console.WriteLine("\nНАБЛЮДАЕМАЯ КОЛЛЕКЦИЯ\n");

        observableShapes = new ObservableCollection<GeometricShape>();
        observableShapes.CollectionChanged += OnCollectionChanged;

        // Добавление элементов
        Console.WriteLine("Добавление элементов:");
        observableShapes.Add(new GeometricShape("Квадрат", 25, "Красный"));
        observableShapes.Add(new GeometricShape("Круг", 30.5, "Синий"));
        observableShapes.Add(new GeometricShape("Треугольник", 15.2, "Зеленый"));

        // Удаление элемента
        Console.WriteLine("\nУдаление элемента:");
        if (observableShapes.Count > 0)
        {
            observableShapes.RemoveAt(1);
        }

        // Замена элемента
        Console.WriteLine("\nЗамена элемента:");
        if (observableShapes.Count > 0)
        {
            observableShapes[0] = new GeometricShape("Прямоугольник", 40, "Желтый");
        }

        // Очистка коллекции
        Console.WriteLine("\nОчистка коллекции:");
        observableShapes.Clear();
    }

    private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                Console.WriteLine($"Добавлен новый элемент: {e.NewItems[0]}");
                break;

            case NotifyCollectionChangedAction.Remove:
                Console.WriteLine($"Удален элемент: {e.OldItems[0]}");
                break;

            case NotifyCollectionChangedAction.Replace:
                Console.WriteLine($"Заменен элемент. Старый: {e.OldItems[0]}, Новый: {e.NewItems[0]}");
                break;

            case NotifyCollectionChangedAction.Reset:
                Console.WriteLine("Коллекция очищена!");
                break;

            case NotifyCollectionChangedAction.Move:
                Console.WriteLine("Элемент перемещен");
                break;
        }

        Console.WriteLine($"Текущее количество элементов: {observableShapes.Count}");
    }
}