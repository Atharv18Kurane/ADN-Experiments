using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace CRMProject.Pages.Customers
{
    public class create : PageModel 
    {
        public string ErrorMessage { get; set; } ="";
         
        [BindProperty, Required(ErrorMessage = "Enter the name")] 
        public string name { get; set; } =""; 
        [BindProperty, Required(ErrorMessage = "Enter the email"), 
        EmailAddress(ErrorMessage = "Invalid email format")] 
        public string email { get; set; } =""; 
        [BindProperty, Required(ErrorMessage = "Enter the phone number")] 
        public string phone { get; set; }=""; 
 
        public void OnGet() 
        { 
        } 

        public void OnPost() 
        { 
            if (!ModelState.IsValid) 
            { 
                 Console.WriteLine("Form is Invalid"); 
                return; 
            } 
            try 
            { 
                using (var connection = new 
                MySqlConnector.MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manger;")) 
                { 
                    connection.Open();    
                    string sql = "INSERT INTO Customers (name, email, phone) VALUES (@CustName, @CustEmail, @CustPhone)";
                    using(var command = new MySqlConnector.MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustName", name); 
                        command.Parameters.AddWithValue("@CustEmail", email); 
                        command.Parameters.AddWithValue("@CustPhone", phone); 
                        int i= command.ExecuteNonQuery();
                        if(i>0) 
                        { 
                            Console.WriteLine("Customer added successfully.");
                            Response.Redirect("/Customers/Index"); 

                        } 
                        else 
                        { 
                            Console.WriteLine("Failed to add customer."); 
                        }
                    }
                }      
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine("Error adding customer."+$"Error: {ex.Message}"); 
                ErrorMessage = ex.Message; 
                return; 
            } 
        }
} 
}