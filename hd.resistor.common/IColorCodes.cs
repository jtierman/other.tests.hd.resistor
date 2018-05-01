using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HD.Resistor
{
    public interface IColorCodes
    {
        IEnumerable<ResistorColor> Values { get; }
        IEnumerable<ResistorColor> Tolerances { get; }
        IEnumerable<ResistorColor> Multipliers { get; }

        IEnumerable<ResistorColor> All { get; }

        IEnumerable<ResistorColor> Get(ResistorColorTypes? types = null);
    }
}
