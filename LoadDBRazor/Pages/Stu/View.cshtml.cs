using LoadDBRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoadDBRazor.Pages.Stu
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_SE1751Context _context;
        public IndexModel(PRN221_SE1751Context context)
        {
            _context = context;
        }
        public List<Student> students = new List<Student>();
        public List<Department> departments = new List<Department>();
        public void OnGet()
        {
            students = _context.Students.Include(s => s.Depart).ToList();
            departments = _context.Departments.ToList();
        }
    }
}
