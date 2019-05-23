using System;
using System.IO;

namespace CsvProcessor.Test
{
    public class FileUtility : IDisposable
    {
        private StreamWriter fileOutput;
        private readonly TextWriter oldOutput;

        public FileUtility(string outFileName)
        {
            oldOutput = Console.Out;
            fileOutput = new StreamWriter(new FileStream(outFileName, FileMode.Create))
                        {
                            AutoFlush = true
                        };

            Console.SetOut(fileOutput);
        }

        public void Dispose()
        {
            Console.SetOut(oldOutput);
            fileOutput.Close();
        }
    }
}
