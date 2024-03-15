using Calculator;

namespace TestCalculator
{
    public class CalculatorOperationTest
    {
       // [SetUp]
        public void Setup()
        {
        }
      
        // error: false - нет ошибки, true - есть ошибка операции 
        [TestCase(5, 10, 15, false)]
        [TestCase(5.4, 10, 15.4, false)]
        [TestCase(5.3, 10.7, 16, false)]
        [TestCase(10, -1, 9, false)]
        [TestCase(10, -50, -40, false)]
        [TestCase(-10, -50, -60, false)]
        [TestCase(0, 0, 0, false)]
        public static void Add(double a, double b, double expected, bool error)
        {
            var math = new CalculatorOperation();
            var actual = math.Add(a, b);
            var hasError = math.IsErrorInLastOperation(); // передаем наш флаг
            var errorDesc = math.GetLastErrorDescription(); // передаем наше последнее сообщение об ошибке
            //Проверяем все изменения связанные с методом: флаг ошибки, последнее описание ошибки и сам результат 
            Assert.That(error, Is.EqualTo(hasError)); 
            Assert.That(errorDesc, Is.EqualTo("")); 
            Assert.That(Math.Abs(expected - actual) < 1e-12, Is.EqualTo(true));
        }


        [TestCase(10, 5, 5, false)]
        [TestCase(20, 30, -10, false)]
        [TestCase(10.3, 5, 5.3, false)] 
        [TestCase(40.5, 20.5, 20, false)]
        [TestCase(100, 0, 100, false)]
        [TestCase(0, 50, -50, false)]
        [TestCase(0, 0, 0, false)]
        public void Sub(double a, double b, double expected, bool error)
        {
            var math = new CalculatorOperation();
            var actual = math.Sub(a, b);
            var hasError = math.IsErrorInLastOperation();
            var errorDesc = math.GetLastErrorDescription();
            Assert.That(error, Is.EqualTo(hasError));
            Assert.That(errorDesc, Is.EqualTo(""));
            Assert.That(Math.Abs(expected - actual) < 1e-12, Is.EqualTo(true)); ;
         }


         [TestCase(20, 10, 200, false)]
         [TestCase(5, 8, 40, false)]
         [TestCase(5.4, 10, 54, false)] 
         [TestCase(5.5, 10.7, 58.85, false)] 
         [TestCase(-5, -2, 10, false)]
         [TestCase(5, -2, -10, false)]
         [TestCase(100, 0, 0, false)]
         [TestCase(0, 50, 0, false)]
         [TestCase(0, 0, 0, false)]
         public void Multip(double a, double b, double expected, bool error)
         {
            var math = new CalculatorOperation();
            var actual = math.Multip(a, b);
            var hasError = math.IsErrorInLastOperation();
            var errorDesc = math.GetLastErrorDescription();
            Assert.That(error, Is.EqualTo(hasError));
            Assert.That(errorDesc, Is.EqualTo(""));
            Assert.That(Math.Abs(expected - actual) < 1e-12, Is.EqualTo(true)); ;
         }


         [TestCase(10, 2, 5, false, "")]
         [TestCase(500, 5, 100, false, "")]
         [TestCase(2, 10, 0.2, false, "")]
         [TestCase(10, -2, -5, false, "")]
         [TestCase(-150, 5, -30, false, "")]
         [TestCase(0, 50, 0, false, "")]
         [TestCase(10, 0, 0, true, "Разделить на ноль нельзя")]
         [TestCase(0, 0, 0, true, "Разделить на ноль нельзя")]
         [TestCase(6.4, 2, 3.2, false, "")] 
         [TestCase(10, 0.2, 50, false, "")] 
         public void Div(double a, double b, double expected, bool error, string description)
         {
            var math = new CalculatorOperation();
            var actual = math.Div(a, b);
            var hasError = math.IsErrorInLastOperation();
            var errorDesc = math.GetLastErrorDescription();
            Assert.That(error, Is.EqualTo(hasError));
            Assert.That(errorDesc, Is.EqualTo(description));
            Assert.That(Math.Abs(expected - actual) < 1e-12, Is.EqualTo(true)); ;
         }

         [TestCase("a", "b", false, true, "Введено не число")]
         [TestCase("1", "2", true, false, "")] //  числа, нет ошибки 
         [TestCase("1.1", "2", true, false, "")] // числа с точкой, нет ошибки
         [TestCase("1", "2.1", true, false, "")]
         [TestCase("1,1", "2", true, false, "")] // числа с запятой, нет ошибки 
         [TestCase("1", "2,1", true, false, "")]
         public void IsNumber(string a, string b, bool expected, bool error, string description)
         {
            var math = new CalculatorOperation();
            var actual = math.IsNumber(a, b);
            var hasError = math.IsErrorInLastOperation();
            var errorDesc = math.GetLastErrorDescription();
            Assert.That(error, Is.EqualTo(hasError));
            Assert.That(errorDesc, Is.EqualTo(description));
            Assert.That(expected, Is.EqualTo(actual));
         }

         [TestCase("+", "Операция: +")]
         [TestCase("-", "Операция: -")]
         [TestCase("*", "Операция: *")]
         [TestCase("/", "Операция: /")]
         [TestCase("++", "Введена не корректная операция")]
         [TestCase(")", "Введена не корректная операция")]
         public void Operat(string a, string expected)
         {
            var math = new CalculatorOperation();
            var actual = math.Operat(a);
            Assert.That(expected, Is.EqualTo(actual));
          }
      
    }
}