using System;

namespace FileManager
{
    public interface ICommand
    {
        public bool CanExecute(ConsoleKeyInfo click);
        public bool Execute();
    }
}
