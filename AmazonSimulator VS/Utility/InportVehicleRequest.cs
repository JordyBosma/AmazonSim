using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class InportVehicleRequest
    {
        private int _interval;
        private double _x;
        private double _y;
        private double _z;
        private double _rotationX;
        private double _rotationY;
        private double _rotationZ;

        public int interval { get { return _interval; } }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rotationX; } }
        public double rotationY { get { return _rotationY; } }
        public double rotationZ { get { return _rotationZ; } }

        public InportVehicleRequest(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            Random rnd = new Random();
            _interval = rnd.Next(89, 180) * 1000;
            _x = x;
            _y = y;
            _z = z;
            _rotationX = rotationX;
            _rotationY = rotationY;
            _rotationZ = rotationZ;
        }

        public bool RunTask(Model w)
        {
            ImportVehicle importVehicle = new ImportVehicle(x, y, z, rotationX, rotationY, rotationZ);
            w.worldObjects.Add(importVehicle);
            foreach (Crate crate in importVehicle.importCrates)
            {
                w.worldObjects.Add(crate);
            }
            return false;
        }
    }
}
