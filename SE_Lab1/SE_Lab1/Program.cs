using System;
using System.Text;
using System.Transactions;

namespace ConsoleApplication1
{
    class FirstLab
    {
        static void Main()
        {
            // task1 a

            int intValue = 25;               
            long longValue = 99999999999999;  
            short shortValue = 12000;          
            byte byteValue = 255;              

            float floatValue = 3.14f;          
            double doubleValue = 3.1458979;    
            decimal decimalValue = 348564392.89m; 

            bool boolValue = true;             

            char charValue = 'V';              

            string stringValue = "Hello World"; 

            Console.WriteLine("Значения переменных:");
            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"long: {longValue}");
            Console.WriteLine($"short: {shortValue}");
            Console.WriteLine($"byte: {byteValue}");
            Console.WriteLine($"float: {floatValue}");
            Console.WriteLine($"double: {doubleValue}");
            Console.WriteLine($"decimal: {decimalValue}");
            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"string: {stringValue}");

            Console.WriteLine("\nВведите значения переменных:");

            Console.Write("Введите int: ");
            intValue = int.Parse(Console.ReadLine());

            Console.Write("Введите float: ");
            floatValue = float.Parse(Console.ReadLine()); 

            Console.Write("Введите bool (true/false): ");
            boolValue = bool.Parse(Console.ReadLine()); 

            Console.Write("Введите char: ");
            charValue = char.Parse(Console.ReadLine()); 

            Console.Write("Введите string: ");
            stringValue = Console.ReadLine(); 

            Console.WriteLine("\nВведенные значения:");
            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"float: {floatValue}");
            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"string: {stringValue}");

            Console.WriteLine("\n_______________________________________\n");

            // task1 b

            Console.WriteLine("Явное приведение");

            
            double dValue = 6.78;
            int iValue = (int)dValue;
            Console.WriteLine("double -> int = " + iValue); 

            long lValue = 50000;
            short sValue = (short)lValue;
            Console.WriteLine("long -> short = " + sValue); 

            float fValue = 8.99f;
            int inValue = (int)fValue;
            Console.WriteLine("float -> int = " + inValue); 

            int bigInt = 255;
            byte byValue = (byte)bigInt;
            Console.WriteLine("int -> byte = " + byValue); 

            string str = "999";
            int numValue = int.Parse(str);
            Console.WriteLine("string -> int = " + numValue); 

            Console.WriteLine(" ");
            Console.WriteLine("Неявное приведение");

            int iNum = 100;
            long lNum = iNum;
            Console.WriteLine("int -> long = " + lNum); 

            short sNum = 32000;
            int inNum = sNum;
            Console.WriteLine("short -> int = " + inNum); 

            float fNum = 3.14f;
            double dNum = fNum;
            Console.WriteLine("float -> double = " + dNum); 

            char charNum = 'A';
            int charToInt = charNum;
            Console.WriteLine("char -> int = " + charToInt); 

            byte byteNum = 255;
            int newIntValue = byteNum;
            Console.WriteLine("byte -> int = " + newIntValue); 

            Console.WriteLine("\n_______________________________________\n");

            // task1 c

            Console.WriteLine(" ");
            Console.WriteLine("Упаковка и распаковка значимых тпиов");

            int i = 123;
            object obj = i;
            Console.WriteLine("Упаковка (Boxing): " + obj);

            int j = (int)obj;
            Console.WriteLine("Распаковка (Unboxing): " + j);

            Console.WriteLine("\n_______________________________________\n");

            // task1 d

            Console.WriteLine(" ");
            Console.WriteLine("Неявно типизированная переменная");

            var dateValue = DateTime.Now;
            Console.WriteLine("dateValue (тип DateTime): " + dateValue);

            Console.WriteLine("\n_______________________________________\n");


            // task1 e

            Console.WriteLine(" ");
            Console.WriteLine("Nullable");

            int? variableIntNullabel = null; 
            variableIntNullabel = 5;
            Console.WriteLine("работа с Nullable переменной " + variableIntNullabel);

            // task1 f
            //var myVariable = 10; 
            //myVariable = "Hello";

            Console.WriteLine("\n_______________________________________\n");

            // task2 a

            string string1 = "Hello!";
            string string2 = "Hello!";
            string string3 = "hello!";

            bool areEqual1 = string1 == string2;  
            bool areEqual2 = string1 == string3;  

            Console.WriteLine($"string1 == string2: {areEqual1}");
            Console.WriteLine($"string1 == string3: {areEqual2}");

            Console.WriteLine("\n_______________________________________\n");

            //task2 b

            string str1 = "Hello";
            string str2 = "World";
            string str3 = "programming programm";

            string concatenated = string.Concat(str1, " ", str2);
            Console.WriteLine($"Сцепление строк: {concatenated}");

            string copied = str3;
            Console.WriteLine($"Копирование строки: {copied}");

            string substring = str3.Substring(1, 8);
            Console.WriteLine($"Выделение подстроки: {substring}");  

            // Разделение строки на слова
            string[] words = str3.Split(' ');
            Console.WriteLine("Слова:");
            foreach (var word in words)  
            {
                Console.WriteLine(word);
            }

            string inserted = str3.Insert(11, " cool ");
            Console.WriteLine($"Вставка подстроки: {inserted}");

            string removed = str3.Remove(12, 8);
            Console.WriteLine($"Удаление подстроки: {removed}");

            string interpolated = $"Интерполяция подстроки: {str1} {str2} is great for {str3}";
            Console.WriteLine(interpolated);

            Console.WriteLine("\n_______________________________________\n");

            //task2 c

            string str7 = "";          
            string str8 = null;        
            string str9 = "   \t   "; 

            if (String.IsNullOrEmpty(str7))
                Console.WriteLine("Str7 пустая или null-строка");
            else
                Console.WriteLine("Str7 не null-строка или не пустая"); 


            if (String.IsNullOrEmpty(str8))
                Console.WriteLine("Str8 пустая или null-строка");  

            if (String.IsNullOrEmpty(str9))
                Console.WriteLine("Str9 пустая или null-строка");
            else
                Console.WriteLine("Str9 не null-строка или не пустая"); 

            if (String.IsNullOrWhiteSpace(str7))
                Console.WriteLine("Str7 пустая или null-строка или строка из пробелов");  


            if (String.IsNullOrWhiteSpace(str8))
                Console.WriteLine("Str8 пустая или null-строка или строка из пробелов");  

            if (String.IsNullOrWhiteSpace(str9))
                Console.WriteLine("Str9 пустая или null-строка или строка из пробелов");   
            Console.WriteLine("\n_______________________________________\n");

            //task2 d

            StringBuilder str10 = new StringBuilder(" an old");
            str10.Remove(2, 5);
            str10.Insert(0, "This is");
            str10.Append(" new string");
            Console.WriteLine(str10);

            // task3 a
            int[,] matrix = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

            Console.WriteLine("Matrix:");
            int rows = matrix.GetLength(0);  
            int cols = matrix.GetLength(1);  

            for (int R = 0; R < rows; R++)   
            {
                for (int J = 0; J < cols; J++) 
                {
                    Console.Write($"{matrix[R, J],4}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n_______________________________________\n");

            // task2 b


            string[] stringArray = { "Violetta", "Anna", "Nelli" };

            Console.WriteLine("Содержимое массива:");      
            foreach (string stringMatrix in stringArray)
            {
                Console.WriteLine(stringMatrix);           
            }

            Console.WriteLine($"Длина массива: {stringArray.Length}");  

            Console.Write("Введите индекс элемента, который требуется изменить :", stringArray.Length - 1);

            int index = int.Parse(Console.ReadLine());  

            if (index >= 0 && index < stringArray.Length) 
            {
                Console.Write("Введите новое значение: ");  

                string newValue = Console.ReadLine();

                stringArray[index] = newValue; 

                Console.WriteLine("Обновленное содержимое массива:");
                foreach (string stringMatrix in stringArray)
                {
                    Console.WriteLine(stringMatrix); 
                }
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }

            Console.WriteLine("\n_______________________________________\n");

            // task2 c
            double[][] jaggedArray = new double[3][];

            jaggedArray[0] = new double[2]; 
            jaggedArray[1] = new double[3];
            jaggedArray[2] = new double[4]; 

            Console.WriteLine("Введите значения для ступенчатого массива:");

            for (int ir = 0; ir < jaggedArray.Length; ir++)
            {
                Console.WriteLine($"Введите значения для строки {ir + 1} (длина {jaggedArray[ir].Length}):");
                for (int jr = 0; jr < jaggedArray[ir].Length; jr++)
                {
                    Console.Write($"Значение на позиции [{ir},{jr}]: ");
                    jaggedArray[ir][jr] = double.Parse(Console.ReadLine()); 
                }
            }

            Console.WriteLine("Содержимое ступенчатого массива:");
            for (int ik = 0; ik < jaggedArray.Length; ik++)
            {
                Console.Write($"Строка {ik + 1}: ");
                for (int jk = 0; jk < jaggedArray[ik].Length; jk++)
                {
                    Console.Write($"{jaggedArray[ik][jk]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n_______________________________________\n");

            // task3 d
            var stValue = "Это строка";
            Console.WriteLine($"Содержание строки: {stValue}");

            Console.WriteLine("\n_______________________________________\n");

            var stArray = new[] { "Apple", "Banana", "Cherry" }; 
            Console.WriteLine("Содержимое массива:"); 
            foreach (var item in stArray) 
            {
                Console.WriteLine(item); 
            }

            Console.WriteLine("\n_______________________________________\n");

            // task4 a,b
            var myTuple = (25, "qwe", 'S', "asd");
            Console.WriteLine($"Весь кортеж: {myTuple}");
            Console.WriteLine($"1-ый элемент: {myTuple.Item1}");
            Console.WriteLine($"3-ий элемент: {myTuple.Item3}");
            Console.WriteLine($"4-ый элемент: {myTuple.Item4}");

            // task4 c
            var (intAlue, stringValue1, charAlue, stringValue2) = myTuple;
            Console.WriteLine($"Число: {intAlue}, Первоя строка: {stringValue1}, Символ: {charAlue}, Втоорая строка: {stringValue2}");

            // Распаковка с использованием 
            var (_, _, _, secondString) = myTuple;
            Console.WriteLine($"Вторая строка (используем _): {secondString}");

            // task4 d
            var anotherTuple = (06, "zxc", 'B', "qwerty");
            Console.WriteLine($"Второй кортеж: {anotherTuple}");
            Console.WriteLine($"Кортеж равен другому кортежу: {myTuple.Equals(anotherTuple)}");

            Console.WriteLine("\n_______________________________________\n");

            // task5
            (int max, int min, int sum, char firstChar) AnalyzeArray(int[] numbers, string text)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Массив не может быть пустым");

                int max = numbers[0];
                int min = numbers[0];
                int sum = 0;

                foreach (int num in numbers)
                {
                    if (num > max) max = num;
                    if (num < min) min = num;
                    sum += num;
                }

                char firstChar = text.Length > 0 ? text[0] : '\0';

                return (max, min, sum, firstChar);
            }

            int[] numbers = { 1, 2, 3, 4, 5 };
            string text = "Hello world:*";

            var result = AnalyzeArray(numbers, text);

            Console.WriteLine($"Строка массва: {text}");

            Console.WriteLine($"Максимальный элемент: {result.max}, Минимальный элемент: {result.min}, Сумма: {result.sum}, Первый символ: {result.firstChar}");

            Console.WriteLine("\n_______________________________________\n");

            // task6

            void CheckedOperation()
            {
                checked
                {
                    try
                    {
                        int maxValue = int.MaxValue;
                        int result = maxValue + 1; // Вызовет переполнение
                    }
                    catch (OverflowException)
                    {
                        // Если переполнение происходит, выбрасывается исключение OverflowException
                        Console.WriteLine("Checked: Переполнение.");
                    }
                }
            }

            void UncheckedOperation()
            {
                unchecked
                {
                    int maxValue = int.MaxValue;
                    // Результат будет отрицательным числом (переполнение)
                    int result = maxValue + 1; // Не вызовет исключение
                    Console.WriteLine($"Unchecked: Результат переполнения {result}");
                }
            }

            CheckedOperation();
            UncheckedOperation();

        }
    }
}
    