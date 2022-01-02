using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    public static class RegisterMinimalApi
    {
        static RegisterMinimalApi()
        {
            Assemblies = new List<Assembly?>() 
            { 
                Assembly.GetEntryAssembly() 
            };
        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return services;
        }


        public static List<Assembly?> Assemblies { get; internal set; }
        public static void MapMinimalApi(this IEndpointRouteBuilder app)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var types = Assemblies.Where(x => x != null).SelectMany(x => x.GetTypes())
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                .Where(x => typeof(IEndPointApi).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();


            foreach (var type in types)
            {
                (Activator.CreateInstance(type) as IEndPointApi)?.Register(app);
            }
        }
    }
}