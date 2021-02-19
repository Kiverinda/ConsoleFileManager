using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
    static class Action
    {
        private static int windowHight = Console.WindowHeight;
        private static int windowFileHight = windowHight - 9;

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

        public static void Enter()
        {
            FilesPanel currentPanel = Desktop.ActivePanel;
            List<FileAttributes> currentList = currentPanel.CurrentListDirAndFiles;
            string currentPath = currentList[currentPanel.AbsoluteCursorPosition].Path;
            string currentExtension = currentList[currentPanel.AbsoluteCursorPosition].Extension;

            if (currentList[currentPanel.AbsoluteCursorPosition].IsFile)
            {
                if (currentExtension == ".exe")
                {
                    Process open = new Process();
                    open.StartInfo.FileName = currentPath;
                    open.Start();
                }
                else
                {
                    return;
                }
            }
            else
            {
                Desktop.ActivePanel.UpdatePath(currentPath);
                Desktop.Update();
            }
        }

        public static void DownArrow()
        {
            FilesPanel panel = Desktop.ActivePanel;
            View view = new View();
            Clear clear = new Clear();
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
            ColorTextAndBackground.Base();
            Desktop.ActivePanel.UpdatePath(path);
            Desktop.Update();
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

            ColorTextAndBackground.Base();
            Desktop.LeftPanel.UpdatePath(path);
            Desktop.RightPanel.UpdatePath(path);
            Desktop.Update();
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
                foreach (int i in activePanel.BufferSelectedPositionCursor)
                {
                    FileAttributes attributes = activePanel.CurrentListDirAndFiles[i];
                    delete.DeleteFileOrDirectory(attributes);
                }
            }
            activePanel.BufferSelectedPositionCursor.Clear();
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
            activePanel.BufferSelectedPositionCursor.Clear();
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
    }
}
