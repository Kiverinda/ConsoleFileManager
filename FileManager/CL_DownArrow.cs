using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CL_DownArrow : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.DownArrow;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().BufferCommands.Count != 0)
            {
                if (CommandLine.GetInstance().CommandNumberInBuffer > 0)
                {
                    CommandLine.GetInstance().CommandNumberInBuffer--;
                    CommandLine.GetInstance().Line = CommandLine.GetInstance().BufferCommands[CommandLine.GetInstance().CommandNumberInBuffer];
                    CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
                }
            }
            return false;
        }
    }
}
