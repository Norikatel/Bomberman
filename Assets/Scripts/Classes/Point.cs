namespace Assets.Scripts
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(Point a, Point b) {
            return ((a.X == b.X) && (a.Y == b.Y));
        }

        public static bool operator !=(Point a, Point b)
        {
            return ((a.X != b.X) || (a.Y != b.Y));
        }      
    }
}