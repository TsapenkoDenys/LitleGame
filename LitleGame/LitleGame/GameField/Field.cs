using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Field
    {
        private string[,] MainField = new string[16, 60];
        private int playerX = 27;
        private int playerY = 15;

        private int ballX = 27;
        private int ballY = 14;

        private int ballDX = 1;
        private int ballDY = 1;

       

        //public void DrawFieldAndPlaterMovment()
        //{
        //    string[,] Field = new string[16, 60];



        //    for (int y = 0; y < Field.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < Field.GetLength(1); x++)
        //        {
        //            if (y >= 8)
        //            {
        //                Field[y, x] = " ";
        //            }
        //            else
        //            {
        //                Field[y, x] = "#";
        //            }

        //            if (y == playerY && x == playerX)
        //            {
        //                Field[y, x] = "P";   
        //            }

        //            if (y == ballY && x == ballX)
        //            {
        //                Field[y, x] = "O";
        //            }



        //        }
        //    }

        //    while (true)
        //    {
        //        Console.SetCursorPosition(0, 0);

        //        for (int y = 0; y < Field.GetLength(0); y++)
        //        {
        //            for (int x = 0; x < Field.GetLength(1); x++)
        //            {
        //                Console.Write(Field[y, x]);
        //            }

        //            Console.WriteLine();
        //        }

        //        for (int y = 0; y < Field.GetLength(0); y++)
        //        {
        //            for (int x = 0; x < Field.GetLength(1); x++)
        //            {
        //                if (y == ballY && x == ballX)
        //                {
        //                    Field[y,x] = " ";
        //                    ballX--;
        //                    ballY++;
        //                }
        //            }
        //            Console.WriteLine();

        //        }

        //        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        //        if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 0)
        //        {
        //            Field[playerY, playerX] = " ";

        //            playerX--;

        //            Field[playerY, playerX] = "P";
        //        }
        //        else if (keyInfo.Key == ConsoleKey.RightArrow &&
        //                 playerX < Field.GetLength(1) - 1)
        //        {
        //            Field[playerY, playerX] = " ";

        //            playerX++;

        //            Field[playerY, playerX] = "P";
        //        }
        //    }
        //}

        private void InitializeField()
        {
            for (int y = 0; y < MainField.GetLength(0); y++)
            {
                for (int x = 0; x < MainField.GetLength(1); x++)
                {
                    if (y >= 8)
                        MainField[y, x] = " ";
                    else
                        MainField[y, x] = "#";
                }
            }
        }
        private void UpdateBall()
        {
            Player player = new Player();

            int nextX = ballX + ballDX;
            int nextY = ballY + ballDY;

            if (nextX < 0 || nextX >= MainField.GetLength(1))
            {
                ballDX *= -1;
                nextX = ballX + ballDX;
            }

            if (nextY < 0 || nextY >= MainField.GetLength(0))
            {
                ballDY *= -1;
                nextY = ballY + ballDY;
                //Console.WriteLine("Game Over! Final Points: " + player.Points);
            }

            if (MainField[ballY, nextX] == "#")
            {
                MainField[ballY, nextX] = " ";
                ballDX *= -1;
                nextX = ballX + ballDX;
                //player.Points+=1;
                //Console.WriteLine("Points: ", player.Points);
            }

            if (MainField[nextY, ballX] == "#")
            {
                MainField[nextY, ballX] = " ";
                ballDY *= -1;
                nextY = ballY + ballDY;
            }

            ballX = nextX;
            ballY = nextY;

           
        }

        private void UpdatePlayer(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && playerX > 0)
            {
                playerX--;
            }
            else if (key == ConsoleKey.RightArrow &&
                     playerX < MainField.GetLength(1) - 1)
            {
                playerX++;
            }
        }

        private void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Player player = new Player();
            for (int y = 0; y < MainField.GetLength(0); y++)
            {
                for (int x = 0; x < MainField.GetLength(1); x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write("-");
                    }
                    else if (x == ballX && y == ballY)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(MainField[y, x]);
                    }
                }

                Console.WriteLine();
               
            }
           
        }

        public void DrawFieldAndPlaterMovment()
        {
            Console.CursorVisible = false;

            InitializeField();

            while (true)
            {
                Draw();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                UpdatePlayer(keyInfo.Key);

                UpdateBall();
            }
        }

    }
}
