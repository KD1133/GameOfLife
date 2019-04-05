using GameOfLife.ConsoleAccesors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Menus
{
    class PauseMenu
    {
        private NewGameMenu _newGameMenu;
        private ChangeDisplayedMenu _changeDisplayedMenu;
        private EditBoardMenu _editBoardMenu;
        private IReader _reader;
        private IDrawer _drawer;
        private IValidatator _validator;
        private BoardsController _boardsController;

        public PauseMenu(IReader reader, IDrawer drawer, IValidatator validator, BoardsController boardsController, NewGameMenu newGameMenu, ChangeDisplayedMenu changeDisplayedMenu, EditBoardMenu editBoardMenu)
        {
            _newGameMenu = newGameMenu;
            _changeDisplayedMenu = changeDisplayedMenu;
            _editBoardMenu = editBoardMenu;
            _reader = reader;
            _drawer = drawer;
            _validator = validator;
            _boardsController = boardsController;
        }

        public string Run()
        {
            _drawer.DisplayHeader("Press E to edit field, R to chose displayed fields, N to start a new game, Esc to exit, any other key to continue");
            var input = _reader.ReadKey();
            _boardsController.Save();
            switch (input)
            {

                case "E":
                    _drawer.DisplayHeader("Scelect board to edit : ");
                    int i = _reader.ReadIntiger();
                    if (_validator.ValidateExistsInArray(i, _boardsController.Boards.Count))
                    {
                        _editBoardMenu.Run(_boardsController.Boards[i - 1]);
                    }
                    break;
                case "N":
                    _newGameMenu.Run();
                    break;
                case "R":
                    _changeDisplayedMenu.Run();
                    break;
            }
            return input;
        }
    }
}
