using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Domain
{
    public class Repository<T> : IRepository<T> where T : Model, new()
    {
        public int Count { get => items.Count; }

        protected ICollection<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public virtual bool IsValid(T item)
        {
            var context = new ValidationContext(item, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(item, context, results, true))
            {
                return false;
            }
            return true;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return items;
        }

        public virtual T? Get(int id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }

        public virtual T? Add(T item)
        {
            items.Add(item);
            return item;
        }

        public virtual bool Update(T item)
        {
            if (Get(item.Id) is T result)
            {
                return true;
            }
            return false;
        }

        public virtual bool Delete(int id)
        {
            if (Get(id) is T result)
            {
                return true;
            }
            return false;
        }
    }
}
