using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// Task for logic to be given after timer with defined time interval elapsed.
    /// </summary>
    public interface InboundLogicTask
    {
        /// <summary>
        /// Returns time untill timer elapsed.
        /// </summary>
        /// <returns></returns>
        int GetInterval();
    }
}
