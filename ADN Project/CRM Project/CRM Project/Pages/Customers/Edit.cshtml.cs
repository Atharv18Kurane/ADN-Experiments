using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CRM_Project.Pages.Customers
{
    public class Edit : PageModel
    {
      
        [BindProperty]
        public int Id { get; set; }

        [BindProperty, Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [BindProperty, Required(ErrorMessage = "Enter email")]
        public string Email { get; set; }

        [BindProperty, Required(ErrorMessage = "Enter phone")]
        public string Phone { get; set; }

        // LOAD CUSTOMER DATA
        public IActionResult OnGet(int id)
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();

                    string query = "SELECT * FROM customers WHERE id=@id";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Id = reader.GetInt32(0);
                            Name = reader.GetString(1);
                            Email = reader.GetString(2);
                            Phone = reader.GetString(3);
                        }
                        else
                        {
                            return RedirectToPage("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Page();
        }

        // UPDATE CUSTOMER
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();

                    string query =
                        @"UPDATE customers 
                          SET name=@name,
                              email=@email,
                              phone=@phone
                          WHERE id=@id";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@name", Name);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@phone", Phone);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage("/Customers/Index");
        }
    }
}