using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    public class ExportVehicleRequest : LogicTask
    {
        private int _interval;
        private double _x;
        private double _y;
        private double _z;
        private ExportVehicle exportVehicle = null;

        public int interval { get { return _interval; } }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }

        public ExportVehicleRequest(double x, double z)
        {
            Random rnd = new Random();
            _interval = rnd.Next(89, 180) * 1000;
            _x = x;
            _y = 400;
            _z = z;
        }

        public bool RunTask(Model w)
        {
            if (exportVehicle == null)
            {
                exportVehicle = new ExportVehicle(x, y, z, 0, Math.PI, 0);
                w.worldObjects.Add(exportVehicle);
                exportVehicle.Move(exportVehicle.x, exportVehicle.y, exportVehicle.z);
            }
            if (exportVehicle.CheckArrived())                                                                 
            {
                List<StorageNode> filledRefinedStorageNodes = new List<StorageNode>();
                foreach (Node node in w.nodeGrid.nodes)                 //WARNING!!!
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

                //loop:
                int lastWeight = -1;
                while (filledRefinedStorageNodes.Count() != 0)
                {
                    bool lowestvalue = true;
                    for (int i = 0; i < filledRefinedStorageNodes.Count(); i++)
                    {
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
