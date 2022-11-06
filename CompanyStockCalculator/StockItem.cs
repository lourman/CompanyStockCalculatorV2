using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    internal class StockItem : Item
    {
        uint StockLevel;

        public StockItem(String ItemName, Decimal ItemPrice) : base(ItemName, ItemPrice)
        {
            StockLevel = 0;
        }

        public void Restock(ushort Qty)
        {
            StockLevel += Qty;
        }

        public override ushort Sell(ushort Qty)
        {
            ushort result = 0;

            if (Qty <= StockLevel)
            {
                result = Qty;
            }
            else
            {
                result = (ushort)StockLevel;
            }
            StockLevel -= result;
            return result;
        }

        public uint GetStockLevel
        {
            get
            {
                return StockLevel;
            }
        }

        public override string ToString()
        {
            return base.GetName + " (" + StockLevel.ToString() + ")";
        }
    }
}
