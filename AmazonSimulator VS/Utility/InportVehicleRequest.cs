﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public class InportVehicleRequest : LogicTask
    {
        private int _interval;
        private ImportVehicle importVehicle;
        private int cratesBeingHandeld = -1;

        public int interval { get { return _interval; } }

        public InportVehicleRequest(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            Random rnd = new Random();
            _interval = rnd.Next(10, 20) * 1000;
            importVehicle = new ImportVehicle(x, y, z, rotationX, rotationY, rotationZ);
        }

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
                while (cratesBeingHandeld != 0)
                {
                    Crate crate = importVehicle.importCrates[cratesBeingHandeld - 1];
                    Object emptyStorageNode = null;
                    foreach (Node node in w.nodeGrid.nodes)
                    {

                        if (node is StorageNode)
                        {
                            if (((StorageNode)node).CheckImport() && (!((StorageNode)node).GetReserved() && !((StorageNode)node).CheckCrate()))
                            {
                                emptyStorageNode = node;
                                ((StorageNode)node).ReserveNode();
                                break;
                            }
                        }
                    }
                    if (emptyStorageNode == null)
                    {
                        return true;
                    }
                    w.tasksForRobot.Add(new TaskForRobot(new double[] { crate.x, crate.z }, ((Node)emptyStorageNode).position, crate, (Target)emptyStorageNode));
                    w.worldObjects.Add(crate);
                    cratesBeingHandeld--;
                }
                return false;
            }
            return true;
            
        }
    }
}
