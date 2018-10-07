using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    /// <summary>
    /// Request to make a task for a robot to move a refinedcrate in a refinery to empty export storage node.
    /// </summary>
    public class PickUpRefinedCrateRequest : LogicTask
    {
        private PickUpTarget refinery;
        private Crate crate;

        public PickUpRefinedCrateRequest(PickUpTarget refinery, Crate crate)
        {
            this.refinery = refinery;
            this.crate = crate;
        }

        /// <summary>
        /// Runs the PickUpRefinedCrateRequest. 
        /// This will try to make a task for a robot to move a refined crate in refinery to a empty export storage node. 
        /// Returns true if succesfully completed.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool RunTask(Model w)
        {
            Node emptyStorageNode = null;
            foreach (Node node in w.nodeGrid.nodes)
            {

                if (node is StorageNode)
                {
                    if (!((StorageNode)node).CheckImport() && !((StorageNode)node).GetReserved())
                    {
                        emptyStorageNode = node;
                        ((StorageNode)node).ReserveNode();
                        break;
                    }
                }
            }
            if (emptyStorageNode == null)
            {
                return false;
            }
            w.tasksForRobot.Add(new TaskForRobot(new double[] { -25, 1 }, ((Node)emptyStorageNode).position, crate, refinery, (DropOffTarget)emptyStorageNode));
            w.worldObjects.Add(crate);
            crate.Move(crate.x, crate.y, crate.z);
            return true;
        }
    }
}
