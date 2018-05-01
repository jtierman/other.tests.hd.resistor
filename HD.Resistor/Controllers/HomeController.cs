using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HD.Resistor.Controllers
{
    public class HomeController : Controller, IOhmValueCalculator
    {
        private readonly IColorCodes colorCodes;

        private readonly IOhmValueCalculator calculator;

        public HomeController(IColorCodes colorCodes, IOhmValueCalculator calculator)
        {
            this.colorCodes = colorCodes;
            this.calculator = calculator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("colors")]
        public ActionResult Colors(ResistorColorTypes types = ResistorColorTypes.Digit | ResistorColorTypes.Multiplier | ResistorColorTypes.Tolerance)
        {
            var colors = colorCodes.Get(types);
            return Json(colors);
        }


        [Route("calc")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor) => calculator.CalculateOhmValue(bandAColor, bandBColor, bandCColor, bandDColor);
    }
}