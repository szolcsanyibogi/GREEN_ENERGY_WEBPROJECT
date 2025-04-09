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

        public List<FACT> GetFACTS(int FACTORY_ID, int DATE_ID, int METRIC_ID, int UNIT_ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE FACTORY_ID = @FACTORY_ID and DATE_ID=@DATE_ID and METRIC_ID=@METRIC_ID and UNIT_ID=@UNIT_ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FACTORY_ID", FACTORY_ID);
            cmd.Parameters.AddWithValue("@DATE_ID", DATE_ID);
            cmd.Parameters.AddWithValue("@METRIC_ID", METRIC_ID);
            cmd.Parameters.AddWithValue("@UNIT_ID", UNIT_ID);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // 1. Beolvasás memóriába
            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : (float)reader.GetDouble(0),
                    FACTORY_ID = reader.GetInt32(1),
                    DATE_ID = reader.GetInt32(2),
                    METRIC_ID = reader.GetInt32(3),
                    UNIT_ID = reader.GetInt32(4),
                };

                facts.Add(fact);
            }

            reader.Close();

            // 2. Kapcsolódó adatok betöltése csak a reader bezárása után
            foreach (var fact in facts)
            {
                fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
                fact.DATE = GetDATE(fact.DATE_ID);
                fact.METRIC = GetMETRIC(fact.METRIC_ID);
                fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
            ;
        }

        public List<FACT> GetFACTS_BY_UNITID(int ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE UNIT_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // 1. Beolvasás memóriába
            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : (float)reader.GetDouble(0),
                    FACTORY_ID = reader.GetInt32(1),
                    DATE_ID = reader.GetInt32(2),
                    METRIC_ID = reader.GetInt32(3),
                    UNIT_ID = reader.GetInt32(4),
                };

                facts.Add(fact);
            }

            reader.Close();

            // 2. Kapcsolódó adatok betöltése csak a reader bezárása után
            foreach (var fact in facts)
            {
                fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
                fact.DATE = GetDATE(fact.DATE_ID);
                fact.METRIC = GetMETRIC(fact.METRIC_ID);
                fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
        }

        public List<FACT> GetFACTS_BY_METRICID(int ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE METRIC_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // 1. Beolvasás memóriába
            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : (float)reader.GetDouble(0),
                    FACTORY_ID = reader.GetInt32(1),
                    DATE_ID = reader.GetInt32(2),
                    METRIC_ID = reader.GetInt32(3),
                    UNIT_ID = reader.GetInt32(4),
                };

                facts.Add(fact);
            }

            reader.Close();

            // 2. Kapcsolódó adatok betöltése csak a reader bezárása után
            foreach (var fact in facts)
            {
                fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
                fact.DATE = GetDATE(fact.DATE_ID);
                fact.METRIC = GetMETRIC(fact.METRIC_ID);
                fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
        }

        public List<FACT> GetFACTS_BY_DATEID(int ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE DATE_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // 1. Beolvasás memóriába
            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : (float)reader.GetDouble(0),
                    FACTORY_ID = reader.GetInt32(1),
                    DATE_ID = reader.GetInt32(2),
                    METRIC_ID = reader.GetInt32(3),
                    UNIT_ID = reader.GetInt32(4),
                };

                facts.Add(fact);
            }

            reader.Close();

            // 2. Kapcsolódó adatok betöltése csak a reader bezárása után
            foreach (var fact in facts)
            {
                fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
                fact.DATE = GetDATE(fact.DATE_ID);
                fact.METRIC = GetMETRIC(fact.METRIC_ID);
                fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
        }

        public List<FACT> GetFACTS_BY_FACTORYID(int ID)
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE WHERE FACTORY_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // 1. Beolvasás memóriába
            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : (float)reader.GetDouble(0),
                    FACTORY_ID = reader.GetInt32(1),
                    DATE_ID = reader.GetInt32(2),
                    METRIC_ID = reader.GetInt32(3),
                    UNIT_ID = reader.GetInt32(4),
                };

                facts.Add(fact);
            }

            reader.Close();

            // 2. Kapcsolódó adatok betöltése csak a reader bezárása után
            foreach (var fact in facts)
            {
                fact.FACTORY = GetFACTORY(fact.FACTORY_ID);
                fact.DATE = GetDATE(fact.DATE_ID);
                fact.METRIC = GetMETRIC(fact.METRIC_ID);
                fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
        }

        public FACTORY GetFACTORY(int ID)
        {
            string query = "SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE FACTORY_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;

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

        public UNIT GetUNIT(int ID)
        {
            string Query = "SELECT [Unit_ID],[Unit_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Unit] WHERE Unit_ID = @ID";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;
                return new UNIT()
                {
                    UNIT_ID = reader.GetInt32(0),
                    UNIT_NAME = reader.GetString(1)
                };
            }
        }

        public METRIC GetMETRIC(int ID)
        {
            string Query = "SELECT [Metric_ID], [Metric_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Metric] WHERE Metric_ID = @ID";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;
                return new METRIC()
                {
                    METRIC_ID = reader.GetInt32(0),
                    METRIC_NAME = reader.GetString(1)
                };
            }
        }

        public DATE GetDATE(int ID)
        {
            string Query = "SELECT [Date_ID], [Year] FROM [ATH_STAR_GREEN].[dbo].[dim_Date] WHERE Date_ID = @ID";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;
                return new DATE()
                {
                    DATE_ID = reader.GetInt32(0),
                    YEAR = reader.GetInt32(1)
                };
            }
        }
    }
}
