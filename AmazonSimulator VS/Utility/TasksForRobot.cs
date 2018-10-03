using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class TaskForRobot
    {
        private double[] _pickUpPoint;
        private double[] _dropOffPoint;
        private Crate _crate;
        private Target _target;
        
        public double[] pickUpPoint { get { return _pickUpPoint; } }
        public double[] dropOffPoint { get { return _dropOffPoint; } }
        public Crate crate { get { return _crate; } }
        public Target target { get { return _target; } }

        public TaskForRobot(double[] pickUpPoint, double[] dropOffPoint, Crate crate, Target target)
        {
            _pickUpPoint = pickUpPoint;
            _dropOffPoint = dropOffPoint;
            _crate = crate;
            _target = target;
        }
    }
}
