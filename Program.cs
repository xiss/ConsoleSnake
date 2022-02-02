using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = Field.GetInstance(20, 40);
            var snake = Snake.Instance;
            Console.CursorVisible = false;

            

            while (true)
            {
                Engine.DrawFrame();
                Engine.Update();

                

                //System.Threading.Thread.Sleep(50);

            }
        }   

    }
}
