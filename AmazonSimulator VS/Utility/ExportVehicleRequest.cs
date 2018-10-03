using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class ExportVehicleRequest : LogicTask
    {
        private int _interval;
        private double _x;
        private double _y;
        private double _z;

        public int interval { get { return _interval; } }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }

        public ExportVehicleRequest(double x, double z)
        {
            Random rnd = new Random();
            _interval = rnd.Next(89, 180) * 1000;
            //_interval = rnd.Next(20, 30) * 1000;
            _x = x;
            _y = 400;
            _z = z;
        }

        public bool RunTask(Model w)
        {
            ExportVehicle exportVehicle = new ExportVehicle(x, y, z);
            w.worldObjects.Add(exportVehicle);
            exportVehicle.Move(exportVehicle.x, exportVehicle.y, exportVehicle.z);
            return false;
        }
    }
}
