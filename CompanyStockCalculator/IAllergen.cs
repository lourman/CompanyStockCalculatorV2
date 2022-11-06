using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStockCalculator
{
    interface IAllergen
    {
        HashSet<String> AllergySet();
        bool AllergenPresent(String Allergen);
    }
}
