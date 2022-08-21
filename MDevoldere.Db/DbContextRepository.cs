using MDevoldere.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Db
{
    public class DbContextRepository<T> : Repository<T>, IDbRepository<T> where T : Model, new()
    {
        protected DbContext db;

        protected new DbSet<T> items;

        public DbContextRepository(DbContext ctx) : base() 
        {
            db = ctx;
            items = db.Set<T>();
        }

        public bool Save()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
