using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : Object3D, IUpdatable
    {
        private List<double[]> pickupTask;
        private List<double[]> dropoffTask;
        private List<double[]> currentTask;

        private double[] pointOne = new double[2];
        private double[] pointTwo = new double[2];

        private double _endPos = 0;
        private bool _isMoving = false;
        private string _movementAxis = "";

        public double endPos { get { return _endPos; } }
        public bool isMoving { get { return _isMoving; } }
        public string movementAxis { get { return _movementAxis; } }

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Robot")
        {

        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            base.Rotate(rotationX, rotationY, rotationZ);
        }

        public void GetTask(Controllers.RobotTask rt)
        {
            pickupTask = rt.pickupTask;
            dropoffTask = rt.dropoffTask;
            HandleTask();
        }

        public void HandleTask()
        {
            if (currentTask == null)
            {
                currentTask = pickupTask;
            }

            if (currentTask.Count != 1)
            {
                pointOne = currentTask[0];
                pointTwo = currentTask[1];
                SetRoute(pointOne, pointTwo);
            }
            else
            {
                if (currentTask == pickupTask)
                {
                    currentTask = dropoffTask;
                }
                else
                {
                    currentTask = null;
                    _movementAxis = "";
                    _isMoving = false;
                }
            }

            //if (currentTask == dropoffTask)
            //{
            //    pointOne = currentTask[0];
            //    pointTwo = currentTask[1];
            //    SetRoute(pointOne, pointTwo);
            //}
        }

        /*
        if (currentTask != dropoffTask && currentTask.Count != 1)
        {
            if (currentTask.Count != 1 currentTask != dropoffTask)
            {
                pointOne = currentTask[0];
                pointTwo = currentTask[1];
                SetRoute(pointOne, pointTwo);
            }
            else
            {
                currentTask = dropoffTask;
            }
            pointOne = currentTask[0];
            pointTwo = currentTask[1];
            SetRoute(pointOne, pointTwo);
        }
        */
        /*
        else
        {
            _movementAxis = "";
            _isMoving = false;
        }
        */

        public void SetRoute(double[] pointOne, double[] pointTwo)
        {
            if (pointOne[0] == pointTwo[0])
            {
                _movementAxis = "z";
                TaskPos(pointTwo[1]);
            }
            else
            {
                _movementAxis = "x";
                TaskPos(pointTwo[0]);
            }
        }

        public void TaskPos(double endPos)
        {
            _endPos = endPos;
            _isMoving = true;
        }

        public void DeletePos()
        {
            currentTask.RemoveAt(0);
        }

        private void MoveToPosX()
        {
            if (endPos > Math.Round(this.x, 1))
            {
                this.Move(this.x + 0.1, this.y, this.z);
            }
            else if (endPos < Math.Round(this.x, 1))
            {
                this.Move(this.x - 0.1, this.y, this.z);
            }

            if (endPos == Math.Round(this.x, 1))
            {
                DeletePos();
                HandleTask();
            }
        }

        private void MoveToPosZ()
        {
            if (endPos > Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z + 0.1);
            }
            else if (endPos < Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z - 0.1);
            }

            if (endPos == Math.Round(this.z, 1))
            {
                DeletePos();
                HandleTask();
            }
        }

        public override bool Update(int tick)
        {
            if (isMoving == true)
            {
                if (movementAxis == "x")
                {
                    MoveToPosX();
                }
                else
                {
                    MoveToPosZ();
                }
            }

            return base.Update(tick);
        }
    }
}