using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace FileManager
{
    class Action
    {
        static Action instance;

        private Action() { }

        public static Action GetInstance()
        {
            if(instance == null)
            {
                instance = new Action();
            }
            return instance;
        }


        //public void ChangeActivePanel()
        //{
        //    View view = new View();
        //    view.OldCursor(Desktop.GetInstance().ActivePanel);
        //    if (Desktop.GetInstance().ActivePanel == Desktop.GetInstance().LeftPanel)
        //    {
        //        Desktop.GetInstance().ActivePanel = Desktop.GetInstance().RightPanel;
        //    }
        //    else
        //    {
        //        Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
        //    }
        //    view.CurrentCursor(Desktop.GetInstance().ActivePanel);
        //}

        //public void SelectCommandLine(FilesPanel currentPanel)
        //{
        //    View view = new View();
        //    view.OldCursor(Desktop.GetInstance().ActivePanel);
        //    CommandLine line = new CommandLine(currentPanel);
        //    line.Parse();
        //    view.CurrentCursor(Desktop.GetInstance().ActivePanel);
        //}

        //public void Enter()
        //{
        //    FilesPanel currentPanel = Desktop.GetInstance().ActivePanel;
        //    List<FileAttributes> currentList = currentPanel.CurrentListDirAndFiles;
        //    string currentPath = currentList[currentPanel.AbsoluteCursorPosition].Path;
        //    View view = new View();
        //    if (currentList[currentPanel.AbsoluteCursorPosition].IsFile)
        //    {
        //        FileLaunch(currentList[currentPanel.AbsoluteCursorPosition]);
        //    }
        //    else
        //    {
        //        Desktop.GetInstance().ActivePanel.UpdatePath(currentPath);
        //        view.CurrentCursor(Desktop.GetInstance().ActivePanel);
        //    }
        //}

        //private void FileLaunch(FileAttributes attributes)
        //{
        //    View view = new View();
        //    try
        //    {
        //        if (attributes.Extension == ".exe")
        //        {
        //            Process open = new Process();
        //            open.StartInfo.FileName = attributes.Path;
        //            open.Start();
        //        }
        //        else if (attributes.Extension == ".txt")
        //        {
        //            Process open = new Process();
        //            open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
        //            open.StartInfo.Arguments = attributes.Path;
        //            open.Start();
        //        }
        //        else if (attributes.Extension == ".mp4")
        //        {
        //            Process open = new Process();
        //            open.StartInfo.FileName = @"c:\Program Files\Windows Media Player\wmplayer.exe";
        //            open.StartInfo.Arguments = attributes.Path;
        //            open.Start();
        //        }
        //        else if (attributes.Extension == ".mp3")
        //        {
        //            Process open = new Process();
        //            open.StartInfo.FileName = @"c:\Program Files\Windows Media Player\wmplayer.exe";
        //            open.StartInfo.Arguments = attributes.Path;
        //            open.Start();
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (System.ComponentModel.Win32Exception)
        //    {
        //        view.Message("Не найден файл");
        //    }
        //}

        //public void DownArrow()
        //{
        //    FilesPanel panel = Desktop.GetInstance().ActivePanel;
        //    View view = new View();
        //    Clear clear = new Clear();
        //    int windowFileHight = Console.WindowHeight - 9;

        //    if (panel.AbsoluteCursorPosition >= Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles.Count - 1)
        //    {
        //        return;
        //    }
        //    else if (panel.AbsoluteCursorPosition < windowFileHight + panel.FirstLineWhenScrolling)
        //    {
        //        view.OldCursor(panel);
        //        panel.AbsoluteCursorPosition++;
        //        panel.RelativeCursorPosition++;
        //        view.CurrentCursor(panel);
        //    }
        //    else if (panel.AbsoluteCursorPosition >= windowFileHight + panel.FirstLineWhenScrolling)
        //    {
        //        panel.AbsoluteCursorPosition++;
        //        panel.FirstLineWhenScrolling++;
        //        clear.FPanel(panel);
        //        view.FPanel(panel);
        //        view.CurrentCursor(panel);
        //    }

        //}

        //public void UpArrow()
        //{
        //    FilesPanel panel = Desktop.GetInstance().ActivePanel;
        //    View view = new View();
        //    Clear clear = new Clear();
        //    if (panel.AbsoluteCursorPosition == 0)
        //    {
        //        return;
        //    }
        //    else if (panel.RelativeCursorPosition > 0)
        //    {
        //        view.OldCursor(panel);
        //        panel.AbsoluteCursorPosition--;
        //        panel.RelativeCursorPosition--;
        //        view.CurrentCursor(panel);
        //    }
        //    else if (panel.RelativeCursorPosition <= 0)
        //    {
        //        panel.AbsoluteCursorPosition--;
        //        panel.FirstLineWhenScrolling--;
        //        clear.FPanel(panel);
        //        view.FPanel(panel);
        //        view.CurrentCursor(panel);
        //    }
        //}

        //public void CreateFile()
        //{
        //    string path = Desktop.GetInstance().ActivePanel.CurrentPath;
        //    string message = "Введите имя файла";
        //    View view = new View();
        //    string nameFile = view.MessageCreate(message);

        //    string pathToFile = Path.Combine(path, nameFile);
        //    if (!File.Exists(pathToFile) && nameFile != "")
        //    {
        //        using (File.Create(pathToFile)) { }
        //    }
        //}

        //public void EditFile()
        //{
        //    List<FileAttributes> currentList = Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles;
        //    string currentPath = currentList[Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition].Path;

        //    if (currentList[Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition].IsFile)
        //    {
        //        Process open = new Process();
        //        open.StartInfo.FileName = @"c:\Windows\System32\notepad.exe";
        //        open.StartInfo.Arguments = currentPath;
        //        open.Start();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //public void CreateDirectory()
        //{
        //    string path = Desktop.GetInstance().ActivePanel.CurrentPath;
        //    string message = "Введите имя директории";
        //    View view = new View();
        //    string nameDirectory = view.MessageCreate(message);

        //    string pathToDirectory = Path.Combine(path, nameDirectory);
        //    if (!Directory.Exists(pathToDirectory) && nameDirectory != "")
        //    {
        //        Directory.CreateDirectory(pathToDirectory);
        //    }
        //}

        //public void Delete()
        //{
        //    FilesPanel activePanel = Desktop.GetInstance().ActivePanel;

        //    Delete delete = new Delete(activePanel);
        //    if (activePanel.BufferSelectedPositionCursor.Count == 0)
        //    {
        //        delete.Check();
        //    }
        //    else
        //    {
        //        List<FileAttributes> ListToDelete = new List<FileAttributes>();
        //        foreach (int i in activePanel.BufferSelectedPositionCursor)
        //        {
        //            ListToDelete.Add(activePanel.CurrentListDirAndFiles[i]);
        //        }
        //        foreach (FileAttributes i in ListToDelete)
        //        {
        //            delete.DeleteFileOrDirectory(i);
        //        }
        //    }
        //}

        //public void Copy()
        //{
        //    FilesPanel activePanel = Desktop.GetInstance().ActivePanel;
        //    Copy copy = new Copy(activePanel);

        //    if (activePanel.BufferSelectedPositionCursor.Count == 0)
        //    {
        //        copy.Check();
        //    }
        //    else
        //    {
        //        foreach (int i in activePanel.BufferSelectedPositionCursor)
        //        {
        //            FileAttributes attributes = activePanel.CurrentListDirAndFiles[i];
        //            copy.CheckingExistenceObjectInDestinationFolder(attributes);
        //        }
        //    }
        //}

        //public void Rename()
        //{
        //    FilesPanel activePanel = Desktop.GetInstance().ActivePanel;
        //    View view = new View();
        //    string newName = view.MessageCreate("ВВЕДИТЕ НОВОЕ ИМЯ");
        //    FileAttributes source = activePanel.CurrentListDirAndFiles[activePanel.AbsoluteCursorPosition];
        //    string oldName = source.Name;

        //    if (source.IsFile)
        //    {
        //        File.Move(source.Path, source.Path.Replace(oldName, newName));
        //    }
        //    else
        //    {
        //        Directory.Move(source.Path, source.Path.Replace(oldName, newName));
        //    }
        //    Desktop.GetInstance().Update();
        //}


        //public void Move()
        //{
        //    //Copy();
        //    //Delete();
        //}

        //public void SelectDisk(FilesPanel panel)
        //{
        //    int positionCursor = 0;
        //    View view = new View();
        //    FileAttributes[] allDrives = new RequestToDisk().AllDrives();
        //    view.SelectDrive(panel, positionCursor);
        //    while (true)
        //    {

        //        if (Console.KeyAvailable)
        //        {
        //            ConsoleKeyInfo userKey = Console.ReadKey(true);
        //            switch (userKey.Key)
        //            {
        //                case ConsoleKey.Enter:
        //                    Desktop.GetInstance().ActivePanel.UpdatePath(allDrives[positionCursor].Name);
        //                    Desktop.GetInstance().Update();
        //                    return;
        //                case ConsoleKey.Escape:
        //                    Desktop.GetInstance().Update();
        //                    return;
        //                case ConsoleKey.DownArrow:
        //                    if (positionCursor < allDrives.Length - 1)
        //                    {
        //                        positionCursor++;
        //                    }
        //                    view.SelectDrive(panel, positionCursor);
        //                    break;
        //                case ConsoleKey.UpArrow:
        //                    if (positionCursor > 0)
        //                    {
        //                        positionCursor--;
        //                    }
        //                    view.SelectDrive(panel, positionCursor);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}

        //public void Select()
        //{
        //    View view = new View();
        //    int currentPositionCursor = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
        //    Desktop.GetInstance().ActivePanel.AddBufferSelectedPositionCursor(currentPositionCursor);
        //    view.SelectedItems(Desktop.GetInstance().ActivePanel);
        //}

        public void Tree(string path)
        {
            int countView = Console.WindowHeight - 9;
            int startPosition = 0;
            ConsoleKeyInfo click;
            RequestToDisk request = new RequestToDisk(path);
            View view = new View();
            view.Message("ИДЕТ ПОСТРОЕНИЕ ДЕРЕВА");
            List<FileAttributes> source = request.GetListDirectoryAndFiles();
            view.FPanel(Desktop.GetInstance().ActivePanel);
            Clear clear = new Clear();
            
            FilesPanel viewPanel;
            if (Desktop.GetInstance().ActivePanel.IsLeftPanel)
            {
                viewPanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                viewPanel = Desktop.GetInstance().LeftPanel;
            }

            while (true)
            {
                clear.FPanel(viewPanel);
                view.Tree(viewPanel, source, startPosition);
                click = Console.ReadKey();
                if (click.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (click.Key == ConsoleKey.DownArrow)
                {
                    if (startPosition + countView < source.Count)
                    {
                        startPosition += countView;
                    }
                }
                if (click.Key == ConsoleKey.UpArrow)
                {
                    if(startPosition >= countView)
                    {
                        startPosition -= countView;
                    }
                }

            }
        }

        public void Quit()
        {
            UserData.GetInstance().Serialize();
        }
    }
}
