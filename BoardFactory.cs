

using GameOfLife.Services;

namespace GameOfLife
{
    class BoardFactory
    {
        public BoardFactory()
        {
        }

        public Board CreateBoard(int width, int height)
        {
            CellIterator cellIterator = new CellIterator();
            Board board = new Board(width, height, cellIterator);
            return board;
        }
    }
}
