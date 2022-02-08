using System;

namespace Snake
{
    // А зачем делать клас статическим? Мы ведь проходили ->  статика, только для вспомогательных классов (Console, Environment...)
    internal class Game
    {
        private static Game instance;
        private readonly Field field;
        private readonly Snake snake;
        private readonly int startSpeed = 500;
        public int Score { get; private set; } = 0;
        public int Speed { get; private set; } = 0;
        private Game() { }
        private Game(int height, int width)
        {
            Console.Title = "Snake 1.1";
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            field = Field.GetInstance(height, width);
            snake = Snake.GetInstance(field);
        }
        public static Game GetInstance(int height = 10, int width = 20)
        {
            if (instance == null)
            {
                instance = new Game(height, width);
                return instance;
            }
            return instance;
        }

        public bool Run()
        {
            while (true)
            {
                //если update возвращает false значит игра окончена
                if (!Update())
                    break;
                if (Score < startSpeed)
                    System.Threading.Thread.Sleep(startSpeed - Speed);
            }
            PrintStat();
            return false;
        }

        // Много дублирования кода. Можно же логику в один метод уместить?!
        // Это нормально что метод статический? Иначе везде нужно экземпляр Engine протаскивать
        public static void PrintPixel(Point point, ConsoleColor color)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.BackgroundColor = color;
            Console.Write("  ");
            Console.ResetColor();
        }

        public bool Update()
        {
            ConsoleKeyInfo key = new();
            if (Console.KeyAvailable)
                key = Console.ReadKey(true);

            // check ESC
            if (key.Key == ConsoleKey.Escape)
                return false;
            // check max score
            if (Score >= field.Width * field.Height - 10)
                return false;

            return snake.Update(key.Key, field);
        }

        public void AddScore()
        {
            Score++;
            if (Speed < startSpeed)
                Speed += 10;
        }

        private void PrintStat()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER!");
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Speed: {Speed}");
        }
    }
}

