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
                List<StorageNode> filledRefinedStorageNodes = null;
                foreach (Node node in w.nodeGrid.nodes)                 //WARNING!!!
                {
                    if (node is StorageNode)
                    {
                        if (!((StorageNode)node).CheckImport() && ((StorageNode)node).GetIsDone())
                        {
                            filledRefinedStorageNodes.Add((StorageNode)node);
                            //((StorageNode)node).SetIsDone();
                            //break;
                        }
                    }
                }
                if (filledRefinedStorageNodes.Count() == 0)
                {
                    return false;
                }
                //loop door filledRefinedStorageNodes hier onder:



                if (exportVehicle.assignedWeightLeft > 5)
                {
                    if (filledRefinedStorageNodes.Count() > 3)
                    {
                        int biggestW = 0, biggestP = -1;
                        for (int i = 0; i < filledRefinedStorageNodes.Count(); i++)
                        {
                            int weightOnNode = filledRefinedStorageNodes[i].GetCrate().weight;
                            if (weightOnNode > biggestW && weightOnNode <= exportVehicle.weightLeft)
                            {
                                biggestP = i;
                                if (exportVehicle.assignedWeightLeft == biggestW)
                                {
                                    break;
                                } else
                                {
                                    biggestW = weightOnNode;
                                }
                            }
                        }
                        if (biggestP != -1)
                        {
                            StorageNode pickUpTarget = filledRefinedStorageNodes[biggestP];
                            w.tasksForRobot.Add(new TaskForRobot(pickUpTarget.position, new double[] { exportVehicle.x, exportVehicle.z }, pickUpTarget.GetCrate(), (PickUpTarget)pickUpTarget, (DropOffTarget)exportVehicle));
                            pickUpTarget.SetIsDone();
                        }
                        else
                        {
                            exportVehicle.LoadWeight(exportVehicle.weightLeft);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
