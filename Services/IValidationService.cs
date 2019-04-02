using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    interface IValidationService
    {
        bool ValidateIntiger(string input);
        bool ValidateExistsInArray(int input, int arraySize);
    }
}
