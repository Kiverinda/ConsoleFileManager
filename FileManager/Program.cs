using System;


namespace FileManager
{
    class Program
    {
        static void Main()
        {
            Console.SetBufferSize(170, 55);
            Console.SetWindowSize(170, 55);
            ThreadControlSizeWindow Control = ThreadControlSizeWindow.GetInstance();
            Control.Resize += ControlSize;
            Control.Start();
            
            Desktop.GetInstance().Run();
        }

        static void ControlSize()
        {
            Desktop.GetInstance().ViewDesktop();
        }
    }
}  
