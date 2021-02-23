using System;
using System.IO;
using System.Xml.Serialization;

namespace FileManager
{
    class ErrorLog
    {
        public ErrorLog(object ob, string message, string trace)
        {
            Log(ob, message, trace);
        }

        private void Log(object ob, string message, string trace)
        {
            if (!File.Exists(@"errorlog.txt"))
            {
                using (File.Create(@"errorlog.txt")) { }
            }
            using StreamWriter w = File.AppendText(@"errorlog.txt");
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{ob}");
            w.WriteLine($"  :{message}");
            w.WriteLine($"  :{trace}");
            w.WriteLine("-------------------------------");
        }
    }
}
