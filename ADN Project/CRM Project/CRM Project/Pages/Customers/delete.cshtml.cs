using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CRM_Project.Pages.Customers
{
    public class Delete : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        // LOAD customer for confirmation
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

        public IActionResult OnPost()
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();

                    string query = "DELETE FROM customers WHERE id=@id";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", Id);

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