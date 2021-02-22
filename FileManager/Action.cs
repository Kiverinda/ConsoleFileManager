using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
    static class Action
    {
        public static void ChangeActivePanel()
        {
            View view = new View();
            view.OldCursor(Desktop.ActivePanel);
            if (Desktop.ActivePanel == Desktop.LeftPanel)
            {
                Desktop.ActivePanel = Desktop.RightPanel;
            }
            else
            {
                Desktop.ActivePanel = Desktop.LeftPanel;
            }
            view.CurrentCursor(Desktop.ActivePanel);
        }

        public static void SelectCommandLine(FilesPanel currentPanel)
        {
            View view = new View();
            view.OldCursor(Desktop.ActivePanel);
            CommandLine line = new CommandLine(currentPanel);
            line.Parse();
            view.CurrentCursor(Desktop.ActivePanel);
        }

        public static void Enter()
        {
            FilesPanel currentPanel = Desktop.ActivePanel;
            List<FileAttributes> currentList = currentPanel.CurrentListDirAndFiles;
            string currentPath = currentList[currentPanel.AbsoluteCursorPosition].Path;

            if (currentList[currentPanel.AbsoluteCursorPosition].IsFile)
            {
                FileLaunch(currentList[currentPanel.AbsoluteCursorPosition]);
            }
            else
            {
                Desktop.ActivePanel.UpdatePath(currentPath);
            }
        }

        private static void FileLaunch(FileAttributes attributes)
        {
            if (attributes.Extension == ".exe")
            {
                Process open = new Process();
                open.StartInfo.FileName = attributes.Path;
                open.Start();
            }
            else if (attributes.Extension == ".txt")
            {
                Process open = new Process();
                open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
                open.StartInfo.Arguments = attributes.Path;
                open.Start();
            }
            else if (attributes.Extension == ".mp4")
            {
                Process open = new Process();
                open.StartInfo.FileName = @"C:\Program Files (x86)\K-Lite Codec Pack\MPC-HC64\mpc-hc64.exe";
                open.StartInfo.Arguments = attributes.Path;
                open.Start();
            }
            else
            {
                // Process.Start(attributes.Path);
                return;
            }
        }

        public static void DownArrow()
        {
            FilesPanel panel = Desktop.ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            int windowFileHight = Console.WindowHeight - 9;

            if (panel.AbsoluteCursorPosition >= Desktop.ActivePanel.CurrentListDirAndFiles.Count - 1)
            {
                return;
            }
            else if (panel.AbsoluteCursorPosition < windowFileHight + panel.FirstLineWhenScrolling)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition++;
                panel.RelativeCursorPosition++;
                view.CurrentCursor(panel);
            }
            else if (panel.AbsoluteCursorPosition >= windowFileHight + panel.FirstLineWhenScrolling)
            {
                panel.AbsoluteCursorPosition++;
                panel.FirstLineWhenScrolling++;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }

        }

        public static void UpArrow()
        {
            FilesPanel panel = Desktop.ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            if (panel.AbsoluteCursorPosition == 0)
            {
                return;
            }
            else if (panel.RelativeCursorPosition > 0)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition--;
                panel.RelativeCursorPosition--;
                view.CurrentCursor(panel);
            }
            else if (panel.RelativeCursorPosition <= 0)
            {
                panel.AbsoluteCursorPosition--;
                panel.FirstLineWhenScrolling--;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }
        }

        public static void CreateFile()
        {
            string path = Desktop.ActivePanel.CurrentPath;
            string message = "Введите имя файла";
            View view = new View();
            string nameFile = view.MessageCreate(message);
            
            string pathToFile = Path.Combine(path, nameFile);
            if (!File.Exists(pathToFile) && nameFile != "")
            {
                using (File.Create(pathToFile)) { }
            }
        }

        public static void EditFile()
        {
            List<FileAttributes> currentList = Desktop.ActivePanel.CurrentListDirAndFiles;
            string currentPath = currentList[Desktop.ActivePanel.AbsoluteCursorPosition].Path;

            if (currentList[Desktop.ActivePanel.AbsoluteCursorPosition].IsFile)
            {
                Process open = new Process();
                open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
                open.StartInfo.Arguments = currentPath;
                open.Start();
            }
            else
            {
                return;
            }
        }

        public static void CreateDirectory()
        {
            string path = Desktop.ActivePanel.CurrentPath;
            string message = "Введите имя директории";
            View view = new View();
            string nameDirectory = view.MessageCreate(message);

            string pathToDirectory = Path.Combine(path, nameDirectory); 
            if (!Directory.Exists(pathToDirectory) && nameDirectory != "")
            {
                Directory.CreateDirectory(pathToDirectory);
            }
        }

        public static void Delete()
        {
            FilesPanel activePanel = Desktop.ActivePanel;
            
            Delete delete = new Delete(activePanel);
            if (activePanel.BufferSelectedPositionCursor.Count == 0)
            {
                delete.Check();
            }
            else
            {
                List<FileAttributes> ListToDelete = new List<FileAttributes>();
                foreach (int i in activePanel.BufferSelectedPositionCursor)
                {
                    ListToDelete.Add(activePanel.CurrentListDirAndFiles[i]);
                }
                foreach (FileAttributes i in ListToDelete)
                {
                    delete.DeleteFileOrDirectory(i);
                }
            }
        }

        public static void Copy()
        {
            FilesPanel activePanel = Desktop.ActivePanel;
            Copy copy = new Copy(activePanel);

            if(activePanel.BufferSelectedPositionCursor.Count == 0)
            {
                copy.Check();
            }
            else
            {
                foreach (int i in activePanel.BufferSelectedPositionCursor)
                {
                    FileAttributes attributes = activePanel.CurrentListDirAndFiles[i];
                    copy.CheckingExistenceObjectInDestinationFolder(attributes);
                }
            }
        }

        public static void Rename()
        {
            FilesPanel activePanel = Desktop.ActivePanel;
            View view = new View();
            string newName = view.MessageCreate("ВВЕДИТЕ НОВОЕ ИМЯ");
            FileAttributes source = activePanel.CurrentListDirAndFiles[activePanel.AbsoluteCursorPosition];
            string oldName = source.Name;

            if (source.IsFile)
            {
                File.Move(source.Path, source.Path.Replace(oldName, newName));
            }
            else
            {
                Directory.Move(source.Path, source.Path.Replace(oldName, newName));
            }
            Desktop.Update();
        }


        public static void Move()
        {
            Copy();
            Delete();
        }

        public static void SelectDisk(FilesPanel panel)
        {
            int positionCursor = 0;
            View view = new View();
            FileAttributes[] allDrives = new RequestToDisk().AllDrives();
            view.SelectDrive(panel, positionCursor);
            while (true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.Enter:
                            Desktop.ActivePanel.UpdatePath(allDrives[positionCursor].Name);
                            Desktop.Update();
                            return;
                        case ConsoleKey.Escape:
                            Desktop.Update();
                            return;
                        case ConsoleKey.DownArrow:
                            if (positionCursor < allDrives.Length - 1)
                            {
                                positionCursor++;
                            }
                            view.SelectDrive(panel, positionCursor);
                            break;
                        case ConsoleKey.UpArrow:
                            if (positionCursor > 0)
                            {
                                positionCursor--;
                            }
                            view.SelectDrive(panel, positionCursor);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void Select()
        {
            View view = new View();
            int currentPositionCursor = Desktop.ActivePanel.AbsoluteCursorPosition;
            Desktop.ActivePanel.AddBufferSelectedPositionCursor(currentPositionCursor);
            view.SelectedItems(Desktop.ActivePanel);
        }

        public static void Tree(string path)
        {
            RequestToDisk request = new RequestToDisk(path);
            List<FileAttributes> source = request.GetListDirectoryAndFiles();
            Clear clear = new Clear();
            View view = new View();
            int WindowFileHeight = Console.WindowHeight - 9;
            int count = 0;
            int countView = 0;
            FilesPanel viewPanel;

            if (Desktop.ActivePanel.IsLeftPanel)
            {
                viewPanel = Desktop.RightPanel;
            }
            else
            {
                viewPanel = Desktop.LeftPanel;
            }
            clear.FPanel(viewPanel);
            while(count < source.Count)
            {
                if(countView <= WindowFileHeight)
                {
                    view.Tree(viewPanel, source[count].Path, countView);
                    count++;
                    countView++;
                }
                else
                {
                    Console.ReadLine();
                    countView = 0;
                }
            }
        }
    }
}
