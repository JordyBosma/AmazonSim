using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ImportVehicle : Object3D, IUpdatable
    {
        private List<Crate> _importCrates = new List<Crate>(); 
        private bool _isArrived = false;

        public List<Crate> importCrates { get { return _importCrates; } }

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
    }
}
