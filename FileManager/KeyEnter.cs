using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileManager
{
    class KeyEnter : ICommand
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Enter;
        }

        public bool Execute()
        {
            FilesPanel currentPanel = Desktop.GetInstance().ActivePanel;
            List<Attributes> currentList = currentPanel.CurrentListDirAndFiles;
            string currentPath = currentList[currentPanel.AbsoluteCursorPosition].Path;
            View view = new View();
            if (currentList[currentPanel.AbsoluteCursorPosition].IsFile)
            {
                FileLaunch(currentList[currentPanel.AbsoluteCursorPosition]);
            }
            else
            {
                Desktop.GetInstance().ActivePanel.UpdatePath(currentPath);
                view.CurrentCursor(Desktop.GetInstance().ActivePanel);
            }
            Desktop.GetInstance().ViewDesktop();
            return false;
        }

        private void FileLaunch(Attributes attributes)
        {
            View view = new View();
            try
            {
                if (attributes.Extension == ".exe")
                {
                    Process open = new Process();
                    open.StartInfo.FileName = attributes.Path;
                    open.Start();
                }
                else if (attributes.Extension == ".txt")
                {
                    Process open = new Process();
                    open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
                    open.StartInfo.Arguments = attributes.Path;
                    open.Start();
                }
                else if (attributes.Extension == ".mp4")
                {
                    Process open = new Process();
                    open.StartInfo.FileName = @"c:\Program Files\Windows Media Player\wmplayer.exe";
                    open.StartInfo.Arguments = attributes.Path;
                    open.Start();
                }
                else if (attributes.Extension == ".mp3")
                {
                    Process open = new Process();
                    open.StartInfo.FileName = @"c:\Program Files\Windows Media Player\wmplayer.exe";
                    open.StartInfo.Arguments = attributes.Path;
                    open.Start();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                view.Message("ФАЙЛ НЕ НАЙДЕН");
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
        }
    }
}
