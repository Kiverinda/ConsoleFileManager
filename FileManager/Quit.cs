using System;

namespace FileManager
{
    class Quit : ICommand
    {
        public bool Close = false;

        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Escape;
        }

        public bool Execute()
        {
            ThreadControlSizeWindow.GetInstance().Close = true;
            UserData.GetInstance().Serialize();
            return true;
        }
    }
}
