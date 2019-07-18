namespace RIT.Optimization.KDTree
{
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;

    public class KDTree
    {
        public readonly Node Root;

        public KDTree(Vector3[] points)
        {
            Insert(ref Root, points);
        }

        public void Insert(ref Node root, Vector3[] points)
        {
            root = Node.Insert(points, 0);
        }

        public static Vector3[] FindNearestPositionsWithinSqrRadius(ref Vector3[] points, in Vector3 query, in float sqrSearchRadius)
        {
            List<Node> neighbors = new List<Node>();
            KDTree model = new KDTree(points);
            NearestNeighborsWithinSqrRadius(query, model.Root, sqrSearchRadius, ref neighbors);

            return neighbors.Select(x=>x.Location).ToArray();
        }

        public static Vector3 FindNearestPosition(ref Vector3[] points, in Vector3 query)
        {
            KDTree model = new KDTree(points);
            return NearestNeighbor(query, model.Root).Location;
        }

        static Node NearestNeighbor(in Vector3 query, in Node root, double sqrBestDistance = Mathf.Infinity, Node nearestNode = null)
        {
            Node currentNode = root;
            Stack<Node> visitedNodes = new Stack<Node>();

            while (currentNode != null)
            {
                visitedNodes.Push(currentNode);
                double sqrDistanceToNode = SqrDistance(query, currentNode);
                if (sqrDistanceToNode <= sqrBestDistance)
                {
                    (nearestNode, sqrBestDistance) = (currentNode, sqrDistanceToNode);
                }
                int splitAxis = currentNode.SplitAxis;
                currentNode = query[splitAxis] <= currentNode.Location[splitAxis]
                    ? currentNode.LeftNode
                    : currentNode.RightNode;
            }
            while (visitedNodes.Count > 1)
            {
                Node child = visitedNodes.Peek();
                visitedNodes.Pop();
                Node parent = visitedNodes.Peek();

                if (parent != null)
                {
                    int splitAxis = parent.SplitAxis;
                    if (Mathf.Abs(query[splitAxis] - parent.Location[splitAxis]) <= sqrBestDistance)
                    {
                        nearestNode = NearestNeighbor(query, Sibling(child, parent), sqrBestDistance, nearestNode);
                    }
                }
            }
            return nearestNode;
        }

        static void NearestNeighborsWithinSqrRadius(in Vector3 query, in Node root, in float sqrSearchRadius, ref List<Node> nearestNeighbors)
        {
            Node currentNode = root;
            Stack<Node> visitedNodes = new Stack<Node>();

            while (currentNode != null)
            {
                visitedNodes.Push(currentNode);
                if (SqrDistance(query, currentNode) <= sqrSearchRadius)
                {
                    nearestNeighbors.Add(currentNode);
                }
                int splitAxis = currentNode.SplitAxis;
                currentNode = query[splitAxis] <= currentNode.Location[splitAxis]
                    ? currentNode.LeftNode
                    : currentNode.RightNode;
            }
            while (visitedNodes.Count > 1)
            {
                Node child = visitedNodes.Peek();
                visitedNodes.Pop();
                Node parent = visitedNodes.Peek();

                if (parent != null)
                {
                    int splitAxis = parent.SplitAxis;
                    if (Mathf.Abs(query[splitAxis] - parent.Location[splitAxis]) <= sqrSearchRadius)
                    {
                        NearestNeighborsWithinSqrRadius(query, Sibling(child, parent), sqrSearchRadius, ref nearestNeighbors);
                    }
                }
            }
        }

        static double SqrDistance(in Vector3 query, in Node node)
            => node != null ? (node.Location - query).sqrMagnitude : Mathf.Infinity;

        static Node Sibling(in Node child, in Node parent)
            => ReferenceEquals(parent.LeftNode, child) ? parent.RightNode : parent.LeftNode;
    }

    public class Node
    {
        Vector3[] heldPoints;
        public int SplitAxis    { get; private set; }
        public Vector3 Location { get; private set; }
        public Node LeftNode    { get; private set; }
        public Node RightNode   { get; private set; }

        internal static Node Insert(Vector3[] points, in int currentDepth)
        {
            var node = new Node();
            node.SplitAxis = currentDepth % 3;

            if (points.Length == 0)
            {
                return null;
            }
            else
            {
                points = points.OrderBy(point => point[node.SplitAxis]).ToArray();
                node.heldPoints = points;

                int medianLeft = (points.Length + 1) / 2 - 1;
                node.Location = points[medianLeft];

                (node.LeftNode, node.RightNode) = (Insert(points.Take(medianLeft).ToArray(), currentDepth + 1), Insert(points.Skip(medianLeft+1).ToArray(), currentDepth + 1));
            }
            return node;
        }
    }
}


