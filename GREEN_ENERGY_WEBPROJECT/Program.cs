// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");


string ConnectionString = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_DW_GREEN;Integrated Security=True;Trust Server Certificate=True";

using (SqlConnection con = new SqlConnection(ConnectionString))
{
    con.Open();
    string Query = "SELECT TOP (1000) [PUE_ID], [Country] FROM [ATH_DW_GREEN].[dbo].[dw_PUE]";
    SqlCommand cmd = new SqlCommand(Query, con);

    SqlDataReader reader = cmd.ExecuteReader();

    // Print the column names first
    Console.WriteLine("PUE_ID\tCountry");

    // Loop through the rows and print them
    while (reader.Read())
    {
        int pueId = reader.GetInt32(0); // Column 0: PUE_ID
        string country = reader.IsDBNull(1) ? "NULL" : reader.GetString(1); // Column 1: Country

        Console.WriteLine($"{pueId}\t{country}");
    }

    reader.Close();
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();