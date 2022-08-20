//using MDevoldere.Http;
//using System;
//using System.Net;
//using System.Text;
//using System.Text.Json;

//namespace MDevoldere.Http.Legacy
//{
//    public class HttpServer
//    {
//        public readonly HttpServerConfig Config;
//        public readonly Router Router;

//        public delegate void RequestHandler(HttpListenerContext context);

//        public event RequestHandler? OnRequest;

//        private HttpListener? httpListener;

//        public HttpServer(ICollection<IResponseHandler> routes)
//        {
//            Config = new HttpServerConfig();
//            Router = new Router(routes);
//        }

//        public void Start()
//        {
//            if (httpListener == null)
//            {
//                httpListener = new HttpListener();
//                httpListener.Prefixes.Add(Config.Prefix);
//                httpListener.Prefixes.Add(Config.LocalPrefix);
//                httpListener.Start();
//                httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), httpListener);
//            }
//        }

//        public void Start(string host, int port)
//        {
//            if (httpListener == null)
//            {
//                Config.SetPrefix(host, port);
//                Start();
//            }
//        }

//        public void Stop()
//        {
//            try
//            {
//                if (httpListener != null)
//                {
//                    httpListener.Stop();
//                    httpListener.Close();
//                    httpListener.Abort();
//                }

//                httpListener = null;
//                OnRequest = null;
//            }
//            catch (Exception){}
//        }
        
//        private void GetContextCallBack(IAsyncResult ar)
//        {
//            try
//            {
//                if (ar.AsyncState is HttpListener httplis && httplis.IsListening)
//                {
//                    HttpListenerContext context = httplis.EndGetContext(ar);

//                    OnRequest?.Invoke(context);

//                    Router.HandleRequest(context);

//                    httplis.GetContext();
//                    httpListener?.BeginGetContext(new AsyncCallback(GetContextCallBack), httpListener);
//                }
//            }
//            catch
//            {
//                //throw new ArgumentException(ex.Message);
//                Stop();
//            }
//        }
//    }
//}