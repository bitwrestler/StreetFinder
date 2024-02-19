using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StreetFinder.Controllers;

namespace StreetFinder.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public int MinSearchPattern => SearchController.MIN_SEARCH_PATTERN;

        public void OnGet()
        {

        }
    }
}
