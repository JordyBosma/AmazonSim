using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    /// <summary>
    /// Request to give a task to the given robot.
    /// </summary>
    public class RobotTaskRequest : LogicTask
    {
        private Robot _rqRobot;
        public Robot rqRobot { get { return _rqRobot; } }

        public RobotTaskRequest(Robot rqRobot)
        {
            _rqRobot = rqRobot;
        }

        /// <summary>
        /// Runs the RobotTaskRequest. 
        /// This will try to give a task to the given robot if there is one.
        /// Returns true if succesfully completed.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool RunTask(Model w)
        {
            if (w.tasksForRobot.Count() != 0)
            {
                TaskForRobot tsk = w.tasksForRobot.First();
                w.tasksForRobot.RemoveAt(0);
                rqRobot.GiveTask(new RobotTask(new DijkstraPathFinding(new double[] { Math.Round(rqRobot.x, 1), Math.Round(rqRobot.z, 1) }, tsk.pickUpPoint, w.nodeGrid).GetPath(), new DijkstraPathFinding(tsk.pickUpPoint, tsk.dropOffPoint, w.nodeGrid).GetPath(), tsk.pickUpCrate, tsk.pickUpTarget, tsk.dropOffTarget));
                rqRobot.Move(rqRobot.x, rqRobot.y, rqRobot.z);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
