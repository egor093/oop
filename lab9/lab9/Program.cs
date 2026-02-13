using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Демонстрация работы с классом GeometricShape и Stack
        Console.WriteLine("1. РАБОТА С КОЛЛЕКЦИЕЙ ГЕОМЕТРИЧЕСКИХ ФИГУР\n");

        ShapeCollection shapeCollection = new ShapeCollection();

        // Добавление фигур
        shapeCollection.AddShape(new GeometricShape("Квадрат", 16, "Красный"));
        shapeCollection.AddShape(new GeometricShape("Круг", 12.5, "Синий"));
        shapeCollection.AddShape(new GeometricShape("Треугольник", 8.3, "Зеленый"));
        shapeCollection.AddShape(new GeometricShape("Прямоугольник", 24, "Желтый"));

        // Вывод всех фигур
        shapeCollection.DisplayAllShapes();

        // Поиск фигур
        Console.WriteLine("\nПоиск фигур:");
        shapeCollection.FindShapeByName("Круг");
        shapeCollection.FindShapeByName("Ромб");

        // Удаление фигур
        Console.WriteLine("\nУдаление фигур:");
        shapeCollection.RemoveShape();
        shapeCollection.RemoveShape();

        // Вывод оставшихся фигур
        shapeCollection.DisplayAllShapes();

        // 2. Демонстрация универсальных коллекций
        GenericCollectionsDemo.DemonstrateGenericCollections();

        // 3. Демонстрация наблюдаемой коллекции
        ObservableCollectionDemo.DemonstrateObservableCollection();
    }
}