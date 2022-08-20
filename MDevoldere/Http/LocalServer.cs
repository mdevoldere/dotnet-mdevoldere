using MDevoldere.Threads;
using System.Net;

namespace MDevoldere.Http
{
    public class LocalServer : IDisposable
    {
        private readonly Router router;
        private HttpListener? httpListener;
        private Thread? httpThread;
        private readonly ThreadWorkers<HttpListenerContext> workers;


        public LocalServer(Router _router, int _threads = 8)
        {
            router = _router;
            workers = new(router.HandleRequest, _threads);
        }

        public virtual void Start(int port)
        {
            httpListener = new HttpListener();
            httpThread = new Thread(HandleRequests)
            {
                Name = String.Format("http:{0}", port)
            };

            httpListener.Prefixes.Add(String.Format(@"http://+:{0}/", port));
            httpListener.Start();
            httpThread.Start();

            workers.Start();
        }

        public virtual void Stop()
        {
            workers.Stop();
            httpThread?.Join();
            httpListener?.Stop();
        }

        public void Dispose()
        { 
            Stop(); 
        }

        private void HandleRequests()
        {
            while (httpListener?.IsListening ?? false)
            {
                IAsyncResult ctx = httpListener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new [] { workers.StopEvent, ctx.AsyncWaitHandle}))
                    return;
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                if(httpListener != null)
                {
                    workers.HandleJob(httpListener.EndGetContext(ar));
                }
            }
            catch { return; }
            
        }
    }
}
