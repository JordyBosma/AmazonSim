using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    /// <summary>
    /// Task to be executed by the logic.
    public interface LogicTask
    {
        /// <summary>
        /// Runs the LogicTask. Returns true if succesfully completed.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        bool RunTask(Model w);
    }
}
