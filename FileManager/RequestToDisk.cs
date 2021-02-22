using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileManager
{
    class RequestToDisk
    {
        public string CurrentPath { get; set; }

        public RequestToDisk()
        {
        }

        public RequestToDisk(string path)
        {
            CurrentPath = path;
        }

        public FileAttributes[] AllDrives()
        {
            DriveInfo[] alldisk = DriveInfo.GetDrives();
            FileAttributes[] disks = new FileAttributes[alldisk.Length];
            for(int i = 0; i < alldisk.Length; i++)
            {
                disks[i] = new FileAttributes(alldisk[i].Name, alldisk[i].Name);
            }
            return disks;
        }

        public string GetRoot()
        {
            return Path.GetPathRoot(CurrentPath);
        }

        public bool IsRoot()
        {
            bool isRoot = false;

            foreach (var str in DriveInfo.GetDrives())
            {
                if (CurrentPath == str.Name.Remove(str.Name.LastIndexOf(@"\"), str.Name.Length - str.Name.LastIndexOf(@"\")))
                {
                    isRoot = true;
                    CurrentPath += @"\";
                }
                else if (CurrentPath.ToUpper() == str.Name.Remove(str.Name.LastIndexOf(@"\"), str.Name.Length - str.Name.LastIndexOf(@"\")))
                {
                    isRoot = true;
                    CurrentPath += @"\";
                    CurrentPath = CurrentPath.ToUpper();
                }
                else if (CurrentPath == str.Name)
                {
                    isRoot = true;
                    CurrentPath += @"\";
                    CurrentPath = CurrentPath.ToUpper();
                }
                else if (CurrentPath.ToUpper() == str.Name)
                {
                    isRoot = true;
                    CurrentPath += @"\";
                }
            }

            return isRoot;
        }

        public List<FileAttributes> GetListCurrentDirectory()
        {
            List<FileAttributes> newTreeFiles = new List<FileAttributes>();

            if (!IsRoot())
            {
                string upLevel = CurrentPath.Remove(CurrentPath.LastIndexOf(@"\"), CurrentPath.Length - CurrentPath.LastIndexOf(@"\"));
                newTreeFiles.Add(new FileAttributes("[..]", upLevel));
            }

            newTreeFiles = AddedFilesToList(newTreeFiles);

            return newTreeFiles;
        }

        public List<FileAttributes> AddedFilesToList(List<FileAttributes> list)
        {
            try
            {
                string[] directories = Directory.GetDirectories(CurrentPath);
                string[] files = Directory.GetFiles(CurrentPath);
                foreach (var str in directories)
                {
                    string name = Path.GetFileName(str);
                    string subname = Substring(name);
                    list.Add(new FileAttributes(name, subname, str, 0, false, ""));
                }
                foreach (var str in files)
                {
                    FileInfo file = new FileInfo(str);
                    long size = file.Length;
                    string name = Path.GetFileName(str);
                    string subname = Substring(name);

                    list.Add(new FileAttributes(name, subname, str, size, true, Path.GetExtension(str)));
                }
                return list;
            }
            catch
            {
                return list;
            }
        }

        public long GetFreeSpace()
        {
            string root = Path.GetPathRoot(CurrentPath);
            DriveInfo di = new DriveInfo(root);
            return di.AvailableFreeSpace;
        }

        public long GetSizeDirectory()
        {
            List<FileAttributes> list = GetListDirectoryAndFiles();
            long sizeDir = GetSizeDirectory(list);
            return sizeDir;
        }
        
        public long GetSizeDirectory(List<FileAttributes> list)
        {
            long sizeDir = 0;

            foreach (FileAttributes el in list)
            {
                sizeDir += el.Size;
            }
            return sizeDir;
        }

        public List<FileAttributes> GetListDirectoryAndFiles()
        {
            List<FileAttributes> list = new List<FileAttributes>();

            Stack<string> dirs = new Stack<string>();
            dirs.Push(CurrentPath);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();

                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }
                string[] files;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }
                foreach (string pathToFile in files)
                {
                    try
                    {
                        FileInfo file = new FileInfo(pathToFile);
                        long size = file.Length;
                        string name = Path.GetFileName(pathToFile);
                        string subname = Substring(name);
                        list.Add(new FileAttributes(name, subname, pathToFile, size, true, Path.GetExtension(pathToFile)));
                    }
                    catch (FileNotFoundException)
                    {
                        continue;
                    }
                }
                foreach (string pathToDir in subDirs)
                {
                    dirs.Push(pathToDir);
                    string name = Path.GetFileName(pathToDir);
                    string subname = Substring(name);
                    list.Add(new FileAttributes(name, subname, pathToDir, 0, false, ""));
                }
            }
            return list;
        }

        public string Substring(string str)
        {
            int length = 30;
            string stringTemp = string.Concat(Enumerable.Repeat(' ', length));
            str = str + stringTemp;
            return str.Substring(0, length);
        }
    }
}
