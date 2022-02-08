using System;
using System.Collections.Generic;

namespace Snake
{
    internal class Snake
    {
        private ConsoleKey direction = ConsoleKey.W;
        private int X { get => snake.First.Value.X; }
        private int Y { get => snake.First.Value.Y; }
        private static Snake inst;
        private int eatenFood = 0;
        private readonly Field field;
        // На мой взгляд связный список как раз то что нужно, мы не перерисовываем всю змею а только голову и хвост, связнный список как раз позволяет удобно это делать
        private readonly LinkedList<Point> snake;
        private Snake() { }
        public static Snake GetInstance(Field field)
        {
            if (inst == null)
                inst = new Snake(field);
            return inst;
        }

        private Snake(Field field)
        {
            this.field = field;
            snake = new LinkedList<Point>();
            snake.AddLast(new Point(10, 10));
            snake.AddLast(new Point(9, 10));
            snake.AddLast(new Point(8, 10));

            DrawSnake();
        }

        public bool Update(ConsoleKey key, Field field)
        {
            // Проверяем куда змея движется, сохраняем направление
            switch (key)
            {
                case ConsoleKey.A when direction != ConsoleKey.D:
                    direction = ConsoleKey.A;
                    break;
                case ConsoleKey.D when direction != ConsoleKey.A:
                    direction = ConsoleKey.D;
                    break;
                case ConsoleKey.W when direction != ConsoleKey.S:
                    direction = ConsoleKey.W;
                    break;
                case ConsoleKey.S when direction != ConsoleKey.W:
                    direction = ConsoleKey.S;
                    break;
            }
            // пробуем сделать шаг на одну клетку
            switch (direction)
            {
                case ConsoleKey.W:
                    if (!TryStep(field, Y - 1, X))
                        return false;
                    DrawNewSegment(Y - 1, X);
                    break;
                case ConsoleKey.S:
                    if (!TryStep(field, Y + 1, X))
                        return false;
                    DrawNewSegment(Y + 1, X);
                    break;
                case ConsoleKey.A:
                    if (!TryStep(field, Y, X - 2))
                        return false;
                    DrawNewSegment(Y, X - 2);
                    break;
                case ConsoleKey.D:
                    if (!TryStep(field, Y, X + 2))
                        return false;
                    DrawNewSegment(Y, X + 2);
                    break;
            }
            return true;
        }

        // Именование с большой буквы...
        private bool TryStep(Field field, int y, int x)
        {
            if (field[y, x] == Field.PixelType.Food)
            {
                eatenFood++;
                field[y, x] = Field.PixelType.Space;
                Game.GetInstance().AddScore();
                field.AddFood();
            }
            return field[y, x] == Field.PixelType.Space;
        }

        private void DrawSnake()
        {
            bool isHead = true;
            foreach (var segment in snake)
            {
                Game.PrintPixel(segment, isHead ? ConsoleColor.Red : ConsoleColor.Gray);
                isHead = false;
            }
        }
        private void DrawNewSegment(int y, int x)
        {
            // Рисуем новый сигмент на поле и добавляем его в змейку
            Game.PrintPixel(snake.First.Value, ConsoleColor.Gray);
            snake.AddFirst(new Point(x, y));
            Game.PrintPixel(snake.First.Value, ConsoleColor.DarkGreen);
            field[y, x] = Field.PixelType.Tail;

            //Удаляем последний сегмент из массива и с поля

            if (eatenFood == 0)
            {
                Game.PrintPixel(snake.Last.Value, ConsoleColor.Black);
                field[snake.Last.Value.Y, snake.Last.Value.X] = Field.PixelType.Space;
                snake.RemoveLast();
            }
            else
            {
                eatenFood--;
            }
        }
    }
}
