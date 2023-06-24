using System.Collections.Generic;

namespace ToastyQoL.Content.UI.PotionUI
{
    public class AToZPotionSort : IPotionSorting
    {
        public string Name => "AToZ";

        public List<PotionElement> SortPotions(ref List<PotionElement> potions)
        {
            potions.Sort((x, y) => string.Compare(x.PotionName, y.PotionName));
            return potions;
        }
    }
}
