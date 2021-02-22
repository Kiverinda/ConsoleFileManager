using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    public class Delete
    {
        public FilesPanel CurrentPanel { get; set; }

        public Delete(FilesPanel filesPanel)
        {
            CurrentPanel = filesPanel;
        }

        public void Check()
        {
            List<FileAttributes> currentList = CurrentPanel.CurrentListDirAndFiles;
            if (currentList[CurrentPanel.AbsoluteCursorPosition].Name == "[..]")
            {
                return;
            }
            DeleteFileOrDirectory(currentList[CurrentPanel.AbsoluteCursorPosition]);
        }

        public void DeleteFileOrDirectory(FileAttributes attributes)
        {
            if (attributes.IsFile)
            {
                if (Confirmation(attributes.Path)) File.Delete(attributes.Path);
            }
            else
            {
                if (Confirmation(attributes.Path)) Directory.Delete(attributes.Path, true);
            }
        }

        public bool Confirmation(string path)
        {
            View view = new View();
            view.Confirmation("Подтвердите удаление", path);
            
            ConsoleKeyInfo click;
            do
            {
                click = Console.ReadKey(true);
                if (click.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (click.Key == ConsoleKey.N)
                {
                    return false;
                }

            } while (click.Key != ConsoleKey.Escape);
            
            return false;
        }
    }
}
