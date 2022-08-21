using MDevoldere.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Db.Sqlite
{
    abstract public class DbContextSqlite : DbContext
    {
        private string dbPath;

        public DbContextSqlite(string filename)
        {
            dbPath = LocalPath.AppDataFile("mdevoldere", filename + ".sqlite");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
