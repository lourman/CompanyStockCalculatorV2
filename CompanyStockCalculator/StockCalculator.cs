using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace CompanyStockCalculator
{
    internal class StockCalculator
    {
        // Lists of all types of items being stocked.
        List<Item> itemList;
        //List<Drink> drinksList;

        #region Properties
        /// <summary>
        /// Porperty to return the list of food objects irrespective if they are currently stocked.
        /// </summary>
        public List<Item> Items { get { return itemList; } }

        /// <summary>
        /// Porperty to return the list of food objects currently being stocked.
        /// </summary>
        public List<FoodItem> StockedFoods
        {
            get
            {
                List<FoodItem> result = new List<FoodItem>();

                foreach (FoodItem f in itemList)
                {
                    if (f.GetStockLevel > 0)
                        result.Add(f);
                }
                return result;
            }
        }

        public List<Item> StockedItems
        {
            get
            {
                List<Item> result = new List<Item>();

                foreach (Item f in itemList)
                {
                    if (f is StockItem)
                    {
                        if (((StockItem)f).GetStockLevel > 0)
                            result.Add(f);
                    }
                }
                return result;
            }

        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constrctor initialises all item lists.
        /// Loads the items from the database.
        /// When in DEBUG the connection would be with a test database function.
        /// </summary>
        public StockCalculator()
        {
            itemList = new List<Item>();

#if DEBUG
            TestDB();
#endif

        }
        #endregion

        #region Food Object Methods
        /// <summary>
        /// Returns a list of low stock items according to a limit
        /// </summary>
        /// <param name="MinLevel">The limit by which the list should be constructed</param>
        /// <returns>The list of low stock items</returns>
        public List<StockItem> LowStock(int MinLevel)
        {
            List<StockItem> result = new List<StockItem>();

            foreach (Item i in itemList)
            {
                if (i is StockItem)
                {
                    StockItem f = (StockItem)i;
                    if (f.GetStockLevel <= MinLevel)
                        result.Add(f);
                }
            }
            return result;
        }

        public HashSet<string> GetAllAllergensInBar()
        {
            HashSet<string> allergenlist = new HashSet<string>();
            foreach (Item i in itemList)
            {
                if (i is StockItem)
                {
                    FoodItem food = (FoodItem)i;
                    allergenlist.UnionWith(food.AllergySet());
                }
            }
            return allergenlist;
        }

        public void AddToList(Item f)
        {
            itemList.Add(f);
        }
        #endregion

#if DEBUG
        /// <summary>
        /// Method to create test items for testing the UI of the application.
        /// </summary>
        public void TestDB()
        {
            //drinks
            Drink d = new Drink("Coola", 0.55m, true, 330);
            d.Restock(10);
            itemList.Add(d);

            d = new Drink("Funta", 0.50m, true, 330);
            d.Restock(8);
            itemList.Add(d);

            d = new Drink("Warter", 0.40m, false, 500);
            itemList.Add(d);

            d = new Drink("Miylk", 0.70m, false, 568);
            d.Restock(3);
            itemList.Add(d);


            //ingredients for foods
            Ingredient flour = new Ingredient("Flour"); ;
            flour.AddAllergen("Gluten");
            Ingredient sugar = new Ingredient("Sugar");
            Ingredient cocoa = new Ingredient("Cocoa");
            Ingredient nuts = new Ingredient("Hazelnuts");
            nuts.AddAllergen("Nuts");
            Ingredient yeast = new Ingredient("Yeast");
            Ingredient water = new Ingredient("Water");
            Ingredient cheese = new Ingredient("Cheese");
            cheese.AddAllergen("Dairy");
            Ingredient ham = new Ingredient("Ham");
            Ingredient butter = new Ingredient("Butter");
            butter.AddAllergen("Dairy");
            Ingredient milk = new Ingredient("Milk");
            milk.AddAllergen("Dairy");
            Ingredient egg = new Ingredient("Egg");
            egg.AddAllergen("Egg");

            //Foods
            FoodItem f;
            MadeToOrderFood m;
            f = new FoodItem("Brownie", 1.1m, 200);
            f.AddAllergen("Nuts"); f.AddAllergen("Gluten");
            f.Restock(10);
            AddToList(f);

            m = new MadeToOrderFood("Ham Sandwich", 1.8m);
            m.AddIngredient(flour, 40); m.AddIngredient(yeast, 2); m.AddIngredient(sugar, 3);
            m.AddIngredient(butter, 4); m.AddIngredient(ham, 40);
            AddToList(m);

            m = new MadeToOrderFood("Cheese Sandwich", 1.7m);
            m.AddIngredient(flour, 40); m.AddIngredient(yeast, 2); m.AddIngredient(sugar, 3);
            m.AddIngredient(butter, 4); m.AddIngredient(cheese, 40);

            AddToList(m);

            f = new FoodItem("Cake Slice", 1.1m, 350);
            f.AddAllergen("Gluten"); f.AddAllergen("Egg"); f.AddAllergen("Dairy");
            f.Restock(6);
            AddToList(f);

            m = new MadeToOrderFood("Ham Salad", 1.7m);
            m.AddIngredient(ham, 40); m.AddIngredient(new Ingredient("Salad Leaves"), 120);
            AddToList(m);
        }
#endif
    }
}
