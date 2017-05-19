namespace Assets.Scripts
{
    public class PathNode
    {
        public Point Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public int ApproximatePathLength { get; set; }
        public int ExpectedPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.ApproximatePathLength;
            }
        }

        public PathNode(Point position, PathNode cameFrom, int pathLengthFromStart,  int approximatePathLength)
        {
            Position = position;
            PathLengthFromStart = pathLengthFromStart;
            CameFrom = cameFrom;
            ApproximatePathLength = approximatePathLength;
        }
    }
}
