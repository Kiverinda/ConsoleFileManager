using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public class CL_Escape : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Escape;
        }

        public bool Execute()
        {
            return true;
        }
    }
}
