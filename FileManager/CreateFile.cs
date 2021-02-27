using System;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Класс для создания файла
    /// </summary>
    public class CreateFile : ICommand<ConsoleKeyInfo>
    {
        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F2;
        }

        /// <summary>
        /// Создание нового файла при помощи метода Create
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            Create();
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Создание нового файла в текущей директории
        /// </summary>
        public void Create()
        {
            string path = Desktop.GetInstance().ActivePanel.CurrentPath;
            string nameFile = GetUserNameFile();
            string pathToFile = Path.Combine(path, nameFile);
            if (!File.Exists(pathToFile) && nameFile != "")
            {
                using (File.Create(pathToFile)) { }
            }
        }

        /// <summary>
        /// Получение от пользователя имени нового файла
        /// </summary>
        /// <returns>Имя файла</returns>
        public string GetUserNameFile()
        {
            string message = "Введите имя файла";
            View view = new View();
            return view.MessageCreate(message);
        }

    }
}
