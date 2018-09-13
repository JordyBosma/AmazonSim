using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D r = CreateRobot(0,0,0);
            r.Move(4.6, 0, 13);
            Object3D r = CreateRobot(0, 0, 0);
            r.Move(5.6, 0, 15);
        }

        private Object3D CreateRobot(double x, double y, double z) {
            Object3D r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }
    }
}