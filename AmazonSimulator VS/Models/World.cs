using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D robot = CreateObject(0, 0, 0, "Robot");
            Object3D rocket = CreateObject(15, 0, 5, "Export");
        }

        public void MoveRobot(RobotTask rt)
        {
            foreach (Object3D r in worldObjects)
            {
                if (r.type == "Robot")
                {
                    ((Robot)r).GetTask(rt);
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
                    Object3D e = new ExportVehicle(x, y, z, 0, 0, 0);
                    worldObjects.Add(e);
                    return e;
                default:
                    throw new ArgumentException("there is no model that corresponds with that type");
            }
        }
    }
}