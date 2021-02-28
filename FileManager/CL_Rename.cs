using System.IO;

namespace FileManager
{
    class CL_Rename : ICommand<string>
    {
        public bool CanExecute(string value)
        {
            return value.ToLower() == "rename";
        }

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
