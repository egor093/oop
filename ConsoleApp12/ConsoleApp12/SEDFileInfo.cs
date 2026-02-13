using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public static class SEDFileInfo
    {
        public static void WriteFileInfo(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                Console.WriteLine($"Полный путь к файлу {fileInfo.Name}: {fileInfo.FullName}\n" +
                    $"Размер: {fileInfo.Length}\n" +
                    $"Расширение: {fileInfo.Extension}\n" +
                    $"Дата создания: {fileInfo.CreationTime}\n" +
                    $"Дата изменения: {fileInfo.LastWriteTime}");
            }
            else
                throw new FileNotFoundException();
        }
    }
}
