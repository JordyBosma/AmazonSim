using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class RobotTaskRequest : LogicTask
    {
        private Robot _rqRobot;
        public Robot rqRobot { get { return _rqRobot; } }

        public RobotTaskRequest(Robot rqRobot)
        {
            _rqRobot = rqRobot;
        }

        public void RunTask(Model w)
        {
            TasksForRobot tsk = w.tasksForRobot.First();
            w.tasksForRobot.RemoveAt(0);
            rqRobot.GiveTask(new RobotTask(new DijkstraPathFinding(new double[]{rqRobot.x, rqRobot.z}, tsk.pickUpPoint, w.nodeGrid).GetPath(), new DijkstraPathFinding(tsk.pickUpPoint, tsk.dropOffPoint, w.nodeGrid).GetPath(), tsk.crate, tsk.target));
        }
    }
}
