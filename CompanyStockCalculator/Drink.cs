using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    internal class Drink : StockItem
    {
        private bool canned; // True if it is a can or false if not
        private int volume; // The volume of the drink in litres

        /// <summary>
        /// Property for retuning the volume in 
        /// </summary>
        public int GetVolume { get => volume; }

        public bool IsCanned { get => canned; }

        /// <summary>
        /// Constructor of a drink item
        /// </summary>
        /// <param name="Name">The name of the drink stored in the base</param>
        /// <param name="Price">The price of the drink stored in the base</param>
        /// <param name="IsCanned">The drink if canned or not</param>
        /// <param name="DrinkVolume">The volume of the drink item</param>
        public Drink(String Name, Decimal Price, bool IsCanned, int DrinkVolume) : base(Name, Price)
        {
            canned = IsCanned;
            volume = DrinkVolume;
        }

        /// <summary>
        /// Calculates the price per ml
        /// </summary>
        /// <returns>Price per ml ratio</returns>
        public Decimal PricePerML()
        {
            Decimal result;

            result = GetPrice / volume;

            return result;
        }
    }
}
