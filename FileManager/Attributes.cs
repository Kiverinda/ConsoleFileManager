using System;

namespace FileManager
{
    /// <summary>
    /// Тип данных для хранения информации об обьектах файловой системы
    /// </summary>
    public class Attributes
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool IsFile { get; set; }
        public string Extension { get; set; }
        public DateTime Date { get; set; }

        public Attributes(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public Attributes(string name, string path, long size, bool isFile, string extension)
        {
            Name = name;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
        }

        public Attributes(string name, string path, long size, bool isFile, string extension, DateTime date)
        {
            Name = name;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
            Date = date;
        }
    }
}
