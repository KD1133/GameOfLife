using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class ValidationService : IValidationService
    {
        public bool ValidateIntiger(string input)
        {
            if(int.TryParse(input, out int output))
            {
                return true;
            }
            return false;
        }

        public bool ValidateExistsInArray(int input, int arraySize)
        {
            if(input <= arraySize && input >=0)
            {
                return true;
            }
            return false;
        }
    }
}
