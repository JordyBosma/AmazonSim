using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Models
{
    public class ImportVehicle : Object3D, IUpdatable, PickUpTarget
    {
        private List<Crate> _importCrates = new List<Crate>(); 
        private bool _isArrived = true;
        private bool _isDone = false;
        private int picktUpCount;

        public List<Crate> importCrates { get { return _importCrates; } }
        public bool isDone { get { return _isDone; } }

        public ImportVehicle(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "ImportVehicle")
        {
            LoadCrates();
        }

        public void LoadCrates()
        {
            for (int i = 0; i < 5; i++)
            {
                Crate newCrate = new Crate(this.x, this.y, this.z);
                importCrates.Add(newCrate);
            }
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            return base.Update(tick);
        }

        public bool CheckArrived()
        {
            return _isArrived;
        }

        public void HandelPickUp()
        {
            picktUpCount++;
            if (picktUpCount == 5)
            {
                _isDone = true;   //ga moven
            }
        }
    }
}
