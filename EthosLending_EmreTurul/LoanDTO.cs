using System;
using Newtonsoft.Json;

namespace EthosLending_EmreTurul
{
    public class LoanDTO
    {

        [JsonProperty(PropertyName = "monthly payment")]
        public double MonthlyPayment { get; set; }

        [JsonProperty(PropertyName = "total interest")]
        public double TotalInterest { get; set; }

        [JsonProperty(PropertyName = "total payment")]
        public double TotalPayment { get; set; }
    }
}
