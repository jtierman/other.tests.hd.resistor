using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HD.Resistor.Controllers
{
    [Route("api/[controller]")]
    public class ColorCodesController : Controller
    {
        private readonly IColorCodes colorCodes;

        public ColorCodesController(IColorCodes colorCodes)
        {
            this.colorCodes = colorCodes;
        }

        [HttpGet("[action]")]
        public IEnumerable<ResistorColor> Get(ResistorColorTypes? types = null)
        {
            return colorCodes.Get(types);
        }
    }
}
