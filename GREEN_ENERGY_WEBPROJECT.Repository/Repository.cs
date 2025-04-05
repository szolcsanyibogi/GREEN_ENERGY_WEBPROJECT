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
        public CFE PutCFE(string country, string regional_grid, string factory, string unit, int cfe_per)
        {
            string Query = $"INSERT INTO DW_CFE (cfe_id, country, regional_grid, factory, unit, CFE_PERCENTAGE) VALUES ((SELECT COALESCE(MAX(cfe_id), 0) + 1 FROM DW_CFE), '{country}', '{regional_grid}', '{factory}', {unit}', '{cfe_per}');";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new CFE()
            {
                Country = country,
                Regional_Grid = regional_grid,
                Factory = factory,
                Unit = unit,
                CFE_Percentage = cfe_per
            };
        }

        public ENERGY PutENERGY(string energy_c, string factory, string unit, int y2019, int y2020, int y2021, int y2022, int y2023)
        {
            string Query = $"INSERT INTO DW_ENERGY (energy_id, Energy_Consumption, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(energy_id), 0) + 1 FROM DW_ENERGY), '{energy_c}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new ENERGY()
            {
                Energy_Consumption = energy_c,
                Factory = factory,
                Unit = unit,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }
        
        public GHG PutGHG(string carbon_i, string factory, string unit, float y2019, float y2020, float y2021, float y2022, float y2023)
        {
            string Query = $"INSERT INTO DW_GHG (GHG_id, Carbon_Intensity, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(GHG_id), 0) + 1 FROM DW_GHG), '{carbon_i}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new GHG()
            {
                Carbon_Intensity = carbon_i,
                Factory = factory,
                Unit = unit,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }

        public PUE PutPUE(string counrty, string location, string factory, string unit, float y2019, float y2020, float y2021, float y2022, float y2023)
        {
            string Query = $"INSERT INTO DW_PUE (PUE_id, Country, Location, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(PUE_id), 0) + 1 FROM DW_PUE), '{counrty}', '{location}' '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new PUE()
            {
                Country = counrty,
                Location = location,
                Factory = factory,
                Unit = unit,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }

        public WASTE PutWASTE(string waste_m, string factory, string unit, float y2019, float y2020, float y2021, float y2022, float y2023)
        {
            string Query = $"INSERT INTO DW_WASTE (Waste_id, Waste_Metric, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(Waste_id), 0) + 1 FROM DW_Waste), '{waste_m}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new WASTE()
            {
                Waste_Metric = waste_m,
                Factory = factory,
                Unit = unit,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }

        public WATER PutWater(string water_m, string factory, string unit, float y2019, float y2020, float y2021, float y2022, float y2023)
        {
            string Query = $"INSERT INTO DW_Water (water_id, water_Metric, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(water_id), 0) + 1 FROM DW_water), '{water_m}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new WATER()
            {
                Water_Metric = water_m,
                Factory = factory,
                Unit = unit,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }

        public WATERBYREGIO PutWaterByRegio(string regio, string factory, string unit, string category, float y2019, float y2020, float y2021, float y2022, float y2023)
        {
            // 1. Vedd ki a megfelelő water_id-t a DW_Water táblából, ahol unit == category
            int waterId = -1;
            using (SqlCommand getIdCmd = new SqlCommand("SELECT water_id FROM DW_Water WHERE unit = @category", con))
            {
                getIdCmd.Parameters.AddWithValue("@category", category);
                if (con.State != ConnectionState.Open) con.Open();

                var result = getIdCmd.ExecuteScalar();
                if (result != null)
                    waterId = Convert.ToInt32(result);
            }

            // 2. Insert a DW_Water_By_Regio táblába
            string insertQuery = @"
                INSERT INTO DW_Water_By_Regio 
                (water_Regio_id, regio, factory, unit, Category, water_id, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) 
                VALUES ((SELECT COALESCE(MAX(water_Regio_id), 0) + 1 FROM DW_Water_By_Regio), 
                @regio, @factory, @unit, @category, @waterId, @y2019, @y2020, @y2021, @y2022, @y2023);";

            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@regio", regio);
                cmd.Parameters.AddWithValue("@factory", factory);
                cmd.Parameters.AddWithValue("@unit", unit);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@waterId", waterId);
                cmd.Parameters.AddWithValue("@y2019", y2019);
                cmd.Parameters.AddWithValue("@y2020", y2020);
                cmd.Parameters.AddWithValue("@y2021", y2021);
                cmd.Parameters.AddWithValue("@y2022", y2022);
                cmd.Parameters.AddWithValue("@y2023", y2023);

                cmd.ExecuteNonQuery();
                con.Close();
            }

            return new WATERBYREGIO()
            {
                Regio = regio,
                Factory = factory,
                Unit = unit,
                Category = category,
                Water_ID = waterId,
                year_2019 = y2019,
                year_2020 = y2020,
                year_2021 = y2021,
                year_2022 = y2022,
                year_2023 = y2023
            };
        }

        //public WATERBYREGIO PutWaterByRegio(string regio, string factory, string unit, string category,  float y2019, float y2020, float y2021, float y2022, float y2023)
        //{
        //    string Query = $"INSERT INTO DW_Water_By_Regio (water_Regio_id, regio, factory, unit, Category,  YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(water_id), 0) + 1 FROM DW_water), '{water_m}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
        //    SqlCommand cmd = new SqlCommand(Query, con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    return new WATERBYREGIO()
        //    {
        //        Regio = regio,
        //        Factory = factory,
        //        Unit = unit,
        //        Category = category,
        //        year_2019 = y2019,
        //        year_2020 = y2020,
        //        year_2021 = y2021,
        //        year_2022 = y2022,
        //        year_2023 = y2023,
        //        if (category == ) { }
        //    };
        //}
    }
}
