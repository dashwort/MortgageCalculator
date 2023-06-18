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
    public class ReceivedMortgageDataModel
    {
        [Required]
        [JsonProperty("mortgageAmount")]
        public decimal MortgageAmount { get; set; }

        [Required]
        [JsonProperty("interestRate")]
        public decimal InterestRate { get; set; }

        [Required]
        [JsonProperty("term")]
        public int Term { get; set; }

        [JsonProperty("overpayment")]
        public decimal Overpayment { get; set; }

        [JsonProperty("housePrice")]
        public decimal HousePrice { get; set; }

        [JsonProperty("depositAmount")]
        public decimal DepositAmount { get; set; }
    }
}
