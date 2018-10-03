using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class TasksForRobot
    {
        private double[] _pickUpPoint;
        private double[] _dropOffPoint;
        private Crate _crate;
        private Object3D _target;
        
        public double[] pickUpPoint { get { return _pickUpPoint; } }
        public double[] dropOffPoint { get { return _dropOffPoint; } }
        public Crate crate { get { return _crate; } }
        public Object3D target { get { return _target; } }

        public TasksForRobot(double[] pickUpPoint, double[] dropOffPoint, Crate crate, Object3D target)
        {
            _pickUpPoint = pickUpPoint;
            _dropOffPoint = dropOffPoint;
            _crate = crate;
            _target = target;
        }
    }
}
