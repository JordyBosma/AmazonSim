using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : Object3D, IUpdatable
    {
        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Robot")
        {

        }

        public override void Move(double x, double y, double z) 
        {
            base.Move(x, y, z);
        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            base.Rotate(rotationX, rotationY, rotationZ);
        }

        public override bool Update(int tick)
        {
            return base.Update(tick);
        }
    }
}