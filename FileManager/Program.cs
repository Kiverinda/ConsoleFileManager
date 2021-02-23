﻿using System;


namespace FileManager
{
    class Program
    {
        static void Main()
        {
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
