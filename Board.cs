using System;

namespace GameOfLife
{
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Iteration { get; private set; }


        public bool[,] Cells { get; private set; }
        public bool[,] PreviousCells { get; private set; }
        public int LiveCells { get; private set; }
        static private Random _Rnd = new Random();

        public Board(int width,int height)
        {
            this.Width = width;
            this.Height = height;
            Cells = new bool[Width, Height];
            PreviousCells = new bool[Width, Height];
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    
                    Cells[i,j] = _Rnd.Next(2) < 1 ? true : false;
                }
            }
        }

        public void UpdateCell(int x, int y)
        {
            this.Cells[x, y] = !this.Cells[x, y];
        }

        public void ClearCells()
        {
            for (int i = 0; i < this.Width; i++) 
            {
                for (int j = 0; j < this.Height; j++)
                {
                    this.PreviousCells[i, j] = true;
                    this.Cells[i, j] = false;
                }
            }
        }

        public void Iterate()
        {
            PreviousCells = Cells.Clone() as bool[,];
            LiveCells = 0;
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    int livingNeighbours = 0;
                    int iLooped;
                    int jLooped;
                    if (i == 0)
                    {
                        iLooped = Width;
                    }
                    else
                    {
                        iLooped = i;
                    }
                    if (PreviousCells[iLooped - 1, j] == true) livingNeighbours++; //left neigbour

                    if (j == 0)
                    {
                        jLooped = Height;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[iLooped - 1, jLooped - 1] == true) livingNeighbours++; //top left neigbour
                    

                    if (j + 1 == Height)
                    {
                        jLooped = -1;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[iLooped - 1, jLooped + 1] == true) livingNeighbours++; //bottom left neigbour
                    
                    if (i + 1 == Width)
                    {
                        iLooped = -1;
                    }
                    else
                    {
                        iLooped = i;
                    }
                    if (PreviousCells[iLooped + 1, j] == true) livingNeighbours++; //right neigbour

                    if (j == 0)
                    {
                        jLooped = Height;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[iLooped + 1, jLooped - 1] == true) livingNeighbours++; //top right neigbour


                    if (j + 1 == Height)
                    {
                        jLooped = -1;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[iLooped + 1, jLooped + 1] == true) livingNeighbours++; //bottom right neigbour

                    if (j == 0)
                    {
                        jLooped = Height;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[i, jLooped - 1] == true) livingNeighbours++; //top neigbour


                    if (j + 1 == Height)
                    {
                        jLooped = -1;
                    }
                    else
                    {
                        jLooped = j;
                    }
                    if (PreviousCells[i, jLooped + 1] == true) livingNeighbours++; //bottom neigbour

                    if (PreviousCells[i, j] == false)
                    {
                        if (livingNeighbours == 3)
                        {
                            Cells[i, j] = true;
                            LiveCells++;
                        }
                    }
                    else
                    {
                        if (livingNeighbours < 2 || livingNeighbours > 3)
                        {
                            Cells[i, j] = false;
                        }
                        else
                        {
                            LiveCells++;
                        }
                    }
                }
            }
        }
    }
}
