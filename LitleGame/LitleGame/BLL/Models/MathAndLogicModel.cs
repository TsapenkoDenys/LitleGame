using LitleGame.GameField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.BLL.Models
{
    /// <summary>
    /// Contains the game rules: ball movement, collisions and
    /// player input handling.
    /// </summary>
    public class MathAndLogicModel
    {

        /// <summary>
        /// Calculates the next position along one axis, reversing
        /// the direction at the screen edges.
        /// </summary>
        /// <param name="p">The current position.</param>
        /// <param name="screenMax">The maximum index on the axis.</param>
        /// <returns>The new position.</returns>
        public Position CallcPositon(Position p, int screenMax)
        {
            Position position = new Position(p);

            if ((position.index >= screenMax) || (position.index <= 0))
                position.IsReverse = !position.IsReverse;

            if (position.IsReverse)
                position.index++;
            else
                position.index--;

            return position;
        }
        
        /// <summary>
        /// Reads a key and moves the player's dash accordingly.
        /// </summary>
        /// <param name="gameModel">The current game state.</param>
        /// <param name="field">The field that holds the grid.</param>
        public void PlayerMovement(GameModel gameModel, Field field)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if ((keyInfo.Key == ConsoleKey.LeftArrow)
                && (gameModel.xDash.index > 0))
            {
                field.GameField[field.PlayerY, gameModel.xDash.index] = " ";

                gameModel.xDash.index--;

                field.GameField[field.PlayerY, gameModel.xDash.index] = "-";
            }
            else if ((keyInfo.Key == ConsoleKey.RightArrow)
                     && (gameModel.xDash.index < field.GameField.GetLength(1) - 1))
            {
                field.GameField[field.PlayerY, gameModel.xDash.index] = " ";

                gameModel.xDash.index++;

                field.GameField[field.PlayerY, gameModel.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }
        }

        /// <summary>
        /// Checks whether an intact brick occupies the cell.
        /// </summary>
        /// <param name="field">The field that holds the bricks.</param>
        /// <param name="x">The column index.</param>
        /// <param name="y">The row index.</param>
        /// <returns>True if an intact brick is in the cell.</returns>
        private bool IsBrickAt(Field field, int x, int y)
        {
            if (y < 0 || y >= field.BrickHeight)
            {
                return false;
            }

            if (!field.IsInsideField(x, y))
            {
                return false;
            }

            return !field.IsBrickDestroyed(x, y);
        }

        /// <summary>
        /// Moves the ball one step and handles brick collisions.
        /// </summary>
        /// <param name="gameModel">The current game state.</param>
        /// <param name="field">The field that holds the bricks.</param>
        public void MoveBall(GameModel gameModel, Field field)
        {
            int currentX = gameModel.x_ball.index;
            int currentY = gameModel.y_ball.index;

            int nextX = gameModel.x_ball.IsReverse
                ? currentX + 1
                : currentX - 1;

            int nextY = gameModel.y_ball.IsReverse
                ? currentY + 1
                : currentY - 1;

            if (IsBrickAt(field, nextX, currentY))
            {
                gameModel.x_ball.IsReverse =
                    !gameModel.x_ball.IsReverse;

                field.DestroyBrickAt(nextX, currentY);
            }

            if (IsBrickAt(field, currentX, nextY))
            {
                gameModel.y_ball.IsReverse =
                    !gameModel.y_ball.IsReverse;

                field.DestroyBrickAt(currentX, nextY);
            }

            gameModel.y_ball = CallcPositon(
                gameModel.y_ball,
                field.FieldHeight);

            gameModel.x_ball = CallcPositon(
                gameModel.x_ball,
                field.FieldWidth);
        }
    }
}
