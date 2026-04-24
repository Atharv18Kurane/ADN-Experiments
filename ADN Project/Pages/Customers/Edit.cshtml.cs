using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM_Project.Pages.Customers
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet(int id)
        {
            // Load data
            Id = id;
            Name = "Existing Name";
            Email = "existing@email.com";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update logic

            return RedirectToPage("Index");
        }
    }
}