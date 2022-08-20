using System.Net;

namespace MDevoldere.Http
{
    public interface IResponseHandler
    {
        bool IsMatch(string? route);
        void HandleResponse(HttpListenerRequest request, HttpListenerResponse response);
    }
}
