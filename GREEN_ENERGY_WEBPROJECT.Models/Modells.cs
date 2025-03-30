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
        public int DATE_ID { get; set; }
        public int METRIC_ID { get; set; }
        public int UNIT_ID { get; set; }
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
    public class DATE{
        public int DATE_ID { get; set; }
        public int YEAR { get; set; }
        public int MONTH { get; set; }
    }

}
