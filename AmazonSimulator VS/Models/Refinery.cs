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
        private int refinedCratesCount = 0;
        private int _tick = 0;

        public Refinery(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Refinery")
        {
        }

        public List<Crate> GetRefinedList()
        {
            return refined;
        }

        public void HandelDropOff(Crate crate)
        {
            unrefined.Add(crate);
        }

        public void HandelPickUp()
        {
            refinedCratesCount--;
        }

        public void RefineCrate()
        {
            Crate refinedCrate = new Crate(-1, 0, 1, unrefined[0].weight, unrefined[0].invetory, true);
            refined.Add(refinedCrate);
            unrefined[0].Refine();
            unrefined.RemoveAt(0);
        }

        public override bool Update(int tick)
        {
            if(unrefined.Count() != 0 && refinedCratesCount < 5)
            {
                _tick++;
                if(_tick > 100)
                {
                    RefineCrate();
                    refinedCratesCount++;
                    _tick = 0;
                }
            }
            return base.Update(tick);
        }
    }
}
