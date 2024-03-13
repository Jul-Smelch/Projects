using System;
using System.Globalization;

namespace Calculator1
{
    internal class Program
    {
        public static void Main()
        {
            double num1 = 0;
            double num2 = 0;

            Console.WriteLine("Привет. Это калькулятор!");

            //Ввод 1 числа с проверкой, если введено не число. Дополнительно число с плавающей запятой меняем на число с плавающей точкой.
            Console.WriteLine("Введи первое число.");
            while (!Double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out num1))
            {
                Console.WriteLine("Введено не число, попробуй еще раз. Введи первое число.");
            }

            //Ввод символа операции с проверкой, если введена строка больше 1 символа или символ не из допустимых символов операций
            Console.WriteLine("Введи операцию '+, -, *, /'");
            string str = Console.ReadLine();
            while (str.Length > 1 || !"+-*/".Contains(str))
            {
                Console.WriteLine("Введена не корректная операция, попробуй еще раз. Введи операцию '+, -, *, /'");
                str = Console.ReadLine(); 

            }

            //Ввод 2 числа с проверкой, если введено не число. Дополнительно число с плавающей запятой меняем на число с плавающей точкой.
            Console.WriteLine("Введи второе число.");
            while (!Double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
            {
                Console.WriteLine("Введено не число, попробуй еще раз. Введи второе число.");
            }

            char operation = Convert.ToChar(str);
            double result = 0;
            switch (operation)
            {
                case '+':
                        result = num1 + num2;
                        Console.WriteLine("Результат: " + result);
                        Console.WriteLine("Для выхода нажми любую клавишу.");
                        Console.ReadKey();
                        break;
                case '-':
                        result = num1 - num2;
                        Console.WriteLine("Результат: " + result);
                        Console.WriteLine("Для выхода нажми любую клавишу.");
                        Console.ReadKey();
                        break;
  
                case '*':
                         result = num1 * num2;
                        Console.WriteLine("Результат: " + result);
                        Console.WriteLine("Для выхода нажмите любую клавишу.");
                        Console.ReadKey();
                        break;
                 case '/':
                         //Проверка деления на ноль
                        while (num2 == 0)
                        {
                            Console.WriteLine("Разделить на ноль нельзя, попробуй еще раз. Введи второе число.");
                            //Присваиваем снова второе число, которое может оказаться не числом.
                            while (!Double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
                            {
                                Console.WriteLine("Введено не число, попробуй еще раз. Введи второе число.");
                            }
                        }
                        result = num1 / num2;
                        Console.WriteLine("Результат: " + result);
                        Console.WriteLine("Для выхода нажми любую клавишу.");
                        Console.ReadKey();
                        break;
                  default: 
                        Console.WriteLine("Введите допустимую операцию '+, -, *, /'");
                        break;
             }
                        
        }
    }
}
