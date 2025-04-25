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
        public IActionResult ThirdDiagram(int? factoryId, int? year, string category)
        {
            ViewBag.Factories = new SelectList(_repository.GetDistinctFactoryNames(), "FACTORY_ID", "FACTORY_NAME", factoryId);
            ViewBag.Years = new SelectList(_repository.GetYears(), "YEAR", "YEAR", year);
            ViewBag.Categories = new SelectList(new List<string> { "Water withdrawal", "Water consumption", "Water discharges" }, category);

            if (factoryId == null || year == null || string.IsNullOrEmpty(category))
            {
                ViewBag.WaterMapDataJson = "[]";
                return View("ThirdDiagram");
            }

            var selectedFactory = _repository.GetFACTORY(factoryId.Value);
            var dateId = _repository.GetAllDates().FirstOrDefault(d => d.YEAR == year)?.DATE_ID ?? -1;
            if (selectedFactory == null || dateId == -1)
            {
                ViewBag.WaterMapDataJson = "[]";
                return View("ThirdDiagram");
            }

            var facts = _repository.GetAllFacts()
                .Where(f => f.FACTORY_ID == selectedFactory.FACTORY_ID && f.DATE_ID == dateId)
                .ToList();

            var metrics = _repository.GetDIM_METRIC();

            var factsWithMetric = (from fact in facts
                                   join metric in metrics on fact.METRIC_ID equals metric.METRIC_ID
                                   select new
                                   {
                                       fact.FACTORY_ID,
                                       fact.DATE_ID,
                                       fact.VALUE,
                                       MetricName = metric.METRIC_NAME
                                   }).ToList();

            if (!factsWithMetric.Any())
            {
                ViewBag.WaterMapDataJson = "[]";
                return View("ThirdDiagram");
            }

            // 1. Eldobjuk a legnagyobb VALUE értéket a csoportonként (Factory + Date)
            var grouped = factsWithMetric
                .GroupBy(f => new { f.FACTORY_ID, f.DATE_ID })
                .SelectMany(g =>
                {
                    var maxValue = g.Max(x => x.VALUE);
                    return g.Where(x => x.VALUE != maxValue);
                })
                .Where(f => f.MetricName == category)
                .ToList();

            // 2. Leképzés Factory + Category → Regio
            var categoryRegionMap = new List<(string Factory, string Category, string Regio)>
    {
        ("Microsoft", "Water withdrawal", "Asia"),
        ("Microsoft", "Water withdrawal", "Europe, Middle East, Africa"),
        ("Microsoft", "Water withdrawal", "Latin America"),
        ("Microsoft", "Water withdrawal", "North America"),
        ("Microsoft", "Water consumption", "Asia"),
        ("Microsoft", "Water consumption", "Europe, Middle East, Africa"),
        ("Microsoft", "Water consumption", "Latin America"),
        ("Microsoft", "Water consumption", "North America"),
        ("Microsoft", "Water discharges", "Asia"),
        ("Microsoft", "Water discharges", "Europe, Middle East, Africa"),
        ("Microsoft", "Water discharges", "Latin America"),
        ("Microsoft", "Water discharges", "North America"),
        ("Google", "Water withdrawal", "Asia"),
        ("Google", "Water withdrawal", "Europe, Middle East, Africa"),
        ("Google", "Water withdrawal", "Latin America"),
        ("Google", "Water withdrawal", "North America"),
        ("Google", "Water consumption", "Asia"),
        ("Google", "Water consumption", "Europe, Middle East, Africa"),
        ("Google", "Water consumption", "Latin America"),
        ("Google", "Water consumption", "North America"),
        ("Google", "Water discharges", "Asia"),
        ("Google", "Water discharges", "Europe, Middle East, Africa"),
        ("Google", "Water discharges", "Latin America"),
        ("Google", "Water discharges", "North America")
    };

            var factoryName = selectedFactory.FACTORY_NAME;

            // 3. Hozzárendeljük a régiókat, csak ha létezik rá párosítás
            var finalData = grouped
                .SelectMany(fact =>
                    categoryRegionMap
                        .Where(map => map.Factory == factoryName && map.Category == category)
                        .Select(map => new
                        {
                            Region = map.Regio,
                            Amount = fact.VALUE
                        })
                )
                .ToList();

            // 4. Régiók szerint összesítünk
            var regionGrouped = finalData
                .GroupBy(x => x.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    Amount = Math.Round(g.Sum(x => x.Amount), 2)
                }).ToList();

            ViewBag.WaterMapDataJson = JsonConvert.SerializeObject(regionGrouped);

            return View("ThirdDiagram");
        }

        [HttpGet]
        [HttpGet]
        public IActionResult FourthDiagram(int? factoryId, string source)
        {
            var allowedSources = new List<string>
    {
        "Fuel",
        "Electricity",
        "Hot water",
        "Steam",
        "Cooling",
        "On-site renewable electricity",
        "Total energy consumption",
        "Total electricity consumption"
    };

            ViewBag.Factories = new SelectList(_repository.GetDistinctFactoryNames(), "FACTORY_ID", "FACTORY_NAME", factoryId);
            ViewBag.Sources = new SelectList(new[] { "All Source" }.Concat(allowedSources), source ?? "All Source");

            var allFacts = _repository.GetAllFacts();
            var metrics = _repository.GetDIM_METRIC();
            var dates = _repository.GetAllDates();
            var factories = _repository.GetAllFactories();

            if (string.IsNullOrEmpty(source))
            {
                source = "All Source";
            }

            // Összekapcsoljuk a tényadatokat a metrikákkal
            var factWithMetric = allFacts
                .Join(metrics, f => f.METRIC_ID, m => m.METRIC_ID, (f, m) => new
                {
                    Fact = f,
                    MetricName = m.METRIC_NAME
                });

            List<dynamic> result = new List<dynamic>();

            if (source == "All Source")
            {
                // Minden energiaforrást hozunk, csoportosítva gyár és forrás szerint
                result = factWithMetric
                    .Where(x => allowedSources.Contains(x.MetricName))
                    .Join(dates, x => x.Fact.DATE_ID, d => d.DATE_ID, (x, d) => new
                    {
                        FactoryId = x.Fact.FACTORY_ID,
                        Source = x.MetricName,
                        Year = d.YEAR,
                        Value = x.Fact.UNIT.UNIT_NAME == "GJ"
                            ? Math.Round(x.Fact.VALUE / 3.6f, 2)
                            : Math.Round(x.Fact.VALUE, 2)
                    })
                    .Join(factories, f => f.FactoryId, fac => fac.FACTORY_ID, (f, fac) => new
                    {
                        Factory = fac.FACTORY_NAME,
                        Source = f.Source,
                        Year = f.Year,
                        Value = f.Value
                    })
                    .ToList<dynamic>();

                if (factoryId != null)
                {
                    string factoryName = factories.FirstOrDefault(f => f.FACTORY_ID == factoryId.Value)?.FACTORY_NAME;
                    result = result.Where(r => r.Factory == factoryName).ToList<dynamic>();
                }
            }
            else
            {
                // Csak a kiválasztott energiaforrást hozzuk, gyáronként csoportosítva
                result = factWithMetric
                    .Where(x => x.MetricName == source)
                    .Join(dates, x => x.Fact.DATE_ID, d => d.DATE_ID, (x, d) => new
                    {
                        FactoryId = x.Fact.FACTORY_ID,
                        Year = d.YEAR,
                        Value = x.Fact.UNIT.UNIT_NAME == "GJ"
                            ? Math.Round(x.Fact.VALUE / 3.6f, 2)
                            : Math.Round(x.Fact.VALUE, 2)
                    })
                    .Join(factories, f => f.FactoryId, fac => fac.FACTORY_ID, (f, fac) => new
                    {
                        Factory = fac.FACTORY_NAME,
                        Source = source,
                        Year = f.Year,
                        Value = f.Value
                    })
                    .ToList<dynamic>();

                if (factoryId != null)
                {
                    string factoryName = factories.FirstOrDefault(f => f.FACTORY_ID == factoryId.Value)?.FACTORY_NAME;
                    result = result.Where(r => r.Factory == factoryName).ToList<dynamic>();
                }
            }

            ViewBag.EnergyDataJson = JsonConvert.SerializeObject(result);

            return View("FourthDiagram");
        }








    }
}
