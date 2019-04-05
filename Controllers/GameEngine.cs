using GameOfLife.ConsoleAccesors;
using GameOfLife.Menus;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameEngine
    {
        private NewGameMenu _newGameMenu;
        private PauseMenu _pauseMenu;
        private IReader _reader;
        private IDrawer _drawer;
        private BoardsController _boardsController;

        public GameEngine(IDrawer drawer, IReader reader, BoardsController boardsController, NewGameMenu newGameMenu, PauseMenu pauseMenu)
        {
            _newGameMenu = newGameMenu;
            _pauseMenu = pauseMenu;
            _reader = reader;
            _drawer = drawer;
            _boardsController = boardsController;

            _drawer.WriteLine("Welcome to the game of life");
        }
        
        public void Start()
        {
            bool saveExists =_boardsController.Load();
            if(!saveExists)
            {
                _newGameMenu.Run();
            }

            Run();
        }
        
        private void Run()
        {
            string input;
            do
            {
                _drawer.Clear();
                _drawer.DisplayHeader("Press P to Pause");
                _boardsController.Setup();
                do
                {
                    while(!_reader.KeyPresed())
                    {
                        Parallel.Invoke(
                            () =>
                            {
                                System.Threading.Thread.Sleep(1000); //Timer starts same time as iterating logic
                            },
                            () =>
                            {
                                _boardsController.Iterate();
                                _boardsController.Display();
                            }
                        );
                    }

                } while (_reader.ReadKey() != "P");
                input = _pauseMenu.Run();
            } while (input != "Escape");
        }
    }
}
