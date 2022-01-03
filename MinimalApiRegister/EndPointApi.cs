namespace Microsoft.AspNetCore.Mvc
{
    public abstract class EndPointApi : IEndPointApi
    {
        public string Name { get; init; }
        public EndPointApi(string name)
        {
            Name = name;
        }
        public EndPointApi(Type type)
        {
            Name = type.Name.Replace("EndPointApi", string.Empty, StringComparison.InvariantCultureIgnoreCase).Replace("EndPoint", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }
        public abstract void Register(IEndpointRouteBuilder app);
        public void MapGet(IEndpointRouteBuilder endpoints, string pattern, Delegate handler)
        {
            endpoints.MapGet(pattern, handler).WithTags(Name);
        }
        protected void MapPost(IEndpointRouteBuilder endpoints, string pattern, Delegate handler)
        {
            endpoints.MapPost(pattern, handler).WithTags(Name);
        }
        protected void MapPut(IEndpointRouteBuilder endpoints, string pattern, Delegate handler)
        {
            endpoints.MapPut(pattern, handler).WithTags(Name);
        }
        protected void MapDelete(IEndpointRouteBuilder endpoints, string pattern, Delegate handler)
        {
            endpoints.MapDelete(pattern, handler).WithTags(Name);
        }
    }
}