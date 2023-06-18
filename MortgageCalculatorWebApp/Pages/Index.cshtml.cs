using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MortgageCalculatorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// runs when a razor pages receives a get request
        /// </summary>
        public void OnGet()
        {

        }

        /// <summary>
        /// runs when a razor pages receives a post request
        /// </summary>
        public void OnPost()
        {

        }
    }
}