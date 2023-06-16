using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MortgageCalculator.SharedLogic.Models
{
    public class ReceivedMortgageData
    {
        [Required]
        [JsonProperty("mortgageAmount")]
        public double MortgageAmount { get; set; }

        [Required]
        [JsonProperty("interestRate")]
        public double InterestRate { get; set; }

        [Required]
        [JsonProperty("term")]
        public double Term { get; set; }
    }
}
