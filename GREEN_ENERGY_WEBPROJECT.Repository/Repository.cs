using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GREEN_ENERGY_WEBPROJECT.Models;

namespace GREEN_ENERGY_WEBPROJECT.Repository
{
    public class Repository
    {
        SqlConnection con;
        public Repository(string ConnectionString) {
            con = new SqlConnection(ConnectionString);
        }

        public FACTORY GetFACTORY(int ID)
        {
            string Query = $"SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE FACTORY_ID={ID}";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            return new FACTORY()
            {
                FACTORY_ID = reader.GetInt32(0),
                FACTORY_NAME = reader.GetString(1),
                COUNTRY = reader.GetString(2),
                LOCATION = reader.GetString(3),
                REGIO = reader.GetString(4)
            };
        }
    }
}
