
namespace FileManager
{
    public class FileAttributes
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool IsFile { get; set; }
        public string Extension { get; set; }

        public FileAttributes(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public FileAttributes(string name, string path, long size, bool isFile, string extension)
        {
            Name = name;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
        }

        public FileAttributes(string name, string subname, string path, long size, bool isFile, string extension)
        {
            Name = name;
            SubName = subname;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
        }
    }
}
