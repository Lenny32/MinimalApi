using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    public static class RegisterMinimalApi
    {
        static RegisterMinimalApi()
        {
            _assemblies = new List<Assembly>();
        }

        /// <summary>
        /// Register the entry point as source of the endpoint. 
        /// Microsoft.AspNetCore.Mvc.IEndPointApi
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return AddEndpoints(services, Assembly.GetEntryAssembly());
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            return AddEndpoints(services, new Assembly[] { assembly });
        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException($"The parameter {nameof(assemblies)} cannot be null.");
            foreach (var assembly in assemblies)
            {
                if (!_assemblies.Contains(assembly))
                    _assemblies.Add(assembly);
            }
            return services;
        }

        private static List<Assembly> _assemblies { get; set; }
        public static Assembly[] Assemblies => _assemblies.ToArray();
        public static void MapMinimalApi(this IEndpointRouteBuilder app)
        {
            if (!_assemblies.Any())
                throw new InvalidOperationException("Unable to find the required services. Please add all the required services by calling 'IServiceCollection.AddEndpoints' with or without extra parameters inside the call to 'ConfigureServices(...)' in the application startup code.");

            var types = _assemblies.Where(x => x != null).SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEndPointApi).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x.GetConstructors().Any(y=> !y.GetParameters().Any()))
                .ToList();

            foreach (var type in types)
            {
                (Activator.CreateInstance(type) as IEndPointApi)?.Register(app);
            }
        }
    }
}