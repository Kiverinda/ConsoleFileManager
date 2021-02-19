using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public class FilesPanel
    {
        public bool IsLeftPanel { get; set; }
        public string CurrentPath { get; set; }
        public List<FileAttributes> CurrentListDirAndFiles { get; set; }
        public int FirstLineWhenScrolling { get; set; }
        public int AbsoluteCursorPosition { get; set; }
        public int RelativeCursorPosition { get; set; }
        public List<FileAttributes> ListTo { get; set; }
        public HashSet<int> BufferSelectedPositionCursor { get; set; }

    
        public FilesPanel(bool isLeftPanel)
        {
            IsLeftPanel = isLeftPanel;
            CurrentPath = DriveInfo.GetDrives()[0].Name;
            CurrentListDirAndFiles = new RequestToDisk(CurrentPath).GetListCurrentDirectory();
            ListTo = new List<FileAttributes>();
            BufferSelectedPositionCursor = new HashSet<int>();
        }

        public void AddListTo(FileAttributes linkToFileOrDirectory)
        {
            ListTo.Add(linkToFileOrDirectory);
            if (!linkToFileOrDirectory.IsFile)
            {
                List<FileAttributes> list = new RequestToDisk(linkToFileOrDirectory.Path).GetListDirectoryAndFiles();
                foreach(FileAttributes fa in list)
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
            foreach(FileAttributes fa in ListTo)
            {
                size += fa.Size;
            }
            return size;
        }
        
        public void UpdatePath(string path)
        {
            CurrentListDirAndFiles = new RequestToDisk(path).GetListCurrentDirectory();
            CurrentPath = path;
            AbsoluteCursorPosition = 0;
            RelativeCursorPosition = 0;
            BufferSelectedPositionCursor = new HashSet<int>();
            ListTo = new List<FileAttributes>();
        }
    }
}
