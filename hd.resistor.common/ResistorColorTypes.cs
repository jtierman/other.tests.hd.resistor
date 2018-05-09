using Newtonsoft.Json;
using System;
using System.Composition;

namespace HD.Resistor
{
    [JsonExport]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter), false)]
    [Flags]
    public enum ResistorColorTypes
    {
        None        = 0,
        Digit       = 1,
        Multiplier  = 2,
        Tolerance   = 4
    }
}
