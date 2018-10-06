using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    public class Refinery : Object3D, IUpdatable, PickUpTarget, DropOffTarget
    {
        private List<Crate> unrefined = new List<Crate>();
        private List<Crate> refined = new List<Crate>();
        private int cratesCount = 0;
        private int _tick = 0;

        public Refinery(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Refinery")
        {
        }

        public List<Crate> GetRefinedList()
        {
            return refined;
        }

        public bool IsNotFilled()
        {
            if (cratesCount != 5)
            {
                cratesCount++;
                return true;
            } else
            {
                return false;
            }
        }

        public void HandelDropOff(Crate crate)
        {
            unrefined.Add(crate);
        }

        public void HandelPickUp()
        {
            cratesCount--;
        }

        public void RefineCrate()
        {
            Crate refinedCrate = new Crate(-25, 0, -1, unrefined[0].weight, unrefined[0].invetory, true);
            refined.Add(refinedCrate);
            unrefined[0].Refine();
            unrefined.RemoveAt(0);
        }

        public override bool Update(int tick)
        {
            if(unrefined.Count() != 0)
            {
                _tick++;
                if(_tick > 200)
                {
                    RefineCrate();
                    _tick = 0;
                }
            }
            return base.Update(tick);
        }
    }
}
