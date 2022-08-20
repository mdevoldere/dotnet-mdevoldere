using MDevoldere.Workers;

namespace MDevoldere.Threads
{
    /// <summary>
    /// Performs 1 task at regular intervals
    /// </summary>
    public class ThreadLoop : IWorker
    {
        public bool IsActive { get => running; }

        private bool running;

        private Thread? th;

        public readonly int Interval;

        protected event Action? Callback;

        public ThreadLoop(int interval = 1000) : this(null, interval) {}

        /// <summary>
        /// Performs 1 task at regular intervals
        /// </summary>
        /// <param name="callback">Action to perform</param>
        /// <param name="interval">Interval between 2 actions</param>
        public ThreadLoop(Action? callback, int interval = 1000)
        {
            Interval = interval >= 10 ? interval : 10;
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
                //th?.Join();
                th = null;
            }
        }
    }
}
