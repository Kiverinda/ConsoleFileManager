using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    
    /// <summary>
    /// Класс переименования обьекта
    /// </summary>
    public class RenameObject : ICommand
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F4;
        }

        /// <summary>
        /// Получение пути к обьекту и запуск переименования
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            FilesPanel activePanel = Desktop.GetInstance().ActivePanel;
            string path = activePanel.CurrentListDirAndFiles[activePanel.AbsoluteCursorPosition].Path;
            Rename(path);
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Получение от пользователя нового имени и переименование обьекта
        /// </summary>
        /// <param name="path">Путь к обьекту</param>
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
