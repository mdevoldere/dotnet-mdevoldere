using System.Net;

namespace MDevoldere.Http
{
    public  class Router : IRequestHandler
    {
        public readonly ICollection<IResponseHandler> Routes;

        public Router(ICollection<IResponseHandler> routes)
        {
            Routes = routes;
        }

        public virtual void HandleRequest(HttpListenerContext context)
        {
            if(Routes.FirstOrDefault(r => r.IsMatch(context.Request.RawUrl)) is IResponseHandler handler)
            {
                handler.HandleResponse(context.Request, context.Response);
                return;
            }

            Response.NotFound(context.Response);
        }
    }
}
