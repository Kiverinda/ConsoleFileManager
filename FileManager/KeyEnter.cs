using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileManager
{
    /// <summary>
    /// Класс для осуществления действий, при нажатии на клавишу Enter
    /// </summary>
    public class KeyEnter : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Enter;
        }

        /// <summary>
        /// Проверка, находится курсор на директории или на файле
        /// </summary>
        /// <returns>Выход из приложения</returns>
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

        /// <summary>
        /// Действия с файлом в зависимости от его типа
        /// </summary>
        /// <param name="attributes">Путь к файлу</param>
        private void FileLaunch(Attributes attributes)
        {
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
                new View().Message("ФАЙЛ НЕ НАЙДЕН");
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
        }
    }
}
