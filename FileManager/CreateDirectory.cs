using System;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Класс для создания директории
    /// </summary>
    public class CreateDirectory : ICommand<ConsoleKeyInfo>
    {
        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F7;
        }

        /// <summary>
        /// Создание новой директории при помощи метода Create и обновление
        /// окна приложения после создания
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            Create();
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Создание новой директории в текущей директории
        /// </summary>
        private void Create()
        {
            string path = Desktop.GetInstance().ActivePanel.CurrentPath;
            string nameDirectory = GetUserNameDirectory();
            string pathToDirectory = Path.Combine(path, nameDirectory);
            if (!Directory.Exists(pathToDirectory) && nameDirectory != "")
            {
                Directory.CreateDirectory(pathToDirectory);
            }
        }

        /// <summary>
        /// Получение от пользователя имени новой директории
        /// </summary>
        /// <returns>Имя директории</returns>
        public string GetUserNameDirectory()
        {
            string message = "Введите имя директории";
            View view = new View();
            return view.MessageCreate(message);
        }
    }
}
