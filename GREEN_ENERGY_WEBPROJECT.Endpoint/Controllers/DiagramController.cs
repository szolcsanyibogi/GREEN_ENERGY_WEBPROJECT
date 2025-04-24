using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using GREEN_ENERGY_WEBPROJECT.Repository;
using System.Linq;
using System;
using System.Collections.Generic;

namespace GREEN_ENERGY_WEBPROJECT.Controllers
{
    public class DiagramController : Controller
    {
        private readonly Repository_GET _repository;

        public DiagramController()
        {
            string connectionString = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_STAR_GREEN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            _repository = new Repository_GET(connectionString);
        }

        [HttpGet]
        public IActionResult FirstDiagram(int? factoryId, int? year)
        {
            var unit = _repository.GetUNIT_NAME("PUE").FirstOrDefault();
            if (unit == null) return View();

            var allFacts = _repository.GetFACTS_BY_UNITID(unit.UNIT_ID);

            var filtered = allFacts
                .Where(f => (factoryId == null || f.FACTORY.FACTORY_NAME == _repository.GetFACTORY(factoryId.Value)?.FACTORY_NAME) &&
                            (year == null || f.DATE.YEAR == year))
                .ToList();

            var grouped = filtered
                .GroupBy(f => new { f.FACTORY.FACTORY_NAME, f.DATE.YEAR })
                .Select(g => new
                {
                    FactoryName = g.Key.FACTORY_NAME,
                    Year = g.Key.YEAR,
                    AvgPUE = Math.Round(g.Average(f => f.VALUE), 2)
                }).ToList();

            ViewBag.PUEDataJson = JsonConvert.SerializeObject(grouped);
            ViewBag.Factories = new SelectList(_repository.GetDistinctFactoryNames(), "FACTORY_ID", "FACTORY_NAME");
            ViewBag.Years = new SelectList(_repository.GetYears(), "YEAR", "YEAR");

            return View("FirstDiagram");
        }

        [HttpGet]
        public IActionResult SecondDiagram(int? factoryId, int? year)
        {
            var metric = _repository.GetMETRIC_NAME("Waste generated").FirstOrDefault();
            if (metric == null) return View();

            var allFacts = _repository.GetFACTS_BY_METRICID(metric.METRIC_ID);

            var filtered = allFacts
                .Where(f => (factoryId == null || f.FACTORY.FACTORY_NAME == _repository.GetFACTORY(factoryId.Value)?.FACTORY_NAME) &&
                            (year == null || f.DATE.YEAR == year))
                .ToList();

            var grouped = filtered
                .GroupBy(f => new { f.FACTORY.FACTORY_NAME, f.DATE.YEAR })
                .Select(g => new
                {
                    FactoryName = g.Key.FACTORY_NAME,
                    Year = g.Key.YEAR,
                    AvgWaste = Math.Round(g.Average(f => f.VALUE), 2)
                }).ToList();

            ViewBag.WasteDataJson = JsonConvert.SerializeObject(grouped);
            ViewBag.Factories = new SelectList(_repository.GetDistinctFactoryNames(), "FACTORY_ID", "FACTORY_NAME");
            ViewBag.Years = new SelectList(_repository.GetYears(), "YEAR", "YEAR");

            return View("SecondDiagram");
        }

        [HttpGet]
        public IActionResult ThirdDiagram()
        {
            var waterUsageByRegion = new List<object>
            {
                new { Region = "North America", Amount = 12985.53 },
                new { Region = "Europe, Middle East, Africa", Amount = 6320.6 },
                new { Region = "Latin America", Amount = 1053.5 },
                new { Region = "Asia", Amount = 7294.3 }
            };

            ViewBag.WaterMapDataJson = JsonConvert.SerializeObject(waterUsageByRegion);
            return View();
        }

    }
}
