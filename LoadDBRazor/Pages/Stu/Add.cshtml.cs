using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoadDBRazor.Pages.Stu
{
    public class AddModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost() {
            return Redirect("View");
        }
    }
}
