using System;


namespace FileManager
{
    class Program
    {
        static void Main()
        {
            
            Console.SetBufferSize(170, 55);
            Console.SetWindowSize(170, 55);
            ThreadControlSizeWindow Control = new ThreadControlSizeWindow();
            Control.Resize += ControlSize;
            Control.Start();
            
            Desktop.GetInstance().Run();
        }

        static void ControlSize(ref bool Close)
        {
            Desktop.GetInstance().ViewDesktop();
            Close = false;
        }
    }
}
