using GameOfLife.Menus;
using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class Startup
    {
        public void Start()
        {
            Validator validator = new Validator();
            ConsoleFacade consoleFacade = new ConsoleFacade();
            Drawer drawer = new Drawer(consoleFacade);
            Reader reader = new Reader(validator, consoleFacade);
            Cursor cursor = new Cursor();
            List<Board> boards = new List<Board>();
            BoardFactory boardFactory = new BoardFactory();
            FileAccesor fileAccesor = new FileAccesor(boardFactory, boards);
            BoardsController boardsController = new BoardsController(fileAccesor, drawer, boards, boardFactory);

            NewGameMenu newGameMenu = new NewGameMenu(validator, drawer, reader, boardsController);
            ChangeDisplayedMenu changeDisplayedMenu = new ChangeDisplayedMenu(drawer, reader, boardsController);
            EditBoardMenu editBoardMenu = new EditBoardMenu(drawer, reader, boardsController, cursor);
            PauseMenu pauseMenu = new PauseMenu(reader, drawer, validator, boardsController, newGameMenu, changeDisplayedMenu, editBoardMenu);
            GameEngine menuController = new GameEngine(drawer, reader, boardsController, newGameMenu, pauseMenu);
            menuController.Start();
        }
    }
}
