using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HD.Resistor.Impl;
using System;

namespace HD.Resistor.Common.Tests
{
    [TestClass]
    public class OhmValueCalculatorTests
    {
        public IConfigurationRoot Configuration { get; private set; }

        private IColorCodes colorCodes;
        private IOhmValueCalculator calculator;

        [TestInitialize]
        public void Init()
        {
            var cb = new ConfigurationBuilder();
            
            cb.AddJsonFile(j =>
            {
                j.Path = "appsettings.json";
                j.OnLoadException = e => { e.Ignore = false; };
            });
            Configuration = cb.Build();           

            colorCodes = new Impl.ColorCodes(Configuration);
            calculator = new Impl.OhmValueCalulator(colorCodes);
        }
        [TestMethod]
        public void Test0Bands()
        {
            var bands = new[] { string.Empty, null, null, null };
            var ex = null as Exception;

            try { var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]); }
            catch(Exception e) { ex = e; }

            Assert.IsNotNull(ex);
        }
        [TestMethod]
        public void Test1Band_0_First()
        {
            var bands = new[] { "black", null, null, null };
            var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]);

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Test1Band_Invalid_First()
        {
            var bands = new[] { "orange", null, null, null };
            var ex = null as Exception;

            try { var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]); }
            catch (Exception e) { ex = e; }

            Assert.IsNotNull(ex);
        }
        [TestMethod]
        public void Test1Band_0_Middle()
        {
            var bands = new[] { null, "black", null, null };
            var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]);

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Test2Bands_First()
        {
            var bands = new[] { "orange", "black", null, null };

            var ex = null as Exception;

            try { var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]); }
            catch (Exception e) { ex = e; }

            Assert.IsNotNull(ex);
        }
        [TestMethod]
        public void Test2Bands_Last()
        {
            var bands = new[] { " ",  "   ", "orange", "black" };

            var ex = null as Exception;

            try { var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]); }
            catch (Exception e) { ex = e; }

            Assert.IsNotNull(ex);
        }


        [TestMethod]
        public void Test3Band_330()
        {
            var bands = new[] { "orange", "orange", "brown", null };
            var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]);

            Assert.AreEqual(330, result);
        }
        [TestMethod]
        public void Test4Band_82k()
        {
            var bands = new[] { "gray", "red", "orange", "yellow" };
            var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]);

            Assert.AreEqual(82000, result);
        }
        [TestMethod]
        public void Test4Band_4700()
        {
            var bands = new[] { "yellow", "Violet", "red", "silver" };
            var result = calculator.CalculateOhmValue(bands[0], bands[1], bands[2], bands[3]);

            Assert.AreEqual(4700, result);
        }
    }
}
