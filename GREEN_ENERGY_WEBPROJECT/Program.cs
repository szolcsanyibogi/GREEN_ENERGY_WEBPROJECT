// See https://aka.ms/new-console-template for more information
using GREEN_ENERGY_WEBPROJECT.Repository_PUT;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using GREEN_ENERGY_WEBPROJECT;



Console.WriteLine("Hello, World!");




string ConnectionString_DW = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_DW_GREEN;Integrated Security=True;Trust Server Certificate=True";
string ConnectionString_STAR = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_STAR_GREEN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

var repo1 = new Repository(ConnectionString_STAR);
var repo2 = new Repository_PUT(ConnectionString_DW);

repo1.GetDATE(2020);
//repo2.PutCFE("teszt", "teszt", "teszt", "teszt", 123);





Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();