using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    /// <summary>
    /// Request to spawn a import vehicle and make a tasks for robots to move cargo of unrefined crates from the import vehicle to a empty inport storage node.
    /// </summary>
    public class ImportVehicleRequest : LogicTask, InboundLogicTask
    {
        private ImportVehicle importVehicle;
        private int cratesBeingHandeld = -1;

        public ImportVehicleRequest(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            importVehicle = new ImportVehicle(x, y, z, rotationX, rotationY, rotationZ);
        }

        /// <summary>
        /// Returns time untill timer elapsed.
        /// </summary>
        /// <returns></returns>
        public int GetInterval()
        {
            Random rnd = new Random();
            return rnd.Next(30, 70) * 1000;
        }

        /// <summary>
        /// Runs the ImportVehicleRequest. 
        /// This will spawn a import vehicle and keeps trys to make a tasks for robots to move a unrefined crate in the cargo of the import vehicle to a empty inport storage node until for all crates in cargo a task for a robot is made. 
        /// Returns true if succesfully completed.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool RunTask(Model w)
        {
            if (cratesBeingHandeld == -1)
            {
                w.worldObjects.Add(importVehicle);
                importVehicle.Move(importVehicle.x, importVehicle.y, importVehicle.z);
                cratesBeingHandeld = importVehicle.importCrates.Count();
            }
            if (importVehicle.CheckArrived())
            {
                while (importVehicle.importCrates.Count() != 0)
                {
                    Crate crate = importVehicle.importCrates[importVehicle.importCrates.Count() - 1];
                    Node emptyStorageNode = null;
                    foreach (Node node in w.nodeGrid.nodes)
                    {
                        if (node is StorageNode)
                        {
                            if (((StorageNode)node).CheckImport() && !((StorageNode)node).GetReserved())
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
                    w.tasksForRobot.Add(new TaskForRobot(new double[] { -1, 25 }, ((Node)emptyStorageNode).position, crate, (PickUpTarget)importVehicle, (DropOffTarget)emptyStorageNode));
                    w.worldObjects.Add(crate);
                    importVehicle.importCrates.RemoveAt(importVehicle.importCrates.Count() - 1);
                }
                return true;
            }
            return false;
            
        }
    }
}
