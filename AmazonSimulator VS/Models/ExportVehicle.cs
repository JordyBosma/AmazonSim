using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ExportVehicle : Object3D, IUpdatable
    {
        private int _exportWeight = 0;
        private int _loadedWeight = 0;
        private bool _isMoving = true;
        private bool _moveDirection = false;
        private bool _isDone = false;

        public int exportWeight { get { return _exportWeight; } }
        public bool isDone { get { return _isDone; } }

        public ExportVehicle() : base(49, 400, 4, 0, 0, 0, "ExportVehicle")
        {
            Random random = new Random();
            _exportWeight = random.Next(20, 51);
        }

        public void LoadWeight(int loadWeight)
        {
            _loadedWeight += loadWeight;
            if (_loadedWeight == exportWeight)
            {
                _isMoving = true;
                _moveDirection = true;
                this.Move(this.x, this.y + 0.1, this.z);
            }
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public void MoveRocket()
        {
            if (_moveDirection == false)
            {
                if (Math.Round(this.y, 0) != 0)
                {
                    if (Math.Round(this.y) > 10)
                    {
                        this.Move(this.x, this.y - 1, this.z);
                    }
                    else
                    {
                        this.Move(this.x, this.y - 0.2, this.z);
                    }
                }
                else
                {
                    _isMoving = false;
                    _moveDirection = true;
                }
            }
            else
            {
                if (Math.Round(this.y, 0) != 400)
                {
                    if (Math.Round(this.y) > 10)
                    {
                        this.Move(this.x, this.y + 1, this.z);
                    }
                    else
                    {
                        this.Move(this.x, this.y + 0.2, this.z);
                    }
                }
                else
                {
                    _isMoving = false;
                    _isDone = true;
                }
            }
        }

        public override bool Update(int tick)
        {
            if (_isMoving == true)
            {
                MoveRocket();
            }

            return base.Update(tick);
        }
    }
}
