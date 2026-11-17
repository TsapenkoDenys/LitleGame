using LitleGame.GameField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.BLL.Models
{
    
    public class MathAndLogicModel
    {
        public Position CallcPositon(Position p, int screenMax)
        {
            Position position = new Position(p);

            if (position.index >= screenMax || position.index <= 0)
                position.IsReverse = !position.IsReverse;

            if (position.IsReverse)
                position.index++;
            else
                position.index--;

            return position;
        }
        
        public void PlayerMovement(GameModel gameModel, Field field)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.LeftArrow && gameModel.xDash.index > 0)
            {
                field.GameField[field.PlayerY, gameModel.xDash.index] = " ";

                gameModel.xDash.index--;

                field.GameField[field.PlayerY, gameModel.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow &&
                     gameModel.xDash.index < field.GameField.GetLength(1) - 1)
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
    }
}
