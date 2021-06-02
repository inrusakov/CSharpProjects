using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using HSEApiTraining.Models.Events;

namespace HSEApiTraining
{
    // Делегат для события об ошибке.
    public delegate void ErrorTextHandler(object sender, ErrorTextEventArgs e);
    // Делегат для метода операции. 
    public delegate double MathOperation(double a, double b);

    public interface ICalculatorService
    {
        double CalculateExpression(string expression);
        IEnumerable<double> CalculateBatchExpressions(IEnumerable<string> expressions);
    }

    public class CalculatorService : ICalculatorService
    {
        // Обработчик события о получении ошибки при вычислении выражения.
        public event ErrorTextHandler ErrorTextEvent;

        public IEnumerable<double> CalculateBatchExpressions(IEnumerable<string> expressions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Проверка результата на наличие дробной части
        /// </summary>
        /// <param name="result"> Результат операций </param>
        /// <returns> Число в корректной форме </returns>
        static double GetRightAns(string result)
        {
            try
            {
                return (double)int.Parse(result);
            }
            catch (Exception)
            {
                return double.Parse((double.Parse(result) - double.Parse(result) % 0.0001).ToString("F4"));
            }

        }

        // Словарь для делегатов операции.
        static Dictionary<char, MathOperation> operations = new Dictionary<char, MathOperation>
        {
            {'+', (a, b) => a + b},
            {'-', (a, b) => a - b},
            {'*', (a, b) => a * b},
            {'/', (a, b) => a / b},
            {'%', (a, b) => a % b}
        };

        /// <summary>
        /// Метод вычисления выражения
        /// </summary>
        /// <param name="expression">Искомое выражение</param>
        /// <returns>Результат вычисления</returns>
        public double CalculateExpression(string expression)
        {
            // Определяем культуру.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                // Привязываем к событию метод для получения текста ошибки.
                ErrorTextEvent += Controllers.CalculatorController.GetTextError;

                expression = expression.Replace(',', '.').Replace(" ", "");
                // Символ операции.
                char operation;

                // Определяем индекс знака операции.
                List<int> indexOperations = new List<int>();

                // Блок проверки выражения с плюсами.
                int amountOfPlus = expression.ToCharArray().Count(x => x == '+');
                if (amountOfPlus > 0)
                {
                    if (expression.LastIndexOf('+') == expression.IndexOf('+'))
                    {
                        indexOperations.Add(expression.IndexOf('+'));
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }

                // Блок проверки выражения с минусами.
                int amountOfmin = expression.ToCharArray().Count(x => x == '-');
                if (expression.IndexOf('-') != 0 && amountOfmin > 1)
                {
                    indexOperations.Add(expression.IndexOf('-'));
                }
                else if (expression.IndexOf('-') == 0 && amountOfmin == 1)
                {
                    indexOperations.Add(expression.LastIndexOf('-'));
                }
                else if (expression.IndexOf('-') == 0 && amountOfmin > 2)
                {
                    for (int i = 1; i < expression.Length; i++)
                    {
                        if (expression[i] == '-')
                        {
                            indexOperations.Add(i);
                            break;
                        }
                    }
                }
                else if (expression.IndexOf('-') != 0 && amountOfmin == 1)
                {
                    indexOperations.Add(expression.LastIndexOf('-'));
                }

                indexOperations.Add(expression.IndexOf('*'));
                indexOperations.Add(expression.IndexOf('/'));
                indexOperations.Add(expression.IndexOf('%'));

                // Определяем использованную операцию и разделяем строку на 2 части.
                for (int i = 0; i < indexOperations.Count; i++)
                {
                    if (indexOperations[i] > 0)
                    {
                        operation = expression[indexOperations[i]];

                        // Разбиваем выражение на две части. 
                        string expressionX = expression.Substring(0, indexOperations[i]);
                        string expressionY = expression.Substring(indexOperations[i] + 1);
                        double x = double.Parse(expressionX);
                        double y = double.Parse(expressionY);

                        if (operation == '/' && y == 0) throw new DivideByZeroException();
                        if (operation == '%' && y == 0) throw new DivideByZeroException();
                        foreach (var item in operations)
                        {
                            string result = operations[operation](x, y).ToString();
                            return GetRightAns(result);
                        }
                    }
                }
                // Исключение при неверном знаке операции.
                throw new KeyNotFoundException();
            }
            catch (DivideByZeroException)
            {
                ErrorTextEvent?.Invoke(this, new ErrorTextEventArgs("Can not divide by zero!!!"));
            }
            catch (ArithmeticException e)
            {
                ErrorTextEvent?.Invoke(this, new ErrorTextEventArgs(e.GetType().ToString()));
            }
            catch (KeyNotFoundException)
            {
                ErrorTextEvent?.Invoke(this, new ErrorTextEventArgs("Wrong operation symbol!!!"));
            }
            catch (FormatException)
            {
                ErrorTextEvent?.Invoke(this, new ErrorTextEventArgs("Wrong values!!!"));
            }
            catch (Exception e)
            {
                ErrorTextEvent?.Invoke(this, new ErrorTextEventArgs(e.GetType().ToString()));
            }
            return -1;
        }
    }
}
