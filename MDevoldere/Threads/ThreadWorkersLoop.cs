using MDevoldere.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Threads
{
    public class ThreadWorkersLoop<T>: ThreadLoop, IWorker
    {
        public delegate T JobInvoker();

        private event JobInvoker GetJob;

        private readonly ThreadWorkers<T> Workers;

        public ThreadWorkersLoop(JobInvoker getJobCallback, Action<T> doJobCallback, int _max = 8, int interval = 1000)
            : base(interval)
        {
            Callback += OnLoopEvent;
            GetJob += getJobCallback;
            Workers = new ThreadWorkers<T>(doJobCallback, _max);
        }

        private void OnLoopEvent()
        {
            T job = GetJob.Invoke();
            Workers.HandleJob(job);
        }

        public override void Stop()
        {
            base.Stop();
            Workers.Stop();
        }

        public override void Start()
        {
            Workers.Start();
            base.Start();
        }
    }
}
