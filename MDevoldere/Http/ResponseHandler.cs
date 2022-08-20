using System.Net;

namespace MDevoldere.Http
{
    public class ResponseHandler : IResponseHandler
    {
        public readonly string Route;

        public ResponseHandler() : this("/") { }

        public ResponseHandler(string _route)
        {
            Route = _route;
        }

        public virtual bool IsMatch(string? _route)
        {
            return Route == (_route ?? "/");
        }

        public virtual void HandleResponse(HttpListenerRequest request, HttpListenerResponse response)
        {
            if(request.HttpMethod == "GET")
            {
                Get(request, response);
            }
            else if (request.HttpMethod == "POST")
            {
                Post(request, response);
            }
            else if (request.HttpMethod == "PUT")
            {
                Put(request, response);
            }
            else if (request.HttpMethod == "DELETE")
            {
                Delete(request, response);
            }
            else
            {
                Response.NotFound(response);
            }
        }

        public virtual void Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            Response.EndJson(response, new
            {
                code = response.StatusCode,
                description = response.StatusDescription,
                data = string.Format("{0} '{1}' Request completed !", DateTime.Now.ToString(), request.RawUrl)
            });
        }

        public virtual void Post(HttpListenerRequest request, HttpListenerResponse response)
        {
            Response.NotFound(response);
        }

        public virtual void Put(HttpListenerRequest request, HttpListenerResponse response)
        {
            Response.NotFound(response);
        }

        public virtual void Delete(HttpListenerRequest request, HttpListenerResponse response)
        {
            Response.NotFound(response);
        }
    }
}
