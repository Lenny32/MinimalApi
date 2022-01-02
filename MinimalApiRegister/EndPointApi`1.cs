namespace Microsoft.AspNetCore.Mvc
{
    public abstract class EndPointApi<T> : EndPointApi
    {
        protected EndPointApi() : base(typeof(T))
        {
        }
    }
}