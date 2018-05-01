using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.Resistor
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterResistorCommon(this IServiceCollection serviceCollection)
        {
            if (!serviceCollection.Any(d => d.ServiceType == typeof(IConfiguration)))
                throw new InvalidProgramException("Configuration has not been loaded yet");

            serviceCollection.AddSingleton<IColorCodes, Impl.ColorCodes>();
            serviceCollection.AddTransient<IOhmValueCalculator, Impl.OhmValueCalulator>();

            return serviceCollection;
        }
    }
}
