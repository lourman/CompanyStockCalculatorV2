using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStockCalculator
{
    public partial class Form1 : Form
    {
        // Object that records all stock
        StockCalculator stockCalculator;

        /// <summary>
        /// Form constructor that initialises the object for all stock.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            stockCalculator = new StockCalculator();
        }

        #region UI_Methods
        /// <summary>
        /// Finds the selected item from the stock list
        /// </summary>
        /// <returns>The object of the selected item</returns>
        private Item SelectedItem()
        {
            Item result;

            result = stockCalculator.Items[listBoxCurrentStock.SelectedIndex];

            return result;
        }

        /// <summary>
        /// Fills the stock list control with all stock
        /// </summary>
        private void ShowStock()
        {
            // Record the last index selected in the list box food
            int selectedindex = (listBoxCurrentStock.SelectedIndex != -1) ? listBoxCurrentStock.SelectedIndex : 0;

            listBoxCurrentStock.Items.Clear();
            listBoxCurrentStock.Items.AddRange(stockCalculator.Items.ToArray());
            
            listBoxCurrentStock.SelectedIndex = selectedindex;
        }

        /// <summary>
        /// Shows in the information textbox the allergens for a selected food object
        /// </summary>
        public void ShowAllergens()
        {
            Item f;
            f = SelectedItem();
            if (f is IAllergen)
            {
                HashSet<String> allergens;
                allergens = ((IAllergen)f).AllergySet();

                richTextBoxSelectedItemInfo.Clear();

                foreach (String s in allergens)
                {
                    richTextBoxSelectedItemInfo.AppendText(s + "\n");
                }
            }
            else
            {
                richTextBoxSelectedItemInfo.Clear();
            }
        }
        #endregion

        #region Control_Events
        /// <summary>
        /// Make sure that all stock is displayed in the stock list control, when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowStock();
        }
        
        /// <summary>
        /// The button should sell an item and reduce the stock level of the item
        /// the user has selected in the stock list control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSellItem_Click(object sender, EventArgs e)
        {
            Item f;

            f = SelectedItem();
            f.Sell(1);
            ShowStock();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowLowStock_Click(object sender, EventArgs e)
        {
            listBoxQueryStock.Items.Clear();
            List<StockItem> alex = stockCalculator.LowStock((int)numericUpDownLowStockLimit.Value);
            listBoxQueryStock.Items.AddRange(stockCalculator.LowStock((int)numericUpDownLowStockLimit.Value).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRestockItem_Click(object sender, EventArgs e)
        {
            if (stockCalculator.Items[listBoxCurrentStock.SelectedIndex] is StockItem)
            {
                ((StockItem)stockCalculator.StockedItems[listBoxCurrentStock.SelectedIndex]).Restock(10);
                ShowStock();
            }
        }

        /// <summary>
        /// When an item in the stock list is selected makes sure that the correct information 
        /// is shown in the information textbox. For Food that is the allergens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxCurrentStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAllergens();
        }
        #endregion

        /// <summary>
        /// Filters the stocklist on the allergen found. Displays the results
        /// in the listBoxQueryStock 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilterAllergen_Click(object sender, EventArgs e)
        {
            bool fooditemwithallergenfound = false;
            listBoxQueryStock.Items.Clear();
            if (textBoxFilterAllergen.Text != "")
            {
                foreach (Item i in stockCalculator.Items)
                {
                    if (i is IAllergen)
                    {
                        if (((IAllergen)i).AllergySet().Contains(textBoxFilterAllergen.Text))
                        {
                            listBoxQueryStock.Items.Add(i);
                            fooditemwithallergenfound = true;
                        }
                    }
                }
                if (!fooditemwithallergenfound)
                {
                    listBoxQueryStock.Items.Add("Specific Allergen was not found");
                }
                listBoxQueryStock.SelectedIndex = 0;
            }
            else
            {
                ShowStock();
            }
        }
    }
}
