using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public interface LogicTask
    {
         bool RunTask(Model w);
    }
}
