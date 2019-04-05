using GameOfLife.ConsoleAccesors;

namespace GameOfLife
{
    class NewGameMenu
    {
        private IValidatator _validator;
        private IReader _reader;
        private IDrawer _drawer;
        private BoardsController _boardsController;

        public NewGameMenu(IValidatator validator, IDrawer drawer, IReader reader, BoardsController boardsController)
        {
            _validator = validator;
            _reader = reader;
            _drawer = drawer;
            _boardsController = boardsController;
        }

        public void Run()
        {
            _drawer.Clear();
            _drawer.WriteLine("Board Width : ");
            int width = _reader.ReadIntiger();
            _drawer.WriteLine("Board Height : ");
            int height = _reader.ReadIntiger();
            _drawer.WriteLine("Board count : ");
            int n = _reader.ReadIntiger();

            _boardsController.NewGame(width, height);

            _boardsController.AddBoards(n);

            int input;
            do
            {
                _drawer.WriteLine("Input board to be displayed number(maximum of 8 boards, input 0 to continue) : ");
                input = _reader.ReadIntiger();
                
                if (input == 0 || !_validator.ValidateExistsInArray(input, _boardsController.Boards.Count))
                {
                    break;
                }
                _boardsController.ChangeDisplayed(input, true);
                if (_boardsController.DisplayedBoards.Count == 8)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
