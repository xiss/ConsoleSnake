using System;
namespace Snake
{
    internal class Field
    {
        private static Field instance;
        private readonly int minHeight = 10;
        private readonly int maxHeight = 40;
        private readonly int minWidth = 20;
        private readonly int maxWidth = 80;

        public int Width { get; private set; }
        public int Height { get; private set; }
        private Obj[,] field; // никогда не видел массива перечислений на котором бы работала программа. :)
        public Obj this[int y, int x]
        {
            get => field[y, x];
            set => field[y, x] = value;
        }

        // Хорошая попытка реализации паттерна синглтон!
        public static Field GetInstance(int height, int width)
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

            field = new Obj[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x += 2)
                {
                    if (x == 0 || y == 0 || y == Height - 1 || x == Width - 2)
                    {
                        field[y, x] = Obj.Wall;
                    }
                    else
                    {
                        field[y, x] = Obj.Space;
                    }
                }
            }
        }

        // Тут перечисление лишнее. Непонятно по имени для чего оно...
        public enum Obj
        {
            Space,
            Wall,
            Head,
            Tail,
            Food
        }

        public void AddFood()
        {
            Random rnd = new();
            while (true)
            {
                int x = rnd.Next(1, Width - 1);
                x = x - x % 2;
                int y = rnd.Next(1, Height - 1);
                if (field[y, x] == Obj.Space)
                {

                    field[y, x] = Obj.Food;
                    break;
                }
            }

        }
    }
}
