namespace Snake
{
    public struct Point
    {
        public int Y { get; }
        public int X { get; }
       
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}