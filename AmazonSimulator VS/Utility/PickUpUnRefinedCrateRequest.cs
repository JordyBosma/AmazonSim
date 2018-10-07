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
            foreach (Object3D obj in w.worldObjects)
            {
                if (obj is Refinery)
                {
                    if (((Refinery)obj).CheckForSpaceLeft())
                    {
                        w.tasksForRobot.Add(new TaskForRobot(node.position, new double[] { -25, -1 }, node.GetCrate(), (PickUpTarget)node, (DropOffTarget)obj));
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
