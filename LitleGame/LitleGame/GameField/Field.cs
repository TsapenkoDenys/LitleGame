using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Field
    {
        public void PrintField()
        {
            string[,] Field = new string[10, 10];

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i, j] = "-";
                }
            }

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Console.Write(Field[i, j]);
                }

                Console.WriteLine();
            }
        }

        public void PlayerMovement()
        {
            string[,] Field = new string[10, 10];
            int playerX = 5;
            int playerY = 0;
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey(true);
            while (true)
            {
                Console.Clear();
                Field[playerY, playerX] = "-";
                for (int i = 0; i < Field.GetLength(0); i++)
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        if (keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            if (playerX > 0)
                            {
                                playerX--;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            if (playerX < Field.GetLength(1) - 1)
                            {
                                playerX++;
                            }
                        }
                    }
                }
                for (int i = 0; i < Field.GetLength(0); i++)
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write(Field[i, j]);
                    }
                    Console.WriteLine();
                }
                keyInfo = Console.ReadKey(true);
            }
        }
    }
}
