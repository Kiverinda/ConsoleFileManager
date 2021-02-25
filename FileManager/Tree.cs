using System;
using System.Collections.Generic;

namespace FileManager
{
    class Tree : ICommand
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F9;
        }

        public bool Execute()
        {
            int cursorPosition = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            string path = Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles[cursorPosition].Path;
            TreeFilesAndDirectory(path);
            Desktop.GetInstance().Update();
            return false;
        }

        public void TreeFilesAndDirectory(string path)
        {
            int countView = Console.WindowHeight - 9;
            int startPosition = 0;
            ConsoleKeyInfo click;
            RequestToDisk request = new RequestToDisk(path);
            View view = new View();
            view.Message("ИДЕТ ПОСТРОЕНИЕ ДЕРЕВА");
            List<Attributes> source = request.GetListDirectoryAndFiles();
            view.FPanel(Desktop.GetInstance().ActivePanel);
            Clear clear = new Clear();

            FilesPanel viewPanel;
            if (Desktop.GetInstance().ActivePanel.IsLeftPanel)
            {
                viewPanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                viewPanel = Desktop.GetInstance().LeftPanel;
            }

            while (true)
            {
                clear.FPanel(viewPanel);
                view.Tree(viewPanel, source, startPosition);
                click = Console.ReadKey();
                if (click.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (click.Key == ConsoleKey.DownArrow)
                {
                    if (startPosition + countView < source.Count)
                    {
                        startPosition += countView;
                    }
                }
                if (click.Key == ConsoleKey.UpArrow)
                {
                    if (startPosition >= countView)
                    {
                        startPosition -= countView;
                    }
                }

            }
        }
    }
}
