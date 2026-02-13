using System;
using System.Collections;
using System.Collections.Generic;

namespace Stack
{
    public class MyStack : IEnumerable<int>
    {
        private List<int> elements = new List<int>();

        // Конструкторы
        public MyStack() { }
        public MyStack(IEnumerable<int> items)
        {
            if (items != null) elements.AddRange(items);
        }

        // Индексатор
        public int this[int index]
        {
            get => elements[index];
            set => elements[index] = value;
        }

        // Добавление элемента
        public void Push(int item) => elements.Add(item);

        // Извлечение элемента (возвращает элемент и изменяет стек)
        public int Pop()
        {
            if (elements.Count == 0) throw new InvalidOperationException("Стек пуст");
            int item = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            return item;
        }

        // Свойство размера
        public int Count => elements.Count;

        public override string ToString() => $"[{string.Join(", ", elements)}]";

        // Реализация IEnumerable<int> чтобы можно было итерировать
        public IEnumerator<int> GetEnumerator() => elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // ---------------- Перегрузки операторов ----------------

        // + добавить элемент в стек (возвращает новый стек, исходный не меняется)
        public static MyStack operator +(MyStack stack, int item)
        {
            if (stack == null) stack = new MyStack();
            var newStack = new MyStack(stack.elements);
            newStack.Push(item);
            return newStack;
        }

        // -— извлечь элемент из стека (возвращаем новый стек без верхнего элемента)
        public static MyStack operator --(MyStack stack)
        {
            if (stack == null) return new MyStack();
            var newStack = new MyStack(stack.elements);
            if (newStack.Count > 0)
                newStack.Pop();
            return newStack;
        }

        // true/false — проверка пуст ли стек
        public static bool operator true(MyStack stack) => stack == null || stack.Count == 0;
        public static bool operator false(MyStack stack) => !(stack == null || stack.Count == 0);

        // > и < — сравнение по количеству элементов (пара операторов).
        public static bool operator >(MyStack a, MyStack b)
        {
            if (ReferenceEquals(a, b)) return false;
            int ca = a?.Count ?? 0;
            int cb = b?.Count ?? 0;
            return ca > cb;
        }
        public static bool operator <(MyStack a, MyStack b)
        {
            if (ReferenceEquals(a, b)) return false;
            int ca = a?.Count ?? 0;
            int cb = b?.Count ?? 0;
            return ca < cb;
        }

        // ^ — объединение содержимого двух стеков и сортировка по возрастанию
        public static MyStack operator ^(MyStack a, MyStack b)
        {
            var merged = new List<int>();
            if (a != null) merged.AddRange(a.elements);
            if (b != null) merged.AddRange(b.elements);
            merged.Sort();
            return new MyStack(merged);
        }

        public int[] ToArray() => elements.ToArray();

        // Вложенные классы
        public class Production
        {
            public int Id { get; set; }
            public string OrganizationName { get; set; }

            public Production(int id, string name)
            {
                Id = id;
                OrganizationName = name;
            }

            public override string ToString() => $"Production: {OrganizationName} (ID={Id})";
        }

        public class Developer
        {
            public string FullName { get; set; }
            public int Id { get; set; }
            public string Department { get; set; }

            public Developer(string fullName, int id, string department)
            {
                FullName = fullName;
                Id = id;
                Department = department;
            }

            public override string ToString() => $"Developer: {FullName}, Dept: {Department}, ID={Id}";
        }

        // Вложенные объекты по умолчанию
        public Production ProdInfo { get; set; } = new Production(1, "TechCorp");
        public Developer DevInfo { get; set; } = new Developer("Иванов Иван", 101, "R&D");
    }

    public static class StatisticOperation
    {
        // 1) Сумма элементов
        public static int Sum(MyStack stack)
        {
            if (stack == null) return 0;
            int sum = 0;
            foreach (int v in stack)
                sum += v;
            return sum;
        }

        // 2) Разница между макс. и мин. 
        public static int Difference(MyStack stack)
        {
            if (stack == null) return 0;
            bool any = false;
            int min = 0, max = 0;
            foreach (int v in stack)
            {
                if (!any)
                {
                    min = max = v;
                    any = true;
                }
                else
                {
                    if (v < min) min = v;
                    if (v > max) max = v;
                }
            }
            return any ? (max - min) : 0;
        }

        // 3) Количество элементов
        public static int Count(MyStack stack) => stack?.Count ?? 0;

        // ===== Методы расширения =====

        // 1) Подсчет количества предложений в строке
        public static int SentenceCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return 0;
            char[] delimiters = { '.', '!', '?' };
            int count = 0;
            int currentLen = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                bool isDelimiter = false;
                for (int d = 0; d < delimiters.Length; d++)
                    if (c == delimiters[d]) { isDelimiter = true; break; }

                if (isDelimiter)
                {
                    if (currentLen > 0)
                    {
                        count++;
                        currentLen = 0;
                    }
                }
                else
                {
                    if (!char.IsWhiteSpace(c))
                        currentLen++;
                }
            }
            if (currentLen > 0) count++;
            return count;
        }

        // 2) Определение среднего элемента стека 
        public static double AverageElement(this MyStack stack)
        {
            if (stack == null) return 0;
            long sum = 0;
            long cnt = 0;
            foreach (int v in stack)
            {
                sum += v;
                cnt++;
            }
            return cnt == 0 ? 0 : (double)sum / cnt;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                MyStack s1 = new MyStack(new[] { 3, 1, 4 });
                MyStack s2 = new MyStack(new[] { 5, 9, 2, 7 });

                Console.WriteLine("Исходные стеки:");
                Console.WriteLine($"s1 = {s1}");
                Console.WriteLine($"s2 = {s2}");

                // + добавить элемент
                s1 = s1 + 10;
                Console.WriteLine("\nПосле добавления элемента (s1 + 10):");
                Console.WriteLine($"s1 = {s1}");

                // -- извлечь элемент
                s1 = --s1;
                Console.WriteLine("\nПосле удаления верхнего (-s1):");
                Console.WriteLine($"s1 = {s1}");

                // ^ копирование с сортировкой (в возрастающем порядке)
                MyStack s3 = s1 ^ s2;
                Console.WriteLine("\nКопирование с сортировкой по возрастанию (s1 ^ s2):");
                Console.WriteLine($"s3 = {s3}");

                // Проверка пустоты через оператор true/false
                Console.WriteLine($"\nПустой ли s1? {(s1 ? "Да (оператор true возвращает true => стек пуст)" : "Нет (оператор false)")}");

                // StatisticOperation
                Console.WriteLine($"\nСумма элементов s3: {StatisticOperation.Sum(s3)}");
                Console.WriteLine($"Разница между макс. и мин.: {StatisticOperation.Difference(s3)}");
                Console.WriteLine($"Количество элементов: {StatisticOperation.Count(s3)}");
                Console.WriteLine($"Средний элемент: {s3.AverageElement():F2}");

                // Метод расширения для строки
                string text = "Привет! Как дела? Всё хорошо.";
                Console.WriteLine($"\nКоличество предложений в строке: \"{text}\" = {text.SentenceCount()}");

                // Вложенные классы
                Console.WriteLine($"\n{s1.ProdInfo}");
                Console.WriteLine($"{s1.DevInfo}");

                // Демонстрация сравнения >/< (по количеству элементов)
                Console.WriteLine($"\ns1.Count = {s1.Count}, s2.Count = {s2.Count}");
                Console.WriteLine($"s1 > s2 ? {(s1 > s2)}");
                Console.WriteLine($"s1 < s2 ? {(s1 < s2)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
