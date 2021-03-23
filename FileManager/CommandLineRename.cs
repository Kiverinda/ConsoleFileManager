using System.IO;

namespace FileManager
{
    /// <summary>
    /// Класс переименования обьекта файловой системы
    /// </summary>
    public class CommandLineRename : ICommand<string>
    {
        /// <summary>
        /// Сравнение входящей строки с контрольной строкой
        /// </summary>
        /// <param name="value">Входящая строка</param>
        /// <returns>true or false</returns>
        public bool CanExecute(string value)
        {
            return value.ToLower() == "rename";
        }

        /// <summary>
        /// Переименование обьекта файловой системы
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            View view = new View();
            string currentPath = CommandLine.GetInstance().ListUserCommands[1];
            string oldName = Path.GetFileName(CommandLine.GetInstance().ListUserCommands[1]);
            string newName = CommandLine.GetInstance().ListUserCommands[2];

            if (File.Exists(currentPath))
            {
                File.Move(currentPath, currentPath.Replace(oldName, newName));
            }
            else if (Directory.Exists(currentPath))
            {
                Directory.Move(currentPath, currentPath.Replace(oldName, newName));
            }
            else
            {
                view.Message("ОБЪЕКТ НЕ НАЙДЕН");
            }
            return true;
        }
    }
}
