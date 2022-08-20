using MDevoldere.Workers;

namespace MDevoldere.Threads
{

    public class ThreadWorker<T> : IWorker
    {
        public readonly ManualResetEvent StopEvent;
        private readonly ManualResetEvent ReadyEvent;
        private readonly WaitHandle[] wait;
        
        private Thread? worker;
        private readonly Queue<T> queue;

        private event Action<T> Callback;

        public ThreadWorker(Action<T> _callback)
        {
            StopEvent = new ManualResetEvent(false);
            ReadyEvent = new ManualResetEvent(false);
            wait = new[] { ReadyEvent, StopEvent };
            Callback = _callback;
            queue = new Queue<T>();
        }

        public void Stop()
        {
            lock (queue)
            {
                queue.Clear();
            }
            StopEvent.Set();
            worker?.Join();
        }

        public void Start()
        {
            StopEvent.Reset();
            worker = new(Worker);
            worker.Name = "threadWorker";
            worker.Start();
        }

        public void HandleJob(T job)
        {
            try
            {
                lock (queue)
                {
                    queue.Enqueue(job);
                    ReadyEvent.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            // Ready signal (wait[0])
            while (0 == WaitHandle.WaitAny(wait))
            {
                T job;

                lock (queue)
                {
                    if (queue.Count > 0)
                    {
                        job = queue.Dequeue();
                    }
                    else
                    {
                        ReadyEvent.Reset();
                        continue;
                    }
                }
                // event !
                Callback.Invoke(job);
            }
        }
    }
}
