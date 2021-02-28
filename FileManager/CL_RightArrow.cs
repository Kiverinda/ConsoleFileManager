﻿using System;

namespace FileManager
{
    class CL_RightArrow : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.RightArrow;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().Line.Length > 0 && CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine ++;
            }

            return false;
        }
    }
}
