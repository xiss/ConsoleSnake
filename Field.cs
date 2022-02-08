using System;
namespace Snake
{
    internal class Field
    {
        private static Field instance;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private readonly PixelType[,] field; // никогда не видел массива перечислений на котором бы работала программа. :)
        // А как иначе? Можно сделать массив символов, но мне кажется это хуже. Как узнать цвет фона в конкретном месте я незнаю.

        public PixelType this[int y, int x]
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
        private Field() { }// Запрещаем создавать поле напрямую
        private Field(int height, int width)
        {
            Width = width;
            Height = height;
            field = new PixelType[height, width];
            // Рисуем поле и создаем массив с этим полем
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x += 2)
                {
                    if (x == 0 || y == 0 || y == height - 1 || x == width - 2)
                    {
                        field[y, x] = PixelType.Wall;
                        Game.PrintPixel(new Point(x, y), ConsoleColor.White);
                    }
                    else
                    {
                        field[y, x] = PixelType.Space;
                        continue;
                    }
                }
            }
            AddFood(6);
        }

        // Тут перечисление лишнее. Непонятно по имени для чего оно...
        /// <summary>
        /// Тип пикселя для поля
        /// </summary>
        public enum PixelType
        {
            Space,
            Wall,
            Head,
            Tail,
            Food
        }

        public void AddFood(int foodToAdd = 1)
        {
            Random rnd = new();
            while (foodToAdd !=0)
            {
                // Выбираем случайную позицию для еды
                int x = rnd.Next(1, Width - 1);
                x -= x % 2;
                int y = rnd.Next(1, Height - 1);
                // Если попали в змею или стенку - пересоздаем
                if (field[y, x] == PixelType.Space)
                {
                    field[y, x] = PixelType.Food;
                    Game.PrintPixel(new Point(x, y), ConsoleColor.Red);
                    foodToAdd--;
                }
            }
        }
    }
}
