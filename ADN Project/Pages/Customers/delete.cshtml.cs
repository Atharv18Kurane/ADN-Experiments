using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM_Project.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }

        public IActionResult OnPost()
        {
            // Delete logic

            return RedirectToPage("Index");
        }
    }
}