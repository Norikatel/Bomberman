using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public static class AStar
    {
        public static List<Point> FindPath(int[,] field, Point start, Point goal)
        {
            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();
            PathNode startNode = new PathNode(start, null, 0, GetApproximatePathLength(start, goal));
            openSet.Add(startNode);
            while (openSet.Count() > 0)
            {
                var currentNode = openSet.OrderBy(node =>
                  node.ExpectedPathLength).First();
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        private static int GetApproximatePathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        private static List<PathNode> GetNeighbours(PathNode pathNode, Point goal, int[,] field)
        {
            var result = new List<PathNode>();

            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                if (field[point.X, point.Y] != 0)
                    continue;
                var neighbourNode = new PathNode(point, pathNode, pathNode.PathLengthFromStart + 1,
                    GetApproximatePathLength(point, goal));
                result.Add(neighbourNode);
            }
            return result;
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
