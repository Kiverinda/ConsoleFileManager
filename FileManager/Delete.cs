using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    public class Delete : ICommand
    {
        public FilesPanel ActivePanel { get; set; }

        public Delete(FilesPanel filesPanel)
        {
            ActivePanel = filesPanel;
        }

        public Delete()
        {

        }

        public void Check()
        {
            List<FileAttributes> currentList = ActivePanel.CurrentListDirAndFiles;
            if (currentList[ActivePanel.AbsoluteCursorPosition].Name == "[..]")
            {
                return;
            }
            DeleteFileOrDirectory(currentList[ActivePanel.AbsoluteCursorPosition]);
        }

        public void DeleteFileOrDirectory(FileAttributes attributes)
        {
            try
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
            catch(Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
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

        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F8;
        }

        public bool Execute()
        {
            ActivePanel = Desktop.GetInstance().ActivePanel;

            if (ActivePanel.BufferSelectedPositionCursor.Count == 0)
            {
                Check();
            }
            else
            {
                List<FileAttributes> ListToDelete = new List<FileAttributes>();
                foreach (int i in ActivePanel.BufferSelectedPositionCursor)
                {
                    ListToDelete.Add(ActivePanel.CurrentListDirAndFiles[i]);
                }
                foreach (FileAttributes i in ListToDelete)
                {
                    DeleteFileOrDirectory(i);
                }
            }
            Desktop.GetInstance().Update();
            return false;
        }
    }
}
