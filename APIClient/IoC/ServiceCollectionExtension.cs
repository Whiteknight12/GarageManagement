using APIClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void AddAPIClientService(this IServiceCollection services, Action<APIClientOption> option)
        {
            services.Configure(option);
            services.AddSingleton(providers =>
            {
                var options = providers.GetRequiredService<IOptions<APIClientOption>>().Value;
                return new APIClientService(options);
            });
        }
    }
}
