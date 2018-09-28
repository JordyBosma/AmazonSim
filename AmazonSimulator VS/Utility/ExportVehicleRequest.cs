using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class ExportVehicleRequest : LogicTask
    {
        public bool RunTask(Model w)
        {
            w.worldObjects.Add(new ExportVehicle());
            return true;
        }
    }
}
