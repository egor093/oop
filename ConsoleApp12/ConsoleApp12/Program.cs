using System;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("*****************************************************************************************************");
                SEDDiskInfo.WriteDiskInfo("E:\\");
                SEDLog.WriteInLog("SEDDiskInfo.getFreeDrivesSpace()");

                Console.WriteLine("******************************************************************************************************");
                SEDFileInfo.WriteFileInfo(@"E:\study\oop\ConsoleApp12\12_Потоки_файловая система.pdf");
                SEDLog.WriteInLog("SEDFileInfo.WriteFileInfo()", "SEDLogfile.txt", @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDLogfile.txt");

                Console.WriteLine("******************************************************************************************************");
                SEDDirInfo.WriteDirInfo(@"E:\study\oop");
                SEDLog.WriteInLog("SEDDirInfo.WriteDirInfo()", @"E:\study\oop");

                BVSFileManager.InspectDriver("E:\\");
                SEDLog.WriteInLog("SEDFileManager.InspectDriver()", "E:\\");
                BVSFileManager.CopyFiles(@"E:\study\oop\Лекции", ".docx");
                SEDLog.WriteInLog("SEDFileManager.CopyFiles()", @"E:\study\oop\Лекции");
                Console.WriteLine("******************************************************************************************************");
                BVSFileManager.CreateArchive(@"E:\study\oop\ConsoleApp12\ConsoleApp12\ForArchive");
                SEDLog.WriteInLog("SEDFileManager.CreateArchive()");

                SEDLog.FindInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
