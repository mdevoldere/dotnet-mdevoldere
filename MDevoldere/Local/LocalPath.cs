using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Local
{
    public class LocalPath
    {
        public static string AppDataDirectory(string foldername)
        {
            
            string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), foldername);
            DirectoryCreate(p);
            return p;
        }

        public static string AppDataFile(string foldername, string filename)
        {
            return Path.Combine(AppDataDirectory(foldername), filename);
        }

        public static bool DirectoryCreate(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch { return false; }
        }
    }
}
