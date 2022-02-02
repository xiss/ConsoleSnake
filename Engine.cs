using System;

namespace Snake
{
    internal static class Engine
    {
        private static Field field;
        private static Snake snake;

        static Engine()
        {
            field = Field.GetInstance(40, 80);
            snake = Snake.Instance;
        }

        public static void DrawFrame()
        {
            Console.Clear();
            for (int y = 0; y <= field.Height; y++)
            {
                for (int x = 0; x <= field.Width; x += 2)
                {
                    if (x == 0 || y == 0 || y == field.Height || x == field.Width)
                    {
                        printWall();
                    }
                    else
                    {
                        printSpace();
                    }
                    if (snake.IsSnake(x, y))
                    {
                        printSnake(ref x);
                    }
                }
                Console.WriteLine();
            }
        }

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
        private static void printSnake(ref int x)
        {
            x += 2;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ResetColor();
        }

        public static void Update()
        {
            char key = ' ';
            // snake
            if (Console.KeyAvailable)
                key = Console.ReadKey().KeyChar;


            snake.Update(key);
        }
    }
}

