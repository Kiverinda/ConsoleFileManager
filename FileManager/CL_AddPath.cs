using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CL_AddPath : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.P && (click.Modifiers & ConsoleModifiers.Control) != 0;
        }

        public bool Execute()
        {
            CommandLine.GetInstance().Line += CommandLine.GetInstance().CurrentPanel.CurrentPath;
            CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
            return false;
        }
    }
}
