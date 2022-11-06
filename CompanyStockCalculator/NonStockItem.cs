using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    internal abstract class NonStockItem : Item
    {
        public NonStockItem(String Name, Decimal Price) : base(Name, Price)
        {

        }

        public abstract void Prepare(); // method to prepare item

        public override string ToString()
        {
            return base.GetName;
        }

    }

    internal class MadeToOrderFood : NonStockItem, IAllergen
    {
        private Dictionary<Ingredient, int> IngredientsQty;

        public MadeToOrderFood(String Name, Decimal Price) : base(Name, Price)
        {
            IngredientsQty = new Dictionary<Ingredient, int>();
        }

        public void AddIngredient(Ingredient Ingredient, int QtyGrams)
        {
            IngredientsQty.Add(Ingredient, QtyGrams);

        }


        public bool Contains(Ingredient Ingredient)
        {
            bool result;
            result = IngredientsQty.ContainsKey(Ingredient);
            return result;
        }

        public HashSet<String> AllergySet()
        {
            HashSet<String> result = new HashSet<string>();

            foreach (Ingredient ing in IngredientsQty.Keys)
            {
                List<String> ingAllergens = ing.Allergens;
                foreach (String allergen in ingAllergens)
                {
                    result.Add(allergen);
                }
            }
            return result;
        }

        
        public bool AllergenPresent(String Allergen)
        {
            bool result;

            result = (AllergySet().Contains(Allergen));

            return result;
        }


        public override void Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
