using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileManager
{
    class EditFile : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F3;
        }

        public bool Execute()
        {
            List<FileAttributes> currentList = Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles;
            string currentPath = currentList[Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition].Path;
            Edit(currentPath);
            return false;
        }

        public void Edit(string path)
        {
            if (File.Exists(path))
            {
                Process open = new Process();
                open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
                open.StartInfo.Arguments = path;
                open.Start();
            }
            else
            {
                return;
            }
        }
    }
}
