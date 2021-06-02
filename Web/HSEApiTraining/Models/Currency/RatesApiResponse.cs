using System;
using System.Collections.Generic;

namespace HSEApiTraining.Models.Currency
{
    public class RatesApiResponse
    {
        public string Base { get; set; }
        public Dictionary<string, double> Rates { get; set; }
        public DateTime Date { get; set; }
    }
}
