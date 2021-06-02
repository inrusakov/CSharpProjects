using System.Collections.Generic;

namespace HSEApiTraining.Models.Currency
{
    public class CurrencyResponse
    {
        public List<Dictionary<string, double>> Currencies { get; set; }
        public string Error { get; set; }

    }
}
