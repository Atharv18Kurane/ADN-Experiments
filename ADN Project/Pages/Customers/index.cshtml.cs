using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CRM_Project.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> Customers = new List<CustomerInfo>();

        public void OnGet()
        {
            // Dummy data
            Customers.Add(new CustomerInfo { Id = 1, Name = "John", Email = "john@gmail.com" });
            Customers.Add(new CustomerInfo { Id = 2, Name = "Sam", Email = "sam@gmail.com" });
        }
    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}