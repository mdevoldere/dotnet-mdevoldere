
namespace MDevoldere.Domain
{
    public interface IRepository<T> where T : Model, new()
    {
        bool IsValid(T item);
        T? Add(T item);
        bool Delete(int id);
        T? Get(int id);
        IEnumerable<T> GetAll();
        bool Update(T item);
    }
}