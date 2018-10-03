using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Utility;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D robot = CreateObject(0, 0, 0, "Robot");
            Object3D crate = CreateObject(5, 1, 5, "Crate");
            Object3D train = CreateObject(15, 0, 49, "Import");
            SetVehicleInboundTimer(new ExportVehicleRequest(30, 30));

            LoadGrid();
            showGrid = true;

            //test robot run
            List<double[]> pickupTask = new List<double[]>();
            List<double[]> dropoffTask = new List<double[]>();

            pickupTask.Add(new double[2] { 0, 0 });
            pickupTask.Add(new double[2] { 10, 0 });
            pickupTask.Add(new double[2] { 10, 10 });
            pickupTask.Add(new double[2] { 20, 10 });
            pickupTask.Add(new double[2] { 20, 20 });
            pickupTask.Add(new double[2] { 30, 20 });
            pickupTask.Add(new double[2] { 30, 30 });

            dropoffTask.Add(new double[2] { 30, 30 });
            dropoffTask.Add(new double[2] { 20, 30 });
            dropoffTask.Add(new double[2] { 20, 20 });
            dropoffTask.Add(new double[2] { 10, 20 });
            dropoffTask.Add(new double[2] { 10, 10 });
            dropoffTask.Add(new double[2] { 0, 10 });
            dropoffTask.Add(new double[2] { 0, 0 });

            //RobotTask rt = new RobotTask(pickupTask, dropoffTask, (Crate)crate, null);
            //MoveRobot(rt);
            //((Robot)robot).GiveTask(new RobotTask(new DijkstraPathFinding(new double[] { 0, 0 }, new double[] { 2, 20 }, _nodeGrid).GetPath(), new DijkstraPathFinding(new double[] { 2, 20 }, new double[] { 0, 0 }, _nodeGrid).GetPath(), (Crate)crate, null));
        }

        public void MoveRobot(RobotTask rt)
        {
            foreach (Object3D r in worldObjects)
            {
                if (r.type == "Robot")
                {
                    ((Robot)r).GiveTask(rt);
                    break;
                }
            }
        }

        private void LoadGrid()
        {
            //positive x + z
            _nodeGrid.NodesAdd(new double[] { 1, 1 }, new List<int>() { }); // central square
            _nodeGrid.NodesAdd(new double[] { 5, 1 }, new List<int>() { 0 });
            _nodeGrid.NodesAdd(new double[] { 12, 1 }, new List<int>() { 1 }); 
            _nodeGrid.NodesAdd(new double[] { 16, 1 }, new List<int>() { 2 }); // inner boundry square
            _nodeGrid.NodesAdd(new double[] { 18, 1 }, new List<int>() { 3, 5 }); // outer boundry square

            // outer circle
            _nodeGrid.NodesAdd(new double[] { 18, 18 }, new List<int>() { 6 }); // corner
            _nodeGrid.NodesAdd(new double[] { 1, 18 }, new List<int>() { 3 }); 
        }
        
        private Object3D CreateObject(double x, double y, double z, string type) {
            switch (type)
            {
                case "Robot":
                    Object3D r = new Robot(x, y, z, 0, 0, 0);
                    worldObjects.Add(r);
                    return r;
                case "Export":
                    Object3D e = new ExportVehicle(x,y,z);
                    worldObjects.Add(e);
                    return e;
                case "Import":
                    Object3D i = new ImportVehicle(x, y, z, 0, 0.5 * Math.PI, 0, worldObjects);
                    worldObjects.Add(i);
                    return i;
                case "Crate":
                    Object3D c = new Crate(x, y, z);
                    worldObjects.Add(c);
                    return c;
                default:
                    throw new ArgumentException("there is no model that corresponds with that type");
            }
        }
    }
}