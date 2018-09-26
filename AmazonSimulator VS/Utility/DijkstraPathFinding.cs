using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class DijkstraPathFinding
    {
        private double[] dist;
        private List<Node> prev;
        private List<Node> unvisited;
        private NodeGrid nodeGrid;
        Node currentNode = null;
        Node endNode = null;

        public DijkstraPathFinding(double[] firstPoint, double[] secondPoint, NodeGrid nodeGrid)
        {
            int nodesCount = nodeGrid.nodes.Count;
            dist = new double[nodesCount];
            for (int i = 0; i < dist.Count(); i++)
            {
                dist[i] = double.MaxValue;
            }
            prev = new List<Node>(nodesCount);
            unvisited = nodeGrid.nodes;
            this.nodeGrid = nodeGrid;               //labda

            //make the begin situasions and find the nodes by the cordienets: 
            for (int i = 0; i < nodesCount; i++)
            {
                if (nodeGrid.nodes[i].position == firstPoint)
                {
                    currentNode = unvisited[i];
                    dist[i] = 0;
                    break;
                }
                if (nodeGrid.nodes[i].position == secondPoint)
                {
                    endNode = nodeGrid.nodes[i];
                }
            }
        }

        //get the path
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
                return null;
            }
        }

        public void Dijkstra()
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
                    prev[i] = nextNode;
                    dist[i] = newDist;
                }
            }
        }

        public List<double[]> GetShortestPath(Node next, List<Node> prev)
        {
            if (prev[next.id] == null)
            {
                return new List<double[]> { next.position };
            }
            else
            {
                List<double[]> path = GetShortestPath(prev[next.id], prev);
                path.Add(prev[next.id].position);
                return path;
            }

        }
    }
}
