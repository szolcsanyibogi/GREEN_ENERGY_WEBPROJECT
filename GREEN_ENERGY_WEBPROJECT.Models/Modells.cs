using System.ComponentModel.DataAnnotations;

namespace GREEN_ENERGY_WEBPROJECT.Models
{
    public class FACTORY
    {
        public int FACTORY_ID { get; set; }
        public string FACTORY_NAME { get; set; }
        public string COUNTRY { get; set; }
        public string LOCATION { get; set; }
        public string REGIO { get; set; }
    }
    public class FACT
    {
        public float VALUE { get; set; }
        public int FACTORY_ID { get; set; }
        public FACTORY FACTORY { get; set; }
        public int DATE_ID { get; set; }
        public DATE DATE { get; set; }
        public int METRIC_ID { get; set; }
        public METRIC METRIC { get; set; }
        public int UNIT_ID { get; set; }
        public UNIT UNIT { get; set; }
    }
    public class UNIT
    {
        public int UNIT_ID { get; set; }
        public string UNIT_NAME { get; set; }
    }
    public class METRIC
    {
        public int METRIC_ID { get; set; }
        public string METRIC_NAME { get; set; }
    }
    public class DATE
    {
        public int DATE_ID { get; set; }
        public int YEAR { get; set; }
        //public int MONTH { get; set; }
    }
    public class CFE
    {
        public int CFE_ID { get; set; }
        public string Country { get; set; }
        public string Regional_Grid { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public int CFE_Percentage { get; set; }
    }
    public class ENERGY
    {
        public int Energy_ID { get; set; }
        public string Energy_Consumption { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public int year_2019 { get; set; }
        public int year_2020 { get; set; }
        public int year_2021 { get; set; }
        public int year_2022 { get; set; }
        public int year_2023 { get; set; }
    }
    public class GHG
    {
        public int GHG_ID { get; set; }
        public string Carbon_Intensity { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public float year_2019 { get; set; }
        public float year_2020 { get; set; }
        public float year_2021 { get; set; }
        public float year_2022 { get; set; }
        public float year_2023 { get; set; }
    }
    public class PUE
    {
        public int PUE_ID { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public float year_2019 { get; set; }
        public float year_2020 { get; set; }
        public float year_2021 { get; set; }
        public float year_2022 { get; set; }
        public float year_2023 { get; set; }
    }
    public class WASTE
    {
        public int Waste_ID { get; set; }
        public string Waste_Metric { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public float year_2019 { get; set; }
        public float year_2020 { get; set; }
        public float year_2021 { get; set; }
        public float year_2022 { get; set; }
        public float year_2023 { get; set; }
    }
    public class WATER
    {
        public int Water_ID { get; set; }
        public string Water_Metric { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public float year_2019 { get; set; }
        public float year_2020 { get; set; }
        public float year_2021 { get; set; }
        public float year_2022 { get; set; }
        public float year_2023 { get; set; }
    }
    public class WATERBYREGIO
    {
        public int Water_By_Regio_ID { get; set; }
        public string Regio { get; set; }
        public string Factory { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
        public float year_2019 { get; set; }
        public float year_2020 { get; set; }
        public float year_2021 { get; set; }
        public float year_2022 { get; set; }
        public float year_2023 { get; set; }
        public int Water_ID { get; set; }
    }
}
