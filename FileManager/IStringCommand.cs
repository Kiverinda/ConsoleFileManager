using System;

namespace FileManager
{
    public interface IStringCommand
    {
        public bool CanExecute(string command);
        public void Execute(string[] enteredData);
    }
}
