using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Field
    {
        public void DrawFieldAndPlaterMovment()
        {
            string[,] Field = new string[8, 19];

            int playerX = 14;
            int playerY = 0;

            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (y == playerY && x == playerX)
                    {
                        Field[y, x] = "P";
                    }
                    else
                    {
                        Field[y, x] = "-";
                    }
                }
            }

            while (true)
            {
                Console.Clear();

                for (int y = 0; y < Field.GetLength(0); y++)
                {
                    for (int x = 0; x < Field.GetLength(1); x++)
                    {
                        Console.Write(Field[y, x]);
                    }

                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 0)
                {
                    Field[playerY, playerX] = "-";

                    playerX--;

                    Field[playerY, playerX] = "P";
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow &&
                         playerX < Field.GetLength(1) - 1)
                {
                    Field[playerY, playerX] = "-";

                    playerX++;

                    Field[playerY, playerX] = "P";
                }
            }
        }
    }
}
