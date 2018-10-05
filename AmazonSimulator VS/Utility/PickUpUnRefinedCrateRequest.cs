using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Utility;

namespace Utility
{
    public class PickUpUnRefinedCrateRequest : LogicTask
    {
        private StorageNode node;

        public PickUpUnRefinedCrateRequest(StorageNode node)
        {
            this.node = node;
        }

        public bool RunTask(Model w)
        {
            List<Object3D> refinerys = new List<Object3D>();
            foreach (Object3D obj in w.worldObjects)
            {
                if (obj is Refinery)
                {
                    refinerys.Add(obj);
                }
            }
            Random rnd = new Random();
            int ramdomIndex = rnd.Next(0, refinerys.Count() -1);
            w.tasksForRobot.Add(new TaskForRobot(node.position, new double[] { -18, 1 }, node.GetCrate(), (PickUpTarget)node, (DropOffTarget)refinerys[ramdomIndex]));
            return true;
        }
    }
}
