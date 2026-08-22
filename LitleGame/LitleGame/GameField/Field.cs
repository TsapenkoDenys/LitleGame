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
    public class Position {
        public Position() { }

        public Position(int index, bool isReverse) {
            this.index = index;
            this.isReverse = isReverse;
        }

        public Position(Position p)
        { 
            this.index = p.index;
            this.isReverse = p.isReverse;
        }

        public int index;
        public bool isReverse;
    }

    public class GameModel {

        public Position x = new Position(27, true);
        public Position y = new Position(14, false);

        public Position xDash = new Position(27, false);

    }

    public class Coordinate {
        public int x;
        public int y;
    }

    public class Field
    {

        private string[,] GameField = new string[16, 60];
        private int playerY = 15;  
        private int Reverse = 0;

        GameModel gm  = new GameModel();

        HashSet<Coordinate> coordibates = new HashSet<Coordinate>();

        public Position CallcPositon(Position p,  int screenMax) {
            Position position = new Position(p); 

            if(position.index >= screenMax || position.index <= 0)
                position.isReverse = !position.isReverse;

            if (position.isReverse) 
                position.index++; 
            else 
                position.index--;
            

            return position;
        }

        public void DrawMainField(GameModel gm)
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
                        if(coordibates.Any(s=>s.y == y && s.x == x))
                            GameField[y, x] = " ";
                        else
                            GameField[y, x] = "#";
                    }
                    if(y == playerY && x == gm.xDash.index)
                    {
                        GameField[y, x] = "-";
                    }
                    if(y == gm.y.index && x == gm.x.index)
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

        public void PlayerMovment(GameModel gm)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.LeftArrow && gm.xDash.index > 0)
            {
                GameField[playerY, gm.xDash.index] = " ";
                //GameField[ballY, ballX] = " ";
                gm.xDash.index--;

                //ballY--;
                //ballX++;
                GameField[playerY, gm.xDash.index] = "-";
                
                //GameField[ballY, ballX] = "O";
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow &&
                     gm.xDash.index < GameField.GetLength(1) - 1)
            {
                GameField[playerY, gm.xDash.index] = " ";
                gm.xDash.index++;
                GameField[playerY, gm.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

        }

        public void BallMovment(GameModel gm)
        {
            gm.y = CallcPositon(gm.y, GameField.GetLength(0));
            gm.x = CallcPositon(gm.x, GameField.GetLength(1));
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


            //if(ballY != 0 && Reverse != 1)
            //{
            //    MovingBallUp();
            //}
            //else
            //{
            //    Reverse = 1;
            //    ballX += 2;
            //    MovingBallDown();

            //}





            //if(ballY < 1)
            //{
            //    ballY++;
            //}

        }

        //public void MovingBallDown()
        //{
        //    GameField[ballY, ballX] = " ";

        //    if (ballY >= 15)
        //    {
        //        Reverse = 0;
        //        ballX = 56;
        //        MovingBallUp();
        //        return;
        //    }

        //    ballY++;
        //    ballX--;

        //    GameField[ballY, ballX] = "O";

        //    if (ballX < 0)
        //    {
        //        Reverse = 1;
        //    }
        //}


        //public void MovingBallUp()
        //{
        //    GameField[ballY, ballX] = " ";

        //    if (ballX >= 59)
        //    {
        //        Reverse = 0;
        //        ballY += 2;
        //        MovingBallDown();

        //        return;
        //    }

        //    ballY--;
        //    ballX++;

        //    GameField[ballY, ballX] = "O";
        //}


        //public void MovingBallUppLeft()
        //{
        //    GameField[ballY, ballX] = " ";

        //    if (ballY > 0)
        //        ballY--;

        //    if (ballX > 0)
        //        ballX--;

        //    GameField[ballY, ballX] = "O";
        //}

        //public void MovingBallDownLeft()
        //{
        //    GameField[ballY, ballX] = " ";

        //    if (ballY < GameField.GetLength(0) - 1)
        //        ballY++;

        //    if (ballX > 0)
        //        ballX--;

        //    GameField[ballY, ballX] = "O";
        //}


        public void DrawGame()
        {
            while (true)
            {
                DrawMainField(this.gm);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                PlayerMovment(this.gm);
                BallMovment(this.gm);
                
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
