using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    /// <summary>
    /// Request to spawn a export vehicle and make tasks for robots to move a refined crates stored on a export storage nodes to the export vehicle to fill it up so closes as posible to its maximal weight capacity.
    /// </summary>
    public class ExportVehicleRequest : LogicTask, InboundLogicTask
    {
        private double _x;
        private double _y;
        private double _z;
        private ExportVehicle exportVehicle = null;

        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }

        public ExportVehicleRequest(double x, double z)
        {
            _x = x;
            _y = 400;
            _z = z;
        }

        /// <summary>
        /// Returns time untill timer elapsed.
        /// </summary>
        /// <returns></returns>
        public int GetInterval()
        {
            Random rnd = new Random();
            return rnd.Next(89, 180) * 1000;
        }

        /// <summary>
        /// Runs the ExportVehicleRequest. 
        /// This will spawn a export vehicle and will try to make a tasks for robots to move refined crates stored on a export storage nodes to the export vehicle to fill it up so closes as posible to its maximal weight capacity. 
        /// Returns true if succesfully completed.
        /// </summary>
        public bool RunTask(Model w)
        {
            //Spawn export vehicle:
            if (exportVehicle == null)
            {
                exportVehicle = new ExportVehicle(x, y, z, 0, Math.PI, 0);
                w.worldObjects.Add(exportVehicle);
                exportVehicle.Move(exportVehicle.x, exportVehicle.y, exportVehicle.z);
            }
            //Make tasks for robots if vehicle has arrived:
            if (exportVehicle.CheckArrived())                                                                 
            {
                //Find filled export storage nodes and order them by the weight of the crates in the nodes:
                List<StorageNode> filledRefinedStorageNodes = new List<StorageNode>();
                foreach (Node node in w.nodeGrid.nodes)                 //WARNING!!! could lead to "change to collections" exeption, but i doubt it.
                {
                    if (node is StorageNode)
                    {
                        if (!((StorageNode)node).CheckImport() && ((StorageNode)node).GetIsDone())
                        {
                            filledRefinedStorageNodes.Add((StorageNode)node);
                        }
                    }
                }
                if (filledRefinedStorageNodes.Count() == 0)
                {
                    return false;
                }
                filledRefinedStorageNodes = filledRefinedStorageNodes.OrderBy(node => node.GetCrate().weight).ToList();

                //Loop that creates tasks for robots to move crates to the export vehicle with the found filled refined storage nodes:
                int lastWeight = -1;
                while (filledRefinedStorageNodes.Count() != 0)
                {
                    bool lowestvalue = true;
                    for (int i = 0; i < filledRefinedStorageNodes.Count(); i++)
                    {
                        //Export vehicle is almost full, will try fill it as full as possible:
                        if (exportVehicle.assignedWeightLeft <= 10)
                        {
                            bool enoughChoice = filledRefinedStorageNodes.Count() >= 3;
                            int biggestW = 0, biggestP = -1;
                            for (int j = 0; j < filledRefinedStorageNodes.Count(); j++)
                            {
                                int weightOnNode = filledRefinedStorageNodes[j].GetCrate().weight;
                                if (exportVehicle.assignedWeightLeft == weightOnNode)
                                {
                                    StorageNode pickUpTarget = filledRefinedStorageNodes[j];
                                    w.tasksForRobot.Add(new TaskForRobot(pickUpTarget.position, new double[] { 1, -25 }, pickUpTarget.GetCrate(), (PickUpTarget)pickUpTarget, (DropOffTarget)exportVehicle));
                                    pickUpTarget.SetIsDone();
                                    exportVehicle.AssignWeight(weightOnNode);
                                    return true;
                                }
                                else if (enoughChoice)
                                {
                                    if (weightOnNode > biggestW && weightOnNode < exportVehicle.assignedWeightLeft)
                                    {
                                        biggestP = j;
                                        biggestW = weightOnNode;
                                    }
                                } 
                            }
                            if (biggestP != -1)
                            {
                                StorageNode pickUpTarget = filledRefinedStorageNodes[biggestP];
                                w.tasksForRobot.Add(new TaskForRobot(pickUpTarget.position, new double[] { 1, -25 }, pickUpTarget.GetCrate(), (PickUpTarget)pickUpTarget, (DropOffTarget)exportVehicle));
                                filledRefinedStorageNodes.RemoveAt(biggestP);
                                pickUpTarget.SetIsDone();
                                exportVehicle.AssignWeight(biggestW);
                            }
                            else
                            {
                                exportVehicle.LoadWeight(exportVehicle.assignedWeightLeft);
                                return true;
                            }

                        }
                        //Export vehicle has more than enough space, fill the export vehicle with biggest weight to smallest weight, a smaller weight each time. 
                        //this until lowest weight has been found, than this will sart over again:
                        else if (filledRefinedStorageNodes[i].GetCrate().weight < lastWeight || lastWeight == -1)
                        {
                            StorageNode pickUpTarget = filledRefinedStorageNodes[i];
                            w.tasksForRobot.Add(new TaskForRobot(pickUpTarget.position, new double[] { 1, -25 }, pickUpTarget.GetCrate(), (PickUpTarget)pickUpTarget, (DropOffTarget)exportVehicle));
                            lastWeight = pickUpTarget.GetCrate().weight;
                            pickUpTarget.SetIsDone();
                            exportVehicle.AssignWeight(lastWeight);
                            filledRefinedStorageNodes.RemoveAt(i);
                            lowestvalue = false;
                            break;
                        }
                    }
                    if (lowestvalue == true) lastWeight = -1;
                }
            }
            return false;
        }
    }
}
