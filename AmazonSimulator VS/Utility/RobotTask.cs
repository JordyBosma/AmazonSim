﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class RobotTask
    {
        private List<double[]> _pickupTask;
        private List<double[]> _dropoffTask;
        private Crate _pickupCrate;

        public List<double[]> pickupTask { get { return _pickupTask; } }
        public List<double[]> dropoffTask { get { return _dropoffTask; } }
        public Crate pickupCrate { get { return _pickupCrate; } }

        public RobotTask(List<double[]> pickupTask, List<double[]> dropoffTask, Crate pickupCrate)
        {
            this._pickupTask = pickupTask;
            this._dropoffTask = dropoffTask;
            this._pickupCrate = pickupCrate;
        }
    }
}
