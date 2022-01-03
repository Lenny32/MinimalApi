namespace Microsoft.AspNetCore.Mvc
{
    public abstract class EndPointApi<T> : EndPointApi
    {
        public EndPointApi() : base(typeof(T))
        {
        }
    }
}