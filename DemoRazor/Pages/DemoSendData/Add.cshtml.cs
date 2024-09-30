using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace DemoRazor.Pages.DemoSendData
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; } = 0;
        [BindProperty]
        public string Name { get; set; } = "Nguyen Van A";
        public void OnGet()
        {
        }

        // C1 use Request.Form
        //public void OnPost()
        //{
        //    Id = int.Parse(Request.Form["id"]);
        //    Name = Request.Form["name"];
        //}

        // C2 use IFormCollection
        //public void OnPost(IFormCollection f)
        //{
        //    Id = int.Parse(f["id"]) + 5;
        //    Name = f["name"];
        //}

        // C3 use BindProperty
        //public void OnPost(int id, string name)
        //{
        //    this.Id = id + 10;
        //    this.Name = name;
        //}

        // C4 use asp-for and BindProperty
        public IActionResult OnPost()
        {
            Id += 1;
            Name = Name + " - " + Id;
            return Page();
        }
    }
}
