using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CL_Backspace : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Backspace;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine --;
                CommandLine.GetInstance().Line = CommandLine.GetInstance().Line.Remove(CommandLine.GetInstance().CursorPositionInLine, 1);
            }
            return false;
        }
    }
}
