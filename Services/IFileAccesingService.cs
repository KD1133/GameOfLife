using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameOfLife.FileAccesingService;

namespace GameOfLife
{
    interface IFileAccesingService
    {
        void Save(List<Board> boards, int iteration, int width, int height, List<int> displayedBoards);
        LoadData Load();
    }
}
