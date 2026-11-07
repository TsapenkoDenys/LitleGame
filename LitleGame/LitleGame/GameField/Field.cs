using LitleGame.GameField.Models;
using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace LitleGame.GameField
{
    public class Field
    {

        private string[,] GameField = new string[16, 60];
        private int playerY = 15;  
        private int Reverse = 0;

        GameModel gm  = new GameModel();
        Coordinate cords = new Coordinate();

        HashSet<Coordinate> coordibates = new HashSet<Coordinate>();


        public Position CallcPositon(Position p, int screenMax) {
            Position position = new Position(p);

            if (position.index >= screenMax || position.index <= 0 )
                position.isReverse = !position.isReverse;

            if (position.isReverse ) 
                position.index++; 
            else 
                position.index--;

            return position;
        }

        public void DrawMainField(GameModel gm)
        {
            Position position = new Position();

            for (int y_elementArrPos = 0; y_elementArrPos < GameField.GetLength(0); ++y_elementArrPos)
            {
                for (int x_elementArrPos = 0; x_elementArrPos < GameField.GetLength(1); ++x_elementArrPos)
                {
                    cords.x_gameElement = x_elementArrPos;
                    cords.y_gameElement = y_elementArrPos;
                    
                    if (y_elementArrPos >= 8)
                    {
                        GameField[y_elementArrPos, x_elementArrPos] = " ";
                    }
                    else
                    {
                        if (coordibates.Any(s => s.y_ball_cord == cords.y_gameElement && s.x_ball_cord == cords.x_gameElement))
                        {
                                GameField[y_elementArrPos, x_elementArrPos] = " ";
                        }
                        else
                            GameField[y_elementArrPos, x_elementArrPos] = "#";
                           

                    }
                    if (y_elementArrPos == playerY && x_elementArrPos == gm.xDash.index)
                    {
                        GameField[y_elementArrPos, x_elementArrPos] = "-";
                    }
                    if (y_elementArrPos == gm.y_ball.index && x_elementArrPos == gm.x_ball.index)
                    {
                        GameField[y_elementArrPos, x_elementArrPos] = "O";

                        coordibates.Add(new Coordinate
                        {
                            x_ball_cord = x_elementArrPos,
                            y_ball_cord = y_elementArrPos
                        });
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
                gm.xDash.index--;

                GameField[playerY, gm.xDash.index] = "-";
                
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

        private bool isBrickAt(int x, int y)
        {
            if (y < 0 || y >= 8)
            {
                return false;
            }

            bool alreadyDestroyed = coordibates.Any(c => c.x_ball_cord == x && c.y_ball_cord == y);
            return !alreadyDestroyed;

        }

        private void DestroyBrickAt(int x, int y)
        {
            coordibates.Add(new Coordinate { x_ball_cord = x, y_ball_cord = y });
        }

        public void BallMovment(GameModel gm)
        {

            int nextX = gm.x_ball.isReverse ? gm.x_ball.index + 1 : gm.x_ball.index - 1;
            int nextY = gm.y_ball.isReverse ? gm.y_ball.index + 1 : gm.y_ball.index - 1;

            if (isBrickAt(nextX, gm.y_ball.index))
            {
                gm.x_ball.isReverse = !gm.x_ball.isReverse;
                DestroyBrickAt(nextX, gm.y_ball.index);
            }

            if (isBrickAt(gm.x_ball.index, nextY))
            {
                gm.y_ball.isReverse = !gm.y_ball.isReverse;
                DestroyBrickAt(gm.x_ball.index, nextY);
            }


            gm.y_ball = CallcPositon(gm.y_ball, GameField.GetLength(0));
            gm.x_ball = CallcPositon(gm.x_ball, GameField.GetLength(1));

        }
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
    }
}
