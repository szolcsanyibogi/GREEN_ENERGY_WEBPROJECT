using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GREEN_ENERGY_WEBPROJECT.Models;
using System.Reflection.PortableExecutable;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Data;

namespace GREEN_ENERGY_WEBPROJECT.Repository_PUT
{
    public class Repository
    {
        SqlConnection con;
        public Repository(string ConnectionString)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
        }

        

        public FACT GetFACT(int ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE ID = @ID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                con.Close();
                return null; // vagy dobj kivételt, ha úgy jobb
            }

            var fact = new FACT()
            {
                VALUE = reader.GetFloat(0),
                FACTORY_ID = reader.GetInt32(1),
                DATE_ID = reader.GetInt32(2),
                METRIC_ID = reader.GetInt32(3),
                UNIT_ID = reader.GetInt32(4),
            };

            reader.Close();

            // Töltsd be a kapcsolódó entitásokat
            fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
            fact.DATE = GetDATE(fact.DATE_ID);
            fact.METRIC = GetMETRIC(fact.METRIC_ID);
            fact.UNIT = GetUNIT(fact.UNIT_ID);

            con.Close();
            return fact;
        }

        //public FACT GetFACT(int ID) //MEGNÉZNI
        //{
        //    string Query = $"SELECT [Date_ID], [Year], [Month] FROM [ATH_STAR_GREEN].[dbo].[dim_Date] WHERE Date_ID = '{ID}'";
        //    SqlCommand cmd = new SqlCommand(Query, con);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    return new FACT()
        //    {
        //        VALUE = reader.GetInt32(0),
        //        FACTORY_ID = reader.GetInt32(1),
        //        FACTORY = GetFACTORY(reader.GetInt32(1)),
        //        DATE_ID = reader.GetInt32(2),
        //        DATE = GetDATE(reader.GetInt32(2)),
        //        METRIC_ID = reader.GetInt32(3),
        //        METRIC = GetMETRIC(reader.GetInt32(3)),
        //        UNIT_ID = reader.GetInt32(4),
        //        UNIT = GetUNIT(reader.GetInt32(4))
        //    };
        //}

        public FACTORY GetFACTORY(int ID)
        {
            string Query = $"SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE Factory_ID={ID}";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var return_value = new FACTORY()
            {
                FACTORY_ID = reader.GetInt32(0),
                FACTORY_NAME = reader.GetString(1),
                COUNTRY = reader.GetString(2),
                LOCATION = reader.GetString(3),
                REGIO = reader.GetString(4)
            };
            reader.Close();
            return return_value;
        }

        public UNIT GetUNIT(int ID)
        {
            string Query = $"SELECT [Unit_ID],[Unit_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Unit] WHERE Unit_ID = {ID}";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var return_value = new UNIT()
            {
                UNIT_ID = reader.GetInt32(0),
                UNIT_NAME = reader.GetString(1)
            };
            reader.Close();
            return return_value;
        }

        public METRIC GetMETRIC(int ID)
        {
            string Query = $"SELECT [Metric_ID] ,[Metric_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Metric] WHERE Metric_ID = {ID}";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var return_value = new METRIC()
            {
                METRIC_ID = reader.GetInt32(0),
                METRIC_NAME = reader.GetString(1)
            };
            reader.Close();
            return return_value;
        }

        public DATE GetDATE(int ID)
        {
            string Query = $"SELECT [Date_ID], [Year] FROM [ATH_STAR_GREEN].[dbo].[dim_Date] WHERE Date_ID = {ID}";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var return_value = new DATE()
            {
                DATE_ID = reader.GetInt32(0),
                YEAR = reader.GetInt32(1),
                //MONTH = reader.GetInt32(2)
            };
            reader.Close();
            return return_value;
        }
        

    }
}
