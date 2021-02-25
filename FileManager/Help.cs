using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class Help : ICommand
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F1;
        }

        public bool Execute()
        {
            new View().Help();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Escape);

            Desktop.GetInstance().Update();
            return false;
        }
    }
}
