using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models;

namespace MortgageCalculatorWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MortgageCalculatorController : ControllerBase
    {
        readonly IMonthlyMortgage _monthlyMortgageCalculator;
        readonly IValidator _validator;

        public MortgageCalculatorController(IMonthlyMortgage monthlyMortgageCalculator, IValidator validator)
        {
            _monthlyMortgageCalculator = monthlyMortgageCalculator;
            _validator = validator;
        }

        [HttpPost]
        public JsonResult SubmitMortgageDetails([FromBody] ReceivedMortgageDataModel data)
        {
            var validationResult = _validator.Validate(new ValidationContext<ReceivedMortgageDataModel>(data));

            if (!validationResult.IsValid)
            {
                return new JsonResult(BadRequest(validationResult.Errors));
            }

            _monthlyMortgageCalculator.InterestRatePercent = data.InterestRate;
            _monthlyMortgageCalculator.MortgageAmount = data.MortgageAmount;
            _monthlyMortgageCalculator.Term = data.Term;
            _monthlyMortgageCalculator.MonthlyOverPayment = data.Overpayment;
            _monthlyMortgageCalculator.HousePrice = data.HousePrice;
            _monthlyMortgageCalculator.DepositAmount = data.DepositAmount;

            return new JsonResult(Ok(_monthlyMortgageCalculator));
        }

        [HttpGet]
        public JsonResult GetControllerStatus()
        {
            return new JsonResult(Ok());
        }
    }
}
