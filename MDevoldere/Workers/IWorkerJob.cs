namespace MDevoldere.Workers
{
    public interface IWorkerJob<T> : IWorker
    {
        void HandleJob(T job);
    }
}
