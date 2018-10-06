using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Utility;
using static Utility.RobotTask;

namespace Models
{
    /// <summary>
    /// Deze Class handled de movement en rotatie van de robot, en het crate dat zich eventueel op de robot bevind.
    /// </summary>
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
                _pickupCrate.Move(x, 0.39, z);
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

        /// <summary>
        /// Deze Methode zorgt ervoor dat de pickuptask en dropofftask gehandled worden, ook geeft deze methode aan naar welk punt de robot moet bewegen.
        /// </summary>
        private void HandleTask()
        {
            if (currentTask == null)
            {
                currentTask = pickupTask;
            }

            if (currentTask.Count > 1)
            {
                _pointOne = currentTask[0];
                _pointTwo = currentTask[1];
                SetRoute(_pointOne, _pointTwo);
            }
            else
            {
                if (currentTask == pickupTask)
                {
                    currentTask = dropoffTask;
                    _moveCrate = true;
                    _pickUpTarget.HandelPickUp();
                    SetRoute(currentTask[0], currentTask[1]);  
                }
                else
                {
                    currentTask = null;
                    _movementAxis = "";
                    _isMoving = false;
                    _isDone = true;
                    _moveCrate = false;
                    _dropOffTarget.HandelDropOff(_pickupCrate);
                }
            }
        }

        private void SetRoute(double[] pointOne, double[] pointTwo)
        {
            if (pointOne[0] == pointTwo[0])
            {
                _movementAxis = "z";
                SetRotation(_movementAxis);
                TaskPos(pointTwo[1]);
            }
            else
            {
                _movementAxis = "x";
                SetRotation(_movementAxis);
                TaskPos(pointTwo[0]);
            }
        }

        /// <summary>
        /// deze methode handled de rotatie van de robot doormiddel van de movementAxis en beweging over die axis.
        /// </summary>
        /// <param name="movmentAxis"></param>
        private void SetRotation(string movmentAxis)
        {
            _rotationAxis = _newRotationAxis;
            if (_movementAxis == "z")
            {
                if (_pointOne[1] < _pointTwo[1])
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
                if (_pointOne[0] < _pointTwo[0])
                {
                    _newRotationAxis = 1;
                }
                else
                {
                    _newRotationAxis = 3;
                }
            }

            int rotation = _newRotationAxis - _rotationAxis;

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
            if (_rotationValue != 0)
            {
                _isRotating = true;
            }
            _isMoving = true;
        }

        /// <summary>
        /// deze method beweegt de robot over de x as
        /// </summary>
        private void MoveToPosX()
        {
            if (_endPos > Math.Round(this.x, 1))
            {
                this.Move(this.x + 0.1, this.y, this.z);
            }
            else if (_endPos < Math.Round(this.x, 1))
            {
                this.Move(this.x - 0.1, this.y, this.z);
            }

            if (_endPos == Math.Round(this.x, 1))
            {
                currentTask.RemoveAt(0);
                HandleTask();
            }
        }

        /// <summary>
        /// deze methode beweegt de robot over de y as
        /// </summary>
        private void MoveToPosZ()
        {
            if (_endPos > Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z + 0.1);
            }
            else if (_endPos < Math.Round(this.z, 1))
            {
                this.Move(this.x, this.y, this.z - 0.1);
            }

            if (_endPos == Math.Round(this.z, 1))
            {
                currentTask.RemoveAt(0);
                HandleTask();
            }
        }

        /// <summary>
        /// deze methode roteert de robot naar zijn rotation target in stappen van 20
        /// </summary>
        private void RotateRobot()
        {
            if (_rotationTick != 21)
            {
                Rotate(0, (_rotation + (_rotationValue / 20) * _rotationTick), 0);
                _rotationTick++;
            }
            else
            {
                _isRotating = false;
                _rotation = _rotation + _rotationValue;
                _rotationTick = 0;
            }
        }

        public override bool Update(int tick)
        {
            if (_isMoving == true)
            {
                if (_isRotating == true)
                {
                    RotateRobot();
                }
                else
                {
                    if (_movementAxis == "x")
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