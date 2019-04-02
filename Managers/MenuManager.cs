using GameOfLife.ConsoleAccesors;
using System;
using System.Timers;

namespace GameOfLife
{
    class MenuManager
    {
        private Timer _Timer;
        private bool _TimerRuning { get; set; }
        private IReadingService _ReadingService;
        private IDrawingService _DrawingService;
        private Cursor _Cursor;
        private IValidationService _ValidationService;
        private IFileAccesingService _FileAccesingService;
        private BoardsManager _BoardsManager;

        public MenuManager(IFileAccesingService fileAccesorService, IValidationService validationService, IDrawingService drawingService, IReadingService readingService)
        {
            _Timer = new System.Timers.Timer();
            _Timer.Interval = 1000;

            _Timer.Elapsed += OnTimedEvent;
            _Timer.AutoReset = false;

            _ValidationService = validationService;
            _ReadingService = readingService;
            _DrawingService = drawingService;
            _Cursor = new Cursor();
            _FileAccesingService = fileAccesorService;

            _DrawingService.WriteLine("Welcome to the game of life");
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _BoardsManager.Iterate();
            _BoardsManager.Display();
            if (_TimerRuning)
            {
                _Timer.Start();
            }
        }

        public void NewGame()
        {
            _DrawingService.Clear();
            _DrawingService.WriteLine("Board Width : ");
            int x = _ReadingService.ReadIntiger();
            _DrawingService.WriteLine("Board Height : ");
            int y = _ReadingService.ReadIntiger();
            _DrawingService.WriteLine("Board count : ");
            int n = _ReadingService.ReadIntiger();
            
            _BoardsManager = new BoardsManager(x, y, _FileAccesingService);

            for (int i = 0; i < n; i++)
            {
                _BoardsManager.AddBoard();
            }

            int input;
            do
            {
                _DrawingService.WriteLine("Input board to be displayed number(maximum of 8 boards, input 0 to continue) : ");
                input = _ReadingService.ReadIntiger();
                if (input == 0 || !_ValidationService.ValidateExistsInArray(input, _BoardsManager.Boards.Count))
                {
                    break;
                }
                _BoardsManager.ChangeDisplayed(input, true);
                if (_BoardsManager.DisplayedBoards.Count == 8)
                {
                    break;
                }
            }
            while (true);

            this.Run();
        }
        
        public void ManageDisplayedBoards()
        {
            _DrawingService.Clear();
            _DrawingService.WriteLine("Press A to add displayed fields, R to remove displayed fields : ");
            string input = _ReadingService.ReadKey();
            int inputInteger;
            switch (input)
            {
                case "A":
                    _DrawingService.WriteLine("Field to add to diplayed number : ");
                    inputInteger = _ReadingService.ReadIntiger();
                    bool addStatus = _BoardsManager.ChangeDisplayed(inputInteger, true);
                    if (!addStatus)
                    {
                        _DrawingService.WriteLine("Wrong field number or field already added, or max field display reached");
                    }
                    break;
                case "R":
                    _DrawingService.WriteLine("Field to remove to diplayed number : ");
                    inputInteger = _ReadingService.ReadIntiger();
                    bool removeStatus = _BoardsManager.ChangeDisplayed(inputInteger, false);
                    if (!removeStatus)
                    {
                        _DrawingService.WriteLine("Field dosent exist or isnt displayed");
                    }
                    break;
            }
        }

        public void Start()
        {
            _BoardsManager = new BoardsManager(0, 0, _FileAccesingService);
            bool saveExists =_BoardsManager.Load(_FileAccesingService.Load());
            if(!saveExists)
            {
                this.NewGame();
            }

            this.Run();
        }

        public void EditBoard(Board board)
        {
            _DrawingService.Clear();
            _Cursor.ResetPosition();
            _DrawingService.WriteLine("use W A S D to move cursor, E to place or remove cell, C to clear all cells,R to change displayed game , F to run simulation");
            _DrawingService.DisplayInitial(board.Cells, _BoardsManager.GlobalHeight, _BoardsManager.GlobalWidth, 0);
            string input;
            _DrawingService.DispalyCursor(_Cursor.X, _Cursor.Y);
            do
            {
                input = _ReadingService.ReadKey();
                switch (input)
                {
                    case "A":
                        _Cursor.MoveLeft();
                        break;
                    case "D":
                        _Cursor.MoveRight(_BoardsManager.GlobalWidth);
                        break;
                    case "W":
                        _Cursor.MoveUp();
                        break;
                    case "S":
                        _Cursor.MoveDown(_BoardsManager.GlobalHeight);
                        break;
                    case "E":
                        board.UpdateCell(_Cursor.X, _Cursor.Y);
                        _DrawingService.UpdateCell(board.Cells[_Cursor.X, _Cursor.Y], _Cursor.X, _Cursor.Y); //TODO needs a facade?
                        break;
                    case "C":
                        board.ClearCells();
                        _DrawingService.Display(board.Cells, board.PreviousCells, _BoardsManager.GlobalHeight, _BoardsManager.GlobalWidth, 0); //TODO needs a facade?
                        break;
                    
                }
                _DrawingService.DispalyCursor(_Cursor.X, _Cursor.Y);
            }
            while (!input.Equals("F"));
        }

        public void Run()
        {
            string input;
            do
            {
                _DrawingService.Clear();
                _DrawingService.DisplayHeader("Press P to Pause");
                _BoardsManager.Setup();
                do
                {
                    _TimerRuning = true;
                    _Timer.Start();
                } while (_ReadingService.ReadKey() != "P");

                _TimerRuning = false;
                _DrawingService.DisplayHeader("Press E to edit field, R to chose displayed fields, N to start a new game, Esc to exit, any other key to continue");
                input = _ReadingService.ReadKey();
                _BoardsManager.Save();
                switch (input)
                {

                    case "E":
                        _DrawingService.DisplayHeader("Scelect board to edit : ");
                        int i = _ReadingService.ReadIntiger();
                        if (_ValidationService.ValidateExistsInArray(i, _BoardsManager.Boards.Count))
                        {
                            this.EditBoard(_BoardsManager.Boards[i - 1]);
                        }
                        break;
                    case "N":
                        this.NewGame();
                        break;
                    case "R":
                        this.ManageDisplayedBoards();
                        break;
                }

            } while (input != "Escape");
        }
    }
}
