using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoRazor.Pages.DemoSendData
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public int id { get; set; } = 0;
        [BindProperty]
        public string name { get; set; } = "Nguyen Van A";

        public void OnGet()
        {
        }


        // C1 use Request.Form
        //public void OnPost()
        //{
        //    id = int.Parse(Request.Form["id"]);
        //    name = Request.Form["name"];
        //}

        // C2 use IFormCollection
        //public void OnPost(IFormCollection f)
        //{
        //    id = int.Parse(f["id"]) + 10;
        //    name = f["name"];
        //}

        // C3 use BindProperty
        //public void OnPost(int id, string name)
        //{
        //    this.id = id + 10;
        //    this.name = name;
        //}

        // C4 use asp-for and BindProperty
        public IActionResult OnPost()
        {
            id += 1;
            name = name + " - " + id;
            return Page();
        }
    }
}
