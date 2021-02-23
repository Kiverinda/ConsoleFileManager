using System;

namespace FileManager
{
    class Quit : ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Escape;
        }

        public bool Execute()
        {
            UserData.GetInstance().Serialize();
            return true;
        }
    }
}
