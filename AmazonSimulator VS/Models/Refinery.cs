﻿using System;
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
        private int tick = 0;

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
            int weight = unrefined[0].weight;
            string type = unrefined[0].type;
            Crate refinedCrate = new Crate(18, 0, 18, weight, type, true);
            refined.Add(refinedCrate);
            unrefined[0].Refine();
            unrefined.RemoveAt(0);
        }

        public override bool Update(int tick)
        {
            if(unrefined.Count() != 0 && refinedCratesCount < 5)
            {
                tick++;
                if(tick == 100)
                {
                    RefineCrate();
                    refinedCratesCount++;
                    tick = 0;
                }
            }
            return false;
        }
    }
}
