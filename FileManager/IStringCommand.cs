using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public interface IStringCommand
    {
        public bool CanExexute(string command);
        public bool Execute();
    }
}
