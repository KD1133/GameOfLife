using GameOfLife.ConsoleAccesors;
namespace GameOfLife.Menus
{
    class EditBoardMenu
    {
        private IReader _reader;
        private IDrawer _drawer;
        private BoardsController _boardsController;
        private Cursor _cursor;

        public EditBoardMenu(IDrawer drawer, IReader reader, BoardsController boardsController, Cursor cursor)
        {
            _reader = reader;
            _drawer = drawer;
            _boardsController = boardsController;
            _cursor = cursor;
        }

        public void Run(Board board)
        {
            _drawer.Clear();
            _cursor.ResetPosition();
            _drawer.WriteLine("use W A S D to move cursor, E to place or remove cell, C to clear all cells,R to change displayed game , F to run simulation");
            _drawer.DisplayInitial(board.GetCells(), _boardsController.GlobalHeight, _boardsController.GlobalWidth, 0);
            string input;
            _drawer.DispalyCursor(_cursor.X, _cursor.Y);
            do
            {
                input = _reader.ReadKey();
                switch (input)
                {
                    case "A":
                        _cursor.MoveLeft();
                        break;
                    case "D":
                        _cursor.MoveRight(_boardsController.GlobalWidth);
                        break;
                    case "W":
                        _cursor.MoveUp();
                        break;
                    case "S":
                        _cursor.MoveDown(_boardsController.GlobalHeight);
                        break;
                    case "E":
                        board.InvertCell(_cursor.X, _cursor.Y);
                        _drawer.UpdateCell(board.GetCells()[_cursor.X, _cursor.Y], _cursor.X, _cursor.Y); //TODO needs a facade?
                        break;
                    case "C":
                        board.ClearCells();
                        _drawer.Display(board.GetCells(), board.GetPreviousCells(), _boardsController.GlobalHeight, _boardsController.GlobalWidth, 0); //TODO needs a facade?
                        break;

                }
                _drawer.DispalyCursor(_cursor.X, _cursor.Y);
            }
            while (!input.Equals("F"));
        }
    }
}
