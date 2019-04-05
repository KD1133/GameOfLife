namespace GameOfLife.Services
{
    class CellIterator
    {
        public int CheckIfXLoopsLeft(int x,int width)
        {
            int xLooped = x;
            if (x == 0)
            {
                xLooped = width;
            }
            return xLooped;
        }

        public int CheckIfXLoopsRight(int x, int width)
        {
            int xLooped = x;
            if (x + 1 == width)
            {
                xLooped = -1;
            }
            return xLooped;
        }

        public int CheckIfYLoopsTop(int y, int height)
        {
            int yLooped = y;
            if (y == 0)
            {
                yLooped = height;
            }
            return yLooped;
        }

        public int CheckIfYLoopsBottom(int y, int height)
        {
            int yLooped = y;
            if (y + 1 == height)
            {
                yLooped = -1;
            }
            return yLooped;
        }

        public int CountLiveNeigbours(int x, int y, bool[,] cells, bool[,] previousCells)
        {
            int livingNeighbours = 0;
            int xLooped;
            int yLooped;

            xLooped = CheckIfXLoopsLeft(x, cells.GetLength(0));
            if (previousCells[xLooped - 1, y] == true) livingNeighbours++; //left neigbour

            yLooped = CheckIfYLoopsTop(y, cells.GetLength(1));
            if (previousCells[xLooped - 1, yLooped - 1] == true) livingNeighbours++; //top left neigbour


            yLooped = CheckIfYLoopsBottom(y, cells.GetLength(1));
            if (previousCells[xLooped - 1, yLooped + 1] == true) livingNeighbours++; //bottom left neigbour

            xLooped = CheckIfXLoopsRight(x, cells.GetLength(0));
            if (previousCells[xLooped + 1, y] == true) livingNeighbours++; //right neigbour

            yLooped = CheckIfYLoopsTop(y, cells.GetLength(1));
            if (previousCells[xLooped + 1, yLooped - 1] == true) livingNeighbours++; //top right neigbour


            yLooped = CheckIfYLoopsBottom(y, cells.GetLength(1));
            if (previousCells[xLooped + 1, yLooped + 1] == true) livingNeighbours++; //bottom right neigbour

            yLooped = CheckIfYLoopsTop(y, cells.GetLength(1));
            if (previousCells[x, yLooped - 1] == true) livingNeighbours++; //top neigbour


            yLooped = CheckIfYLoopsBottom(y, cells.GetLength(1));
            if (previousCells[x, yLooped + 1] == true) livingNeighbours++; //bottom neigbour

            return livingNeighbours;
        }

        public bool WillSurvive(int x, int y, int livingNeighbours, bool living)
        {
            if (living == false)
            {
                if (livingNeighbours == 3)
                {
                    return true;
                }
            }
            else
            {
                if (livingNeighbours >= 2 && livingNeighbours <= 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
