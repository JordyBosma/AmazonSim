using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    public class PickUpUnRefinedCrateRequest : LogicTask
    {
        private StorageNode node;

        public PickUpUnRefinedCrateRequest(StorageNode node)
        {
            this.node = node;
        }

        public bool RunTask(Model w)
        {

            w.tasksForRobot.Add(new TaskForRobot(node.position, new double[] { -18, 1 }, node.GetCrate(), node, /*ramndom refinerary*/));
            return false;
        }
    }
}
