using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Crate : Object3D, IUpdatable
    {
        private int _weight;
        private string _invetory;
        private bool _refined = false;
        private bool _isDone = false;

        public int weight { get { return _weight; } }
        public string invetory { get { return _invetory; } }
        public bool refined { get { return _refined; } }
        public bool isDone { get { return _isDone; } }

        public Crate(double x, double y, double z) : base(x, y, z, 0, 0, 0, "Crate")
        {
            Random random = new Random();
            int[] pWeights = new int[] { 1, 2, 3, 4, 5, 10 };
            string[] pInvetory = new string[] {"MoonMilk","Krypto", "Beryllium", "Uranium","Moonrock"};
            _weight = pWeights[random.Next(6)];
            _invetory = pInvetory[random.Next(5)];
        }

        public Crate(double x, double y, double z, int weight, string invetory, bool refined) : base(x, y, z, 0, 0, 0, "Crate")
        {
            this._weight = weight;
            this._invetory = invetory;
            this._refined = refined;
        }

        public void Refine()
        {
            _isDone = true;
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
