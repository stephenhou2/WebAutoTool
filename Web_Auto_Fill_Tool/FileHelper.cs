using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Web_Auto_Fill_Tool
{
    internal static class FileHelper
    {
        public static void CreateDirectory(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SavePyFile(string str,string path)
        {
            File.WriteAllText(path, str);
        }
    }
}
