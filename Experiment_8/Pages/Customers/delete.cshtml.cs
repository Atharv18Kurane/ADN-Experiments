using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace CRMProject.Pages.Customers
{
    public class delete : PageModel
    {
        public void OnGet()
        {
            try
            {
                int CustId  = Convert.ToInt32(Request.Query["id"]);
                using (var connection = new MySqlConnector.MySqlConnection
                                    ("Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manger;"))
                {
                    connection.Open();
                    string sql = "DELETE FROM customers WHERE id=@id";
                    using(var command = new MySqlConnector.MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", CustId);
                        int i= command.ExecuteNonQuery();
                        if(i>0)
                        {
                            Console.WriteLine("Customer deleted successfully.");
                            Response.Redirect("/Customers/Index");
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete customer.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
            }
            Response.Redirect("/Customers/Index");
        }
    }
}