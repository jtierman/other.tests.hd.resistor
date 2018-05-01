using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace HD.Resistor
{
    [JsonObject(
        MemberSerialization = MemberSerialization.OptOut, 
        NamingStrategyType = typeof(Newtonsoft.Json.Serialization.DefaultNamingStrategy),
        ItemTypeNameHandling = TypeNameHandling.None, 
        Id = "Name")]
    public class ResistorColor
    {
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter), false)]
        public ResistorColorTypes ColorType { get; set; }

        [JsonRequired]
        public string Name { get; set; }
        public string Code { get; set; }
        public int? RAL { get; set; }   
        public int? Val { get; set; }

        public Color Color { get { return Color.FromName(Name); } }
        public int RGB { get { return this.Color.ToArgb(); } }

        [JsonProperty(
            ItemTypeNameHandling = TypeNameHandling.None, 
            NamingStrategyType = typeof(Newtonsoft.Json.Serialization.DefaultNamingStrategy))]
        public Tolerance[] Tolerances { get; set; }

        //public ResistorColor( ResistorColorTypes type, string name, string code, int ral, sbyte? value = null, Tolerance[] tolerances = null)
        //{
        //    ColorType = type;
        //    Name = name;
        //    Code = code;
        //    RAL = ral;
        //    Value = value;
        //    Tolerances = tolerances;
        //}

    }
}
