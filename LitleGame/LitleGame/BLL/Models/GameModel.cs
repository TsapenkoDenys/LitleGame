using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    /// <summary>
    /// Main game model class of positions
    /// </summary>
    public class GameModel
    {

        public Position x_ball = new Position(27, true);
        public Position y_ball = new Position(14, false);


        public Position xDash = new Position(27, false);

    }
}
