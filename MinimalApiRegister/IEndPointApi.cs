namespace Microsoft.AspNetCore.Mvc
{
    public interface IEndPointApi
    {
        void Register(IEndpointRouteBuilder app);
    }
}