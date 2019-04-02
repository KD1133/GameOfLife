using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife
{
    class BoardsManager
    {
        private DrawingService _Drawer;
        private IFileAccesingService _FileAccesorService;
        
        public List<Board> Boards { get; private set; }
        public int GlobalWidth { get; private set; }
        public int GlobalHeight { get; private set; }
        public int GlobalIteration { get; private set; }
        public int GlobalLiveCels { get; private set; }
        public List<int> DisplayedBoards { get; private set; }

        public BoardsManager(int width, int height, IFileAccesingService fileAccesorService)
        {
            _Drawer = new DrawingService();
            _FileAccesorService = fileAccesorService;
            Boards = new List<Board>();
            GlobalIteration = 0;
            DisplayedBoards = new List<int>();

            this.GlobalWidth = width;
            this.GlobalHeight = height;
        }

        public bool Load()
        {
            FileAccesingService.LoadData loadData = _FileAccesorService.Load();

            if (loadData.boards == null)
            {
                return false;
            }
            this.Boards = loadData.boards;
            this.GlobalIteration = loadData.iteration;
            this.GlobalWidth = loadData.width;
            this.GlobalHeight = loadData.height;
            this.DisplayedBoards = loadData.displayedBoards;
            return true;
        }

        public void Save()
        {
            _FileAccesorService.Save(this.Boards, this.GlobalIteration, this.GlobalWidth, this.GlobalHeight, this.DisplayedBoards);
        }

        public void Setup()
        {
            int displayPosition = 0;
            foreach (int boardNumber in DisplayedBoards)
            {
                Board board = this.Boards[boardNumber - 1];
                _Drawer.DisplayInitial(board.Cells, this.GlobalHeight, this.GlobalWidth, displayPosition);
                displayPosition++;
            }
        }

        public bool ChangeDisplayed(int boardNumber, bool status)
        {
            if(this.DisplayedBoards.Count < 8 && !this.DisplayedBoards.Contains(boardNumber) && status == true)
            {
                this.DisplayedBoards.Add(boardNumber);
                return true;
            }
            if (this.DisplayedBoards.Count > 0 && this.DisplayedBoards.Contains(boardNumber) && status == false)
            {
                this.DisplayedBoards.Remove(boardNumber);
                return true;
            }
            return false;
        }

        public void AddBoard()
        {
            Board board = new Board(this.GlobalWidth, this.GlobalHeight);
            Boards.Add(board);
        }
                
        public void Iterate()
        {
            this.GlobalLiveCels = 0;
            Parallel.ForEach(Boards, board =>
            {
                board.Iterate();
                this.GlobalLiveCels = this.GlobalLiveCels + board.LiveCells;
            });
            this.GlobalIteration++;
        }

        public void Display()
        {
            int displayNumber = 0;
            int globalLiveCels = 0;
            foreach(int boardNumber in DisplayedBoards)
            {
                Board board = this.Boards[boardNumber - 1];
                _Drawer.Display(board.Cells, board.PreviousCells, this.GlobalHeight, this.GlobalWidth, displayNumber);
                _Drawer.DisplayStats(board.LiveCells, boardNumber, this.GlobalHeight, displayNumber);
                displayNumber++;
            }
            _Drawer.DispalyGlobalStats(globalLiveCels, Boards.Count, this.GlobalIteration);
        }
    }
}
