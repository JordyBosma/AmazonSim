using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    /// <summary>
    /// Object with all data needed to make a task for a robot.
    /// </summary>
    public class RobotTask
    {
        private List<double[]> _pickUpTask;
        private List<double[]> _dropOffTask;
        private Crate _pickUpCrate;
        private PickUpTarget _pickUpTarget;
        private DropOffTarget _dropOffTarget;

        public List<double[]> pickUpTask { get { return _pickUpTask; } }
        public List<double[]> dropOffTask { get { return _dropOffTask; } }
        public Crate pickUpCrate { get { return _pickUpCrate; } }
        public PickUpTarget pickUpTarget { get { return _pickUpTarget; } }
        public DropOffTarget dropOffTarget { get { return _dropOffTarget; } }

        public RobotTask(List<double[]> pickUpTask, List<double[]> dropOffTask, Crate pickUpCrate, PickUpTarget pickUpTarget, DropOffTarget dropOffTarget)
        {
            this._pickUpTask = pickUpTask;
            this._dropOffTask = dropOffTask;
            this._pickUpCrate = pickUpCrate;
            this._pickUpTarget = pickUpTarget;
            this._dropOffTarget = dropOffTarget;
        }
    }
}
