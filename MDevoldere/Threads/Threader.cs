using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Threads
{
    public static class Threader
    {
        public static void SingleTask(Action action)
        {
            Thread t = new Thread(new ThreadStart(action));
            t.Start();
            t.Join();
            Task.Factory.StartNew(action);
        }
    }
}
