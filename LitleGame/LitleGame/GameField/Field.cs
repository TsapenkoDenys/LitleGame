
using LitleGame.BLL.Models;
using LitleGame.GameField.Models;
using LitleGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LitleGame.GameField
{
    /// <summary>
    /// Holds the game field state and renders it to the console.
    /// </summary>
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
            new string[_fieldHeight, _fieldWidth];

        private readonly GameModel _gameModel = new GameModel();

        private readonly MathAndLogicModel _mathAndLogic =
            new MathAndLogicModel();

        private readonly HashSet<(int X, int Y)> _destroyedBricks =
            new HashSet<(int X, int Y)>();

        private int _playerY = _initialPlayerY;

        /// <summary>
        /// Gets the grid of cell symbols.
        /// </summary>
        public string[,] GameField
        {
            get { return _gameField; }
        }

        /// <summary>
        /// Gets or sets the row of the player's dash.
        /// </summary>
        public int PlayerY
        {
            get { return _playerY; }
            set { _playerY = value; }
        }

        /// <summary>
        /// Gets the number of rows occupied by bricks.
        /// </summary>
        public int BrickHeight
        {
            get { return _brickHeight; }
        }

        /// <summary>
        /// Gets the field height in cells.
        /// </summary>
        public int FieldHeight
        {
            get { return _fieldHeight; }
        }

        /// <summary>
        /// Gets the field width in cells.
        /// </summary>
        public int FieldWidth
        {
            get { return _fieldWidth; }
        }

        /// <summary>
        /// Rebuilds the grid from the game model and prints it.
        /// </summary>
        /// <param name="gameModel">The current game state.</param>
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
                    if ((y >= BrickHeight) || IsBrickDestroyed(x, y))
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

        /// <summary>
        /// Checks whether the cell lies within the field bounds.
        /// </summary>
        /// <param name="x">The column index.</param>
        /// <param name="y">The row index.</param>
        /// <returns>True if the cell is inside the field.</returns>
        public bool IsInsideField(int x, int y)
        {
            return (x >= 0)
                   && (x < _fieldWidth)
                   && (y >= 0)
                   && (y < _fieldHeight);
        }

        /// <summary>
        /// Checks whether the brick in the cell was destroyed.
        /// </summary>
        /// <param name="x">The column index.</param>
        /// <param name="y">The row index.</param>
        /// <returns>True if the brick was destroyed.</returns>
        public bool IsBrickDestroyed(int x, int y)
        {
            return _destroyedBricks.Contains((x, y));
        }

        /// <summary>
        /// Marks the brick in the cell as destroyed.
        /// </summary>
        /// <param name="x">The column index.</param>
        /// <param name="y">The row index.</param>
        public void DestroyBrickAt(int x, int y)
        {
            _destroyedBricks.Add((x, y));
        }

        /// <summary>
        /// Runs the main game loop.
        /// </summary>
        public void DrawGame()
        {
            Console.CursorVisible = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                DrawMainField(_gameModel);

                _mathAndLogic.PlayerMovement(_gameModel, this);

                _mathAndLogic.MoveBall(_gameModel, this);
            }
        }
    }
}
