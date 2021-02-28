using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{

    /// <summary>
    /// Класс отвечающий за создание главного окна приложения 
    /// </summary>
    public class Desktop
    {
        private static Desktop instance;

        /// <summary>
        /// Левая файловая панель
        /// </summary>
        public FilesPanel LeftPanel { get; set; }

        /// <summary>
        /// Правая файловая панель
        /// </summary>
        public FilesPanel RightPanel { get; set; }

        /// <summary>
        /// Активная файловая панель
        /// </summary>
        public FilesPanel ActivePanel { get; set; }

        /// <summary>
        /// Список возможных действий с файлами и директориями
        /// </summary>
        public List<ICommand<ConsoleKeyInfo>> Commands { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        private Desktop()
        {
            LeftPanel = new FilesPanel(true);
            RightPanel = new FilesPanel(false);
            ActivePanel = LeftPanel;
            Commands = new List<ICommand<ConsoleKeyInfo>>() { 
                new SelectDisk(), 
                new SelectCommandLine(), 
                new ChangeActivePanel(), 
                new KeyEnter(), 
                new UpArrow(),
                new DownArrow(),
                new SelectObject(),
                new CreateFile(),
                new RenameObject(),
                new EditFile(),
                new Copy(),
                new Delete(),
                new Move(),
                new CreateDirectory(),
                new Tree(),
                new Quit(),
                new Help()
            };
        }

        /// <summary>
        /// Реализация паттерна singlton
        /// </summary>
        /// <returns></returns>
        public static Desktop GetInstance()
        {
            if (instance == null)
            {
                if (File.Exists("config.json"))
                {
                    instance = new Desktop();
                    UserData data = UserData.GetInstance();
                    data.Deserialize();
                }
                else
                {
                    instance = new Desktop();
                }
            }
            return instance;
        }

        /// <summary>
        /// Запуск визуализации приложения и навигации
        /// </summary>
        public void Run()
        {
            ViewDesktop();
            Explorer();
        }

        /// <summary>
        /// Очистка окна и вывод на экран файловых панелей, командной строки
        /// нажней подсказки горячих клавиш и текущего положения курсора
        /// </summary>
        public void ViewDesktop()
        {
            Console.Clear();
            new Clear(); 
            View view = new View();
            view.FPanel(LeftPanel);
            view.FPanel(RightPanel);
            view.Footer();
            view.CurrentCursor(ActivePanel);
        }

        /// <summary>
        /// Обновление данных в файловой панеле
        /// </summary>
        /// <param name="panel"></param>
        public void UpdatePanel(FilesPanel panel)
        {
            new Clear().FPanel(panel);
            View view = new View();
            panel.UpdatePath(panel.CurrentPath);
            view.FPanel(panel);
            view.CurrentCursor(ActivePanel);
        }

        /// <summary>
        /// Обновление всего главного окна приложения
        /// </summary>
        public void Update()
        {
            LeftPanel.UpdatePath(LeftPanel.CurrentPath);
            RightPanel.UpdatePath(RightPanel.CurrentPath);
            ViewDesktop();
        }

        /// <summary>
        /// Навигация по файловым панелям и выбор действия с обьектами
        /// </summary>
        public void Explorer()
        {
            ConsoleKeyInfo click;
            bool quit = false;
            while (!quit)
            {
                click = Console.ReadKey(true);
                foreach (ICommand<ConsoleKeyInfo> command in Commands)
                {
                    if (command.CanExecute(click))
                    {
                        quit = command.Execute();
                        break;
                    }
                }
            } 
        }
    }
}
