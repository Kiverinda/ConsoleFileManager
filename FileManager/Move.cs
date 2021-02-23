using System;
using System.Collections.Generic;

namespace FileManager
{
    class Move : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F6;
        }

        public bool Execute()
        {
            int cursorPosition = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            HashSet<int> bufer = Desktop.GetInstance().ActivePanel.BufferSelectedPositionCursor;
            
            new Copy().Execute();
            
            Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition = cursorPosition;
            Desktop.GetInstance().ActivePanel.BufferSelectedPositionCursor = bufer;
            
            new Delete().Execute();
            
            return false;
        }
    }
}
