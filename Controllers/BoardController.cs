using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife
{
    class BoardsController
    {
        private Drawer _drawer;
        private BoardFactory _boardFactory;
        private IFileAccesor _fileAccesor;

        public List<Board> Boards;
        public int GlobalWidth { get; private set; }
        public int GlobalHeight { get; private set; }
        public int GlobalIteration { get; private set; }
        public int GlobalLiveCels { get; private set; }
        public List<int> DisplayedBoards { get; private set; }

        public BoardsController(IFileAccesor fileAccesor, Drawer drawer, List<Board> boards, BoardFactory boardFactory)
        {
            _drawer = drawer;
            _fileAccesor = fileAccesor;
            _boardFactory = boardFactory;
            Boards = boards;
            GlobalIteration = 0;
            DisplayedBoards = new List<int>();
        }

        public void NewGame(int width, int height)
        {
            GlobalWidth = width;
            GlobalHeight = height;
            DisplayedBoards.Clear();
            Boards.Clear();
        }
        
        public bool Load()
        {
            FileAccesor.LoadData loadData = _fileAccesor.Load();

            if (loadData.boards == null)
            {
                return false;
            }
            Boards = loadData.boards;
            GlobalIteration = loadData.iteration;
            GlobalWidth = loadData.width;
            GlobalHeight = loadData.height;
            DisplayedBoards = loadData.displayedBoards;
            return true;
        }

        public void Save()
        {
            _fileAccesor.Save(Boards, GlobalIteration, GlobalWidth, GlobalHeight, DisplayedBoards);
        }

        public void Setup()
        {
            int displayPosition = 0;
            foreach (int boardNumber in DisplayedBoards)
            {
                var board = Boards[boardNumber - 1];
                _drawer.DisplayInitial(board.GetCells(), GlobalHeight, GlobalWidth, displayPosition);
                displayPosition++;
            }
        }

        public bool ChangeDisplayed(int boardNumber, bool status)
        {
            if(DisplayedBoards.Count < 8 && !DisplayedBoards.Contains(boardNumber) && status == true)
            {
                DisplayedBoards.Add(boardNumber);
                return true;
            }
            if (DisplayedBoards.Count > 0 && DisplayedBoards.Contains(boardNumber) && status == false)
            {
                DisplayedBoards.Remove(boardNumber);
                return true;
            }
            return false;
        }

        public void AddBoards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddBoard();
            }
        }

        public void AddBoard()
        {
            Boards.Add(_boardFactory.CreateBoard(GlobalWidth, GlobalHeight));
            Boards[Boards.Count - 1].RandomizeCells();
        }
                
        public void Iterate()
        {
            GlobalLiveCels = 0;
            Parallel.ForEach(Boards, board =>
            {
                board.Iterate();
                GlobalLiveCels = GlobalLiveCels + board.LiveCells;
            });
            GlobalIteration++;
        }

        public void Display()
        {
            int displayNumber = 0;
            foreach (int boardNumber in DisplayedBoards)
            {
                var board = Boards[boardNumber - 1];
                _drawer.Display(board.GetCells(), board.GetPreviousCells(), GlobalHeight, GlobalWidth, displayNumber);
                _drawer.DisplayStats(board.LiveCells, boardNumber, GlobalHeight, displayNumber);
                displayNumber++;
            }
            _drawer.DispalyGlobalStats(GlobalLiveCels, Boards.Count, GlobalIteration);
        }
    }
}
