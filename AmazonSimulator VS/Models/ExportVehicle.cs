﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Models
{
    public class ExportVehicle : Object3D, IUpdatable, DropOffTarget
    {
        private int _weightLeft = 0;
        private int _assignedWeightLeft = 0;
        private bool _isMoving = true;
        private bool _isArrived = false;
        private bool _moveDirection = false;
        private bool _isDone = false;

        public int weightLeft { get { return _weightLeft; } }
        public int assignedWeightLeft { get { return _assignedWeightLeft; } }
        public bool isDone { get { return _isDone; } }

        public ExportVehicle(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "ExportVehicle")
        {
            Random random = new Random();
            int exportWeight = random.Next(20, 51); 
            _weightLeft = exportWeight;
            _assignedWeightLeft = exportWeight;
        }

        /// <summary>
        /// Load weight on exportVehicle. The exportVehicle will leave if it became full.
        /// </summary>
        /// <param name="loadWeight"></param>
        public void LoadWeight(int loadWeight)  
        {
            _weightLeft -= loadWeight;
            if (_weightLeft == 0)               
            {
                _isMoving = true;
                _moveDirection = true;
                this.Move(this.x, this.y + 0.1, this.z);
            }
        }

        /// <summary>
        /// Assign weight to exportVehicle. The exportVehicle will leave if it became full.
        /// </summary>
        /// <param name="loadWeight"></param>
        public void AssignWeight(int loadWeight)  
        {
            _assignedWeightLeft -= loadWeight;
        }

        /// <summary>
        /// Checks if the exportVehicle has arrived.
        /// </summary>
        /// <returns></returns>
        public bool CheckArrived()
        {
            return _isArrived;
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        /// <summary>
        /// Moves exportVehicle to the loadin docking station and back when done loading in.
        /// </summary>
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
                    _isArrived = true;
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

        /// <summary>
        /// Triggered by robot when droping the crate by DropOffTarget.
        /// </summary>
        /// <param name="crate"></param>
        public void HandelDropOff(Crate crate)
        {
            LoadWeight(crate.weight);
            crate.SetIsDone();
        }
    }
}
