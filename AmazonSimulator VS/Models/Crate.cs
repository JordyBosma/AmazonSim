using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Crate : Object3D, IUpdatable
    {
        public Crate(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "FooBar")
        {

        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            return base.Update(tick);
        }
    }
}
