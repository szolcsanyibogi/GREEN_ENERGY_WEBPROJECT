using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using GREEN_ENERGY_WEBPROJECT.Repository;
using System.Linq;
using System;
using System.Collections.Generic;
using GREEN_ENERGY_WEBPROJECT.Models;
using Microsoft.EntityFrameworkCore;
using GREEN_ENERGY_WEBPROJECT.Endpoint.Data;
using System.Text.RegularExpressions;

namespace GREEN_ENERGY_WEBPROJECT.Controllers
{
    public class DiagramController : Controller
    {
        private readonly Repository_GET _repository;
        private readonly AppDbContext _context;

        public DiagramController(AppDbContext context)
        {
            string connectionString = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_STAR_GREEN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            _repository = new Repository_GET(connectionString);
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> FirstDiagram(int? factoryId, int? year)
        {
            var unit = _repository.GetUNIT_NAME("PUE").FirstOrDefault();
            if (unit == null) return View();

            var allFacts = _repository.GetFACTS_BY_UNITID(unit.UNIT_ID);

            var filtered = allFacts
                .Where(f => (factoryId == null || f.FACTORY.FACTORY_NAME == 
                _repository.GetFACTORY(factoryId.Value)?.FACTORY_NAME) &&
                            (year == null || f.DATE.YEAR == year))
                .ToList();

            var grouped = filtered
                .Where(f => f.FACTORY != null && f.DATE != null)
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

            var description = await _context.FirstDiagramContents.FirstOrDefaultAsync();
            ViewBag.FirstDiagramDescription = description?.Description ?? "Default description for PUE diagram.";

            return View("FirstDiagram");
        }

        [HttpPost]
        public async Task<IActionResult> SaveFirstDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.FirstDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.FirstDiagramContents.Add(new FirstDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.FirstDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("FirstDiagram");
        }

        [HttpGet]
        public async Task<IActionResult> SecondDiagram(int? factoryId, int? year)
        {
            var metric = _repository.GetMETRIC_NAME("Waste generated").FirstOrDefault();
            if (metric == null) return View();

            var allFacts = _repository.GetFACTS_BY_METRICID(metric.METRIC_ID);

            var validFacts = allFacts
                .Where(f =>
                    f.FACTORY != null &&
                    f.DATE != null &&
                    f.VALUE != null &&
                    (factoryId == null || f.FACTORY.FACTORY_ID == factoryId) &&
                    (year == null || f.DATE.YEAR == year))
                .ToList();

            var grouped = validFacts
                .GroupBy(f => new { f.FACTORY.FACTORY_NAME, f.DATE.YEAR })
                .Select(g => new
                {
                    FactoryName = g.Key.FACTORY_NAME,
                    Year = g.Key.YEAR,
                    AvgWaste = Math.Round(g.Average(f => f.VALUE), 2)
                })
                .Where(g => g.AvgWaste > 0)
                .ToList();

            ViewBag.WasteDataJson = JsonConvert.SerializeObject(grouped);


            ViewBag.Factories = new SelectList(_repository.GetDistinctFactoryNames(), "FACTORY_ID", "FACTORY_NAME");
            ViewBag.Years = new SelectList(_repository.GetYears(), "YEAR", "YEAR");

            var description = await _context.SecondDiagramContents.FirstOrDefaultAsync();
            ViewBag.SecondDiagramDescription = description?.Description ?? "Default description for Waste diagram.";

            return View("SecondDiagram");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSecondDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.SecondDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.SecondDiagramContents.Add(new SecondDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.SecondDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("SecondDiagram");
        }

        [HttpGet]
        public IActionResult ThirdDiagram(string factory)
        {
            var facts = _repository.GetAllFacts();
            var metrics = _repository.GetDIM_METRIC();
            var factories = _repository.GetAllFactories();

            ViewBag.Factories = new SelectList(factories.Select(f => f.FACTORY_NAME).Distinct(), factory);

            var waterCategories = new List<string> { "Water withdrawal", "Water discharge", "Water consumption" };

            var data = (from fact in facts
                        join metric in metrics on fact.METRIC_ID equals metric.METRIC_ID
                        join factFactory in factories on fact.FACTORY_ID equals factFactory.FACTORY_ID
                        where waterCategories.Contains(metric.METRIC_NAME)
                        select new
                        {
                            Factory = factFactory.FACTORY_NAME,
                            MetricName = metric.METRIC_NAME,
                            Year = fact.DATE.YEAR,
                            Value = fact.VALUE
                        }).ToList();

            if (!string.IsNullOrEmpty(factory))
            {
                data = data.Where(d => d.Factory == factory).ToList();
            }

            var groupedData = data
                .GroupBy(d => new { d.MetricName, d.Year })
                .Select(g => new
                {
                    Metric = g.Key.MetricName,
                    Year = g.Key.Year,
                    AvgValue = g.Average(x => x.Value)
                }).ToList();

            ViewBag.WaterDataJson = JsonConvert.SerializeObject(groupedData);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveThirdDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.ThirdDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.ThirdDiagramContents.Add(new ThirdDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.ThirdDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ThirdDiagram");
        }

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


            var factWithMetric = allFacts
                .Join(metrics, f => f.METRIC_ID, m => m.METRIC_ID, (f, m) => new
                {
                    Fact = f,
                    MetricName = m.METRIC_NAME
                });

            List<dynamic> result = new List<dynamic>();

            if (source == "All Source")
            {
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

        [HttpPost]
        public async Task<IActionResult> SaveFourthDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.FourthDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.FourthDiagramContents.Add(new FourthDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.FourthDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("FourthDiagram");
        }

        [HttpGet]
        public IActionResult FifthDiagram()
        {
            var unitName = "%";
            var date = _repository.GetAllDates().FirstOrDefault(d => d.YEAR == 2023);

            if (date == null)
            {
                ViewBag.CFEDataJson = "[]";
                ViewBag.CFETableData = new List<dynamic>();
                return View("FifthDiagram");
            }

            var facts = _repository.GetAllFacts()
                .Where(f =>
                    f.DATE_ID == date.DATE_ID &&
                    f.UNIT != null &&
                    f.UNIT.UNIT_NAME == unitName &&
                    f.FACTORY != null &&
                    f.VALUE != null)
                .ToList();

            var grouped = facts
                .GroupBy(f => f.FACTORY.COUNTRY)
                .Select(g => new
                {
                    Country = g.Key,
                    Percentage = Math.Round(g.Average(f => f.VALUE), 2)
                })
                .ToList();

            ViewBag.CFEDataJson = JsonConvert.SerializeObject(grouped);
            return View("FifthDiagram");
        }


        [HttpPost]
        public async Task<IActionResult> SaveFifthDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.FifthDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.FifthDiagramContents.Add(new FifthDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.FifthDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("FifthDiagram");
        }

        [HttpGet]
        public IActionResult SixthDiagram()
        {
            var allFacts = _repository.GetAllFacts();
            var allMetrics = _repository.GetDIM_METRIC();
            var dates = _repository.GetAllDates();

            if (allFacts == null || allMetrics == null || dates == null)
            {
                ViewBag.GHGDataJson = "[]";
                return View("GreenhouseGasIntensityDiagram");
            }

            var energyMetrics = allMetrics
                .Where(m => m.METRIC_NAME.Contains("Total energy consumption"))
                .Select(m => m.METRIC_ID)
                .ToList();

            var ghgMetrics = allMetrics
                .Where(m => m.METRIC_NAME.Contains("GHG intensity per MWh") ||
                            m.METRIC_NAME.Contains("Carbon intensity per megawatthour of energy consumed"))
                .Select(m => m.METRIC_ID)
                .ToList();

            var ghgData = new List<object>();

            foreach (var year in new[] { 2019, 2020, 2021, 2022, 2023 })
            {
                var dateId = dates.FirstOrDefault(d => d.YEAR == year)?.DATE_ID ?? -1;
                if (dateId == -1) continue;

                var energyFacts = allFacts
                    .Where(f => energyMetrics.Contains(f.METRIC_ID) && f.DATE_ID == dateId)
                    .ToList();

                var ghgFacts = allFacts
                    .Where(f => ghgMetrics.Contains(f.METRIC_ID) && f.DATE_ID == dateId)
                    .ToList();

                var joined = energyFacts
                    .Join(
                        ghgFacts,
                        e => e.FACTORY_ID,
                        g => g.FACTORY_ID,
                        (e, g) => new { Factory = e.FACTORY, EnergyConsumption = e.VALUE, GHGIntensity = g.VALUE }
                    )
                    .GroupBy(x => new { x.Factory.FACTORY_NAME, year })
                    .Select(g => new
                    {
                        Company = g.Key.FACTORY_NAME,
                        Year = g.Key.year,
                        EnergyConsumption = Math.Round(g.Average(x => x.EnergyConsumption), 2),
                        GHGIntensity = Math.Round(g.Average(x => x.GHGIntensity), 5)
                    })
                    .ToList();

                ghgData.AddRange(joined);
            }

            ViewBag.GHGDataJson = JsonConvert.SerializeObject(ghgData);

            return View("SixthDiagram");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSixthDiagramDescription(string description)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.SixthDiagramContents.FirstOrDefaultAsync();
                if (existing == null)
                    _context.SixthDiagramContents.Add(new SixthDiagramContent { Description = description });
                else
                {
                    existing.Description = description;
                    _context.SixthDiagramContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("SixthDiagram");
        }



    }
}
