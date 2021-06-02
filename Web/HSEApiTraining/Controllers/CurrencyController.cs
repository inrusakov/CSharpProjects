using Microsoft.AspNetCore.Mvc;
using HSEApiTraining.Models.Currency;
using HSEApiTraining.Models.Events;
using System.Collections.Generic;

namespace HSEApiTraining.Controllers
{
    //Тут все методы-хендлеры вам нужно реализовать самим
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        // Объект типа ICurrenceService для работы контроллера.
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost]
        public CurrencyResponse CurrencyMethod([FromBody] CurrencyRequest Request)
        {
            CurrencyResponse response = new CurrencyResponse();
            response = _currencyService.GetCurrencyRates(Request);
            return response;
        }
    }
}