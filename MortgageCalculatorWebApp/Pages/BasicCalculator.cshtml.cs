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

            SubmitButtonText = "Submit";
        }

        public string SubmitButtonText { get; set; }


        public void OnGet()
        {

        }

        // the IActionResult is returned because when submitting a form, you typically carry out an action e.g. redirect user, refresh page etc.
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                SubmitButtonText = "Update";
                return Page();
            }

            return RedirectToPage("index");
        }


    }
}