using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public static class BVSFileManager
    {
        public static void InspectDriver(string driverName)
        {
            Directory.CreateDirectory(@"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect");
            File.Create(@"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDdirinfo.txt").Close(); 
            var currentDrive = DriveInfo.GetDrives().Single(x => x.Name == driverName);

            using (StreamWriter file = new StreamWriter(@"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDdirinfo.txt"))
            {
                file.WriteLine("Список папок:");
                foreach (var s in currentDrive.RootDirectory.GetDirectories())
                {
                    file.WriteLine(s);
                }
                file.WriteLine("Список файлов:");
                foreach (var f in currentDrive.RootDirectory.GetFiles())
                {
                    file.WriteLine(f);
                }
            }

            File.Copy(@"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDdirinfo.txt", @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDdirinfoCopy.txt", true);
            File.Delete(@"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDdirinfo.txt");
        }

        public static void CopyFiles(string path, string extention)
        {
            string targetPath = @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDFiles";
            Directory.CreateDirectory(targetPath);

            DirectoryInfo sourceDir = new DirectoryInfo(path);

            foreach (var file in sourceDir.GetFiles())
            {
                if (file.Extension.Equals(extention, StringComparison.OrdinalIgnoreCase))
                {
                    file.CopyTo(Path.Combine(targetPath, file.Name), true);
                }
            }

            string inspectPath = @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDFiles";
            if (Directory.Exists(targetPath))
            {
                if (Directory.Exists(inspectPath))
                {
                    Directory.Delete(inspectPath, true);
                }
                Directory.Move(targetPath, inspectPath);
            }
        }

        public static void CreateArchive(string dir)
        {
            string name = @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDInspect\SEDFiles\";
            string archivePath = name.TrimEnd('\\') + ".zip"; 

            if (!File.Exists(archivePath))
            {
                ZipFile.CreateFromDirectory(name, archivePath);
                Console.WriteLine($"Архив создан: {archivePath}");
            }
            else
            {
                Console.WriteLine($"Архив уже существует: {archivePath}");
            }

            if (Directory.Exists(dir))
            {
                ZipFile.ExtractToDirectory(archivePath, dir, overwriteFiles: true);
                Console.WriteLine($"Файлы извлечены в папку: {dir}");
            }
        }
    }
}