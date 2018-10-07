using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Utility;

namespace Models {
    public class World : Model, IUpdatable
    {
        public World() {
            Object3D robot1 = CreateObject(20, 0, -7, "Robot");
            Object3D robot2 = CreateObject(20, 0, -5, "Robot");
            Object3D robot3 = CreateObject(20, 0, -3, "Robot");
            Object3D robot4 = CreateObject(20, 0, -1, "Robot");
            Object3D robot5 = CreateObject(20, 0, 1, "Robot");
            Object3D robot6 = CreateObject(20, 0, 3, "Robot");
            Object3D robot7 = CreateObject(20, 0, 5, "Robot");
            Object3D robot8 = CreateObject(20, 0, 7, "Robot");
            Object3D refinery = CreateObject(-25, 0, -1, "Refinery");

            SetInboundTimer(new ExportVehicleRequest(0, -35));
            SetInboundTimer(new ImportVehicleRequest(400, 0, 34, 0, 0.5 * Math.PI, 0));

            LoadGrid();
            //showGrid = true;

            //((Robot)robot).GiveTask(new RobotTask(new DijkstraPathFinding(new double[] { 1, 1 }, new double[] { -7, -3 }, _nodeGrid).GetPath(), new DijkstraPathFinding(new double[] { -7, -3 }, new double[] { 1, 1 }, _nodeGrid).GetPath(), (Crate)crate, (PickUpTarget)_nodeGrid.nodes[50], (DropOffTarget)refinery));
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
            _nodeGrid.NodesAdd(new double[] { 1, 1 }, new List<int>() { 7, 105 }); // central square
            _nodeGrid.NodesAdd(new double[] { 1, 5 }, new List<int>() { 0, 11, 36 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 1, 12 }, new List<int>() { 1, 23, 37 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 1, 16 }, new List<int>() { 2, 38 }); // inner boundry square
            _nodeGrid.NodesAdd(new double[] { 1, 18 }, new List<int>() { 3, 5 }); // outer boundry square

            // outer circle
            _nodeGrid.NodesAdd(new double[] { 18, 18 }, new List<int>() { 147 }); // corner
            _nodeGrid.NodesAdd(new double[] { 18, 1 }, new List<int>() { 111, 158 }); //

            // inner circle
            _nodeGrid.NodesAdd(new double[] { 16, 1 }, new List<int>() { 8, 6 }); // corner
            _nodeGrid.NodesAdd(new double[] { 16, 5 }, new List<int>() { 9, 146 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 16, 12 }, new List<int>() { 10, 147 }); // storage lane
            _nodeGrid.NodesAdd(new double[] { 16, 16 }, new List<int>() { 3 });

            //storage lane 1
            _nodeGrid.NodesAdd(new double[] { 3, 5 }, new List<int>() { 12, 15, 16 });
            _nodeGrid.NodesAdd(new double[] { 7, 5 }, new List<int>() { 13, 17, 18 });
            _nodeGrid.NodesAdd(new double[] { 11, 5 }, new List<int>() { 14, 19, 20 });
            _nodeGrid.NodesAdd(new double[] { 15, 5 }, new List<int>() { 8, 21, 22 });

            //storage nodes 1
            _nodeGrid.StorageNodesAdd(new double[] { 3, 3 }, new List<int>() { 11 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 3, 7 }, new List<int>() { 11 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 7, 3 }, new List<int>() { 12 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 7, 7 }, new List<int>() { 12 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 11, 3 }, new List<int>() { 13 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 11, 7 }, new List<int>() { 13 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 15, 3 }, new List<int>() { 14 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 15, 7 }, new List<int>() { 14 }, true);

            //storage lane 2
            _nodeGrid.NodesAdd(new double[] { 3, 12 }, new List<int>() { 24, 27, 28 });
            _nodeGrid.NodesAdd(new double[] { 7, 12 }, new List<int>() { 25, 29, 30 });
            _nodeGrid.NodesAdd(new double[] { 11, 12 }, new List<int>() { 26, 31, 32 });
            _nodeGrid.NodesAdd(new double[] { 15, 12 }, new List<int>() { 9, 33, 34 });

            //storage nodes 2
            _nodeGrid.StorageNodesAdd(new double[] { 3, 10 }, new List<int>() { 23 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 3, 14 }, new List<int>() { 23 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 7, 10 }, new List<int>() { 24 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 7, 14 }, new List<int>() { 24 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 11, 10 }, new List<int>() { 25 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 11, 14 }, new List<int>() { 25 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 15, 10 }, new List<int>() { 26 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { 15, 14 }, new List<int>() { 26 }, true);


            // -x + z
            _nodeGrid.NodesAdd(new double[] { -1, 1 }, new List<int>() { 36, 0 }); // central square index 35
            _nodeGrid.NodesAdd(new double[] { -1, 5 }, new List<int>() { 37, 46, 1 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -1, 12 }, new List<int>() { 38, 58, 2 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -1, 16 }, new List<int>() { 39, 45 }); // inner boundry square
            _nodeGrid.NodesAdd(new double[] { -1, 18 }, new List<int>() { 4, 148 }); // outer boundry square

            // outer circle
            _nodeGrid.NodesAdd(new double[] { -18, 18 }, new List<int>() { 39 }); // corner
            _nodeGrid.NodesAdd(new double[] { -18, 1 }, new List<int>() { 142, 42 }); //

            // inner circle
            _nodeGrid.NodesAdd(new double[] { -16, 1 }, new List<int>() { 35, 77 }); // corner
            _nodeGrid.NodesAdd(new double[] { -16, 5 }, new List<int>() { 42, 142 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -16, 12 }, new List<int>() { 43, 143 }); // storage lane
            _nodeGrid.NodesAdd(new double[] { -16, 16 }, new List<int>() { 44 });

            //storage lane 1
            _nodeGrid.NodesAdd(new double[] { -3, 5 }, new List<int>() { 47, 50, 51 });
            _nodeGrid.NodesAdd(new double[] { -7, 5 }, new List<int>() { 48, 52, 53 });
            _nodeGrid.NodesAdd(new double[] { -11, 5 }, new List<int>() { 49, 54, 55 });
            _nodeGrid.NodesAdd(new double[] { -15, 5 }, new List<int>() { 43, 56, 57 });

            //storage nodes 1
            _nodeGrid.StorageNodesAdd(new double[] { -3, 3 }, new List<int>() { 46 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -3, 7 }, new List<int>() { 46 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -7, 3 }, new List<int>() { 47 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -7, 7 }, new List<int>() { 47 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -11, 3 }, new List<int>() { 48 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -11, 7 }, new List<int>() { 48 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -15, 3 }, new List<int>() { 49 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -15, 7 }, new List<int>() { 49 }, true);

            //storage lane 2
            _nodeGrid.NodesAdd(new double[] { -3, 12 }, new List<int>() { 59, 62, 63 });
            _nodeGrid.NodesAdd(new double[] { -7, 12 }, new List<int>() { 60, 64, 65 });
            _nodeGrid.NodesAdd(new double[] { -11, 12 }, new List<int>() { 61, 66, 67 });
            _nodeGrid.NodesAdd(new double[] { -15, 12 }, new List<int>() { 44, 68, 69 });

            //storage nodes 2
            _nodeGrid.StorageNodesAdd(new double[] { -3, 10 }, new List<int>() { 58 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -3, 14 }, new List<int>() { 58 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -7, 10 }, new List<int>() { 59 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -7, 14 }, new List<int>() { 59 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -11, 10 }, new List<int>() { 60 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -11, 14 }, new List<int>() { 60 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -15, 10 }, new List<int>() { 61 }, true);
            _nodeGrid.StorageNodesAdd(new double[] { -15, 14 }, new List<int>() { 61 }, true);


            //negative x - z
            _nodeGrid.NodesAdd(new double[] { -1, -1 }, new List<int>() { 77, 35 }); // central square 70
            _nodeGrid.NodesAdd(new double[] { -1, -5 }, new List<int>() { 70, 81, 106 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -1, -12 }, new List<int>() { 71, 93, 107 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -1, -16 }, new List<int>() { 72, 108 }); // inner boundry square
            _nodeGrid.NodesAdd(new double[] { -1, -18 }, new List<int>() { 73, 75 }); // outer boundry square

            // outer circle
            _nodeGrid.NodesAdd(new double[] { -18, -18 }, new List<int>() { 141 }); // corner
            _nodeGrid.NodesAdd(new double[] { -18, -1 }, new List<int>() { 41, 150 }); //

            // inner circle
            _nodeGrid.NodesAdd(new double[] { -16, -1 }, new List<int>() { 78, 76 }); // corner
            _nodeGrid.NodesAdd(new double[] { -16, -5 }, new List<int>() { 79, 140 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { -16, -12 }, new List<int>() { 80, 141 }); // storage lane
            _nodeGrid.NodesAdd(new double[] { -16, -16 }, new List<int>() { 73 });

            //storage lane 1
            _nodeGrid.NodesAdd(new double[] { -3, -5 }, new List<int>() { 82, 85, 86 });
            _nodeGrid.NodesAdd(new double[] { -7, -5 }, new List<int>() { 83, 87, 88 });
            _nodeGrid.NodesAdd(new double[] { -11, -5 }, new List<int>() { 84, 89, 90 });
            _nodeGrid.NodesAdd(new double[] { -15, -5 }, new List<int>() { 78, 91, 92 });

            //storage nodes 1
            _nodeGrid.StorageNodesAdd(new double[] { -3, -3 }, new List<int>() { 81 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -3, -7 }, new List<int>() { 81 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -7, -3 }, new List<int>() { 82 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -7, -7 }, new List<int>() { 82 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -11, -3 }, new List<int>() { 83 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -11, -7 }, new List<int>() { 83 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -15, -3 }, new List<int>() { 84 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -15, -7 }, new List<int>() { 84 }, false);

            //storage lane 2
            _nodeGrid.NodesAdd(new double[] { -3, -12 }, new List<int>() { 94, 97, 98 });
            _nodeGrid.NodesAdd(new double[] { -7, -12 }, new List<int>() { 95, 99, 100 });
            _nodeGrid.NodesAdd(new double[] { -11, -12 }, new List<int>() { 96, 101, 102 });
            _nodeGrid.NodesAdd(new double[] { -15, -12 }, new List<int>() { 79, 103, 104 });

            //storage nodes 2
            _nodeGrid.StorageNodesAdd(new double[] { -3, -10 }, new List<int>() { 93 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -3, -14 }, new List<int>() { 93 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -7, -10 }, new List<int>() { 94 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -7, -14 }, new List<int>() { 94 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -11, -10 }, new List<int>() { 95 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -11, -14 }, new List<int>() { 95 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -15, -10 }, new List<int>() { 96 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { -15, -14 }, new List<int>() { 96 }, false);


            // postive x - z
            _nodeGrid.NodesAdd(new double[] { 1, -1 }, new List<int>() { 106, 70 }); // central square index 105
            _nodeGrid.NodesAdd(new double[] { 1, -5 }, new List<int>() { 107, 116, 71 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 1, -12 }, new List<int>() { 108, 128, 72 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 1, -16 }, new List<int>() { 109, 115 }); // inner boundry square
            _nodeGrid.NodesAdd(new double[] { 1, -18 }, new List<int>() { 74, 152 }); // outer boundry square

            // outer circle
            _nodeGrid.NodesAdd(new double[] { 18, -18 }, new List<int>() { 109 }); // corner
            _nodeGrid.NodesAdd(new double[] { 18, -1 }, new List<int>() { 162, 112, 154 });

            // inner circle
            _nodeGrid.NodesAdd(new double[] { 16, -1 }, new List<int>() { 105, 7 }); // corner
            _nodeGrid.NodesAdd(new double[] { 16, -5 }, new List<int>() { 112, 144 }); //storage lane
            _nodeGrid.NodesAdd(new double[] { 16, -12 }, new List<int>() { 113, 145 }); // storage lane
            _nodeGrid.NodesAdd(new double[] { 16, -16 }, new List<int>() { 114 });

            //storage lane 1
            _nodeGrid.NodesAdd(new double[] { 3, -5 }, new List<int>() { 117, 120, 121 });
            _nodeGrid.NodesAdd(new double[] { 7, -5 }, new List<int>() { 118, 122, 123 });
            _nodeGrid.NodesAdd(new double[] { 11, -5 }, new List<int>() { 119, 124, 125 });
            _nodeGrid.NodesAdd(new double[] { 15, -5 }, new List<int>() { 113, 126, 127 });

            //storage nodes 1
            _nodeGrid.StorageNodesAdd(new double[] { 3, -3 }, new List<int>() { 116 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 3, -7 }, new List<int>() { 116 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 7, -3 }, new List<int>() { 117 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 7, -7 }, new List<int>() { 117 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 11, -3 }, new List<int>() { 118 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 11, -7 }, new List<int>() { 118 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 15, -3 }, new List<int>() { 119 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 15, -7 }, new List<int>() { 119 }, false);

            //storage lane 2
            _nodeGrid.NodesAdd(new double[] { 3, -12 }, new List<int>() { 129, 132, 133 }); //index 128
            _nodeGrid.NodesAdd(new double[] { 7, -12 }, new List<int>() { 130, 134, 135 });
            _nodeGrid.NodesAdd(new double[] { 11, -12 }, new List<int>() { 131, 136, 137 });
            _nodeGrid.NodesAdd(new double[] { 15, -12 }, new List<int>() { 114, 138, 139 }); 

            //storage nodes 2
            _nodeGrid.StorageNodesAdd(new double[] { 3, -10 }, new List<int>() { 128 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 3, -14 }, new List<int>() { 128 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 7, -10 }, new List<int>() { 129 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 7, -14 }, new List<int>() { 129 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 11, -10 }, new List<int>() { 130 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 11, -14 }, new List<int>() { 130 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 15, -10 }, new List<int>() { 131 }, false);
            _nodeGrid.StorageNodesAdd(new double[] { 15, -14 }, new List<int>() { 131 }, false);

            // grid extra turning points
            _nodeGrid.NodesAdd(new double[] { -18, -5 }, new List<int>() { 78, 76 }); //index 140
            _nodeGrid.NodesAdd(new double[] { -18, -12 }, new List<int>() { 140, 79 });
            _nodeGrid.NodesAdd(new double[] { -18, 5 }, new List<int>() { 143, 43 });
            _nodeGrid.NodesAdd(new double[] { -18, 12 }, new List<int>() { 40, 44 });
            _nodeGrid.NodesAdd(new double[] { 18, -5 }, new List<int>() { 163, 113, 156 });
            _nodeGrid.NodesAdd(new double[] { 18, -12 }, new List<int>() { 110, 114 });
            _nodeGrid.NodesAdd(new double[] { 18, 5 }, new List<int>() { 164, 8, 160 });
            _nodeGrid.NodesAdd(new double[] { 18, 12 }, new List<int>() { 165, 9 });

            _nodeGrid.NodesAdd(new double[] { -1, 25 }, new List<int>() { 149 }); // import index 148
            _nodeGrid.NodesAdd(new double[] { 1, 25 }, new List<int>() { 4 });
            _nodeGrid.NodesAdd(new double[] { -25, -1 }, new List<int>() { 151 }); // refinery dropoff
            _nodeGrid.NodesAdd(new double[] { -25, 1 }, new List<int>() { 41 }); // refinery pickup
            _nodeGrid.NodesAdd(new double[] { 1, -25 }, new List<int>() { 153 }); // export dropoff
            _nodeGrid.NodesAdd(new double[] { -1, -25 }, new List<int>() { 74 });

            // robot start posistions
            _nodeGrid.NodesAdd(new double[] { 20, -1 }, new List<int>() { 111 }); // index 154
            _nodeGrid.NodesAdd(new double[] { 20, -3 }, new List<int>() { 162 });
            _nodeGrid.NodesAdd(new double[] { 20, -5 }, new List<int>() { 144 });
            _nodeGrid.NodesAdd(new double[] { 20, -7 }, new List<int>() { 163 });
            _nodeGrid.NodesAdd(new double[] { 20, 1 }, new List<int>() { 6 });
            _nodeGrid.NodesAdd(new double[] { 20, 3 }, new List<int>() { 164 });
            _nodeGrid.NodesAdd(new double[] { 20, 5 }, new List<int>() { 146 });
            _nodeGrid.NodesAdd(new double[] { 20, 7 }, new List<int>() { 165 });

            _nodeGrid.NodesAdd(new double[] { 18, -3 }, new List<int>() { 144, 155 }); //index 162
            _nodeGrid.NodesAdd(new double[] { 18, -7 }, new List<int>() { 145, 157 });
            _nodeGrid.NodesAdd(new double[] { 18, 3 }, new List<int>() { 6, 159 }); //index 164
            _nodeGrid.NodesAdd(new double[] { 18, 7 }, new List<int>() { 146, 161 });

            foreach (Node n in nodeGrid.nodes)
            {
                if (n is StorageNode)
                {
                    CreateObject(n.position[0], 0, n.position[1], "Shelf");
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
                    Object3D e = new ExportVehicle(x,y,z, 0, Math.PI, 0);
                    worldObjects.Add(e);
                    return e;
                case "Import":
                    Object3D i = new ImportVehicle(x, y, z, 0, 0.5 * Math.PI, 0);
                    worldObjects.Add(i);
                    return i;
                case "Crate":
                    Object3D c = new Crate(x, y, z);
                    worldObjects.Add(c);
                    return c;
                case "Shelf":
                    Object3D s = new StationaryObject(x, y, z, 0, 0, 0, "Shelf");
                    worldObjects.Add(s);
                    return s;
                case "Refinery":
                    Object3D refi = new Refinery(x, y, z, 0, 0, 0);
                    worldObjects.Add(refi);
                    return refi;
                default:
                    throw new ArgumentException("there is no model that corresponds with that type");
            }
        }
    }
}