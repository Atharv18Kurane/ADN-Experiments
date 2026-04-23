using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace CRMProject.Pages.Customers
{
    public class edit : PageModel
    {
        public string ErrorMessage { get; set; } ="";
         
        [BindProperty, Required(ErrorMessage = "Enter the name")] 
        public string name { get; set; } =""; 
        [BindProperty, Required(ErrorMessage = "Enter the email"), 
        EmailAddress(ErrorMessage = "Invalid email format")] 
        public string email { get; set; } =""; 
        [BindProperty, Required(ErrorMessage = "Enter the phone number")] 
        public string phone { get; set; }=""; 

        public int CustId { get; set; }

        public void OnGet()
        {
            try
            {
                int id  = Convert.ToInt32(Request.Query["id"]);
                Console.WriteLine($"Customer ID: {id}");
                using (var connection = new MySqlConnector.MySqlConnection
                                    ("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manger;"))
                {
                    connection.Open();

                    //fetch existing data
                    string sql = "SELECT * FROM customers WHERE id=@id";
                    using(var command = new MySqlConnector.MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader.GetString(1);
                                email = reader.GetString(2);
                                phone = reader.GetString(3);
                            }
                            else
                            {
                                Console.WriteLine("Customer not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer: {ex.Message}");
            }
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
                int id  = Convert.ToInt32(Request.Query["id"]);
                using (var connection = new MySqlConnector.MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manger;"))
                {
                    connection.Open();
                    string sql = "UPDATE Customers SET name=@CustName, email=@CustEmail, phone=@CustPhone WHERE id=@id";
                    using(var command = new MySqlConnector.MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustName", name);
                        command.Parameters.AddWithValue("@CustEmail", email);
                        command.Parameters.AddWithValue("@CustPhone", phone);
                        command.Parameters.AddWithValue("@id", id);
                        int i= command.ExecuteNonQuery();
                        if(i>0)
                        {
                            Console.WriteLine("Customer updated successfully.");
                            Response.Redirect("/Customers/Index");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update customer.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
                ErrorMessage = ex.Message;
            }
        }
    }
}