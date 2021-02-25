using System.IO;

namespace FileManager
{
    /// <summary>
    /// Тип делегата для события OnProgressChanged
    /// </summary>
    /// <param name="Persentage">Проценты</param>
    /// <param name="Cancel">Закрытие потока</param>
    public delegate void ProgressChangeDelegate(double Persentage, ref bool Cancel);

    /// <summary>
    /// Тип делегата для события OnComplete
    /// </summary>
    public delegate void Completedelegate();

    /// <summary>
    /// Класс для копирования файла с возможностью отслеживания промежуточных
    /// сосотояний и завершения процесса через события
    /// </summary>
    public class CustomFileCopy
    {
        /// <summary>
        /// Путь к файлу для копирования
        /// </summary>
        public string SourceFilePath { get; set; }

        /// <summary>
        /// Путь назначения
        /// </summary>
        public string DestFilePath { get; set; }

        /// <summary>
        /// Промежуточное событие
        /// </summary>
        public event ProgressChangeDelegate OnProgressChanged;

        /// <summary>
        /// Событие, генерирующееся при завершении процесса копирования
        /// </summary>
        public event Completedelegate OnComplete;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="Source">Текущий путь к расположению файла</param>
        /// <param name="Dest">Путь к директории назначения</param>
        public CustomFileCopy(string Source, string Dest)
        {
            SourceFilePath = Source;
            DestFilePath = Dest;

            OnProgressChanged += delegate { };
            OnComplete += delegate { };
        }

        /// <summary>
        /// Копирование файла
        /// </summary>
        public void Copy()
        {
            byte[] buffer = new byte[1024 * 64]; // 64Kb buffer
            bool cancelFlag = false;

            using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                using FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write);
                long totalBytes = 0;
                int currentBlockSize = 0;

                while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalBytes += currentBlockSize;
                    double persentage = (double)totalBytes * 100.0 / fileLength;

                    dest.Write(buffer, 0, currentBlockSize);

                    cancelFlag = false;
                    OnProgressChanged(persentage, ref cancelFlag);

                    if (cancelFlag == true)
                    {
                        break;
                    }
                }
            }

            OnComplete();
        }
    }
}
