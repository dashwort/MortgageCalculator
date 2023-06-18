using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MortgageCalculator.SharedLogic.Calculators;

namespace MortgageCalculatorWebApp.Pages
{
    public class BasicCalculatorModel : PageModel
    {
        private readonly ILogger<BasicCalculatorModel> _logger;

        // This decorator allows properties to be written to on a post request using a form.
        // You can use BindProperty(SupportsGet=True) to allow the parameters to be passed in view the url.
        [BindProperty(SupportsGet = true)]
        public MonthlyMortgage Calculator { get; set; }

        public BasicCalculatorModel(ILogger<BasicCalculatorModel> logger)
        {
            _logger = logger;
            Calculator = new MonthlyMortgage();

            WelcomeText = "Click calculate to continue...";
        }

        public string WelcomeText { get; set; }


        public void OnGet()
        {
            if (Calculator.MortgageAmount != 0 || Calculator.InterestRate != 0 || Calculator.Term != 0)
            {
                WelcomeText = "Click calculate to continue...";
            }
        }

        // the IActionResult is returned because when submitting a form, you typically carry out an action e.g. redirect user, refresh page etc.
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
                return Page();

            return RedirectToPage(nameof(IndexModel));
        }


    }
}