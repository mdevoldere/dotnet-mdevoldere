using MDevoldere.Workers;

namespace MDevoldere.Threads
{
    /// <summary>
    /// Performs 1 task at regular intervals
    /// </summary>
    public class ThreadLoopQueue : IWorker
    {
        public bool IsActive { get => running; }

        private bool running;

        private Thread? th;

        public readonly int Interval;

        protected event Action? Callback;

        public ThreadLoopQueue(int interval = 1000) : this(null, interval) {}

        /// <summary>
        /// Performs 1 task at regular intervals
        /// </summary>
        /// <param name="callback">Action to perform</param>
        /// <param name="interval">Interval between 2 actions</param>
        public ThreadLoopQueue(Action? callback, int interval = 1000)
        {
            Interval = interval >= 0 ? interval : 1000;
            running = false;
            Callback = callback;
        }

        private void Execute()
        {
            while(running)
            {
                Callback?.Invoke();
                Thread.Sleep(Interval);
            }
        }

        public virtual void Start()
        {
            if(!running && Callback is not null)
            {
                running = true;
                th = new(Execute);
                th.Name = "threadLoop";
                th.Start();
            }
        }

        public virtual void Stop()
        {
            if(running)
            {
                running = false;
                th?.Join();
            }
        }
    }
}
