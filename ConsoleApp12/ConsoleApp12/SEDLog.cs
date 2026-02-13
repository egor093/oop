using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public static class SEDLog
    {
        private static StreamWriter logfile;
        private static string pathLog = @"E:\study\oop\ConsoleApp12\ConsoleApp12\SEDLogfile.txt";
        public static void WriteInLog(string action, string fileName = "", string path = "")
        {
            if (fileName.Length != 0 && path.Length != 0)
            {
                using (logfile = new StreamWriter(pathLog, true))
                {
                    logfile.WriteLine($"{DateTime.Now.ToString()}");
                    logfile.WriteLine($"Действие: {action}");
                    logfile.WriteLine($"Имя файла: {fileName}");
                    logfile.WriteLine($"Путь к файлу: {path}");
                    logfile.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                }
            }
            else
            {
                using (logfile = new StreamWriter(pathLog, true))
                {
                    logfile.WriteLine($"{DateTime.Now.ToString()}");
                    logfile.WriteLine($"Действие: {action}");
                    logfile.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                }
            }
        }

        public static void FindInfo()
        {
            var output = new StringBuilder();

            using (var stream = new StreamReader(pathLog))
            {
                var textline = "";
                var isActual = false;
                while (!stream.EndOfStream)
                {
                    isActual = false;
                    textline = stream.ReadLine();
                    if (textline != "" && DateTime.Parse(textline).Day == DateTime.Now.Day)
                    {
                        isActual = true;
                        textline += Environment.NewLine;
                        output.AppendFormat(textline);
                    }

                    textline = stream.ReadLine();
                    while (textline != "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬")
                    {
                        if (isActual)
                        {
                            textline += Environment.NewLine;
                            output.AppendFormat(textline);
                        }

                        textline = stream.ReadLine();
                    }

                    if (isActual)
                    {
                        output.AppendFormat("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                        output.AppendFormat(Environment.NewLine);
                    }
                }
            }

            using (var stream = new StreamWriter(@"E:\study\oop\ConsoleApp12\ConsoleApp12\LogV2.txt"))
            {
                stream.WriteLine(output.ToString());
            }
        }
    }
}
