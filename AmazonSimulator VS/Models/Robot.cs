using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : Object3D, IUpdatable
    {
        private double _endZ = 0;
        private bool _isMoving = false;

        public double endZ { get { return _endZ; } }
        public bool isMoving { get { return _isMoving; } }

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Robot")
        {
            this._endZ = z;
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            base.Rotate(rotationX, rotationY, rotationZ);
        }

        public void TaskPos(double endZ)
        {
            _endZ = endZ;
            _isMoving = true;
        }

        private void MoveToPos()
        {
            if(endZ == Math.Round(this.z, 1))
            {
                _isMoving = false;
            }
            else if (endZ > Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z + 0.1);
            }
            else if (endZ < Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z - 0.1);
            }
        }

        public override bool Update(int tick)
        {
            if (isMoving == true) MoveToPos();
            return base.Update(tick);
        }
    }
}