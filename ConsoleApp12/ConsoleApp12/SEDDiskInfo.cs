using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public static class SEDDiskInfo
    {
        public static void WriteDiskInfo(string driverName)
        {
            var driver = DriveInfo.GetDrives().Single(d => d.Name == driverName);
            Console.WriteLine($"Имя диска: {driver.Name}");
            Console.WriteLine($"Свободное место на диске: {driver.TotalFreeSpace}");
            Console.WriteLine($"Файловая система: {driver.DriveFormat}");
            Console.WriteLine($"Тип диска: {driver.DriveType}");
            Console.WriteLine($"Метка тома: {driver.VolumeLabel}");
            Console.WriteLine($"Объём: {driver.TotalSize}");
            Console.WriteLine($"Доступный объём: {driver.AvailableFreeSpace}");
        }
    }
}
