using MDevoldere.Domain;
using MDevoldere.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Db.Json
{
    public class DbJsonRepository<T> : Repository<T>, IDbRepository<T> where T : Model, new()
    {
        protected string file;

        public DbJsonRepository(string filename)
        {
            file = filename;
            items = LocalJson.LoadJson<ICollection<T>>(filename + ".json") ?? new List<T>();
        }

        public bool Save()
        {
            try
            {
                return LocalJson.SaveJson(file, items);
            }
            catch { return false; }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await LocalJson.SaveJsonAsync(file, items);
            }
            catch { return false; }
        }
    }
}
