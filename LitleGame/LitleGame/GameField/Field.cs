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
        private int Reverse = 0;


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
            //for (int y = 0; y < GameField.GetLength(0); y++)
            //{
            //    for (int x = 0; x < GameField.GetLength(1); x++)
            //    {

            //        if(ballY < GameField.GetLength(0) && ballX > GameField.GetLength(1))
            //        {
            //            GameField[ballY, ballX] = " ";
            //            ballY++;
            //            ballX--;
            //            GameField[ballY, ballX] = "O";
            //        }
            //    }
            //}

            
            if(ballY != 0 && Reverse != 1)
            {
                MovingBallUp();
            }
            else
            {
                Reverse = 1;
                ballX += 2;
                MovingBallDown();
                
            }
           
            

         

            //if(ballY < 1)
            //{
            //    ballY++;
            //}

        }

        public void MovingBallDown()
        {
            GameField[ballY, ballX] = " ";

            if (ballY >= 15)
            {
                Reverse = 0;
                ballX = 56;
                MovingBallUp();
                return;
            }

            ballY++;
            ballX--;

            GameField[ballY, ballX] = "O";

            if (ballX < 0)
            {
                Reverse = 1;
            }
        }


        public void MovingBallUp()
        {
            GameField[ballY, ballX] = " ";

            if (ballX >= 59)
            {
                Reverse = 0;
                ballY += 2;
                MovingBallDown();

                return;
            }

            ballY--;
            ballX++;

            GameField[ballY, ballX] = "O";
        }


        public void MovingBallUppLeft()
        {
            GameField[ballY, ballX] = " ";

            if (ballY > 0)
                ballY--;

            if (ballX > 0)
                ballX--;

            GameField[ballY, ballX] = "O";
        }

        public void MovingBallDownLeft()
        {
            GameField[ballY, ballX] = " ";

            if (ballY < GameField.GetLength(0) - 1)
                ballY++;

            if (ballX > 0)
                ballX--;

            GameField[ballY, ballX] = "O";
        }


        public void DrawGame()
        {
            while (true)
            {
                DrawMainField();
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                PlayerMovment();
                BallMovment();
                
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
