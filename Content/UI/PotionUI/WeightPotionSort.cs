using System.Collections.Generic;
using System.Linq;

namespace ToastyQoL.Content.UI.PotionUI
{
    internal class WeightPotionSort : IPotionSorting
    {
        public string Name => "Weight";

        public List<PotionElement> SortPotions(ref List<PotionElement> potions)
        {
            return potions.OrderBy(pe => pe.Weight).ToList();
        }
    }
}
