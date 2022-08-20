using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Workers
{
    public enum WorkState
    {
        CANCELLED = -1,
        PENDING = 0, // must be default state
        ACCEPTED = 1,
        WORKING = 2,
        DONE = 3
    }
}
