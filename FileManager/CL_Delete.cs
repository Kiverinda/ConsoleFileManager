using System;
using System.IO;


namespace FileManager
{
    class CL_Delete : ICommand<string>
    {
        public bool CanExecute(string value)
        {
            return value.ToLower() == "delete";
        }

        public bool Execute()
        {
            string currentPath = CommandLine.GetInstance().ListUserCommands[1];
            try
            {
                if (File.Exists(currentPath))
                {
                    if (Confirmation(currentPath)) File.Delete(currentPath);
                }
                else
                {
                    if (Confirmation(currentPath)) Directory.Delete(currentPath, true);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }

            return true;
        }

        public bool Confirmation(string path)
        {
            View view = new View();
            view.Confirmation("Подтвердите удаление", path);

            ConsoleKeyInfo click;
            do
            {
                click = Console.ReadKey(true);
                if (click.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (click.Key == ConsoleKey.N)
                {
                    return false;
                }

            } while (click.Key != ConsoleKey.Escape);

            return false;
        }
    }
}
