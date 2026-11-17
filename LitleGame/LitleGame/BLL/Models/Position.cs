using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    /// <summary>
    /// Class of objects' positions
    /// </summary>
    public class Position
    {
        public Position() { }

        public Position(int index, bool IsReverse)
        {
            this.index = index;
            this.IsReverse = IsReverse;
        }

        public Position(Position p)
        {
            this.index = p.index;
            this.IsReverse = p.IsReverse;
        }

        public int index;
        public bool IsReverse;
        public int x_game_pos;
        public int y_game_pos;
    }
}
