using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public class DijkstraPathFinding
    {
        //get the path
        public static List<double[]> GetPath(double[] firstPoint, double[] secondPoint, NodeGrid nodeGrid)
        {
            //Initialization:
            int nodesCount = nodeGrid.nodes.Count;
            double[] dist = new double[nodesCount];
            for (int i = 0; i < dist.Count(); i++)
            {
                dist[i] = double.MaxValue;
            }
            List<Node> prev = new List<Node>(nodesCount);
            //int current = 0; //begin punt
            Node currentNode = null;
            Node endNode = null;
            //int[] visited = new int[nodesCount]; // 0 = default, not visited, 1 = visited
            List<Node> unvisited = nodeGrid.nodes;

            //Begin situasion: 
            for (int i = 0; i < nodesCount; i++)
            {
                if (nodeGrid.nodes[i].position == firstPoint)
                {
                    //current = i;
                    currentNode = unvisited[i];
                    dist[i] = 0;
                    break;
                }
                if (nodeGrid.nodes[i].position == secondPoint)
                {
                    endNode = nodeGrid.nodes[i];
                }
            }

            //Dijkstra alg:
            while (unvisited.Count() != 0)
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

            //Get shortestpath:
            if (prev[endNode.id] != null)
            {
                return ShortestPath(endNode, prev);
            }
            else
            {
                return null;
            }
        }

        public static List<double[]> ShortestPath(Node next, List<Node> prev)
        {
            if (prev[next.id] == null)
            {
                return new List<double[]> { next.position };
            }
            else
            {
                List<double[]> path = ShortestPath(prev[next.id], prev);
                path.Add(prev[next.id].position);
                return path;
            }

        }
    }
}
