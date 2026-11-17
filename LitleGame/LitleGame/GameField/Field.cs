
using LitleGame.BLL.Models;
using LitleGame.GameField.Models;
using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LitleGame.GameField
{
    public class Field
    {
        private const int _fieldHeight = 16;
        private const int _fieldWidth = 60;
        private const int _brickHeight = 8;
        private const int _initialPlayerY = 15;

        private const string _emptyCell = " ";
        private const string _brickSymbol = "#";
        private const string _playerSymbol = "-";
        private const string _ballSymbol = "O";

        private readonly string[,] _gameField =
            new string[_fieldHeight, _fieldHeight];

        private readonly GameModel _gameModel = new GameModel();

        private readonly MathAndLogicModel _mathAndLogic =
            new MathAndLogicModel();

        private readonly HashSet<(int X, int Y)> _destroyedBricks =
            new HashSet<(int X, int Y)>();

        private int _playerY = _initialPlayerY;

        public string[,] GameField
        {
            get { return _gameField; }
        }

        public int PlayerY
        {
            get { return _playerY; }
            set { _playerY = value; }
        }
         
        public int BrickHeight
        {
            get { return _brickHeight; }
        }

        public void DrawMainField(GameModel gameModel)
        {
            InitializeField();
            DrawPlayer(gameModel);
            DrawBall(gameModel);
            PrintField();
        }

        private void InitializeField()
        {
            for (int y = 0; y < _fieldHeight; y++)
            {
                for (int x = 0; x < _fieldWidth; x++)
                {
                    if (y >= BrickHeight || IsBrickDestroyed(x, y))
                    {
                        _gameField[y, x] = _emptyCell;
                    }
                    else
                    {
                        _gameField[y, x] = _brickSymbol;
                    }
                }
            }
        }

        private void DrawPlayer(GameModel gameModel)
        {
            int playerX = gameModel.xDash.index;

            if (IsInsideField(playerX, _playerY))
            {
                _gameField[_playerY, playerX] = _playerSymbol;
            }
        }

        private void DrawBall(GameModel gameModel)
        {
            int ballX = gameModel.x_ball.index;
            int ballY = gameModel.y_ball.index;

            if (IsInsideField(ballX, ballY))
            {
                _gameField[ballY, ballX] = _ballSymbol;
            }
        }

        private void PrintField()
        {
            for (int y = 0; y < _fieldHeight; y++)
            {
                for (int x = 0; x < _fieldWidth; x++)
                {
                    Console.Write(_gameField[y, x]);
                }

                Console.WriteLine();
            }
        }

        private bool IsInsideField(int x, int y)
        {
            return x >= 0 &&
                   x < _fieldWidth &&
                   y >= 0 &&
                   y < _fieldHeight;
        }

       

        private bool IsBrickDestroyed(int x, int y)
        {
            return _destroyedBricks.Contains((x, y));
        }

        private void DestroyBrickAt(int x, int y)
        {
            _destroyedBricks.Add((x, y));
        }

        public void MoveBall(GameModel gameModel)
        {
            int currentX = gameModel.x_ball.index;
            int currentY = gameModel.y_ball.index;

            int nextX = gameModel.x_ball.IsReverse
                ? currentX + 1
                : currentX - 1;

            int nextY = gameModel.y_ball.IsReverse
                ? currentY + 1
                : currentY - 1;

            if (IsBrickAt(nextX, currentY))
            {
                gameModel.x_ball.IsReverse =
                    !gameModel.x_ball.IsReverse;

                DestroyBrickAt(nextX, currentY);
            }

            if (IsBrickAt(currentX, nextY))
            {
                gameModel.y_ball.IsReverse =
                    !gameModel.y_ball.IsReverse;

                DestroyBrickAt(currentX, nextY);
            }

            gameModel.y_ball = _mathAndLogic.CallcPositon(
                gameModel.y_ball,
                _fieldHeight);

            gameModel.x_ball = _mathAndLogic.CallcPositon(
                gameModel.x_ball,
                _fieldWidth);
        }

        public void DrawGame()
        {
            Console.CursorVisible = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                DrawMainField(_gameModel);

                _mathAndLogic.PlayerMovement(_gameModel, this);

                MoveBall(_gameModel);
            }
        }
    }
}
