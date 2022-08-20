using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Domain
{
    public class ModelStock<T> : IModelStock<T> where T : Model
    {
        private readonly static object locker = new();

        public T Item { get; protected set; }

        public int Max { get; protected set; }

        public int Quantity { get; protected set; }

        public double Ratio { get => Math.Round((double)Quantity / Max, 4); }

        public double PercentFull { get => Math.Round(Quantity / Max * 100d, 2); }

        public int Free { get => Max - Quantity; }

        public ModelStock(T item, int max, int quantity)
        {
            Item = item;
            Max = max;
            Quantity = quantity;
        }

        public ModelStock(T item, int max = 100) : this(item, max, 0)
        { }

        public bool CanPush(int quantity)
        {
            return quantity <= Quantity;
        }

        public bool TryPush(int quantity)
        {
            if (quantity <= Free)
            {
                Quantity += quantity;
                return true;
            }
            return false;
        }

        public int Push(int quantity)
        {
            lock (locker)
            {
                if (TryPush(quantity))
                {
                    return 0;
                }
                else if (Free > 0)
                {
                    int r = quantity - Free;
                    Quantity = Max;
                    return r;
                }
                return quantity;
            }
        }

        public bool CanPull(int quantity)
        {
            return quantity <= Free;
        }

        public bool TryPull(int quantity)
        {
            if (quantity <= Quantity)
            {
                Quantity -= quantity;
                return true;
            }
            return false;
        }

        public bool TryTranferTo(int quantity, IModelStock<T> o)
        {
            lock (locker)
            {
                if (CanPull(quantity) && o.CanPush(quantity))
                {
                    return TryPull(quantity) && o.TryPush(quantity);
                }
                return false;
            }
        }

        public bool TryTranferFrom(int quantity, IModelStock<T> o)
        {
            lock (locker)
            {
                if (o.CanPull(quantity) && CanPush(quantity))
                {
                    return o.TryPull(quantity) && TryPush(quantity);
                }
                return false;
            }
        }
    }
}
