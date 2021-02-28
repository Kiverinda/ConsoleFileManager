using System;

namespace FileManager
{
    class CL_LeftArrow : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.LeftArrow;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().Line.Length > 0 && CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine --;
            }

            return false;
        }
    }
}
