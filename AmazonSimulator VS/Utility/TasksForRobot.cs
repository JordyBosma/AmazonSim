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
        private string _target;
        private Crate _crate;

        public string target { get { return _target; } }
        public Crate crate { get { return _crate; } }
        public double[] pickUpPoint { get { return _pickUpPoint; } }
        public double[] dropOffPoint { get { return _dropOffPoint; } }
    }
}
