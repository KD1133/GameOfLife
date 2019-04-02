using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Cursor
    {
        public Cursor()
        {
            X = 0;
            Y = 0;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void ResetPosition()
        {
            X = 0;
            Y = 0;
        }

        public void MoveRight(int width)
        {

            if(X + 1 < width)
            {
                this.X++;
            }
        }

        public void MoveLeft()
        {
            if (X > 0)
            {
                this.X--;
            }
        }

        public void MoveUp()
        {
            if (Y > 0)
            {
                this.Y--;
            }
        }

        public void MoveDown(int height)
        {
            if (Y + 1 < height)
            {
                this.Y++;
            }
        }
    }
}
