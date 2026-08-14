using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitleGame.GameField
{
    public class Field
    {
      
        public void DrawFieldAndPlaterMovment()
        {
            string[,] Field = new string[16, 60];
            int playerX = 27;
            int playerY = 15;
            int ballX = 27;
            int ballY = 14;


            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (y >= 8)
                    {
                        Field[y, x] = " ";
                    }
                    else
                    {
                        Field[y, x] = "#";
                    }

                    if (y == playerY && x == playerX)
                    {
                        Field[y, x] = "P";
                    }

                    if (y == ballY && x == ballX)
                    {
                        Field[y, x] = "O";
                    }



                }
            }

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                for (int y = 0; y < Field.GetLength(0); y++)
                {
                    for (int x = 0; x < Field.GetLength(1); x++)
                    {
                        Console.Write(Field[y, x]);
                    }

                    Console.WriteLine();
                }

                for (int y = 0; y < Field.GetLength(0); y++)
                {
                    for (int x = 0; x < Field.GetLength(1); x++)
                    {
                        if (y == ballY && x == ballX)
                        {
                            Field[y, x] = " ";
                            ballX--;
                            ballY++;
                        }
                    }
                    Console.WriteLine();

                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 0)
                {
                    Field[playerY, playerX] = " ";

                    playerX--;

                    Field[playerY, playerX] = "P";
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow &&
                         playerX < Field.GetLength(1) - 1)
                {
                    Field[playerY, playerX] = " ";

                    playerX++;

                    Field[playerY, playerX] = "P";
                }
            }
        }



    }
}
