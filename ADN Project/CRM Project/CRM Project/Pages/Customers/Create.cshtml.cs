using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace CRM_Project.Pages.Customers
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Enter the email"),
        EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Enter the phone number")]
        public string Phone { get; set; } = "";


        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Form is Invalid");
                return;
            }
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();
                    String sql ="INSERT INTO Customers (name, email, phone) VALUES(@CustName, @CustEmail, @CustPhone)";
                    var command = new MySqlCommand(sql, connection); 
                    command.Parameters.AddWithValue("@CustName", Name);
                    command.Parameters.AddWithValue("@CustEmail", Email);
                    command.Parameters.AddWithValue("@CustPhone", Phone);
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Console.WriteLine("Customer Added Successfully ");
                    }
                    else
                    {
                        Console.WriteLine("Failed to Add Customer ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding customer."+$"Error: {ex.Message}"); 
                //ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Index");
        }

        
    }

}