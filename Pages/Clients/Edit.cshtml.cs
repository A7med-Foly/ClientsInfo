using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MySotre.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo=new ClientInfo();
        public string errorMessage = "";
        public string succeddMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
				string connectionString = "Data Source=DESKTOP-6BN7Q1C;Initial Catalog=mystore;Integrated Security=True;TrustServerCertificate=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM clients WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								clientInfo.id = "" + reader.GetInt32(0);
								clientInfo.name = reader.GetString(1);
								clientInfo.email = reader.GetString(2);
								clientInfo.phone = reader.GetString(3);
								clientInfo.address = reader.GetString(4);
							}
						}
					}
				}	
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
			clientInfo.id = Request.Form["id"];
			clientInfo.name= Request.Form["name"];
			clientInfo.email= Request.Form["email"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.address= Request.Form["address"];

			//validate the fields
			if (clientInfo.id.Length==0||clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
			{
				errorMessage = "All The Fields Are Required!";
				return;
			}

			try
			{
				//connection to database to update the data
				string connectionString = "Data Source=DESKTOP-6BN7Q1C;Initial Catalog=mystore;Integrated Security=True;TrustServerCertificate=True";
				using (SqlConnection connection = new(connectionString))
				{
					connection.Open();
					string sql = "UPDATE clients " +
								"SET name=@name, email=@email, phone=@phone, address=@address " +
								"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", clientInfo.id); 
						command.Parameters.AddWithValue("@name", clientInfo.name);
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

			Response.Redirect("/Clients/Index");
		}
    }
}
