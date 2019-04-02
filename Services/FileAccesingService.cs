using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class FileAccesingService : IFileAccesingService
    {
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

            foreach (Board board in boards){
                for (int j = 0; j < board.Height; j++)
                {
                    for (int i = 0; i < board.Width; i++)
                    {
                        tw.Write(board.Cells[i,j] ? 1 : 0);
                    }
                    tw.WriteLine();
                } 
            }
            tw.Close();
        }

        public LoadData Load()
        {

            LoadData loadData = new LoadData();

            List<Board> boards = new List<Board>();

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
                loadData.displayedBoards.Add(int.Parse(tr.Read().ToString()) - 48);
            }
            tr.ReadLine();

            for (int boardNumber = 0; boardNumber < boardCount; boardNumber++)
            {
                Board board = new Board(loadData.width, loadData.height);
                for (int j = 0; j < board.Height; j++)
                {
                    for (int i = 0; i < board.Width; i++)
                    {
                        int a = int.Parse(tr.Read().ToString()); //tr.Read reads ASCII 
                        if (a == 49) //ASCII value
                        {
                            board.Cells[i, j] = true;
                        }
                        else
                        {
                            board.Cells[i, j] = false;
                        }
                    }
                    tr.ReadLine();
                }
                boards.Add(board);
            }
            loadData.boards = boards;

            tr.Close();

            return loadData;
        }

    }
}
