using GameOfLife.ConsoleAccesors;
using System;
namespace GameOfLife
{
    class ReadingService : IReadingService
    {
        private ValidationService _Validator { get; set; }

        public ReadingService()
        {
            _Validator = new ValidationService();
        }

        public bool KeyPresed()
        {
            if (Console.KeyAvailable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ReadKey()
        {
            ConsoleKey input;
            input = Console.ReadKey(true).Key;
            return input.ToString();
        }

        public int ReadIntiger()
        {
            string input;
            input = Console.ReadLine();
            if (!_Validator.ValidateIntiger(input))
            {
                return 0;
            }
            return int.Parse(input);
        }
    }
}
