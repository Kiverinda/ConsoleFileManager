using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class DownArrow : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.DownArrow;
        }

        public bool Execute()
        {
            FilesPanel panel = Desktop.GetInstance().ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            int windowFileHight = Console.WindowHeight - 9;

            if (panel.AbsoluteCursorPosition >= Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles.Count - 1)
            {
                return false;
            }
            else if (panel.AbsoluteCursorPosition < windowFileHight + panel.FirstLineWhenScrolling)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition++;
                panel.RelativeCursorPosition++;
                view.CurrentCursor(panel);
            }
            else if (panel.AbsoluteCursorPosition >= windowFileHight + panel.FirstLineWhenScrolling)
            {
                panel.AbsoluteCursorPosition++;
                panel.FirstLineWhenScrolling++;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }
            return false;
        }
    }
}
