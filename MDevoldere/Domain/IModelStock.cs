namespace MDevoldere.Domain
{
    public interface IModelStock<T> where T : Model
    {
        T Item { get; }
        int Max { get; }
        int Quantity { get; }
        int Free { get; }
        double Ratio { get; }
        double PercentFull { get; }

        bool CanPull(int quantity);
        bool TryPull(int quantity);
        bool CanPush(int quantity);
        bool TryPush(int quantity);
        int Push(int quantity);
        bool TryTranferFrom(int quantity, IModelStock<T> o);
        bool TryTranferTo(int quantity, IModelStock<T> o);
    }
}