using System;

namespace HSEApiTraining.Models.Currency
{
    public class CurrencyRequest
    {
        /// <summary>
        /// Название целевой валюты
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Начало промежутка отслеживания
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Конец промежутка отслеживания
        /// </summary>
        public DateTime? DateEnd { get; set; }
        
    }
}
