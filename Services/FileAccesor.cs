using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class FileAccesor : IFileAccesor
    {
        private BoardFactory _boardFactory;
        public List<Board> Boards;

        public FileAccesor(BoardFactory boardFactory, List<Board> boards)
        {
            _boardFactory = boardFactory;
            Boards = boards;
        }

        public struct LoadData
        {
            public List<Board> boards;
            public int iteration;
            public int width;
            public int height;
            public List<int> displayedBoards;
        }

        public void Save(List<Board> boards,int iteration, int width, int height, List<int> displayedBoards)
        {
            TextWriter tw = new StreamWriter("SavedGame.txt");
            
            tw.WriteLine(iteration); 
            tw.WriteLine(boards.Count);
            tw.WriteLine(width);
            tw.WriteLine(height);
            tw.WriteLine(displayedBoards.Count);
            foreach(int boardNumber in displayedBoards)
            {
                tw.Write(boardNumber);
            }
            tw.WriteLine();

            foreach (var board in boards){
                for (int j = 0; j < board.Height; j++)
                {
                    for (int i = 0; i < board.Width; i++)
                    {
                        tw.Write(board.GetCells()[i,j] ? 1 : 0);
                    }
                    tw.WriteLine();
                } 
            }
            tw.Close();
        }

        public LoadData Load()
        {

            var loadData = new LoadData();
            
            loadData.displayedBoards = new List<int>();

            if (!File.Exists("SavedGame.txt"))
            {
                return loadData;
            }

            TextReader tr = new StreamReader("SavedGame.txt");

            loadData.iteration = int.Parse(tr.ReadLine());
            int boardCount = int.Parse(tr.ReadLine());
            loadData.width = int.Parse(tr.ReadLine());
            loadData.height = int.Parse(tr.ReadLine());
            int displayedBoardCount = int.Parse(tr.ReadLine()); 
            for (int i = 0; i < displayedBoardCount; i++)
            {
                loadData.displayedBoards.Add(int.Parse(tr.Read().ToString()) - 48); //-48 converts from ascii to int value 
            }
            tr.ReadLine();

            for (int boardNumber = 0; boardNumber < boardCount; boardNumber++)
            {
                var board = _boardFactory.CreateBoard(loadData.width, loadData.height);
                for (int j = 0; j < board.Height; j++)
                {
                    for (int i = 0; i < board.Width; i++)
                    {
                        int a = int.Parse(tr.Read().ToString()) - 48; //-48 converts from ascii to int value 
                        if (a == 1) 
                        {
                            board.GetCells()[i, j] = true;
                        }
                        else
                        {
                            board.GetCells()[i, j] = false;
                        }
                    }
                    tr.ReadLine();
                }
                Boards.Add(board);
            }
            loadData.boards = Boards;

            tr.Close();

            return loadData;
        }

    }
}
