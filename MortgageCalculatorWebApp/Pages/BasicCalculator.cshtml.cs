using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models;
using MortgageCalculator.SharedLogic.Models.Chart;
using Newtonsoft.Json;

namespace MortgageCalculatorWebApp.Pages
{
    public class BasicCalculatorModel : PageModel
    {
        private readonly ILogger<BasicCalculatorModel> _logger;

        // This decorator allows properties to be written to on a post request using a form.
        // You can use BindProperty(SupportsGet=True) to allow the parameters to be passed in view the url.
        [BindProperty(SupportsGet = false)]
        public MonthlyMortgage Calculator { get; set; }

        public ChartJs RepaymentChart { get; set; }
        public ChartJs InterestOnlyChart { get; set; }

        public string RepaymentChartJson
        {
            get
            {
                if (Calculator.MortgageAmount <= 0)
                    return string.Empty;

                return JsonConvert.SerializeObject(RepaymentChart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }

        public string InterestOnlyChartJson
        {
            get
            {
                if (Calculator.MortgageAmount <= 0)
                    return string.Empty;

                return JsonConvert.SerializeObject(InterestOnlyChart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }

        public BasicCalculatorModel(ILogger<BasicCalculatorModel> logger)
        {
            _logger = logger;
            Calculator = new MonthlyMortgage();

            SubmitButtonText = "Submit";
        }

        public string SubmitButtonText { get; set; }


        public void OnGet()
        {
            Calculator.MortgageAmount = 250000;
            Calculator.InterestRatePercent = 6;
            Calculator.Term = 25;
            Calculator.MonthlyOverPayment = 200;

            RepaymentChart = ConfigureChart(Calculator.Repayment.ChartData);
            InterestOnlyChart = ConfigureChart(Calculator.InterestOnly.ChartData);
        }

        // the IActionResult is returned because when submitting a form, you typically carry out an action e.g. redirect user, refresh page etc.
        public IActionResult OnPost()
        {
            return Page();
            //if (ModelState.IsValid)
            //{
            //    SubmitButtonText = "Update";
            //    return Page();
            //}

            //return RedirectToPage("index");
        }

        ChartJs ConfigureChart(Data data)
        {
            var chart = new ChartJs();

            chart.type = "line";
            chart.responsive = true;
            chart.data = data;

            return chart;
        }
    }
}