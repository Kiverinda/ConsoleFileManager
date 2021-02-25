using System;
using System.IO;
using System.Xml.Serialization;

namespace FileManager
{
    /// <summary>
    /// Класс для записи логов об ошибках в файл
    /// </summary>
    public class ErrorLog
    {
        
        /// <summary>
        /// Получение информации об ошибке и запись ее в файл
        /// </summary>
        /// <param name="ob">Класс в котором произошла ошибка</param>
        /// <param name="message">Сгенерированное сообщение</param>
        /// <param name="trace">Файл и строка с ошибкой</param>
        public ErrorLog(object ob, string message, string trace)
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
