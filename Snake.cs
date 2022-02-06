using System;
using System.Collections.Generic;

namespace Snake
{
    internal class Snake
    {
        // Уже есть для этого перечисление ConsoleKey и его значния, например UpArrow
        private enum Direction
        {
            up,
            down,
            left,
            right
        }
        private Direction dir;
        private int x { get => snake.First.Value.Item2; }
        private int y { get => snake.First.Value.Item1; }

        // мы кортежи и списки не проходили, поэтому молодцы что пробуете использовать, но немного не в тему...
        private static Snake inst;
        private LinkedList<Tuple<int, int>> snake;
        public static Snake Instance
        {
            get
            {
                if (inst == null)
                    inst = new Snake();
                return inst;
            }
        }

        private Snake()
        {
            snake = new LinkedList<Tuple<int, int>>();
            snake.AddLast(new Tuple<int, int>(10, 10));
            snake.AddLast(new Tuple<int, int>(9, 10));
            snake.AddLast(new Tuple<int, int>(8, 10));
        }

        public bool Update(char key, Field field)
        {
            // update direction
            switch (key)
            {
                case 'a' when dir != Direction.right:
                    dir = Direction.left;
                    break;
                case 'd' when dir != Direction.left:
                    dir = Direction.right;
                    break;
                case 'w' when dir != Direction.down:
                    dir = Direction.up;
                    break;
                case 's' when dir != Direction.up:
                    dir = Direction.down;
                    break;
            }
            //update position
            switch (dir)
            {
                case Direction.up:
                    if (!isFree(field, y - 1, x))
                        return false;
                    snake.AddFirst(new Tuple<int, int>(y - 1, x));
                    break;
                case Direction.down:
                    if (!isFree(field, y + 1, x))
                        return false;
                    snake.AddFirst(new Tuple<int, int>(y + 1, x));
                    break;
                case Direction.left:
                    if (!isFree(field, y, x - 2))
                        return false;
                    snake.AddFirst(new Tuple<int, int>(y, x - 2));
                    break;
                case Direction.right:
                    if (!isFree(field, y, x + 2))
                        return false;
                    snake.AddFirst(new Tuple<int, int>(y, x + 2));
                    break;
            }

            field[snake.Last.Value.Item1, snake.Last.Value.Item2] = Field.Obj.Space; // Глядя сюда можно испугаться)
            field[snake.First.Next.Value.Item1, snake.First.Next.Value.Item2] = Field.Obj.Tail; // Сюда лучше не смотреть))

            snake.RemoveLast();

            field[y, x] = Field.Obj.Head;
            return true;
        }

        // Именование с большой буквы...
        private bool isFree(Field field, int y, int x)
        {
            if (field[y, x] == Field.Obj.Food)
            {
                snake.AddFirst(new Tuple<int, int>(y, x));
                field[y, x] = Field.Obj.Space;
                Engine.AddScore();
                field.AddFood();
            }
            return field[y, x] == Field.Obj.Space;
        }
    }
}
