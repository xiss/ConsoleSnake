namespace Snake
{
    internal class Field
    {
        private static Field instance;
        private int minHeight = 10;
        private int maxHeight = 40;
        private int minWidth = 20;
        private int maxWidth = 80;
        public int Width { get; private set; }
        public int Height { get; private set; }


        public   static Field GetInstance(int height, int width)
        {
            if (instance == null)
                instance = new Field(height, width);
            return instance;
        }

        private Field(int height, int width)
        {
            if (height < minHeight)
                Height = minHeight;
            else if (height > maxHeight)
                Height = maxHeight;
            else
                Height = height;

            if (width < minWidth)
                Width = width;
            else if (width > maxWidth)
                Width = maxWidth;
            else Width = width - (width % 2);
            Height = height;
        }
    }
}
