using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class UpArrow : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.UpArrow;
        }

        public bool Execute()
        {
            FilesPanel panel = Desktop.GetInstance().ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            if (panel.AbsoluteCursorPosition == 0)
            {
                return false;
            }
            else if (panel.RelativeCursorPosition > 0)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition--;
                panel.RelativeCursorPosition--;
                view.CurrentCursor(panel);
            }
            else if (panel.RelativeCursorPosition <= 0)
            {
                panel.AbsoluteCursorPosition--;
                panel.FirstLineWhenScrolling--;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }
            return false;
        }
    }
}
