using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class SelectCommandLine : ICommand
    {

        public bool CanExecute(ConsoleKeyInfo click)
        {
            return ((click.Modifiers & ConsoleModifiers.Control) != 0) && (click.Key == ConsoleKey.Z);
        }

        public bool Execute()
        {
            View view = new View();
            view.OldCursor(Desktop.GetInstance().ActivePanel);
            CommandLine line = new CommandLine(Desktop.GetInstance().ActivePanel);
            line.Parse();
            view.CurrentCursor(Desktop.GetInstance().ActivePanel);
            return false;
        }
    }
}
