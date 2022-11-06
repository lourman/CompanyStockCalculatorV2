using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    internal abstract class Item
    {
        private String Name;
        private Decimal Price;

        public Item(String ItemName, Decimal ItemPrice)
        {
            Name = ItemName;
            Price = ItemPrice;

        }

        public String GetName { get { return Name; } }

        public virtual Decimal GetPrice { get { return Price; } }

        public virtual ushort Sell(ushort Qty)
        {
            return Qty;
        }
    }
}
