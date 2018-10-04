using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

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

        public void StorageNodesAdd(double[] position, List<int> connections, bool importNode)
        {
            nodes.Add(new StorageNode(position, connections, nodes.Count(), importNode));
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

    public class StorageNode : Node, PickUpTarget, DropOffTarget
    {
        private Crate _storedCrate;
        private bool importNode;
        private bool reserved;

        public StorageNode(double[] position, List<int> connections, int id, bool importNode) : base(position, connections, id)
        {
            this.importNode = importNode;
        }

        public void ReserveNode()
        {
            reserved = true;
        }

        public Crate GetCrate()
        {
            Crate returnCrate = this._storedCrate;
            this._storedCrate = null;
            return returnCrate;
        }

        public bool GetReserved()
        {
            return reserved;
        }

        public bool CheckCrate()
        {
            return _storedCrate != null;
        }

        public bool CheckImport()
        {
            return importNode;
        }

        public void HandelPickUp()
        {
            reserved = false;
        }

        public void HandelDropOff(Crate crate)
        {
            this._storedCrate = crate;
            reserved = false;
        }
    }
}
