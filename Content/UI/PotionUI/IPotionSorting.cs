using System.Collections.Generic;

namespace ToastyQoL.Content.UI.PotionUI
{
    public interface IPotionSorting
    {
        public string Name { get; }

        public List<PotionElement> SortPotions(ref List<PotionElement> potions);
    }
}
