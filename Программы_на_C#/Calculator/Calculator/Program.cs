using System;
using System.Globalization;

namespace Calculator
{
    public class Program
    {
        public static void Main()
        {
            double num1 = 0;
            double num2 = 0;

            var operat = new CalculatorOperation();

            Console.WriteLine("Привет. Это калькулятор!");

            // Ввод 1 числа с проверкой, если введено не число. Дополнительно число с плавающей запятой меняем на число с плавающей точкой.
            Console.WriteLine("Введи первое число.");
            while (!Double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out num1))
            {
                Console.WriteLine("Введено не число, попробуй еще раз. Введи первое число.");
            }

            // Ввод символа операции с проверкой, если введена строка больше 1 символа или символ не из допустимых символов операций
            Console.WriteLine("Введи операцию '+, -, *, /'");
            string str = Console.ReadLine();
            while (str.Length > 1 || !"+-*/".Contains(str))
            {
                Console.WriteLine("Введена не корректная операция, попробуй еще раз. Введи операцию '+, -, *, /'");
                str = Console.ReadLine();

            }

            // Ввод 2 числа с проверкой, если введено не число. Дополнительно число с плавающей запятой меняем на число с плавающей точкой.
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
                    result = operat.Add(num1, num2);
                    Console.WriteLine("Результат: " + result);
                    Console.WriteLine("Для выхода нажми любую клавишу.");
                    Console.ReadKey();
                    break;
                case '-':
                    result = operat.Sub(num1, num2);
                    Console.WriteLine("Результат: " + result);
                    Console.WriteLine("Для выхода нажми любую клавишу.");
                    Console.ReadKey();
                    break;

                case '*':
                    result = operat.Multip(num1, num2);
                    Console.WriteLine("Результат: " + result);
                    Console.WriteLine("Для выхода нажмите любую клавишу.");
                    Console.ReadKey();
                    break;
                case '/':
                    while (true)
                    {
                        result = operat.Div(num1, num2); // Запускаем операция деления, вовзвращаем число: 0 / результат деления.
                                                         // Проверяем, если была ошибка при делении, то выводим сообщении об ошибке и предлагаем ввести еще раз второе число.
                                                         // Если ошибки от деления нет, то выводим результат деления.
                        if (operat.IsErrorInLastOperation())
                        {
                            Console.WriteLine(operat.GetLastErrorDescription());
                            Console.WriteLine("Попробуй еще раз. Введи второе число.");
                            // Присваиваем снова второе число, которое может оказаться не числом.
                            while (!Double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
                            {
                                Console.WriteLine("Введено не число, попробуй еще раз. Введи второе число.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Результат: " + result);
                            Console.WriteLine("Для выхода нажми любую клавишу.");
                            Console.ReadKey();
                            return;
                        }
                    }
                default:
                    Console.WriteLine("Введите допустимую операцию '+, -, *, /'");
                    break;
            }

        }
    }
}
