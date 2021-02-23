using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class ChangeActivePanel : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Tab;
        }

        public bool Execute()
        {
            View view = new View();
            view.OldCursor(Desktop.GetInstance().ActivePanel);
            if (Desktop.GetInstance().ActivePanel == Desktop.GetInstance().LeftPanel)
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
            }
            view.CurrentCursor(Desktop.GetInstance().ActivePanel);

            return false;
        }
        
    }
}
