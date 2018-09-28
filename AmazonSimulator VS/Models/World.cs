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
            Object3D crate = CreateObject(30, 1, 30, "Crate");
            Object3D rocket = CreateObject(0, 0, 0, "Export");

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

            RobotTask rt = new RobotTask(pickupTask, dropoffTask, (Crate)crate);
            MoveRobot(rt);
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
        
        private Object3D CreateObject(double x, double y, double z, string type) {
            switch (type)
            {
                case "Robot":
                    Object3D r = new Robot(x, y, z, 0, 0, 0);
                    worldObjects.Add(r);
                    return r;
                case "Export":
                    Object3D e = new ExportVehicle(x, 1000, z, 0, 0, 0);
                    worldObjects.Add(e);
                    return e;
                case "Crate":
                    Object3D c = new Crate(x, y, z, 0, 0, 0);
                    worldObjects.Add(c);
                    return c;
                default:
                    throw new ArgumentException("there is no model that corresponds with that type");
            }
        }
    }
}