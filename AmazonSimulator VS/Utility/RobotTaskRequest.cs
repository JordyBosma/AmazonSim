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
            //new RobotTask(new DijkstraPathFinding(startPoint, pickUpPoint, nodeGrid).GetPath(), new DijkstraPathFinding(pickUpPoint, endPoint, nodeGrid).GetPath());
            throw new NotImplementedException();
        }
    }
}
