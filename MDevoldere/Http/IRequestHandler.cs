using System.Net;

namespace MDevoldere.Http
{
    public interface IRequestHandler
    {
        void HandleRequest(HttpListenerContext context);
    }
}
