using MDevoldere.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Db
{
    public interface IDbRepository<T> : IRepository<T> where T : Model, new()
    {
        bool Save();
        Task<bool> SaveAsync();
    }
}
