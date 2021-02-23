using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class SelectObject : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Spacebar;
        }

        public bool Execute()
        {
            View view = new View();
            int currentPositionCursor = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            Desktop.GetInstance().ActivePanel.AddBufferSelectedPositionCursor(currentPositionCursor);
            view.SelectedItems(Desktop.GetInstance().ActivePanel);
            return false;
        }
    }
}
