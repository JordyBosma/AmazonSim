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

        public void NodesAdd(double[] position, List<int> connections)
        {
            nodes.Add(new Node(position, connections, nodes.Count()));
        }
        
    }

    public class Node
    {
        public int id;
        public double[] position;
        public List<int> connections;

        public Node(double[] position, List<int> connections, int id)
        {
            this.id = id;
            this.position = position;
            this.connections = connections;
        }
    }

}
