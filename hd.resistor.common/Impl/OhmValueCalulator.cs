using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace HD.Resistor.Impl
{
    internal class OhmValueCalulator : IOhmValueCalculator
    {
        private readonly IColorCodes colorCodes;

        public OhmValueCalulator(IColorCodes colorCodes)
        {
            this.colorCodes = colorCodes;
        }


        public int CalculateOhmValue(int bandAColor, int? bandBColor, int? bandCColor, int? bandDColor)
        {
            return CalculateResistance(
                    (name: "bandAColor", value: bandAColor),
                    (name: "bandBColor", value: bandBColor),
                    (name: "bandCColor", value: bandCColor),
                    (name: "bandDColor", value: bandDColor)
                ).value;
        }
        public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            return CalculateResistance(
                    (name: "bandAColor", value: bandAColor), 
                    (name: "bandBColor", value: bandBColor),
                    (name: "bandCColor", value: bandCColor),
                    (name: "bandDColor", value: bandDColor)
                ).value;            
        }

        public (int value, bool reversed) CalculateResistance(params (string name, int? value)[] args)
        {
            var values = args.Where(arg => arg.value.HasValue).ToArray();
            var bands = validate((c, rgb) => c.RGB == rgb, values);

            var parsed = getColors(bands);
            var result = getValue(parsed.colors);

            return (result, parsed.reversed);
        }
        public (int value, bool reversed) CalculateResistance(params (string name, string value)[] args)
        {
            var values = args.Where(arg => !string.IsNullOrWhiteSpace(arg.value)).ToArray();
            var bands = validate<string>((c, s) => Color.FromName(s) == c.Color 
                                        || s.Equals(c.Code, StringComparison.OrdinalIgnoreCase)
                                        || s.Equals((c.RAL ?? int.MinValue).ToString()), 
                                 values);

            var parsed = getColors(bands);
            var result = getValue(parsed.colors);

            return (result, parsed.reversed);
        }

        private IDictionary<(string name, T value), ResistorColor[]> validate<T>(Func<ResistorColor, T, bool> comparer, params (string name, T value)[] args)
        {

            if (args.Length == 0)
                throw new ArgumentOutOfRangeException("no colors provided");

            var matched = args
                .ToDictionary(
                    arg => arg,
                    arg => colorCodes
                        .All
                        .Where(c => !Equals(arg.value, default(T)) && comparer(c, arg.value))
                        .ToArray());

            var invalid = args
                .Where(v => matched[v].All(rc => rc == null))
                .ToArray();

            if (invalid.Length != 0)
            {
                var parms = HttpUtility.JavaScriptStringEncode(string.Join("','", invalid.Select(x => x.name)));

                throw new ArgumentException($"{parms} are not valid");
            }

            var bands = matched
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Where(rc => rc != null).ToArray());


            if (bands.Count == 2)
                throw new ArgumentOutOfRangeException("Resistors need at least 3 bands");

            return bands;
        }
        private (Queue<ResistorColor> colors, bool reversed) getColors<T>(IDictionary<(string name, T value), ResistorColor[]> bands, bool reversed = false)
        {
            var normal = bands.Values
                .First()
                .Any(rc => rc != null 
                        && (rc.ColorType & ResistorColorTypes.Digit) == ResistorColorTypes.Digit);

            if (!normal)
                return getColors(bands.Reverse().ToDictionary(kv => kv.Key, kv => kv.Value), true);

            
            var result = new Queue<ResistorColor>();

            var keys = bands.Keys.ToArray();
            var color = null as ResistorColor;
            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var band = bands[key];
                var expected = ResistorColorTypes.Digit;

                switch (i + 1)
                {
                    case 1: break;
                    case 2: break;
                    case 3: expected = (bands.Count > 4) ? ResistorColorTypes.Digit : ResistorColorTypes.Multiplier; break;
                    case 4: expected = (bands.Count > 4) ? ResistorColorTypes.Multiplier : ResistorColorTypes.Tolerance; break;
                    case 5: expected = (bands.Count > 5) ? ResistorColorTypes.Multiplier : ResistorColorTypes.Tolerance; break;
                    case 6: throw new NotSupportedException("Six bands are currently not supported");
                }

                color = band.FirstOrDefault(rc => rc != null && (rc.ColorType & expected) == expected);

                if (color == null)
                    throw new ArgumentException($"{key} is an invalid color for band {i}");

                result.Enqueue(color);
            }

            if (bands.Count == 1 && color.Color != Color.Black)
                throw new ArgumentOutOfRangeException("Single banded resistors must be black");

            return (result, reversed);
        }
        private int getValue(Queue<ResistorColor> colors)
        {
            var result = 0d;
            var i = 0;
            var count = colors.Count;

            while (colors.TryDequeue(out ResistorColor color))
            {
                var expected = ResistorColorTypes.Digit;

                switch (++i)
                {
                    case 1: break;
                    case 2: break;
                    case 3: expected = (count > 4) ? ResistorColorTypes.Digit : ResistorColorTypes.Multiplier; break;
                    case 4: expected = (count > 4) ? ResistorColorTypes.Multiplier : ResistorColorTypes.Tolerance; break;
                    case 5: expected = (count > 5) ? ResistorColorTypes.Multiplier : ResistorColorTypes.Tolerance; break;
                    case 6: throw new NotSupportedException("Six bands are currently not supported");
                }

                switch (expected)
                {
                    case ResistorColorTypes.Digit:      result += ((byte)color.Val * Math.Pow(10, ((count > 4) ? 3 : 2) - i)); break;
                    case ResistorColorTypes.Multiplier: result *= Math.Pow(10, (sbyte)color.Val); break;
                    default: continue;
                }
            }

            return (int)Math.Round(result, 0);
        }
    }
}
