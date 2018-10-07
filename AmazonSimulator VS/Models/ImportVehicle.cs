using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Models
{
    /// <summary>
    /// dit is de ImportVehicle class, deze class wordt aangeroepen door een timer event en laad de eigenschapen van de import trein in.
    /// Deze classe maakt nieuwe crates aan en regelt de movement van de trein.
    /// </summary>
    public class ImportVehicle : Object3D, IUpdatable, PickUpTarget
    {
        private List<Crate> _importCrates = new List<Crate>();
        private bool _isMoving = true;
        private bool _moveDirection = false;
        private bool _isArrived = false;
        private bool _isDone = false;
        private int picktUpCount = 0;

        public List<Crate> importCrates { get { return _importCrates; } }
        public bool isDone { get { return _isDone; } }

        public ImportVehicle(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "ImportVehicle")
        {
            LoadCrates();
        }

        public void LoadCrates()
        {
            for (int i = 0; i < 10; i++)
            {
                Crate newCrate = new Crate(-1, 0, 25);
                importCrates.Add(newCrate);
            }
        }

        public void MoveTrain()
        {
            if (_moveDirection == false)
            {
                if (Math.Round(this.x, 0) != 0)
                {
                    if (Math.Round(this.x) > 20)
                    {
                        this.Move(this.x -1, this.y, this.z);
                    }
                    else
                    {
                        this.Move(this.x -0.5, this.y, this.z);
                    }
                }
                else
                {
                    _isMoving = false;
                    _isArrived = true;
                    _moveDirection = true;
                }
            }
            else
            {
                if (Math.Round(this.x, 0) != 600)
                {
                    if (Math.Round(this.x) > 20)
                    {
                        this.Move(this.x + 1, this.y, this.z);
                    }
                    else
                    {
                        this.Move(this.x + 0.5, this.y, this.z);
                    }
                }
                else
                {
                    _isMoving = false;
                    _isDone = true;
                }
            }
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }
        
        public bool CheckArrived()
        {
            return _isArrived;
        }

        /// <summary>
        /// Triggered by robot when picking up the crate by PickUpTarget.
        /// </summary>
        public void HandelPickUp()
        {
            picktUpCount++;
            if (picktUpCount == 10)
            {
                _isMoving = true;
                _moveDirection = true;
                this.Move(this.x, this.y, this.z);
            }
        }

        public override bool Update(int tick)
        {
            if (_isMoving == true)
            {
                MoveTrain();
            }

            return base.Update(tick);
        }
    }
}
