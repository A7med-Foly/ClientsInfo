using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MySotre.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() //when sumbit the data of a new client
        {
            clientInfo.name = Request.Form["name"];
			clientInfo.email = Request.Form["email"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.address = Request.Form["address"];

            //validate the fields
            if(clientInfo.name.Length==0|| clientInfo.email.Length==0|| clientInfo.phone.Length==0|| clientInfo.address.Length == 0)
            {
                errorMessage = "All The Fields Are Required!";
                return;
            }

            //save the new client data into the database
            try
            {
				string connectionString = "Data Source=DESKTOP-6BN7Q1C;Initial Catalog=mystore;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection =new(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients " +
						        "(name, email, phone, address) VALUES "+
								"(@name,@email,@phone,@address);";

                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name",clientInfo.name);
						command.Parameters.AddWithValue("@email", clientInfo.email);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
					}
                }
			}
			catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "New Client Added Successfuly ^-^";
            Response.Redirect("/Clients/Index");//if we add data into the database then wil redirected to the client list page
		}

    }
}
