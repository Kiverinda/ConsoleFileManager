using System;

namespace FileManager
{
    public interface ICommand
    {
        public bool CanExexute(ConsoleKeyInfo click);
        public bool Execute();
    }
}
