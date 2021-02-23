using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class Copy : ICommand
    {
        public FilesPanel ActivePanel { get; set; }
        public FilesPanel TargetPanel { get; set; }
        public View ViewCopy { get; set; }

        public Copy(FilesPanel activePanel)
        {
            ActivePanel = activePanel;

            if (activePanel.IsLeftPanel)
            {
                TargetPanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                TargetPanel = Desktop.GetInstance().LeftPanel;
            }

            ViewCopy = new View();
        }

        public Copy()
        {
            ViewCopy = new View();
        }

        public void Check()
        {
            List<FileAttributes> currentList = ActivePanel.CurrentListDirAndFiles;
            
            if (currentList[ActivePanel.AbsoluteCursorPosition].Name == "[..]")
            {
                return;
            }
            else if (ActivePanel.CurrentPath == TargetPanel.CurrentPath)
            {
                ViewCopy.Message($"НЕЛЬЗЯ СКОПИРОВАТЬ ФАЙЛ В ТЕКУЩИЙ КАТАЛОГ");
                Console.ReadKey();
                return;
            }

            CheckingExistenceObjectInDestinationFolder(currentList[ActivePanel.AbsoluteCursorPosition]);
        }

        public void CheckingExistenceObjectInDestinationFolder(FileAttributes attributes)
        {
            if (attributes.IsFile)
            {
                if(File.Exists(Path.Combine(TargetPanel.CurrentPath, attributes.Name)))
                {
                    ViewCopy.Message($"ФАЙЛ УЖЕ СУЩЕСТВУЕТ");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    CheckFreeSpaceFile(attributes);
                }
            }
            else if (!attributes.IsFile)
            {
                if (Directory.Exists(Path.Combine(TargetPanel.CurrentPath, attributes.Name)))
                {
                    ViewCopy.Message($"ДИРЕКТОРИЯ УЖЕ СУЩЕСТВУЕТ");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    CheckFreeSpaceDirectory(attributes);
                }
            }
        }

        public void CheckFreeSpaceFile(FileAttributes attributes)
        {
            long freeSpace = new RequestToDisk(TargetPanel.CurrentPath).GetFreeSpace();
            long size = new FileInfo(attributes.Path).Length;
            if (size > freeSpace)
            {
                ViewCopy.Message($"НЕ ДОСТАТОЧНО МЕСТА НА ДИСКЕ  {Path.GetPathRoot(TargetPanel.CurrentPath)}");
                Console.ReadKey();
                return;
            }
            else
            {
                CopyFile(attributes.Path);
            }
        }

        public void CheckFreeSpaceDirectory(FileAttributes attributes) 
        {
            RequestToDisk list = new RequestToDisk(attributes.Path);
            List<FileAttributes> newTreeFiles = list.GetListDirectoryAndFiles();
            long freeSpace = new RequestToDisk(TargetPanel.CurrentPath).GetFreeSpace();
            long size = list.GetSizeDirectory(newTreeFiles);
            
            if (size > freeSpace)
            {
                ViewCopy.Message($"НЕ ДОСТАТОЧНО МЕСТА НА ДИСКЕ  {Path.GetPathRoot(TargetPanel.CurrentPath)}");
                Console.ReadKey();
                return;
            }
            else
            {
                CopyDirectory(attributes.Path, newTreeFiles);
            }
        }

        public void CopyFile(string path)
        {
            ViewCopy.Copy(Path.GetFileName(path), TargetPanel.CurrentPath);
            CustomFileCopy cs = new CustomFileCopy(path, path.Replace(ActivePanel.CurrentPath, TargetPanel.CurrentPath));
            cs.OnProgressChanged += ViewPersentageToConsole;
            //cs.OnComplete += ViewVessageCompleteToConsole;
            cs.Copy();
        }

        public void CopyDirectory(string path, List<FileAttributes> newTreeFiles)
        {
            string nameDirectory = Path.Combine(TargetPanel.CurrentPath, Path.GetFileName(path));
            Directory.CreateDirectory(nameDirectory);

            foreach (FileAttributes attributes in newTreeFiles)
            {
                if (!attributes.IsFile)
                {
                    Directory.CreateDirectory(attributes.Path.Replace(ActivePanel.CurrentPath, TargetPanel.CurrentPath));
                }
            }
            foreach (FileAttributes attributes in newTreeFiles)
            {
                if (attributes.IsFile)
                {
                    CopyFile(attributes.Path);
                }
            }
        }

        public void ViewPersentageToConsole(double persentage, ref bool cancelFlag)
        {
            ViewCopy.CopyPersentage(persentage);
            if (persentage == 100)
            {
                cancelFlag = true;
            }
        }

        public bool CanExexute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F5;
        }

        public bool Execute()
        {
            ActivePanel = Desktop.GetInstance().ActivePanel;
            if (ActivePanel.IsLeftPanel)
            {
                TargetPanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                TargetPanel = Desktop.GetInstance().LeftPanel;
            }

            if (ActivePanel.BufferSelectedPositionCursor.Count == 0)
            {
                Check();
            }
            else
            {
                foreach (int i in ActivePanel.BufferSelectedPositionCursor)
                {
                    FileAttributes attributes = ActivePanel.CurrentListDirAndFiles[i];
                    CheckingExistenceObjectInDestinationFolder(attributes);
                }
            }
            Desktop.GetInstance().Update();
            return false;
        }

        //public void ViewVessageCompleteToConsole()
        //{
        //    View view = new View();
        //    view.Message("Файл скопирован!");
        //    Console.ReadKey();
        //}

    }
}

