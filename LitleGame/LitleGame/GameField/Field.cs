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
        MathModel mathOperations = new MathModel();
        PlayerModel player_models = new PlayerModel();
        FieldModel field = new FieldModel();
        GameModel gm  = new GameModel();
        Coordinate cords = new Coordinate();

        HashSet<Coordinate> coordibates = new HashSet<Coordinate>();

        public void DrawMainField(GameModel gm)
        {
            Position position = new Position();

            for (int yElementArrPos = 0; yElementArrPos < field.GameField.GetLength(0); ++yElementArrPos)
            {
                for (int xElementArrPos = 0; xElementArrPos < field.GameField.GetLength(1); ++xElementArrPos)
                {
                    cords.x_gameElement = xElementArrPos;
                    cords.y_gameElement = yElementArrPos;

                    field.GameField[yElementArrPos, xElementArrPos] = yElementArrPos >= 8 ? " " : "#";

                    if (coordibates.Any(s => s.y_ball_cord == cords.y_gameElement && s.x_ball_cord == cords.x_gameElement))
                    {
                        field.GameField[yElementArrPos, xElementArrPos] = " ";
                    }else
                        field.GameField[yElementArrPos, xElementArrPos] = "#";

                    field.GameField[yElementArrPos, xElementArrPos] = yElementArrPos == player_models.playerY 
                        && xElementArrPos == gm.xDash.index ? "-" : " ";

                    if (yElementArrPos == gm.y_ball.index && xElementArrPos == gm.x_ball.index)
                    {
                        field.GameField[yElementArrPos, xElementArrPos] = "O";

                        coordibates.Add(new Coordinate
                        {
                            x_ball_cord = xElementArrPos,
                            y_ball_cord = yElementArrPos
                        });
                    }
                }
            }

            for (int y = 0; y < field.GameField.GetLength(0); y++)
            {
                for (int x = 0; x < field.GameField.GetLength(1); x++)
                {
                    Console.Write(field.GameField[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void DestroyBrickAt(int x, int y)
        {
            coordibates.Add(new Coordinate { x_ball_cord = x, y_ball_cord = y });
        }

        public void BallMovment(GameModel gm)
        {

            int nextX = gm.x_ball.isReverse ? gm.x_ball.index + 1 : gm.x_ball.index - 1;
            int nextY = gm.y_ball.isReverse ? gm.y_ball.index + 1 : gm.y_ball.index - 1;

            if (mathOperations.isBrickAt(nextX, gm.y_ball.index))
            {
                gm.x_ball.isReverse = !gm.x_ball.isReverse;
                DestroyBrickAt(nextX, gm.y_ball.index);
            }

            if (mathOperations.isBrickAt(gm.x_ball.index, nextY))
            {
                gm.y_ball.isReverse = !gm.y_ball.isReverse;
                DestroyBrickAt(gm.x_ball.index, nextY);
            }


            gm.y_ball = mathOperations.CallcPositon(gm.y_ball, field.GameField.GetLength(0));
            gm.x_ball = mathOperations.CallcPositon(gm.x_ball, field.GameField.GetLength(1));

        }
        public void DrawGame()
        {
            while (true)
            {
                DrawMainField(this.gm);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                mathOperations.PlayerMovment(this.gm);
                BallMovment(this.gm);
                
            }

        }
    }
}
