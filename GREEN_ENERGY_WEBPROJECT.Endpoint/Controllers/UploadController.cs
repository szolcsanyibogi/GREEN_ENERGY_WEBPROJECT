using System;
using Microsoft.AspNetCore.Mvc;
using GREEN_ENERGY_WEBPROJECT.Models;
using GREEN_ENERGY_WEBPROJECT.Repository_PUT;
using Microsoft.AspNetCore.Http; 

namespace GREEN_ENERGY_WEBPROJECT.Controllers
{
    public class UploadController : Controller
    {
        private readonly Repository_PUT.Repository_PUT _repository;

        public UploadController()
        {
            string connectionString = "Data Source=DESKTOP-09M9588;Initial Catalog=ATH_DW_GREEN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            _repository = new Repository_PUT.Repository_PUT(connectionString);
        }

        [HttpGet]
        public IActionResult Upload_data()
        {
            return View("Upload_data");
        }


        [HttpPost]
        public IActionResult InsertData(string SelectedTable, IFormCollection form)
        {
            try
            {
                switch (SelectedTable)
                {
                    case "DW_CFE":
                        _repository.PutCFE(
                            form["Country"],
                            form["Regional_Grid"],
                            form["Factory"],
                            form["Unit"],
                            int.Parse(form["CFE_Percentage"]));
                        break;

                    case "DW_ENERGY":
                        _repository.PutENERGY(
                            form["Energy_Consumption"],
                            form["Factory"],
                            form["Unit"],
                            int.Parse(form["Year_2019"]),
                            int.Parse(form["Year_2020"]),
                            int.Parse(form["Year_2021"]),
                            int.Parse(form["Year_2022"]),
                            int.Parse(form["Year_2023"]));
                        break;

                    case "DW_GHG":
                        _repository.PutGHG(
                            form["Carbon_Intensity"],
                            form["Factory"],
                            form["Unit"],
                            float.Parse(form["Year_2019"]),
                            float.Parse(form["Year_2020"]),
                            float.Parse(form["Year_2021"]),
                            float.Parse(form["Year_2022"]),
                            float.Parse(form["Year_2023"]));
                        break;

                    case "DW_PUE":
                        _repository.PutPUE(
                            form["Country"],
                            form["Location"],
                            form["Factory"],
                            form["Unit"],
                            float.Parse(form["Year_2019"]),
                            float.Parse(form["Year_2020"]),
                            float.Parse(form["Year_2021"]),
                            float.Parse(form["Year_2022"]),
                            float.Parse(form["Year_2023"]));
                        break;

                    case "DW_WASTE":
                        _repository.PutWASTE(
                            form["Waste_Metric"],
                            form["Factory"],
                            form["Unit"],
                            float.Parse(form["Year_2019"]),
                            float.Parse(form["Year_2020"]),
                            float.Parse(form["Year_2021"]),
                            float.Parse(form["Year_2022"]),
                            float.Parse(form["Year_2023"]));
                        break;

                    case "DW_WATER":
                        _repository.PutWater(
                            form["Water_Metric"],
                            form["Factory"],
                            form["Unit"],
                            float.Parse(form["Year_2019"]),
                            float.Parse(form["Year_2020"]),
                            float.Parse(form["Year_2021"]),
                            float.Parse(form["Year_2022"]),
                            float.Parse(form["Year_2023"]));
                        break;

                    case "DW_WATER_BY_REGIO":
                        _repository.PutWaterByRegio(
                            form["Regio"],
                            form["Factory"],
                            form["Unit"],
                            form["Category"],
                            float.Parse(form["Year_2019"]),
                            float.Parse(form["Year_2020"]),
                            float.Parse(form["Year_2021"]),
                            float.Parse(form["Year_2022"]),
                            float.Parse(form["Year_2023"]));
                        break;

                    default:
                        throw new Exception("Ismeretlen tábla: " + SelectedTable);
                }

                TempData["Message"] = "✅ Upload successful!";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ An error occurred: {ex.Message}";
            }

            return RedirectToAction("Upload_data");
        }
    }
}
