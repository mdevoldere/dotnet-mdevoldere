//using MDevoldere.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace MDevoldere.Http.Legacy
//{
//    public class HttpLocalServer : IDisposable
//    {
//        private HttpListener httpListener;
//        private Thread httpListenerThread;

//        private readonly Thread[] _workers;
//        private readonly ManualResetEvent _stop, _ready;
//        private readonly Queue<HttpListenerContext> _queue;

//        public readonly Router Router;

//        private event Action<HttpListenerContext>? ProcessRequest;

//        public HttpLocalServer(ICollection<IResponseHandler> routes, int maxThreads = 8)
//        {
//            _workers = new Thread[maxThreads];
//            _queue = new Queue<HttpListenerContext>();

//            _stop = new ManualResetEvent(false);
//            _ready = new ManualResetEvent(false);

//            Router = new Router(routes);
//            ProcessRequest += Router.HandleRequest;
//        }

//        public virtual void Start(int port)
//        {
//            httpListener = new HttpListener();
//            httpListenerThread = new Thread(HandleRequests);

//            httpListener.Prefixes.Add(String.Format(@"http://+:{0}/", port));
//            httpListener.Start();
//            httpListenerThread.Start();

//            for (int i = 0; i < _workers.Length; i++)
//            {
//                _workers[i] = new Thread(Worker);
//                _workers[i].Start();
//            }
//        }

//        public virtual void Stop()
//        {
//            _stop.Set();
//            httpListenerThread.Join();
//            foreach (Thread worker in _workers)
//                worker.Join();
//            httpListener.Stop();
//        }

//        public void Dispose()
//        { 
//            Stop(); 
//        }

//        private void HandleRequests()
//        {
//            while (httpListener.IsListening)
//            {
//                var context = httpListener.BeginGetContext(ContextReady, null);

//                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
//                    return;
//            }
//        }

//        private void ContextReady(IAsyncResult ar)
//        {
//            try
//            {
//                lock (_queue)
//                {
//                    _queue.Enqueue(httpListener.EndGetContext(ar));
//                    _ready.Set();
//                }
//            }
//            catch { return; }
//        }

//        private void Worker()
//        {
//            WaitHandle[] wait = new[] { _ready, _stop };

//            while (0 == WaitHandle.WaitAny(wait))
//            {
//                HttpListenerContext context;

//                lock (_queue)
//                {
//                    if (_queue.Count > 0)
//                        context = _queue.Dequeue();
//                    else
//                    {
//                        _ready.Reset();
//                        continue;
//                    }
//                }

//                ProcessRequest?.Invoke(context);
//            }
//        }

//    }
//}
