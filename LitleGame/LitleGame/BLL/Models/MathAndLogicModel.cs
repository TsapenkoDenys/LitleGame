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
                position.isReverse = !position.isReverse;

            if (position.isReverse)
                position.index++;
            else
                position.index--;

            return position;
        }
        
        public void PlayerMovment(GameModel gm, Field fd)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.LeftArrow && gm.xDash.index > 0)
            {
                fd.GameField[fd.PlayerY, gm.xDash.index] = " ";

                gm.xDash.index--;

                fd.GameField[fd.PlayerY, gm.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow &&
                     gm.xDash.index < fd.GameField.GetLength(1) - 1)
            {
                fd.GameField[fd.PlayerY, gm.xDash.index] = " ";

                gm.xDash.index++;

                fd.GameField[fd.PlayerY, gm.xDash.index] = "-";
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
    }
}
