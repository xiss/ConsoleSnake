using System;

namespace Snake
{
    // А зачем делать клас статическим? Мы ведь проходили ->  статика, только для вспомогательных классов (Console, Environment...)
    internal static class Engine
    {
        private static Field field;
        private static Snake snake;
        public static int Score { get; private set; } = 0;
        public static int Speed { get; private set; } = 0;

        static Engine()
        {
            Console.CursorVisible = false;
        }

        public static bool Run(int height, int width)
        {
            field = Field.GetInstance(height, width);
            snake = Snake.Instance;
            field.AddFood();
            while (true)
            {
                DrawFrame();

                if (!Update())
                    break;
                if (Score < 300)
                    System.Threading.Thread.Sleep(300 - Speed);
            }
            return false;
        }

        public static void DrawFrame()
        {
            Console.Clear();
            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x += 2)
                {
                    switch (field[y, x])
                    {
                        case Field.Obj.Wall:
                            printWall();
                            continue;
                        case Field.Obj.Head:
                            printHead();
                            continue;
                        case Field.Obj.Tail:
                            printTail();
                            continue;
                        case Field.Obj.Food:
                            printFood();
                            continue;
                        case Field.Obj.Space:
                            printSpace();
                            continue;
                    }
                }
                Console.WriteLine();
            }
        }

        // Методы всегда с большой буквы!
        private static void printWall()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("  ");
            Console.ResetColor();
        }
        private static void printSpace()
        {
            Console.ResetColor();
            Console.Write("  ");
        }

        // Много дублирования кода. Можно же логику в один метод уместить?!
        //private static void PrintColor(ConsoleColor color)
        //{
        //    Console.BackgroundColor = color;
        //    Console.Write("  ");
        //    Console.ResetColor();
        //}

        private static void printHead()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
            Console.ResetColor();
        }
        private static void printTail()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  ");
            Console.ResetColor();
        }
        private static void printFood()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ResetColor();
        }
        public static bool Update()
        {
            ConsoleKeyInfo key = new();
            if (Console.KeyAvailable)
                key = Console.ReadKey();

            // check ESC
            if (key.Key == ConsoleKey.Escape)
                return false;
            // check score
            if (Score >= field.Width * field.Height - 3)
                return false;

            PrintStat();
            return snake.Update(key.KeyChar, field);
        }

        public static void AddScore()
        {
            Score++;
            if (Speed < 300)
                Speed += 10;
        }

        private static void PrintStat()
        {
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Speed: {Speed}");
        }
    }


}

