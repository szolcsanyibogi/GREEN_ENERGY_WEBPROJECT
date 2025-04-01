using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GREEN_ENERGY_WEBPROJECT.Models;
using System.Reflection.PortableExecutable;

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
            string Query = $"SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE FACTORY_ID='{ID}'";
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

        public FACT GetFACT(int ID) //MEGNÉZNI
        {
            string Query = $"SELECT [Date_ID], [Year], [Month] FROM [ATH_STAR_GREEN].[dbo].[dim_Date] WHERE Date_ID = '{ID}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            return new FACT()
            {
                VALUE = reader.GetInt32(0),
                FACTORY_ID = reader.GetInt32(1),
                FACTORY = GetFACTORY(reader.GetInt32(1)),
                DATE_ID = reader.GetInt32(2),
                DATE = GetDATE(reader.GetInt32(2)),
                METRIC_ID = reader.GetInt32(3),
                METRIC = GetMETRIC(reader.GetInt32(3)),
                UNIT_ID = reader.GetInt32(4),
                UNIT = GetUNIT(reader.GetInt32(4))
            };
        }

        public UNIT GetUNIT(int ID)
        {
            string Query = $"SELECT [Unit_ID],[Unit_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Unit] WHERE Unit_ID = '{ID}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            return new UNIT()
            {
                UNIT_ID = reader.GetInt32(0),
                UNIT_NAME = reader.GetString(1)
            };
        }

        public METRIC GetMETRIC(int ID)
        {
            string Query = $"SELECT [Metric_ID] ,[Metric_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Metric] WHERE Metric_ID = '{ID}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            return new METRIC()
            {
                METRIC_ID = reader.GetInt32(0),
                METRIC_NAME = reader.GetString(1)
            };
        }

        public DATE GetDATE(int ID)
        {
            string Query = $"SELECT [Date_ID], [Year], [Month] FROM [ATH_STAR_GREEN].[dbo].[dim_Date] WHERE Date_ID = '{ID}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            return new DATE()
            {
                DATE_ID = reader.GetInt32(0),
                YEAR = reader.GetInt32(1),
                MONTH = reader.GetInt32(2)
            };
        }
        public FACTORY PutFACTORY(string name, string country, string location, string regio)
        {
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
