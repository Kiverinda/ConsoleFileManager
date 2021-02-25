using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileManager
{

    /// <summary>
    /// Класс для открытия файла в блокноте
    /// </summary>
    public class EditFile : ICommand
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F3;
        }

        /// <summary>
        /// Получение полного пути к файлу
        /// </summary>
        /// <returns>true or false</returns>
        public bool Execute()
        {
            List<Attributes> currentList = Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles;
            string currentPath = currentList[Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition].Path;
            Edit(currentPath);
            return false;
        }

        /// <summary>
        /// Запуск блокнота и открытие в нем файла
        /// </summary>
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
