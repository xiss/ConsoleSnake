using System;

namespace Snake
{
    internal class Snake
    {
        private enum direction
        {
            up,
            down,
            left,
            right
        }
        private direction dir;
        private int x;
        private int y;
        private static Snake inst;
        public static Snake Instance
        {
            get
            {
                if (inst == null)
                    inst = new Snake();
                return inst;
            }
        }

        public bool IsSnake(int x, int y)
        {
            return this.x == x && this.y == y;
        }

        private Snake()
        {
            x = 20;
            y = 10;
        }

        public void Update(char key)
        {
            switch (key)
            {
                case 'a':
                    dir = direction.left;
                    break;
                case 'd':
                    dir = direction.right;
                    break;
                case 'w':
                    dir = direction.up;
                    break;
                case 's':
                    dir = direction.down;
                    break;
            }

            switch (dir)
            {
                case direction.up:
                    y--;
                    break;
                case direction.down:
                    y++;
                    break;
                case direction.left:
                    x -= 2;
                    break;
                case direction.right:
                    x += 2;
                    break;
            }
        }

    }
}
