using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HD.Resistor.Impl
{
    internal class ColorCodes : IColorCodes
    {
        private readonly ResistorColor[] colors;

        public ColorCodes(IConfiguration configuration)
        {
            colors = configuration.GetSection("colors").Get<HD.Resistor.ResistorColor[]>();
        }

        public IEnumerable<ResistorColor> Values => colors.Where(c => c.ColorType == ResistorColorTypes.Digit);
        public IEnumerable<ResistorColor> Tolerances => colors.Where(c => c.ColorType == ResistorColorTypes.Tolerance);
        public IEnumerable<ResistorColor> Multipliers => colors.Where(c => c.ColorType == ResistorColorTypes.Multiplier);
        public IEnumerable<ResistorColor> All => colors;

        public IEnumerable<ResistorColor> Get(ResistorColorTypes? types = null)
        {
            var requested = types ?? ResistorColorTypes.Multiplier | ResistorColorTypes.Tolerance | ResistorColorTypes.Digit;

            return All
                .Where(c => (requested & c.ColorType & ResistorColorTypes.Digit)        == ResistorColorTypes.Digit
                         || (requested & c.ColorType & ResistorColorTypes.Multiplier)   == ResistorColorTypes.Multiplier
                         || (requested & c.ColorType & ResistorColorTypes.Tolerance)    == ResistorColorTypes.Tolerance)
                .ToArray();
        }
    }
}
