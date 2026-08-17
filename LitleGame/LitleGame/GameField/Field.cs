using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Field
    {

        private string[,] GameField = new string[16, 60];
        private int playerX = 27;
        private int playerY = 15;
        private int ballX = 27;
        private int ballY = 14;
       

        public void DrawMainField()
        {
            for(int y = 0; y<GameField.GetLength(0); ++y)
            {
                for (int x = 0; x < GameField.GetLength(1); ++x)
                {
                    if (y >= 8)
                    {
                        GameField[y, x] = " ";

                    }
                    else
                    {
                        GameField[y, x] = "#";
                    }
                    if(y == playerY && x == playerX)
                    {
                        GameField[y, x] = "-";
                    }
                    if(y == ballY && x == ballX)
                    {
                        GameField[y, x] = "O";
                    }
                }
            }

            for (int y = 0; y < GameField.GetLength(0); y++)
            {
                for (int x = 0; x < GameField.GetLength(1); x++)
                {
                    Console.Write(GameField[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void PlayerMovment()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 0)
            {
                GameField[playerY, playerX] = " ";
                //GameField[ballY, ballX] = " ";
                playerX--;

                //ballY--;
                //ballX++;
                GameField[playerY, playerX] = "-";
                
                //GameField[ballY, ballX] = "O";
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow &&
                     playerX < GameField.GetLength(1) - 1)
            {
                GameField[playerY, playerX] = " ";
                playerX++;
                GameField[playerY, playerX] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

        }

        public void BallMovment()
        {
            for (int y = 0; y < GameField.GetLength(0); y++)
            {
                for (int x = 0; x < GameField.GetLength(1); x++)
                {
                    GameField[ballY, ballX] = " ";
                    ballY--;
                    ballX++;
                    GameField[ballY, ballX] = "O";

                    if (ballY >= GameField.GetLength(1))
                    {
                        ballY--;
                        ballX++;
                    }

                }
            }
        }

        //public void BallMovment()
        //{
        //    for (int y = 0; y < GameField.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < GameField.GetLength(1); x++)
        //        {
        //            if (y == ballY && x == ballX)
        //            {
        //                GameField[y, x] = " ";
        //                ballY++;
        //            }

        //            if(ballY >= GameField.GetLength(0))
        //            {
        //                ballY *= -1;
        //            }
        //            else if (ballY == playerY && ballX == playerX)
        //            {
        //                ballX = playerY;
        //                ballY++;
        //            }
        //            else if(ballX >= GameField.GetLength(1))
        //            {
        //                ballX *= -1;
        //            }

        //            GameField[ballY, ballX] = "O";
        //        }
        //    }
        //}

        public void DrawGame()
        {
            while (true)
            {
                DrawMainField();
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                PlayerMovment();
                //BallMovment();
                
            }

        }

        //public void DrawFieldAndPlaterMovment()
        //{
        //    for (int y = 0; y < GameField.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < GameField.GetLength(1); x++)
        //        {
        //            if (y >= 8)
        //            {
        //                GameField[y, x] = " ";
        //            }
        //            else
        //            {
        //                GameField[y, x] = "#";
        //            }

        //            if (y == playerY && x == playerX)
        //            {
        //                GameField[y, x] = "-";
        //            }
        //        }
        //    }

        //    while (true)
        //    {
        //        Console.SetCursorPosition(0, 0);
        //        Console.CursorVisible = false;

        //        for (int y = 0; y < GameField.GetLength(0); y++)
        //        {
        //            for (int x = 0; x < GameField.GetLength(1); x++)
        //            {
        //                Console.Write(GameField[y, x]);
        //            }

        //            Console.WriteLine();
        //        }

        //        for (int y = 0; y < GameField.GetLength(0); y++)
        //        {
        //            for (int x = 0; x < GameField.GetLength(1); x++)
        //            {
        //                if (y == ballY && x == ballX)
        //                {
        //                    GameField[y, x] = " ";
        //                    ballY++;
        //                }
        //            }
        //            Console.WriteLine();

        //        }

        //        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        //        if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 0)
        //        {
        //            GameField[playerY, playerX] = " ";

        //            playerX--;

        //            GameField[playerY, playerX] = "-";
        //        }
        //        else if (keyInfo.Key == ConsoleKey.RightArrow &&
        //                 playerX < GameField.GetLength(1) - 1)
        //        {
        //            GameField[playerY, playerX] = " ";

        //            playerX++;

        //            GameField[playerY, playerX] = "-";
        //        }
        //    }
        //}



    }
}
