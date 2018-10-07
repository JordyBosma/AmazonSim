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

        //public List<Node> nodes { get { return _nodes; } }

        public NodeGrid()
        {
            nodes = new List<Node>();
        }

        /// <summary>
        /// Add a node to the grid where a robot can move to.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="connections"></param>
        public void NodesAdd(double[] position, List<int> connections)
        {
            nodes.Add(new Node(position, connections, nodes.Count()));
        }

        /// <summary>
        /// Add a storage node to the grid for storing a crate and where a robot can move to.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="connections"></param>
        /// <param name="importNode"></param>
        public void StorageNodesAdd(double[] position, List<int> connections, bool importNode)
        {
            nodes.Add(new StorageNode(position, connections, nodes.Count(), importNode));
        }
    }

    /// <summary>
    /// Object representing a point in the world where a robot can move to.
    /// </summary>
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

    /// <summary>
    /// Object representing a point in the world for storing a crate where a robot can move to.
    /// </summary>
    public class StorageNode : Node, PickUpTarget, DropOffTarget
    {
        private Crate _storedCrate;
        private bool importNode;
        private bool reserved;
        private bool _isDone = false;

        public StorageNode(double[] position, List<int> connections, int id, bool importNode) : base(position, connections, id)
        {
            this.importNode = importNode;
        }

        /// <summary>
        /// Get the crate stored on this storagenode.
        /// </summary>
        /// <returns></returns>
        public Crate GetCrate()
        {
            return _storedCrate;
        }

        /// <summary>
        /// Checks if storage node is reserved for import crates or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckImport()
        {
            return importNode;
        }

        /// <summary>
        /// Check if storage node is reserved.
        /// </summary>
        /// <returns></returns>
        public bool GetReserved()
        {
            return reserved;
        }

        /// <summary>
        /// Reserve sorage node for incoming crate.
        /// </summary>
        public void ReserveNode()
        {
            reserved = true;
        }

        public bool GetIsDone()
        {
            return _isDone;
        }

        public void SetIsDone()
        {
            _isDone = false;
        }

        /// <summary>
        /// Triggered by robot when picking up the crate by PickUpTarget.
        /// </summary>
        public void HandelPickUp()
        {
            reserved = false;
        }

        /// <summary>
        /// Triggered by robot when droping the crate by DropOffTarget.
        /// </summary>
        /// <param name="crate"></param>
        public void HandelDropOff(Crate crate)
        {
            this._storedCrate = crate;
            crate.Move(position[0], 0.39, position[1]);
            _isDone = true;
        }
    }
}
