using GameOfLife.ConsoleAccesors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class ChangeDisplayedMenu
    {
        private IReader _reader;
        private IDrawer _drawer;
        private BoardsController _boardsController;

        public ChangeDisplayedMenu(IDrawer drawer, IReader reader, BoardsController boardsController)
        {
            _reader = reader;
            _drawer = drawer;
            _boardsController = boardsController;
        }

       public void Run()
        {
            _drawer.Clear();
            _drawer.WriteLine("Press A to add displayed fields, R to remove displayed fields : ");
            string input = _reader.ReadKey();
            int inputInteger;
            switch (input)
            {
                case "A":
                    _drawer.WriteLine("Field to add to diplayed number : ");
                    inputInteger = _reader.ReadIntiger();
                    bool addStatus = _boardsController.ChangeDisplayed(inputInteger, true);
                    if (!addStatus)
                    {
                        _drawer.WriteLine("Wrong field number or field already added, or max field display reached");
                    }
                    break;
                case "R":
                    _drawer.WriteLine("Field to remove to diplayed number : ");
                    inputInteger = _reader.ReadIntiger();
                    bool removeStatus = _boardsController.ChangeDisplayed(inputInteger, false);
                    if (!removeStatus)
                    {
                        _drawer.WriteLine("Field dosent exist or isnt displayed");
                    }
                    break;
            }
        }
    }
}
