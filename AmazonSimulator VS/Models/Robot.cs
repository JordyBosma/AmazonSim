using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Utility;
using static Utility.RobotTask;

namespace Models
{
    public class Robot : Object3D, IUpdatable
    {
        private List<double[]> pickupTask;
        private List<double[]> dropoffTask;
        private List<double[]> currentTask;

        private Crate _pickupCrate;
        private DropOffTarget _dropOffTarget;
        private PickUpTarget _pickUpTarget;
        private double[] _pointOne = new double[2];
        private double[] _pointTwo = new double[2];
        private double _endPos = 0;
        private bool _isMoving = false;
        private bool _isRotating = false;
        private string _movementAxis = "";
        private int _rotationAxis = 0;
        private int _newRotationAxis = 0;
        private int _rotationTick = 0;
        private double _rotationValue = 0;
        private double _rotation = 0;
        private bool _moveCrate = false;

        private bool _isDone = true;

        public Crate pickupCrate { get { return _pickupCrate; } }
        public DropOffTarget dropOffTarget { get { return _dropOffTarget; } }
        public PickUpTarget pickUpTarget { get { return _pickUpTarget; } }
        public double[] pointOne { get { return _pointOne; } }
        public double[] pointTwo { get { return _pointTwo; } }
        public double endPos { get { return _endPos; } }
        public bool isMoving { get { return _isMoving; } }
        public bool isRotating { get { return _isRotating; } }
        public string movementAxis { get { return _movementAxis; } }
        public int rotationAxis { get { return _rotationAxis; } }
        public int newRotationAxis { get { return _newRotationAxis; } }
        public int rotationTick { get { return _rotationTick; } }
        public double rotationValue { get { return _rotationValue; } }
        public double rotation { get { return _rotation; } }

        public bool isDone { get { return _isDone; } }
        public void SetIsDone()
        {
            _isDone = false;
        }

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(x, y, z, rotationX, rotationY, rotationZ, "Robot")
        {
        }

        public override void Move(double x, double y, double z)
        {
            if (_moveCrate == true)
            {
                _pickupCrate.Move(x, 1, z);
            }

            base.Move(x, y, z);
        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            base.Rotate(rotationX, rotationY, rotationZ);
        }

        public void GiveTask(RobotTask rt)
        {
            pickupTask = rt.pickupTask;
            dropoffTask = rt.dropoffTask;
            _pickupCrate = rt.pickupCrate;
            _pickUpTarget = rt.pickUpTarget;
            _dropOffTarget = rt.dropOffTarget;
            HandleTask();
        }

        private void HandleTask()
        {
            if (currentTask == null)
            {
                currentTask = pickupTask;
            }

            if (currentTask.Count != 1)
            {
                _pointOne = currentTask[0];
                _pointTwo = currentTask[1];
                SetRoute(pointOne, pointTwo);
            }
            else
            {
                if (currentTask == pickupTask)
                {
                    currentTask = dropoffTask;
                    _moveCrate = true;
                    SetRoute(currentTask[0], currentTask[1]);
                    _pickUpTarget.HandelPickUp();
                    _pickUpTarget = null;
                }
                else
                {
                    currentTask = null;
                    _movementAxis = "";
                    _isMoving = false;
                    _isDone = true;
                    _moveCrate = false;
                    _dropOffTarget.HandelDropOff(_pickupCrate);
                    _dropOffTarget = null;
                    _pickupCrate = null;
                }
            }
        }

        private void SetRoute(double[] pointOne, double[] pointTwo)
        {
            if (pointOne[0] == pointTwo[0])
            {
                _movementAxis = "z";
                SetRotation(movementAxis);
                TaskPos(pointTwo[1]);
            }
            else
            {
                _movementAxis = "x";
                SetRotation(movementAxis);
                TaskPos(pointTwo[0]);
            }
        }

        private void SetRotation(string movmentAxis)
        {
            _rotationAxis = newRotationAxis;
            if (movementAxis == "z")
            {
                if (pointOne[1] < pointTwo[1])
                {
                    _newRotationAxis = 0;
                }
                else
                {
                    _newRotationAxis = 2;
                }
            }
            else
            {
                if (pointOne[0] < pointTwo[0])
                {
                    _newRotationAxis = 1;
                }
                else
                {
                    _newRotationAxis = 3;
                }
            }

            int rotation = newRotationAxis - rotationAxis;

            switch (rotation)
            {
                case 3:
                    _rotationValue = -0.5 * (Math.PI);
                    break;
                case -3:
                    _rotationValue = 0.5 * (Math.PI);
                    break;
                default:
                    _rotationValue = rotation * 0.5 * (Math.PI);
                    break;
            }
        }

        private void TaskPos(double endPos)
        {
            _endPos = endPos;
            if (rotationValue != 0)
            {
                _isRotating = true;
            }
            _isMoving = true;
        }

        private void MoveToPosX()
        {
            if (endPos > Math.Round(this.x, 1))
            {
                this.Move(this.x + 0.05, this.y, this.z);
            }
            else if (endPos < Math.Round(this.x, 1))
            {
                this.Move(this.x - 0.05, this.y, this.z);
            }

            if (endPos == Math.Round(this.x, 1))
            {
                currentTask.RemoveAt(0);
                HandleTask();
            }
        }

        private void MoveToPosZ()
        {
            if (endPos > Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z + 0.05);
            }
            else if (endPos < Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z - 0.05);
            }

            if (endPos == Math.Round(this.z, 1))
            {
                currentTask.RemoveAt(0);
                HandleTask();
            }
        }

        private void RotateRobot()
        {
            if (rotationTick != 21)
            {
                Rotate(0, (rotation + (rotationValue / 20) * rotationTick), 0);
                _rotationTick++;
            }
            else
            {
                _isRotating = false;
                _rotation = rotation + rotationValue;
                _rotationTick = 0;
            }
        }

        public override bool Update(int tick)
        {
            if (isMoving == true)
            {
                if (isRotating == true)
                {
                    RotateRobot();
                }
                else
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
            }

            return base.Update(tick);
        }
    }
}