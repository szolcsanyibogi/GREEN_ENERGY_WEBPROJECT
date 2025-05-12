using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEN_ENERGY_WEBPROJECT.Models;
using Microsoft.Data.SqlClient;

namespace GREEN_ENERGY_WEBPROJECT.Repository_PUT
{
    public class Repository_PUT
    {
        SqlConnection con;
        public Repository_PUT(string ConnectionString)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
        }

        public CFE PutCFE(string country, string regional_grid, string factory, string unit, int cfe_per)
        {
            string Query = $"INSERT INTO DW_CFE (cfe_id, country, regional_grid, factory, unit, CFE_PERCENTAGE) VALUES ((SELECT COALESCE(MAX(cfe_id), 0) + 1 FROM DW_CFE), '{country}', '{regional_grid}', '{factory}', '{unit}', '{cfe_per}');";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
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
            string Query = $"INSERT INTO DW_PUE (PUE_id, Country, Location, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(PUE_id), 0) + 1 FROM DW_PUE), '{counrty}', '{location}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
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
            string Query = $"INSERT INTO DW_WASTE (Waste_id, Waste_Metrics, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(Waste_id), 0) + 1 FROM DW_Waste), '{waste_m}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
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
            string Query = $"INSERT INTO DW_Water (water_id, water_Metrics, factory, unit, YEAR_2019, YEAR_2020, YEAR_2021, YEAR_2022, YEAR_2023) VALUES ((SELECT COALESCE(MAX(water_id), 0) + 1 FROM DW_water), '{water_m}', '{factory}', '{unit}', {y2019}, {y2020}, {y2021}, {y2022}, {y2023});";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
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

            int waterId = -1;
            using (SqlCommand getIdCmd = new SqlCommand("SELECT water_id FROM DW_Water WHERE [Water_Metrics] = @category", con))
            {
                getIdCmd.Parameters.AddWithValue("@category", category);
                if (con.State != ConnectionState.Open) con.Open();

                var result = getIdCmd.ExecuteScalar();
                if (result != null)
                    waterId = Convert.ToInt32(result);
            }


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

  
    }

}

