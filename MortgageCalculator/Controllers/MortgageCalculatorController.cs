using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models;
using System.Reflection;

namespace MortgageCalculator.Controllers
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
        public JsonResult SubmitMortgageDetails([FromBody] ReceivedMortgageData data)
        {
            var validationResult = _validator.Validate(new ValidationContext<ReceivedMortgageData>(data));

            if (!validationResult.IsValid)
            {
                return new JsonResult(BadRequest(validationResult.Errors));
            }
            _monthlyMortgageCalculator.InterestRatePercent = data.InterestRate;
            _monthlyMortgageCalculator.MortgageAmount = data.MortgageAmount;
            _monthlyMortgageCalculator.Term = data.Term;

            return new JsonResult(Ok(_monthlyMortgageCalculator));
        }

        [HttpGet]
        public JsonResult GetControllerStatus()
        {
            return new JsonResult(Ok());
        }
    }
}
