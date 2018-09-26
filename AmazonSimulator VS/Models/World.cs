using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using static Utility.RobotData;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D a = CreateRobot(0, 0, 0);
        }

        public void MoveRobot(RobotTask rt)
        {
            foreach (Robot r in worldObjects)
            {
                r.GetTask(rt);
            }
        }
        
        private Object3D CreateRobot(double x, double y, double z) {
            Object3D r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }
    }
}