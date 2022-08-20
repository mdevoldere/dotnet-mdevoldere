using MDevoldere.Workers;

namespace MDevoldere.Workers
{
    public class WorkerManager<T> : IDisposable, IWorker where T : IWorker, new()
    {
        public readonly List<IWorker> Workers;

        public WorkerManager()
        {
            Workers = new List<IWorker>();
        }

        public void Start()
        {
            foreach(IWorker p in Workers)
            {
                p.Start();
            }
        }

        public void Stop()
        {
            foreach (IWorker p in Workers)
            {
                p.Stop();
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
