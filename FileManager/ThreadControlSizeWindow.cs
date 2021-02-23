using System;
using System.Threading;

namespace FileManager
{
    public delegate void ResizeWindow(ref bool Close);

    class ThreadControlSizeWindow
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public event ResizeWindow Resize;

        public ThreadControlSizeWindow()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            Resize += delegate { };
        }

        public void Start()
        {
            Thread controlSize = new Thread(Control);
            controlSize.Priority = ThreadPriority.BelowNormal;
            controlSize.Start();
        }

        public void Control()
        {
            bool Close = false;
            while (!Close)
            {
                if (Console.WindowHeight != Height || Console.WindowWidth != Width)
                {
                    Height = Console.WindowHeight;
                    Width = Console.WindowWidth;
                    Resize(ref Close);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
