using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class StatrupService
    {
        public void Start()
        {
            Console.SetBufferSize(32766, 32766);// helps with scrooling up adn down, and stutering

            FileAccesingService fileAccesingService = new FileAccesingService();
            ValidationService validationService = new ValidationService();
            DrawingService drawingService = new DrawingService();
            ReadingService readingService = new ReadingService();
            MenuManager menuManager = new MenuManager(fileAccesingService, validationService, drawingService, readingService);
            menuManager.Start();
        }
    }
}
