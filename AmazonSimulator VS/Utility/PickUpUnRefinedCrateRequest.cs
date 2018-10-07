using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    /// <summary>
    /// Request to make a task for a robot to move a unrefined crate on a inport storage node to a refinery with space left.
    /// </summary>
    public class PickUpUnRefinedCrateRequest : LogicTask
    {
        private StorageNode node;

        public PickUpUnRefinedCrateRequest(StorageNode node)
        {
            this.node = node;
        }

        /// <summary>
        /// Runs the PickUpUnRefinedCrateRequest. This will try to make a task for a robot to move a unrefined crate on a inport storage node to a refinery with space left. Returns true if succesfully completed.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
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
