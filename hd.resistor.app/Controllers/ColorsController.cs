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
    public class ColorsController : Controller
    {
        private readonly IColorCodes colorCodes;

        public ColorsController(IColorCodes colorCodes)
        {
            this.colorCodes = colorCodes;
        }

        [HttpGet("{types}")]
        public IEnumerable<ResistorColor> Get(ResistorColorTypes? types = ResistorColorTypes.Digit)
        {
            return colorCodes.Get(types);
        }
    }
}
