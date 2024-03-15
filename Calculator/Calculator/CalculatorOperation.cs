using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorOperation
    {
        private bool hasError = false; // Флаг: нет ошибки - false, есть ошибка - true.
        private string lastErrorDescription = "";

        public const string errorNoError = "";
        public const string errorNoNumber = "Введено не число";
        public const string errorDivByZero = "Разделить на ноль нельзя";
        public const string errorNoOperation = "Введена не корректная операция";

        // Создаем разные методы для обработки ошибок:
        // Если нет ошибки
        private void SetNoError()
        {
            hasError = false;
            lastErrorDescription = errorNoError;
        }

        // Если есть ошибка
        private void SetError(string errorMessage)
        {
            hasError = true;
            lastErrorDescription = errorMessage;
        }

        // Возврат флага ошибки при выполнении последней операции 
        public bool IsErrorInLastOperation()
        {
            return hasError;
        }
        // Возврат сообщения последней ошибки
        public string GetLastErrorDescription()
        {
            return lastErrorDescription;
        }


        public double Add(double num1, double num2)
        {
            SetNoError();
            return num1 + num2;
        }
        public double Sub(double num1, double num2)
        {
            SetNoError();
            return num1 - num2;
        }

        public double Multip(double num1, double num2)
        {
            SetNoError();
            return num1 * num2;
        }

        public double Div(double num1, double num2)
        {
            if (num2 == 0)
            {
                SetError(errorDivByZero);
                return 0; // Возвращаем 0 и записываем ошибку "Разделить на ноль нельзя".
            }
            SetNoError();
            return num1 / num2;

        }

        // Если числа 1 и 2 из строки не смогли перобразовать в число, то возвращаем false и записываем ошибку "Введено не число". Иначе ошибки нет, выводим true.
        public bool IsNumber(string num1, string num2)
        {
            double val = 0;
            if (!Double.TryParse(num1.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out val))
            {
                SetError(errorNoNumber);
                return false;
            }
            if (!Double.TryParse(num2.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out val))
            {
                SetError(errorNoNumber);
                return false;
            }
            SetNoError();
            return true;
        }

        public string Operat(string str)
        {
            if (str.Length > 1 || !"+-*/".Contains(str))
            {
                SetError(errorNoOperation);
                return errorNoOperation;
            }
            SetNoError();
            return "Операция: " + str;
        }

    }
}
