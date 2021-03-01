using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Подготовка к копированию обьектов
    /// </summary>
    class Copy : ICommand<ConsoleKeyInfo>
    {
        public FilesPanel ActivePanel { get; set; }
        public FilesPanel TargetPanel { get; set; }
        //public View ViewCopy { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="activePanel">Активная панель</param>
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

            //ViewCopy = new View();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Copy()
        {
            //ViewCopy = new View();
        }

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F5;
        }

        /// <summary>
        /// Запуск цепи проверок для копирования и обновление окна программы после копирования
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            SelectTargetPanel();
            CheckBufferSelected();
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Проверяется наличие выделенных обьектов и если такие есть, то они поочередно отправляются на проверку.
        /// Если выделенных обьектов нет, то на проверку отправляется обьект, на котором находится курсор. 
        /// </summary>
        public void CheckBufferSelected()
        {
            if (ActivePanel.BufferSelectedPositionCursor.Count == 0)
            {
                Check();
            }
            else
            {
                foreach (int i in ActivePanel.BufferSelectedPositionCursor)
                {
                    Attributes attributes = ActivePanel.CurrentListDirAndFiles[i];
                    CheckingExistenceObjectInDestinationFolder(attributes);
                }
            }
        }

        /// <summary>
        /// Вычисление пути назначения
        /// </summary>
        public void SelectTargetPanel()
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
        }

        /// <summary>
        /// Проверка равентства текущего пути и пути назначения
        /// </summary>
        public void Check()
        {
            if (ActivePanel.CurrentPath == TargetPanel.CurrentPath)
            {
                new View().Message($"НЕЛЬЗЯ СКОПИРОВАТЬ ФАЙЛ В ТЕКУЩИЙ КАТАЛОГ");
                Console.ReadKey();
                return;
            }

            CheckingCorrectnessCurrentPath();
        }

        /// <summary>
        /// Проверка корректности текущего пути
        /// </summary>
        public void CheckingCorrectnessCurrentPath()
        {

            List<Attributes> currentList = ActivePanel.CurrentListDirAndFiles;

            if (currentList[ActivePanel.AbsoluteCursorPosition].Name == "[..]")
            {
                return;
            }

            CheckingExistenceObjectInDestinationFolder(currentList[ActivePanel.AbsoluteCursorPosition]);
        }

        /// <summary>
        /// Проверка существования текущего обьекта в папке назначения
        /// </summary>
        /// <param name="attributes">Атрибуты обьекта копирования</param>
        public void CheckingExistenceObjectInDestinationFolder(Attributes attributes)
        {
            if (attributes.IsFile)
            {
                if(File.Exists(Path.Combine(TargetPanel.CurrentPath, attributes.Name)))
                {
                    new View().Message($"ФАЙЛ УЖЕ СУЩЕСТВУЕТ");
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
                    new View().Message($"ДИРЕКТОРИЯ УЖЕ СУЩЕСТВУЕТ");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    CheckFreeSpaceDirectory(attributes);
                }
            }
        }

        /// <summary>
        /// Проверка достаточности свободного места для файла на диске - получателе
        /// </summary>
        /// <param name="attributes">Атрибуты копируемого файла</param>
        public void CheckFreeSpaceFile(Attributes attributes)
        {
            long freeSpace = new RequestToDisk(TargetPanel.CurrentPath).GetFreeSpace();
            long size = new FileInfo(attributes.Path).Length;
            if (size > freeSpace)
            {
                new View().Message($"НЕ ДОСТАТОЧНО МЕСТА НА ДИСКЕ  {Path.GetPathRoot(TargetPanel.CurrentPath)}");
                Console.ReadKey();
                return;
            }
            else
            {
                CopyFile(attributes.Path);
            }
        }

        /// <summary>
        /// Проверка достаточности свободного места для директории на диске - получателе
        /// </summary>
        /// <param name="attributes">Атрибуты копируемой директории</param>
        public void CheckFreeSpaceDirectory(Attributes attributes) 
        {
            RequestToDisk list = new RequestToDisk(attributes.Path);
            List<Attributes> newTreeFiles = list.GetListDirectoryAndFiles();
            long freeSpace = new RequestToDisk(TargetPanel.CurrentPath).GetFreeSpace();
            long size = list.GetSizeDirectory(newTreeFiles);
            
            if (size > freeSpace)
            {
                new View().Message($"НЕ ДОСТАТОЧНО МЕСТА НА ДИСКЕ  {Path.GetPathRoot(TargetPanel.CurrentPath)}");
                Console.ReadKey();
                return;
            }
            else
            {
                CopyDirectory(attributes.Path, newTreeFiles);
            }
        }

        /// <summary>
        /// Создание экземпляра класса для копирования файла, добавление к событию метода, получающего проценты
        /// выполнения и запуск потоков для копирования
        /// </summary>
        /// <param name="path">Путь к текущему</param>
        public void CopyFile(string path)
        {
            new View().Copy(Path.GetFileName(path), TargetPanel.CurrentPath);
            CustomFileCopy cs = new CustomFileCopy(path, path.Replace(ActivePanel.CurrentPath, TargetPanel.CurrentPath));
            cs.OnProgressChanged += ViewPersentageToConsole;
            //cs.OnComplete += ViewVessageCompleteToConsole;
            cs.Copy();
        }

        /// <summary>
        /// Копирование директории
        /// </summary>
        /// <param name="path">Путь к директории</param>
        /// <param name="newTreeFiles">Внутреннее дерево файлов и директорий</param>
        public void CopyDirectory(string path, List<Attributes> newTreeFiles)
        {
            string nameDirectory = Path.Combine(TargetPanel.CurrentPath, Path.GetFileName(path));
            Directory.CreateDirectory(nameDirectory);

            foreach (Attributes attributes in newTreeFiles)
            {
                if (!attributes.IsFile)
                {
                    Directory.CreateDirectory(attributes.Path.Replace(ActivePanel.CurrentPath, TargetPanel.CurrentPath));
                }
            }
            foreach (Attributes attributes in newTreeFiles)
            {
                if (attributes.IsFile)
                {
                    CopyFile(attributes.Path);
                }
            }
        }

        /// <summary>
        /// Получение процентов и завершение процесса копирования 
        /// </summary>
        /// <param name="persentage">Процент выполнения</param>
        /// <param name="cancelFlag">Закрытие потоков</param>
        public void ViewPersentageToConsole(double persentage, ref bool cancelFlag)
        {
            new View().CopyPersentage(persentage);
            if (persentage == 100)
            {
                cancelFlag = true;
            }
        }

        //public void ViewVessageCompleteToConsole()
        //{
        //    View view = new View();
        //    view.Message("Файл скопирован!");
        //    Console.ReadKey();
        //}

    }
}

