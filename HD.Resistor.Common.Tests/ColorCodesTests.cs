using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HD.Resistor.Impl;

namespace HD.Resistor.Common.Tests
{
    [TestClass]
    public class ColorCodesTests
    {
        public IConfigurationRoot Configuration { get; private set; }

        private IColorCodes colorCodes;

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
            Assert.IsNotNull(colorCodes);
        }
        [TestMethod]
        public void EnsureConfiguration()
        {
            var all = colorCodes.All.ToArray();
            Assert.IsNotNull(all);
            CollectionAssert.AllItemsAreNotNull(all);
            CollectionAssert.AllItemsAreUnique(all);

            var values = colorCodes.Values.ToArray();
            Assert.IsNotNull(values);
            CollectionAssert.AllItemsAreNotNull(values);
            CollectionAssert.AllItemsAreUnique(values);
            CollectionAssert.IsSubsetOf(values, all);

            var mul = colorCodes.Multipliers.ToArray();
            Assert.IsNotNull(mul);
            CollectionAssert.AllItemsAreNotNull(mul);
            CollectionAssert.AllItemsAreUnique(mul);
            CollectionAssert.IsSubsetOf(mul, all);

            var tol = colorCodes.Tolerances.ToArray();
            Assert.IsNotNull(tol);
            CollectionAssert.AllItemsAreNotNull(tol);
            CollectionAssert.AllItemsAreUnique(tol);
            CollectionAssert.IsSubsetOf(tol, all);
        }

        [TestMethod]
        public void TestGetValues()
        {
            var values = colorCodes.Get(ResistorColorTypes.Digit).ToArray();
            var all = colorCodes.All.ToArray();

            Assert.IsNotNull(values);
            CollectionAssert.AllItemsAreNotNull(values);
            CollectionAssert.AllItemsAreUnique(values);
            CollectionAssert.IsSubsetOf(values, all);
        }
        [TestMethod]
        public void TestGetMultiplier()
        {
            var mul = colorCodes.Get(ResistorColorTypes.Multiplier).ToArray();
            var all = colorCodes.All.ToArray();

            Assert.IsNotNull(mul);
            CollectionAssert.AllItemsAreNotNull(mul);
            CollectionAssert.AllItemsAreUnique(mul);
            CollectionAssert.IsSubsetOf(mul, all);
        }
        [TestMethod]
        public void TestGetMultiplierAndValues()
        {
            var both = colorCodes.Get(ResistorColorTypes.Multiplier | ResistorColorTypes.Digit).ToArray();
            var all = colorCodes.All.ToArray();

            Assert.IsNotNull(both);
            CollectionAssert.AllItemsAreNotNull(both);
            CollectionAssert.AllItemsAreUnique(both);
            CollectionAssert.IsSubsetOf(both, all);
        }
    }
}
