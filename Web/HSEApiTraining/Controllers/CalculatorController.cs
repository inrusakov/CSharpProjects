using HSEApiTraining.Models.Calculator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HSEApiTraining.Models.Events;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        // В конструкторе контроллера происходит инъекция сервисов через их интерфейсы.
        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        // Переменная, содержащая текст ошибки.
        static string textError = "null";

        /// <summary>
        /// Метод получения текста ошибки из CalculatorService.
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        public static void GetTextError(object sender, ErrorTextEventArgs e) => textError = e.Error;

        [HttpGet]
        public CalculatorResponse Calculate([FromQuery] string expression)
        {
            // Результат вычисления выражения.
            CalculatorResponse response = new CalculatorResponse();

            // Вычисление выражения.
            response.Value = _calculatorService.CalculateExpression(expression);
            // Привязка текста ошибки.
            response.Error = textError;
            textError = null;

            return response;
        }

        [HttpPost]
        public CalculatorBatchResponse CalculateBatch([FromBody] CalculatorBatchRequest Request)
        {
            // Общая ошибка всего списка.
            string totalError = null;

            // Список подготовленных ответов.
            List<CalculatorResponse> answers = new List<CalculatorResponse>();
            if (Request.Expressions.Count() == 0) totalError = "Empty file";
            for (int i = 0; i < Request.Expressions.Count(); i++)
            {
                // Вычисление выражение и сохранение результата в список.
                answers.Add(Calculate(Request.Expressions.ElementAt(i)));
            }

            // Определение общей ошибки.
            for (int i = 0; i < answers.Count; i++)
            {
                // Если имеем хотя бы одну ошибку в списке.
                if (answers[i].Error != null)
                {
                    totalError = "Something went wrong";
                }
            }
            // Если не имеем ошибок в списке.
            return new CalculatorBatchResponse
            {
                Values = answers,
                Error = totalError
            };
        }

        //Примеры-пустышки
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
