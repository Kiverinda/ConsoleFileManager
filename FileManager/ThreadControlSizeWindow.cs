using System;
using System.Threading;

namespace FileManager
{
    public delegate void ResizeWindow();

    class ThreadControlSizeWindow
    {
        private static ThreadControlSizeWindow instance { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Close { get; set; }
        public event ResizeWindow Resize;

        public ThreadControlSizeWindow()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            Resize += delegate { };
        }

        public static ThreadControlSizeWindow GetInstance()
        {
            if (instance == null)
            {
                    instance = new ThreadControlSizeWindow();
            }
            return instance;
        }

        public void Start()
        {
            Thread controlSize = new Thread(Control);
            controlSize.Priority = ThreadPriority.BelowNormal;
            controlSize.Start();
        }

        public void Control()
        {
            Close = false;
            while (!Close)
            {
                if (Console.WindowHeight != Height || Console.WindowWidth != Width)
                {
                    Height = Console.WindowHeight;
                    Width = Console.WindowWidth;
                    Resize();
                    Thread.Sleep(100);
                }
            }
        }
    }
}
