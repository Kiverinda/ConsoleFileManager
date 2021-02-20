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
            LeftPanel.UpdatePath(LeftPanel.CurrentPath);
            RightPanel.UpdatePath(RightPanel.CurrentPath);
            LeftPanel.BufferSelectedPositionCursor.Clear();
            RightPanel.BufferSelectedPositionCursor.Clear();
            ActivePanel.AbsoluteCursorPosition = 0;
            ActivePanel.RelativeCursorPosition = 0;
            ActivePanel.FirstLineWhenScrolling = 0;
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
                    Update();
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
                    Update();
                }
                else if (click.Key == ConsoleKey.F3)
                {
                    Action.EditFile();
                }
                else if (click.Key == ConsoleKey.F5)
                {
                    Action.Copy();
                    Update();
                }
                else if (click.Key == ConsoleKey.F6)
                {
                    Action.Move();
                    Update();
                }
                else if (click.Key == ConsoleKey.F7)
                {
                    Action.CreateDirectory();
                    Update();
                }
                else if (click.Key == ConsoleKey.F8)
                {
                    Action.Delete();
                    Update();
                }

            } while (click.Key != ConsoleKey.Escape);

        }
    }
}
