using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HD.Resistor
{
    [JsonObject(
        MemberSerialization = MemberSerialization.OptOut,
        NamingStrategyType = typeof(Newtonsoft.Json.Serialization.DefaultNamingStrategy),
        ItemTypeNameHandling = TypeNameHandling.None,
        Id = "Code")]
    public class Tolerance
    {
        public string Code { get; }
        public double Percent { get; }
    }
}

