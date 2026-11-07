using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField.Models
{
    public class MathModel
    {
        FieldModel field = new FieldModel();
        Field MainField = new Field();
        PlayerModel player = new PlayerModel();
        GameModel gm = new GameModel();
        Coordinate coords = new Coordinate();

        public void PlayerMovment(GameModel gm)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.LeftArrow && gm.xDash.index > 0)
            {
                field.GameField[player.playerY, gm.xDash.index] = " ";
                gm.xDash.index--;

                field.GameField[player.playerY, gm.xDash.index] = "-";

            }
            else if (keyInfo.Key == ConsoleKey.RightArrow &&
                     gm.xDash.index < field.GameField.GetLength(1) - 1)
            {
                field.GameField[player.playerY, gm.xDash.index] = " ";
                gm.xDash.index++;
                field.GameField[player.playerY, gm.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }
        }

        public Position CallcPositon(Position p, int screenMax)
        {
            Position position = new Position(p);

            if (position.index >= screenMax || position.index <= 0)
                position.isReverse = !position.isReverse;

            if (position.isReverse)
                position.index++;
            else
                position.index--;

            return position;
        }

        public bool isBrickAt(int x, int y)
        {
            if (y < 0 || y >= 8)
            {
                return false;
            }

            bool alreadyDestroyed = coords.coordibates.Any(c => c.x_ball_cord == x && c.y_ball_cord == y);
            return !alreadyDestroyed;

        }


        public void BallMovment(GameModel gm)
        {

            int nextX = gm.x_ball.isReverse ? gm.x_ball.index + 1 : gm.x_ball.index - 1;
            int nextY = gm.y_ball.isReverse ? gm.y_ball.index + 1 : gm.y_ball.index - 1;

            if (isBrickAt(nextX, gm.y_ball.index))
            {
                gm.x_ball.isReverse = !gm.x_ball.isReverse;
                MainField.DestroyBrickAt(nextX, gm.y_ball.index);
            }

            if (isBrickAt(gm.x_ball.index, nextY))
            {
                gm.y_ball.isReverse = !gm.y_ball.isReverse;
                MainField.DestroyBrickAt(gm.x_ball.index, nextY);
            }


            gm.y_ball = CallcPositon(gm.y_ball, field.GameField.GetLength(0));
            gm.x_ball = CallcPositon(gm.x_ball, field.GameField.GetLength(1));

        }

    }
}
