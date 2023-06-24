using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToastyQoL.Content.UI.PotionUI
{
    public interface IPotionSorting
    {
        public string Name { get; }

        public List<PotionElement> SortPotions(ref List<PotionElement> potions);
    }
}
