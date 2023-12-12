//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using MySotre.Pages.Clients;
//using System.Data.SqlClient;

//namespace MySotre.Pages
//{
//	public class PrivacyModel : PageModel
//	{
//		public List<ClientInfo> listClients = new();
//		public void OnGet()
//		{
//			try
//			{
//				string connectionString = "Data Source=DESKTOP-6BN7Q1C;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
//				using SqlConnection connection = new(connectionString);
//				connection.Open();
//				string sql = "SELECT * FROM clients";
//				using SqlCommand command = new(sql, connection);
//				using SqlDataReader reader = command.ExecuteReader();
//				while (reader.Read())
//				{
//					ClientInfo clientInfo = new()
//					{
//						id = "" + reader.GetInt32(0),
//						name = reader.GetString(1),
//						email = reader.GetString(2),
//						phone = reader.GetString(3),
//						address = reader.GetString(4),
//						created_at = reader.GetDateTime(5).ToString()
//					};

//					listClients.Add(clientInfo);
//				}
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine("Exception: " + ex.ToString());
//			}
//		}
//	}
//	public class ClientInfo
//	{
//		public string id;
//		public string name;
//		public string email;
//		public string phone;
//		public string address;
//		public string created_at;
//	}

//}
