using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Niubility.Common.Web
{
    public static class ServiceConfigures
    {
        public static T Configure<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string section)
            where T : class
        {
            var configurationSection = configuration.GetSection(section);
            services.Configure<T>(configurationSection);
            return configurationSection.Get<T>();
        }
    }
}