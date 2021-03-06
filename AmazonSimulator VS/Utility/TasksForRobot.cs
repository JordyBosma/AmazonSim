﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// Object with taskdata for a robot to execute.
    /// </summary>
    public class TaskForRobot
    {
        private double[] _pickUpPoint;
        private double[] _dropOffPoint;
        private Crate _pickUpCrate; 
        private PickUpTarget _pickUpTarget;
        private DropOffTarget _dropOffTarget;

        public double[] pickUpPoint { get { return _pickUpPoint; } }
        public double[] dropOffPoint { get { return _dropOffPoint; } }
        public Crate pickUpCrate { get { return _pickUpCrate; } }
        public PickUpTarget pickUpTarget { get { return _pickUpTarget; } }
        public DropOffTarget dropOffTarget { get { return _dropOffTarget; } }

        public TaskForRobot(double[] pickUpPoint, double[] dropOffPoint, Crate crate, PickUpTarget pickUpTarget, DropOffTarget dropOffTarget)
        {
            _pickUpPoint = pickUpPoint;
            _dropOffPoint = dropOffPoint;
            _pickUpCrate = crate;
            _pickUpTarget = pickUpTarget;
            _dropOffTarget = dropOffTarget;
        }
    }
}
