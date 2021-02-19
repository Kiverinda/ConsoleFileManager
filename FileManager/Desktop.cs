using System;


namespace FileManager
{
    public static class Desktop
    {
        public static FilesPanel LeftPanel { get; set; }
        public static FilesPanel RightPanel { get; set; }
        public static FilesPanel ActivePanel { get; set; }

        public static void Init()
        {
            LeftPanel = new FilesPanel(true);
            RightPanel = new FilesPanel(false);
            ActivePanel = LeftPanel;
            ViewDesktop();
            Explorer();
        }

        public static void ViewDesktop()
        {
            Console.Clear();
            View view = new View();
            view.FPanel(LeftPanel);
            view.FPanel(RightPanel);
            view.Footer();
            view.CurrentCursor(ActivePanel);
        }

        public static void Update()
        {
            ViewDesktop();
        }

        public static void Explorer()
        {
            ConsoleKeyInfo click;

            do
            {
                click = Console.ReadKey(true);
                if (((click.Modifiers & ConsoleModifiers.Control) != 0) && (click.Key == ConsoleKey.F1))
                {
                    Action.SelectDisk(Desktop.ActivePanel);
                }
                else if (click.Key == ConsoleKey.Tab)
                {
                    Action.ChangeActivePanel();
                }
                else if (click.Key == ConsoleKey.Enter)
                {
                    Action.Enter();
                }
                else if (click.Key == ConsoleKey.UpArrow)
                {
                    Action.UpArrow();
                }
                else if (click.Key == ConsoleKey.DownArrow)
                {
                    Action.DownArrow();
                }
                else if (click.Key == ConsoleKey.Spacebar)
                {
                    Action.Select();
                }
                else if (click.Key == ConsoleKey.F2)
                {
                    Action.CreateFile();
                }
                else if (click.Key == ConsoleKey.F3)
                {
                    Action.EditFile();
                }
                else if (click.Key == ConsoleKey.F5)
                {
                    Action.Copy();
                }
                else if (click.Key == ConsoleKey.F6)
                {
                    Action.Move();
                }
                else if (click.Key == ConsoleKey.F7)
                {
                    Action.CreateDirectory();
                }
                else if (click.Key == ConsoleKey.F8)
                {
                    Action.Delete();
                }

            } while (click.Key != ConsoleKey.Escape);

        }
    }
}
