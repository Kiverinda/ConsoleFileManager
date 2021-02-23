using System;
using System.IO;

namespace FileManager
{
    class CreateFile : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F2;
        }

        public bool Execute()
        {
            string path = Desktop.GetInstance().ActivePanel.CurrentPath;
            Create(path);
            Desktop.GetInstance().Update();
            return false;
        }

        private void Create(string path)
        {
            string message = "Введите имя файла";
            View view = new View();
            string nameFile = view.MessageCreate(message);

            string pathToFile = Path.Combine(path, nameFile);
            if (!File.Exists(pathToFile) && nameFile != "")
            {
                using (File.Create(pathToFile)) { }
            }
        }

    }
}
