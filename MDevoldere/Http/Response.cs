using System.Net;
using System.Text;
using System.Text.Json;

namespace MDevoldere.Http
{
    public static class Response
    {
        public static void End(HttpListenerResponse response, byte[] data)
        {
            try
            {
                response.ContentLength64 = data.Length;
                using Stream writer = response.OutputStream;
                writer.Write(data, 0, data.Length);
                writer.Close(); 
                response.Close();
            }
            catch(Exception ex)
            {
                End(response, "An error occurred while receiving data: " + ex.Message, ContentType.TXT, 404);
            }
        }

        public static void End(HttpListenerResponse response, string data)
        {
            try
            {
                response.ContentEncoding = Encoding.UTF8;
                using StreamWriter writer = new(response.OutputStream, Encoding.UTF8);
                writer.Write(data);
                writer.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                End(response, "An error occurred while receiving data: " + ex.Message, ContentType.TXT, 404);
            }
        }

        public static void End(HttpListenerResponse response, byte[] data, string contentType = ContentType.HTML, int code = 200)
        {
            response.StatusCode = code;
            response.ContentType = contentType;
            End(response, data);
        }

        public static void End(HttpListenerResponse response, string data, string contentType = ContentType.HTML, int code = 200)
        {
            response.StatusCode = code;
            response.ContentType = contentType;
            End(response, data);
        }

        public static void EndHtml(HttpListenerResponse response, byte[] resource)
        {
            End(response, resource, ContentType.HTML);
        }

        public static void EndHtml(HttpListenerResponse response, string html)
        {
            End(response, html, ContentType.HTML);
        }

        public static void EndJpg(HttpListenerResponse response, byte[] resource)
        {
            End(response, resource, ContentType.JPG);
        }

        public static void EndPng(HttpListenerResponse response, byte[] resource)
        {
            End(response, resource, ContentType.PNG);
        }

        public static void EndJson(HttpListenerResponse response, object json)
        {
            End(response, JsonSerializer.Serialize(json), ContentType.JSON);
        }

        public static void EndJson(HttpListenerResponse response, string json)
        {
            End(response, json, ContentType.JSON);
        }

        public static void NotFound(HttpListenerResponse response)
        {
            End(response, "Not Found !", ContentType.TXT, 404);
        }
    }
}
