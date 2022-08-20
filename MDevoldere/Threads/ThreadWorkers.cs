using MDevoldere.Workers;

namespace MDevoldere.Threads
{
    /// <summary>
    /// Manages a waiting list of 1 task to be performed in multiple threads 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadWorkers<T> : IWorker
    {
        public readonly ManualResetEvent StopEvent;
        private readonly ManualResetEvent ReadyEvent;

        private readonly WaitHandle[] wait;
        private readonly Thread[] workers;
        private readonly Queue<T> queue;

        private event Action<T> Callback;

        /// <summary>
        /// Create a waiting list of 1 action to be performed in multiple threads
        /// </summary>
        /// <param name="_callback">Action to perform</param>
        /// <param name="_max">Number of threads for this task</param>
        public ThreadWorkers(Action<T> _callback, int _max = 8)
        {
            workers = new Thread[_max];
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
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i]?.Join();
            }            
        }

        public void Start()
        {
            StopEvent.Reset();
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new (Worker);
                workers[i].Name = "worker" + i.ToString();
                workers[i].Start();
            }
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
