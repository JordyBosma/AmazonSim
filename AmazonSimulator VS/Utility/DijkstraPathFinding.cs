using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// Object used for storing pathfinding information for finding the shothest path.
    /// </summary>
    public class DijkstraPathFinding
    {
        private double[] dist;
        private List<Node> prev;
        private List<Node> unvisited;
        private NodeGrid nodeGrid;
        private Node currentNode = null;
        private Node endNode = null;

        /// <summary>
        /// Used for setting up the data used for the pathfinding.
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <param name="nodeGrid"></param>
        public DijkstraPathFinding(double[] firstPoint, double[] secondPoint, NodeGrid nodeGrid)
        {
            //making the begin situasions of data used for finding a path:
            int nodesCount = nodeGrid.nodes.Count;
            prev = new List<Node>();
            dist = new double[nodesCount];
            for (int i = 0; i < dist.Count(); i++)
            {
                prev.Add(null);
                dist[i] = double.MaxValue;
            }
            unvisited = new List<Node>();
            foreach (Node node in nodeGrid.nodes)
            {
                unvisited.Add(node);
            }
            this.nodeGrid = nodeGrid;

            //finding the nodes by the begin- and endpoint: 
            for (int i = 0; i < nodesCount; i++)
            {
                if (nodeGrid.nodes[i].position.SequenceEqual(firstPoint))
                {
                    currentNode = unvisited[i];
                    dist[i] = 0;
                }
                if (nodeGrid.nodes[i].position.SequenceEqual(secondPoint))
                {
                    endNode = nodeGrid.nodes[i];
                }
            }
        }

        /// <summary>
        /// Get the shothest path with the data profided in the construcor of the class.
        /// </summary>
        /// <returns></returns>
        public List<double[]> GetPath()
        {
            //Dijkstra alg:
            while (unvisited.Count() != 0)
            {
                Dijkstra();
            }
            //If path found:
            if (prev[endNode.id] != null)
            {
                return GetShortestPath(endNode, prev);
            }
            else
            {
                return new List<double[]>();
            }
        }

        /// <summary>
        /// Stores the data form calculating the distance form the current piont to all connected pointand comparinging to already found distances.
        /// </summary>
        private void Dijkstra()
        {
            double shortest = double.MaxValue;
            foreach (Node node in unvisited)
            {
                if (dist[node.id] < shortest)
                {
                    currentNode = node;
                    shortest = dist[node.id];
                }
            }
            unvisited.Remove(currentNode);
            foreach (int i in currentNode.connections)
            {
                Node nextNode = nodeGrid.nodes[i];
                double newDist = dist[currentNode.id] + Math.Abs(currentNode.position[0] - nextNode.position[0] + currentNode.position[1] - nextNode.position[1]);
                if (newDist < dist[i])
                {
                    prev[i] = currentNode;
                    dist[i] = newDist;
                }
            }
        }

        /// <summary>
        /// Gets the shortest found path out of the data stored after the dijksta function has looped.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="prev"></param>
        /// <returns></returns>
        private List<double[]> GetShortestPath(Node next, List<Node> prev)
        {
            if (prev[next.id] == null)
            {
                return new List<double[]> { nodeGrid.nodes[next.id].position };
            }
            else
            {
                List<double[]> path = GetShortestPath(prev[next.id], prev);
                path.Add(nodeGrid.nodes[next.id].position);
                return path;
            }

        }
    }
}
