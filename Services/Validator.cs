using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Validator : IValidatator
    {
        public bool ValidateIntiger(string input)
        {
            return (int.TryParse(input, out int output)) ;
        }

        public bool ValidateExistsInArray(int input, int arraySize)
        {
            return (input <= arraySize && input >= 0);
        }
    }
}
