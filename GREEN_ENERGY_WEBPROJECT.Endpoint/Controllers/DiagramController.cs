using Microsoft.AspNetCore.Mvc;
using GREEN_ENERGY_WEBPROJECT.Models;
using GREEN_ENERGY_WEBPROJECT.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Diagrams(int? factoryId, int? year)
        {
            var unit = _repository.GetUNIT_NAME("PUE").FirstOrDefault();
            if (unit == null) return NotFound("PUE unit not found.");

            var allFacts = _repository.GetFACTS_BY_UNITID(unit.UNIT_ID);
            var factories = _repository.GetDistinctFactoryNames(); // már külön névvel szűrve
            var dates = _repository.GetAllDates();

            ViewBag.Factories = new SelectList(factories, "FACTORY_ID", "FACTORY_NAME", factoryId);
            ViewBag.Years = new SelectList(dates, "YEAR", "YEAR", year);

            var facts = allFacts;

            if (factoryId.HasValue)
            {
                var selectedFactory = factories.FirstOrDefault(f => f.FACTORY_ID == factoryId);
                if (selectedFactory != null)
                {
                    string selectedName = selectedFactory.FACTORY_NAME;
                    facts = facts.Where(f => f.FACTORY.FACTORY_NAME == selectedName).ToList();
                }
            }

            if (year.HasValue)
            {
                facts = facts.Where(f => f.DATE.YEAR == year).ToList();
            }

            var grouped = facts
                .GroupBy(f => new { f.FACTORY.FACTORY_NAME, f.DATE.YEAR })
                .Select(g => new
                {
                    FactoryName = g.Key.FACTORY_NAME,
                    Year = g.Key.YEAR,
                    AvgPUE = g.Average(f => f.VALUE)
                })
                .ToList();

            ViewBag.PUEDataJson = JsonConvert.SerializeObject(grouped);
            return View();
        }

    }
}
