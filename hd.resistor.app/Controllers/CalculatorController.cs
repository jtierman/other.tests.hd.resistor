using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HD.Resistor;

namespace hd.resistor.app.Controllers
{
    [ApiExport]
    [Route("api/[controller]")]
    public class CalculatorController : Controller, IOhmValueCalculator
    {
        private readonly IOhmValueCalculator calculator;

        public CalculatorController(IOhmValueCalculator calculator)
        {
            this.calculator = calculator;
        }

        [HttpGet]
        public int CalculateOhmValue(
            [FromQuery] string a,
            [FromQuery] string b,
            [FromQuery] string c,
            [FromQuery] string d
        )
        {
            return calculator.CalculateOhmValue(a, b, c, d);
        }
    }
}
