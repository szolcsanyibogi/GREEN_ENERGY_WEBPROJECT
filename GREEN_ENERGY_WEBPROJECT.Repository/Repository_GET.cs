﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GREEN_ENERGY_WEBPROJECT.Models;
using System.Reflection.PortableExecutable;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Data;

namespace GREEN_ENERGY_WEBPROJECT.Repository
{
    public class Repository_GET
    {
        SqlConnection con;
        public Repository_GET(string ConnectionString)
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

            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : Convert.ToSingle(reader.GetDouble(0)),
                    FACTORY_ID = reader.IsDBNull(1) ? -1 : reader.GetInt32(1),
                    DATE_ID = reader.IsDBNull(2) ? -1 : reader.GetInt32(2),
                    METRIC_ID = reader.IsDBNull(3) ? -1 : reader.GetInt32(3),
                    UNIT_ID = reader.IsDBNull(4) ? -1 : reader.GetInt32(4)
                };

                facts.Add(fact);
            }

            reader.Close();

            foreach (var fact in facts)
            {
                if (fact.FACTORY_ID > 0)
                    fact.FACTORY = GetFACTORY(fact.FACTORY_ID);

                if (fact.DATE_ID > 0)
                    fact.DATE = GetDATE(fact.DATE_ID);

                if (fact.METRIC_ID > 0)
                    fact.METRIC = GetMETRIC(fact.METRIC_ID);

                if (fact.UNIT_ID > 0)
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

            if (con.State != ConnectionState.Open)
                con.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;

                return new FACTORY()
                {
                    FACTORY_ID = reader.GetInt32(0),
                    FACTORY_NAME = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    COUNTRY = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    LOCATION = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    REGIO = reader.IsDBNull(4) ? "" : reader.GetString(4)
                };
            }
        }


        public List<FACTORY> GetFACTORY_NAME(string FACTORY_NAME)
        {
            string query = "SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE FACTORY_NAME = @FACTORY_NAME";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FACTORY_NAME", FACTORY_NAME);

            List<FACTORY> factories = new List<FACTORY>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY()
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.GetString(1),
                        COUNTRY = reader.GetString(2),
                        LOCATION = reader.GetString(3),
                        REGIO = reader.GetString(4)
                    });
                }
            }

            return factories;
        }

        public List<FACTORY> GetFACTORY_COUNTRY(string COUNTRY)
        {
            string query = "SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE COUNTRY = @COUNTRY";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);

            List<FACTORY> factories = new List<FACTORY>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY()
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.GetString(1),
                        COUNTRY = reader.GetString(2),
                        LOCATION = reader.GetString(3),
                        REGIO = reader.GetString(4)
                    });
                }
            }

            return factories;
        }

        public List<FACTORY> GetFACTORY_LOCATION(string LOCATION)
        {
            string query = "SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE LOCATION = @LOCATION";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LOCATION", LOCATION);

            List<FACTORY> factories = new List<FACTORY>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY()
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.GetString(1),
                        COUNTRY = reader.GetString(2),
                        LOCATION = reader.GetString(3),
                        REGIO = reader.GetString(4)
                    });
                }
            }

            return factories;
        }

        public List<FACTORY> GetFACTORY_REGIO(string REGIO)
        {
            string query = "SELECT [FACTORY_ID], [FACTORY_NAME], [COUNTRY], [LOCATION], [REGIO] FROM [ATH_STAR_GREEN].[dbo].[DIM_FACTORY] WHERE REGIO = @REGIO";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@REGIO", REGIO);

            List<FACTORY> factories = new List<FACTORY>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY()
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.GetString(1),
                        COUNTRY = reader.GetString(2),
                        LOCATION = reader.GetString(3),
                        REGIO = reader.GetString(4)
                    });
                }
            }

            return factories;
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

        public List<UNIT> GetUNIT_NAME(string UNIT_NAME)
        {
            string query = "SELECT [Unit_ID],[Unit_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Unit] WHERE UNIT_NAME = @UNIT_NAME";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UNIT_NAME", UNIT_NAME);

            List<UNIT> units = new List<UNIT>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    units.Add(new UNIT()
                    {
                        UNIT_ID = reader.GetInt32(0),
                        UNIT_NAME = reader.GetString(1)
                    });
                }
            }

            return units;
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

        public List<METRIC> GetMETRIC_NAME(string METRIC_NAME)
        {
            string query = "SELECT [Metric_ID], [Metric_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Metric] WHERE METRIC_NAME = @METRIC_NAME";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@METRIC_NAME", METRIC_NAME);

            List<METRIC> metrics = new List<METRIC>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    metrics.Add(new METRIC()
                    {
                        METRIC_ID = reader.GetInt32(0),
                        METRIC_NAME = reader.GetString(1)
                    });
                }
            }

            return metrics;
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

        public List<FACTORY> GetFactories()
        {
            var factories = new List<FACTORY>();
            string query = "SELECT DISTINCT FACTORY_NAME FROM DIM_FACTORY";

            using (SqlCommand cmd = new SqlCommand(query, con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                int tempId = 1;
                while (reader.Read())
                {
                    factories.Add(new FACTORY
                    {
                        FACTORY_ID = tempId++,
                        FACTORY_NAME = reader.GetString(0)
                    });
                }
            }

            return factories;
        }


        public List<DATE> GetYears()
        {
            var years = new List<DATE>();
            string query = "SELECT DISTINCT YEAR FROM DIM_DATE ORDER BY YEAR";

            using (SqlCommand cmd = new SqlCommand(query, con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    years.Add(new DATE
                    {
                        YEAR = reader.GetInt32(0)
                    });
                }
            }

            return years;
        }

        public List<FACTORY> GetAllFactories()
        {
            string query = "SELECT FACTORY_ID, FACTORY_NAME, COUNTRY, LOCATION, REGIO FROM DIM_FACTORY";
            SqlCommand cmd = new SqlCommand(query, con);

            List<FACTORY> factories = new List<FACTORY>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        COUNTRY = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        LOCATION = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        REGIO = reader.IsDBNull(4) ? "" : reader.GetString(4)
                    });
                }
            }
            return factories;
        }


        public List<DATE> GetAllDates()
        {
            string query = "SELECT DATE_ID, YEAR FROM DIM_DATE";
            SqlCommand cmd = new SqlCommand(query, con);

            List<DATE> dates = new List<DATE>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dates.Add(new DATE
                    {
                        DATE_ID = reader.GetInt32(0),
                        YEAR = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                    });
                }
            }
            return dates;
        }

        public List<FACTORY> GetDistinctFactoryNames()
        {
            string query = @"
        SELECT MIN(FACTORY_ID) AS FACTORY_ID, FACTORY_NAME
        FROM DIM_FACTORY
        GROUP BY FACTORY_NAME";

            SqlCommand cmd = new SqlCommand(query, con);

            List<FACTORY> factories = new List<FACTORY>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    factories.Add(new FACTORY
                    {
                        FACTORY_ID = reader.GetInt32(0),
                        FACTORY_NAME = reader.GetString(1)
                    });
                }
            }

            return factories;
        }

        public UNIT GetWasteUnit()
        {
            string query = "SELECT Unit_ID, Unit_Name FROM DIM_UNIT WHERE Unit_Name = 'Waste'";
            SqlCommand cmd = new SqlCommand(query, con);

            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;
                return new UNIT
                {
                    UNIT_ID = reader.GetInt32(0),
                    UNIT_NAME = reader.GetString(1)
                };
            }
        }

        public List<FACT> GetAllFacts()
        {
            string query = "SELECT VALUE, FACTORY_ID, DATE_ID, METRIC_ID, UNIT_ID FROM FACT_TABLE";
            SqlCommand cmd = new SqlCommand(query, con);

            List<FACT> facts = new List<FACT>();

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fact = new FACT()
                {
                    VALUE = reader.IsDBNull(0) ? 0f : Convert.ToSingle(reader.GetDouble(0)),
                    FACTORY_ID = reader.IsDBNull(1) ? -1 : reader.GetInt32(1),
                    DATE_ID = reader.IsDBNull(2) ? -1 : reader.GetInt32(2),
                    METRIC_ID = reader.IsDBNull(3) ? -1 : reader.GetInt32(3),
                    UNIT_ID = reader.IsDBNull(4) ? -1 : reader.GetInt32(4)
                };

                facts.Add(fact);
            }

            reader.Close();

            foreach (var fact in facts)
            {
                if (fact.FACTORY_ID > 0)
                    fact.FACTORY = GetFACTORY(fact.FACTORY_ID);

                if (fact.DATE_ID > 0)
                    fact.DATE = GetDATE(fact.DATE_ID);

                if (fact.METRIC_ID > 0)
                    fact.METRIC = GetMETRIC(fact.METRIC_ID);

                if (fact.UNIT_ID > 0)
                    fact.UNIT = GetUNIT(fact.UNIT_ID);
            }

            return facts;
        }

        public List<METRIC> GetDIM_METRIC()
        {
            var metrics = new List<METRIC>();

            string query = "SELECT [Metric_ID], [Metric_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_Metric]";
            SqlCommand cmd = new SqlCommand(query, con);

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                metrics.Add(new METRIC
                {
                    METRIC_ID = reader.GetInt32(0),
                    METRIC_NAME = reader.GetString(1)
                });
            }

            reader.Close();
            return metrics;
        }

        public List<UNIT> GetDIM_UNIT()
        {
            var units = new List<UNIT>();

            string query = "SELECT [UNIT_ID], [UNIT_Name] FROM [ATH_STAR_GREEN].[dbo].[dim_UNIT]";
            SqlCommand cmd = new SqlCommand(query, con);

            if (con.State != ConnectionState.Open) con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                units.Add(new UNIT
                {
                    UNIT_ID = reader.GetInt32(0),
                    UNIT_NAME = reader.GetString(1)
                });
            }

            reader.Close();
            return units;
        }

        public List<DATE> GetDIM_DATE()
        {
            var dates = new List<DATE>();

            if (con.State != ConnectionState.Open) con.Open();

            string query = "SELECT DATE_ID, YEAR FROM DIM_DATE";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dates.Add(new DATE
                {
                    DATE_ID = reader.GetInt32(0),
                    YEAR = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                });
            }

            reader.Close();
            return dates;
        }




        public List<WaterDataByRegion> GetWaterDataByRegion()
        {
            var facts = GetAllFacts();
            var factories = GetAllFactories();
            var metrics = GetDIM_METRIC();
            var units = GetDIM_UNIT();
            var dates = GetDIM_DATE();

            var waterCategories = new List<string> { "Water withdrawal", "Water consumption", "Water discharges" };

            var data = (from fact in facts
                        join factory in factories on fact.FACTORY_ID equals factory.FACTORY_ID
                        join metric in metrics on fact.METRIC_ID equals metric.METRIC_ID
                        join unit in units on fact.UNIT_ID equals unit.UNIT_ID
                        join date in dates on fact.DATE_ID equals date.DATE_ID
                        where waterCategories.Contains(metric.METRIC_NAME)
                        select new
                        {
                            factory.REGIO,
                            factory.FACTORY_NAME,
                            unit.UNIT_NAME,
                            metric.METRIC_NAME,
                            date.YEAR,
                            fact.VALUE
                        }).ToList();

            var grouped = data
                .GroupBy(d => new { d.REGIO, d.FACTORY_NAME, d.UNIT_NAME, d.METRIC_NAME })
                .Select(g => new WaterDataByRegion
                {
                    Regio = g.Key.REGIO,
                    Factory = g.Key.FACTORY_NAME,
                    Unit = g.Key.UNIT_NAME,
                    Category = g.Key.METRIC_NAME,
                    Year2019 = g.FirstOrDefault(x => x.YEAR == 2019)?.VALUE,
                    Year2020 = g.FirstOrDefault(x => x.YEAR == 2020)?.VALUE,
                    Year2021 = g.FirstOrDefault(x => x.YEAR == 2021)?.VALUE,
                    Year2022 = g.FirstOrDefault(x => x.YEAR == 2022)?.VALUE,
                    Year2023 = g.FirstOrDefault(x => x.YEAR == 2023)?.VALUE
                }).ToList();

            return grouped;
        }

        public class WaterDataByRegion
        {
            public string Regio { get; set; }
            public string Factory { get; set; }
            public string Unit { get; set; }
            public string Category { get; set; }
            public double? Year2019 { get; set; }
            public double? Year2020 { get; set; }
            public double? Year2021 { get; set; }
            public double? Year2022 { get; set; }
            public double? Year2023 { get; set; }
        }


    }

}
