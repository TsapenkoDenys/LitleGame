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
    }
}
