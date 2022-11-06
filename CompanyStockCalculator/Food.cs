using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    internal class FoodItem : StockItem, IAllergen
    {
        private HashSet<String> Allergens;
        private int KCals;

        /// <summary>
        /// Constructor with 3 parameters.
        /// </summary>
        /// <param name="Name">The name of the item</param>
        /// <param name="Price">The price of the item</param>
        /// <param name="kcals">The callories in kcal of the item</param>
        public FoodItem(String Name, Decimal Price, int kcals) : base(Name, Price)
        {
            Allergens = new HashSet<string>();
            KCals = kcals;
        }

        /// <summary>
        /// Ability to add a single allergen to the specific food item
        /// </summary>
        /// <param name="Allergen"></param>
        public void AddAllergen(String Allergen)
        {
            Allergens.Add(Allergen);
        }

        /// <summary>
        /// Ability to add a range of allergens to the specific food item
        /// </summary>
        /// <param name="Allergens"></param>
        public void AddAllergen(List<String> Allergens)
        {
            foreach (String allergen in Allergens)
            {
                AddAllergen(allergen);
            }
        }

        /// <summary>
        /// Adds a specific rate for food
        /// </summary>
        public override decimal GetPrice
        {
            get
            {
                return base.GetPrice * 1.2m; //tax rate at 20% on food
            }
        }

        /// <summary>
        /// Calculates the price per callorie
        /// </summary>
        /// <returns>The ratio of price to kcal</returns>
        public Decimal PricePerCal()
        {
            decimal result;

            result = GetPrice / KCals;

            return result;
        }

        /// <summary>
        /// The allergy set
        /// </summary>
        /// <returns></returns>
        public HashSet<string> AllergySet()
        {
            return Allergens;
        }

        /// <summary>
        /// Finds if the food item contains a specific allergen
        /// </summary>
        /// <param name="Allergen">The allergen to check for</param>
        /// <returns>True if allergen found, false is not</returns>
        public bool AllergenPresent(string Allergen)
        {
            bool result = Allergens.Contains(Allergen);
            return result;
        }
    }
}
