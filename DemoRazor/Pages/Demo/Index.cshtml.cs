using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoRazor.Pages.Demo
{
    public class IndexModel : PageModel
    {
        public string Name { get; set; } = "Nguyen Van A";
        public int Age { get; set; } = 20;
        public void OnGet()
        {
            
        }
    }
}
