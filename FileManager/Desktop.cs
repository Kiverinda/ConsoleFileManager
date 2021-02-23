using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    [Serializable]
    public class Desktop
    {
        private static Desktop instance;
        public FilesPanel LeftPanel { get; set; }
        public FilesPanel RightPanel { get; set; }
        public FilesPanel ActivePanel { get; set; }
        public List<ICommand> Commands { get; set; }

        private Desktop()
        {
            LeftPanel = new FilesPanel(true);
            RightPanel = new FilesPanel(false);
            ActivePanel = LeftPanel;
            Commands = new List<ICommand>() { 
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
                new Quit()};
        }

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

        public void Run()
        {
            ViewDesktop();
            Explorer();
        }

        public void ViewDesktop()
        {
            Console.Clear();
            View view = new View();
            view.FPanel(LeftPanel);
            view.FPanel(RightPanel);
            view.Footer();
            view.CurrentCursor(ActivePanel);
        }

        public void Update()
        {
            LeftPanel.UpdatePath(LeftPanel.CurrentPath);
            RightPanel.UpdatePath(RightPanel.CurrentPath);
            ViewDesktop();
        }


        public void Explorer()
        {
            ConsoleKeyInfo click;
            bool quit = false;
            while (!quit)
            {
                click = Console.ReadKey(true);
                foreach (ICommand command in Commands)
                {
                    if (command.CanExexute(click))
                    {
                        quit = command.Execute();
                    }
                }
            } 
        }
    }
}
