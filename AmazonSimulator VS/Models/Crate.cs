using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// A object3d in the world representing a crate with one of all sorts of materials with different weights in it to be moved around by robots.
    /// </summary>
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

        /// <summary>
        /// Crate constructor for a unrefined new crate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Crate(double x, double y, double z) : base(x, y, z, 0, 0, 0, "Crate")
        {
            Random random = new Random();
            int[] pWeights = new int[] { 1, 2, 3, 4, 5, 10 };
            string[] pInvetory = new string[] {"MoonMilk","Krypto", "Beryllium", "Uranium","Moonrock"};
            _weight = pWeights[random.Next(6)];
            _invetory = pInvetory[random.Next(5)];
        }

        /// <summary>
        /// Crate constructor for a refined crate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="weight"></param>
        /// <param name="invetory"></param>
        /// <param name="refined"></param>
        public Crate(double x, double y, double z, int weight, string invetory, bool refined) : base(x, y, z, 0, 0, 0, "Crate")
        {
            this._weight = weight;
            this._invetory = invetory;
            this._refined = refined;
        }

        /// <summary>
        /// Set isDone to true, this so that the logic knows that there needs something to happen with this object. Here it means the removel of this crate.
        /// </summary>
        public void SetIsDone()
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
