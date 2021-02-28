using System;
using System.IO;
using System.Collections.Generic;

namespace FileManager
{
    /// <summary>
    /// Класс для получения информации с диска
    /// </summary>
    public class RequestToDisk
    {
        /// <summary>
        /// Текущий путь
        /// </summary>
        public string CurrentPath { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RequestToDisk()
        {
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="path">Текущий путь</param>
        public RequestToDisk(string path)
        {
            CurrentPath = path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Attributes[] AllDrives()
        {
            DriveInfo[] alldisk = DriveInfo.GetDrives();
            Attributes[] disks = new Attributes[alldisk.Length];
            for (int i = 0; i < alldisk.Length; i++)
            {
                disks[i] = new Attributes(alldisk[i].Name, alldisk[i].Name);
            }
            return disks;
        }

        /// <summary>
        /// Получение корневой директории для текущего пути
        /// </summary>
        /// <returns></returns>
        public string GetRoot()
        {
            return Path.GetPathRoot(CurrentPath);
        }

        /// <summary>
        /// Является ли текущий путь корневой директорией диска
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Создание списка для текущего пути
        /// </summary>
        /// <returns></returns>
        public List<Attributes> GetListCurrentDirectory()
        {
            List<Attributes> newTreeFiles = new List<Attributes>();

            if (!IsRoot())
            {
                string upLevel = CurrentPath.Remove(CurrentPath.LastIndexOf(@"\"), CurrentPath.Length - CurrentPath.LastIndexOf(@"\"));

                newTreeFiles.Add(new Attributes("[..]", upLevel, 0, false, ""));
            }

            newTreeFiles = AddedFilesToList(newTreeFiles);

            return newTreeFiles;
        }

        /// <summary>
        /// Добавление файлов и директорий в текущий список Attributes
        /// </summary>
        /// <param name="list">Текущий список Attributes</param>
        /// <returns>Заполненный список Attributes</returns>
        public List<Attributes> AddedFilesToList(List<Attributes> list)
        {
            try
            {
                string[] directories = Directory.GetDirectories(CurrentPath);
                string[] files = Directory.GetFiles(CurrentPath);
                foreach (var str in directories)
                {
                    string name = Path.GetFileName(str);
                    list.Add(new Attributes(name, str, 0, false, ""));
                }
                foreach (var str in files)
                {
                    FileInfo file = new FileInfo(str);
                    long size = file.Length;
                    string name = Path.GetFileName(str);

                    list.Add(new Attributes(name, str, size, true, Path.GetExtension(str)));
                }
                return list;
            }
            catch
            {
                return list;
            }
        }

        /// <summary>
        /// Получение размера свободного места на диске
        /// </summary>
        /// <returns></returns>
        public long GetFreeSpace()
        {
            string root = Path.GetPathRoot(CurrentPath);
            DriveInfo di = new DriveInfo(root);
            return di.AvailableFreeSpace;
        }

        /// <summary>
        /// Вычисление размера директории
        /// </summary>
        /// <param name="list">Список Attributes</param>
        /// <returns></returns>
        public long GetSizeDirectory(List<Attributes> list)
        {
            long sizeDir = 0;

            foreach (Attributes el in list)
            {
                sizeDir += el.Size;
            }
            return sizeDir;
        }

        /// <summary>
        /// Получение дерева файлов и директорий
        /// </summary>
        /// <returns>Рекурсивный список директорий и файлов</returns>
        public List<Attributes> GetListDirectoryAndFiles()
        {
            List<Attributes> list = new List<Attributes>();

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
                catch (Exception ex)
                {
                    new ErrorLog(this, ex.Message, ex.StackTrace);
                    continue;
                }
                string[] files;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }
                catch (Exception ex)
                {
                    new ErrorLog(this, ex.Message, ex.StackTrace);
                    continue;
                }
                foreach (string pathToFile in files)
                {
                    try
                    {
                        FileInfo file = new FileInfo(pathToFile);
                        long size = file.Length;
                        string name = Path.GetFileName(pathToFile);
                        DateTime date = File.GetCreationTime(pathToFile);
                        list.Add(new Attributes(name, pathToFile, size, true, Path.GetExtension(pathToFile), date));
                    }
                    catch (Exception ex)
                    {
                        new ErrorLog(this, ex.Message, ex.StackTrace);
                        continue;
                    }
                }
                foreach (string pathToDir in subDirs)
                {
                    dirs.Push(pathToDir);
                    string name = Path.GetFileName(pathToDir);
                    DateTime date = Directory.GetCreationTime(pathToDir);
                    list.Add(new Attributes(name, pathToDir, 0, false, "", date));
                }
            }
            return list;
        }

    }
}
