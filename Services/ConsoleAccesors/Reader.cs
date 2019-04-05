using GameOfLife.ConsoleAccesors;
using System;
namespace GameOfLife
{
    class Reader : IReader
    {
        private IValidatator _validationService { get; set; }
        private ConsoleFacade _consoleFacade { get; set; }

        public Reader(IValidatator validationService, ConsoleFacade consoleFacade)
        {
            _validationService = validationService;
            _consoleFacade = consoleFacade;
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
            return _consoleFacade.ReadKey();
        }

        public int ReadIntiger()
        {
            string input;
            input = _consoleFacade.ReadLine();
            if (!_validationService.ValidateIntiger(input))
            {
                return 0;
            }
            return int.Parse(input);
        }
    }
}
