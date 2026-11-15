using LitleGame.BLL.Models;
using LitleGame.GameField.Models;
using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace LitleGame.GameField
{
    public class Field
    {

        private string[,] _gameField = new string[16, 60];
        private int _playerY = 15;  
        private int Reverse = 0;

        GameModel gm  = new GameModel();
        Coordinate cords = new Coordinate();
        MathAndLogicModel mathAndLogic = new MathAndLogicModel();
        HashSet<Coordinate> coordibates = new HashSet<Coordinate>();
        Field fd =  new Field();

        public string[,] GameField
        {
            get { return _gameField; }
            set { _gameField = value; }
        }

        public int PlayerY
        {
            get { return _playerY; }
            set { _playerY = value; }
        }


        public void DrawMainField(GameModel gm)
        {
            Position position = new Position();

            for (int y_elementArrPos = 0; y_elementArrPos < _gameField.GetLength(0); ++y_elementArrPos)
            {
                for (int x_elementArrPos = 0; x_elementArrPos < _gameField.GetLength(1); ++x_elementArrPos)
                {
                    cords.x_gameElement = x_elementArrPos;
                    cords.y_gameElement = y_elementArrPos;

                    if (y_elementArrPos >= 8)
                    {
                        _gameField[y_elementArrPos, x_elementArrPos] = " ";
                    }
                    else
                    {
                        _gameField[y_elementArrPos, x_elementArrPos] =
                            coordibates.Any(s => s.y_ball_cord == cords.y_gameElement && s.x_ball_cord == cords.x_gameElement) ? " " : "#";
                    }

                    if (y_elementArrPos == _playerY && x_elementArrPos == gm.xDash.index)
                    {
                        _gameField[y_elementArrPos, x_elementArrPos] = "-";
                    }



                    if (y_elementArrPos == gm.y_ball.index && x_elementArrPos == gm.x_ball.index)
                    {
                        _gameField[y_elementArrPos, x_elementArrPos] = "O";

                        coordibates.Add(new Coordinate
                        {
                            x_ball_cord = x_elementArrPos,
                            y_ball_cord = y_elementArrPos
                        });
                    }
                }
            }

            for (int y = 0; y < _gameField.GetLength(0); y++)
            {
                for (int x = 0; x < _gameField.GetLength(1); x++)
                {
                    Console.Write(_gameField[y, x]);
                }
                Console.WriteLine();
            }
        }
        
        private bool isBrickAt(int x, int y)
        {
            if (y < 0 || y >= 8)
            {
                return false;
            }

            bool alreadyDestroyed = coordibates.Any(c => c.x_ball_cord == x && c.y_ball_cord == y);
            return !alreadyDestroyed;

            
        }
        private void DestroyBrickAt(int x, int y)
        {
            coordibates.Add(new Coordinate { x_ball_cord = x, y_ball_cord = y });
        }

        public void BallMovment(GameModel gm)
        {

            int nextX = gm.x_ball.isReverse ? gm.x_ball.index + 1 : gm.x_ball.index - 1;
            int nextY = gm.y_ball.isReverse ? gm.y_ball.index + 1 : gm.y_ball.index - 1;

            if (isBrickAt(nextX, gm.y_ball.index))
            {
                gm.x_ball.isReverse = !gm.x_ball.isReverse;
                DestroyBrickAt(nextX, gm.y_ball.index);
            }

            if (isBrickAt(gm.x_ball.index, nextY))
            {
                gm.y_ball.isReverse = !gm.y_ball.isReverse;
                DestroyBrickAt(gm.x_ball.index, nextY);
            }


            gm.y_ball = mathAndLogic.CallcPositon(gm.y_ball, _gameField.GetLength(0));
            gm.x_ball = mathAndLogic.CallcPositon(gm.x_ball, _gameField.GetLength(1));
            
        }
       
        public void DrawGame()
        {
            while (true)
            {
                DrawMainField(this.gm);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                mathAndLogic.PlayerMovment(this.gm, this.fd);
                BallMovment(this.gm);
                
            }

        }
    }
}
