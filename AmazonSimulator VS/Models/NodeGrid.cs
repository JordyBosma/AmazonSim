using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class NodeGrid
    {
        public List<Node> nodes;

        public NodeGrid()
        {
            nodes = new List<Node>();
        }
    }

    public class Node
    {
        public int x;
        public int z;
        public List<Node> connections;

        public Node(int x, int z, List<Node> connections)
        {
            this.x = x;
            this.z = z;
            this.connections = connections;
        }
    }
}
