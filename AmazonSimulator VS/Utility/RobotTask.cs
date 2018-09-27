using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class RobotTask
    {
        private List<double[]> _pickupTask;
        private List<double[]> _dropoffTask;

        public List<double[]> pickupTask { get { return _pickupTask; } }
        public List<double[]> dropoffTask { get { return _dropoffTask; } }

        public RobotTask(List<double[]> pickupTask, List<double[]> dropoffTask)
        {
            this._pickupTask = pickupTask;
            this._dropoffTask = dropoffTask;
        }
    }
}
