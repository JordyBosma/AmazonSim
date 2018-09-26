using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class RobotData
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

        public class RobotRequest
        {
            private double[] _currentPosision;
            private Guid _id;

            public double[] currentPosision { get { return _currentPosision; } }
            public Guid id { get { return _id; } }

            public RobotRequest(double[] Position, Guid id)
            {
                _currentPosision = Position;
                _id = id;
            }
        }
    }
}
