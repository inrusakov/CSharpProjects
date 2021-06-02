using System.Net.Http;
using System.Threading.Tasks;
using HSEApiTraining.Models.Currency;
using System.Collections.Generic;
using System.Linq;

namespace HSEApiTraining
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Метод получения курсов валюты
        /// </summary>
        /// <param name="request">Запрос с валютой и датами</param>
        /// <returns>Список курсов</returns>
        CurrencyResponse GetCurrencyRates(CurrencyRequest request);
    }

    public class CurrencyService : ICurrencyService
    {
        HttpClient client;
        /// <summary>
        /// Метод получения курсов валюты
        /// </summary>
        /// <param name="request">Запрос с валютой и датами</param>
        /// <returns>Список курсов</returns>
        public CurrencyResponse GetCurrencyRates(CurrencyRequest request)
        {
            client = new HttpClient();
            CurrencyResponse response = new CurrencyResponse();
            response.Currencies = new List<Dictionary<string, double>>();

            try
            {
                // Проверка даты запроса.
                if (request.DateStart == null)
                {
                    request.DateStart = System.DateTime.Now;
                }
                if (request.DateEnd == null)
                {
                    request.DateEnd = request.DateStart;
                }
                if (request.DateEnd < request.DateStart)
                {
                    throw new System.ArgumentException("Неправильная дата!");
                }

                // Проверка валюты запроса.
                request.Symbol = request.Symbol.ToUpper();
                if (request.Symbol.Length != 3)
                {
                    throw new System.ArgumentException("Неверная валюта!");
                }

                // Создание счетчика введенных дней.
                int years = request.DateEnd.Value.Year - request.DateStart.Value.Year;
                int count = (request.DateEnd.Value.DayOfYear - request.DateStart.Value.DayOfYear) + (365 * years);
                System.DateTime? newDate = request.DateStart;

                for (int i = 0; i <= count; i++)
                {
                    response.Currencies.Add(new Dictionary<string, double>());

                    // Генерация ссылки к RatesApi.
                    string path = $"api.ratesapi.io/api/{newDate.Value.Year}-" +
                        $"{newDate.Value.Month}-{newDate.Value.Day}?base={request.Symbol}";

                    // Получение данных с RatesApi.
                    RatesApiResponse responseFromRatesApi = null;
                    responseFromRatesApi = GetRatesApiResponseAsync("https://" + path).Result;

                    // Если дней больше чем 1 добавляем в лист словарей новый словарь со значениями.
                    if (count != 1)
                    {
                        Dictionary<string, double> new1 = responseFromRatesApi.Rates;
                        response.Currencies.Add(new1);
                    }
                    else
                    {
                        // Сохранение полученных данных в наш ответ.
                        response.Currencies[i] = responseFromRatesApi.Rates;
                    }

                    // Если список пустой.
                    if (response.Currencies == null)
                    {
                        throw new System.ArgumentException("Что-то пошло не так!");
                    }

                    // Новое значение счетчика.
                    newDate = newDate.Value.AddDays(1);
                }
                // Убирает пустые словари.
                for (int i = 0; i < response.Currencies.Count; i++)
                {
                    if (response.Currencies[i].Count == 0)
                    {
                        response.Currencies.Remove(response.Currencies[i]);
                    }
                }

                // Оправка полученого ответа.
                return response;
            }
            catch (System.Exception e)
            {
                response.Error = e.Message;
                response.Currencies = null;
                return response;
            }
        }

        /// <summary>
        /// Метод Get запроса с RatesApi
        /// </summary>
        /// <param name="path">Ссылка на RatesApi</param>
        /// <returns>Данные с RatesApi</returns>
        async Task<RatesApiResponse> GetRatesApiResponseAsync(string path)
        {
            RatesApiResponse data = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                // Получение данных с RatesApi.
                data = await response.Content.ReadAsAsync<RatesApiResponse>();
            }
            return data;
        }
    }
}