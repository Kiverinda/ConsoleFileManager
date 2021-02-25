using System.IO;
using System.Collections.Generic;
using System;

namespace FileManager
{
    public class FilesPanel
    {
        public bool IsLeftPanel { get; set; }
        public string CurrentPath { get; set; }
        public List<Attributes> CurrentListDirAndFiles { get; set; }
        public int FirstLineWhenScrolling { get; set; }
        public int AbsoluteCursorPosition { get; set; }
        public int RelativeCursorPosition { get; set; }
        public List<Attributes> ListTo { get; set; }
        public HashSet<int> BufferSelectedPositionCursor { get; set; }

     
        public FilesPanel(bool isLeftPanel)
        {
            IsLeftPanel = isLeftPanel;
            CurrentPath = DriveInfo.GetDrives()[0].Name;
            CurrentListDirAndFiles = new RequestToDisk(CurrentPath).GetListCurrentDirectory();
            ListTo = new List<Attributes>();
            BufferSelectedPositionCursor = new HashSet<int>();
        }

        public void AddListTo(Attributes linkToFileOrDirectory)
        {
            ListTo.Add(linkToFileOrDirectory);
            if (!linkToFileOrDirectory.IsFile)
            {
                List<Attributes> list = new RequestToDisk(linkToFileOrDirectory.Path).GetListDirectoryAndFiles();
                foreach(Attributes fa in list)
                {
                    ListTo.Add(fa);
                }
            }
        }

        public void AddBufferSelectedPositionCursor(int item)
        {
            if (!BufferSelectedPositionCursor.Add(item)) BufferSelectedPositionCursor.Remove(item);
        }

        public double ListToSize()
        {
            double size = 0;
            foreach(Attributes file in ListTo)
            {
                size += file.Size;
            }
            return size;
        }
        
        public void UpdatePath(string path)
        {
            RequestToDisk request = new RequestToDisk(path);
            CurrentPath = path;
            CurrentListDirAndFiles = request.GetListCurrentDirectory();
            AbsoluteCursorPosition = 0;
            RelativeCursorPosition = 0;
            FirstLineWhenScrolling = 0;
            BufferSelectedPositionCursor = new HashSet<int>();
            ListTo = new List<Attributes>();
        }
    }
}
