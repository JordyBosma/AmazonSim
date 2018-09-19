using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class StationaryObject : Object3D
    {
        /// <summary>
        /// Non updateble Object3D that can be used for al non updateble objects in the 3d world
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="rotationX"></param>
        /// <param name="rotationY"></param>
        /// <param name="rotationZ"></param>
        /// <param name="type"></param>
        public StationaryObject(double x, double y, double z, double rotationX, double rotationY, double rotationZ, string type) : base(x, y, z, rotationX, rotationY, rotationZ, type)
        {

        }
    }
}
