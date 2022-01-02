using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    public static class RegisterMinimalApi
    {
        public static void MapMinimalApi(this IEndpointRouteBuilder app)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IEndPointApi).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
            foreach (var type in types)
            {
                (Activator.CreateInstance(type) as IEndPointApi)?.Register(app);
            }
        }
    }
}