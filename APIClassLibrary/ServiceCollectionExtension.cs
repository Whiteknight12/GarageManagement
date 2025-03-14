using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary
{
    public static class ServiceCollectionExtension
    {
        public static void AddAPIClientService<T>(this IServiceCollection services, Action<APIClientOptions> options, string endpoint) where T : class
        {
            services.Configure(options);
            services.AddSingleton(providers =>
            {
                var options = providers.GetRequiredService<IOptions<APIClientOptions>>().Value;
                return new APIClientService<T>(options, endpoint);
            });
        }
    }
}
