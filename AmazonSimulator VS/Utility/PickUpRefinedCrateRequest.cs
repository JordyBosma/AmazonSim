﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    public class PickUpRefinedCrateRequest : LogicTask
    {
        private PickUpTarget refinery;
        private Crate crate;

        public PickUpRefinedCrateRequest(PickUpTarget refinery, Crate crate)
        {
            this.refinery = refinery;
            this.crate = crate;
        }

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
            w.tasksForRobot.Add(new TaskForRobot(new double[] { 18, 18 }, ((Node)emptyStorageNode).position, crate, refinery, (DropOffTarget)emptyStorageNode));
            w.worldObjects.Add(crate);
            crate.Move(crate.x, crate.y, crate.z);
            return true;
        }
    }
}
