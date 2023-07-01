using MortgageCalculator.SharedLogic.Models.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.SharedLogic.Models
{
    public interface IMortgageData
    {
        /// <summary>
        /// This series show the mortgage amount remaining per year of the mortgage
        /// </summary>
        Dataset OutstandingAmountPerYear { get; }

        /// <summary>
        /// 
        /// </summary>
        decimal TotalCostOfMortgage { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTime PayOffDateTime { get; }

        /// <summary>
        /// 
        /// </summary>
        Data ChartData { get; }
    }
}
