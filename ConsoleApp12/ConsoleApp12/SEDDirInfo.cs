using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public static class SEDDirInfo
    {

        public static void WriteDirInfo(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}\n" +
                    $"Время создания: {dirInfo.CreationTime}\n" +
                    $"Количество поддиректориев: {dirInfo.GetDirectories().Length}\n" +
                    $"Список родительских директориев: {dirInfo.Parent}");
            }
            else
                throw new ArgumentException();
        }
    }
}
