using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CreateDirectory : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F7;
        }

        public bool Execute()
        {
            string path = Desktop.GetInstance().ActivePanel.CurrentPath;
            string message = "Введите имя директории";
            View view = new View();
            string nameDirectory = view.MessageCreate(message);

            string pathToDirectory = Path.Combine(path, nameDirectory);
            if (!Directory.Exists(pathToDirectory) && nameDirectory != "")
            {
                Create(pathToDirectory);
            }
            Desktop.GetInstance().Update();
            return false;
        }

        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
