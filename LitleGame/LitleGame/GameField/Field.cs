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
        MathModel math_operations = new MathModel();
        PlayerModel player_models = new PlayerModel();
        FieldModel field = new FieldModel();
        GameModel gm  = new GameModel();
        Coordinate cords = new Coordinate();

        HashSet<Coordinate> coordibates = new HashSet<Coordinate>();

        public void DrawMainField(GameModel gm)
        {
            Position position = new Position();

            for (int y_elementArrPos = 0; y_elementArrPos <  field.GameField.GetLength(0); ++y_elementArrPos)
            {
                for (int x_elementArrPos = 0; x_elementArrPos < field.GameField.GetLength(1); ++x_elementArrPos)
                {
                    cords.x_gameElement = x_elementArrPos;
                    cords.y_gameElement = y_elementArrPos;
                    
                    if (y_elementArrPos >= 8)
                    {
                        field.GameField[y_elementArrPos, x_elementArrPos] = " ";
                    }
                    else
                    {
                        if (coordibates.Any(s => s.y_ball_cord == cords.y_gameElement && s.x_ball_cord == cords.x_gameElement))
                        {
                                field.GameField[y_elementArrPos, x_elementArrPos] = " ";
                        }
                        else
                            field.GameField[y_elementArrPos, x_elementArrPos] = "#";
                           

                    }
                    if (y_elementArrPos == player_models.playerY && x_elementArrPos == gm.xDash.index)
                    {
                        field.GameField[y_elementArrPos, x_elementArrPos] = "-";
                    }
                    if (y_elementArrPos == gm.y_ball.index && x_elementArrPos == gm.x_ball.index)
                    {
                        field.GameField[y_elementArrPos, x_elementArrPos] = "O";

                        coordibates.Add(new Coordinate
                        {
                            x_ball_cord = x_elementArrPos,
                            y_ball_cord = y_elementArrPos
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

            if (math_operations.isBrickAt(nextX, gm.y_ball.index))
            {
                gm.x_ball.isReverse = !gm.x_ball.isReverse;
                DestroyBrickAt(nextX, gm.y_ball.index);
            }

            if (math_operations.isBrickAt(gm.x_ball.index, nextY))
            {
                gm.y_ball.isReverse = !gm.y_ball.isReverse;
                DestroyBrickAt(gm.x_ball.index, nextY);
            }


            gm.y_ball = math_operations.CallcPositon(gm.y_ball, field.GameField.GetLength(0));
            gm.x_ball = math_operations.CallcPositon(gm.x_ball, field.GameField.GetLength(1));

        }
        public void DrawGame()
        {
            while (true)
            {
                DrawMainField(this.gm);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                math_operations.PlayerMovment(this.gm);
                BallMovment(this.gm);
                
            }

        }
    }
}
