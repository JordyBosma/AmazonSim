using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D a = CreateRobot(4.6, 0, 13);
            Object3D b = CreateRobot(5.6, 0, 13);
            Object3D c = CreateRobot(6.6, 0, 13);
            Object3D d = CreateRobot(7.6, 0, 13);
            Object3D e = CreateRobot(8.6, 0, 13);
            Object3D f = CreateRobot(9.6, 0, 13);

            //r.Move(4.6, 0, 13);
        }

        public void MoveRobot(double z)
        {
            foreach (Robot r in worldObjects)
            {
                r.TaskPos(z);
            }
        }

        private Object3D CreateRobot(double x, double y, double z) {
            Object3D r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        //private Object3D CreateCrate(double x, double y, double z)
        //{
        //    Object3D c = new Crate(x, y, z, 0, 0, 0);
        //    worldObjects.Add(c);
        //    return c;
        //}
    }
}