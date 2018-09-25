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
        public double[] posision;
        public int z;
        public List<Node> connections;

        public Node(double[] posision, List<Node> connections)
        {
            this.posision = posision;
            this.connections = connections;
        }
    }
}
