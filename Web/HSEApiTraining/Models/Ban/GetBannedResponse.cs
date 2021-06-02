using System.Collections.Generic;

namespace HSEApiTraining.Models.Customer
{
    public class GetBannedResponse
    {
        public IEnumerable<BannedPhone> BannedPhones { get; set; }
        public string Error { get; set; }
    }
}
