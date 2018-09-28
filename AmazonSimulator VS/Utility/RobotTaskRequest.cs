using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class RobotTaskRequest : LogicTask
    {
        private double[] _currentPosision;
        private Guid _id;

        public double[] currentPosision { get { return _currentPosision; } }
        public Guid id { get { return _id; } }

        public RobotTaskRequest(double[] Position, Guid id)
        {
            _currentPosision = Position;
            _id = id;
        }

        public void RunTask(Model w)
        {
            //new RobotTask(new DijkstraPathFinding(startPoint, pickUpPoint, nodeGrid).GetPath(), new DijkstraPathFinding(pickUpPoint, endPoint, nodeGrid).GetPath());
            throw new NotImplementedException();
        }
    }
}
