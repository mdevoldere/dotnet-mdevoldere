using MDevoldere.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Db.Json
{
    public class DbContextJson
    {
        public string DbPath { get; set; }

        public DbContextJson(string filename)
        {
            DbPath = LocalPath.AppDataFile("mdevoldere", filename + ".json");
        }
    }
}
