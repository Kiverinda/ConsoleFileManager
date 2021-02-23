using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class RenameObject : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F4;
        }

        public bool Execute()
        {
            FilesPanel activePanel = Desktop.GetInstance().ActivePanel;
            string path = activePanel.CurrentListDirAndFiles[activePanel.AbsoluteCursorPosition].Path;
            Rename(path);
            Desktop.GetInstance().Update();
            return false;
        }

        public void Rename(string path)
        {
            string newName = new View().MessageCreate("ВВЕДИТЕ НОВОЕ ИМЯ");
            string oldName = Path.GetFileName(path);
            if (File.Exists(path))
            {
                File.Move(path, path.Replace(oldName, newName));
            }
            else
            {
                Directory.Move(path, path.Replace(oldName, newName));
            }
            Desktop.GetInstance().Update();
        }
    }
}
