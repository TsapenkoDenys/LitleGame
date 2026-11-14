using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Position
    {
        public Position() { }

        public Position(int index, bool isReverse)
        {
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
        public int x_game_pos;
        public int y_game_pos;
    }
}
