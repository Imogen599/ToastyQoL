using System;

namespace ToastyQoL.Content.UI
{
    public struct ToggleBlockInformation
    {
        public Func<bool> CanToggle;

        public string ExtraHoverText;

        public ToggleBlockInformation(Func<bool> canToggleDelegate, string extraHoverText)
        {
            CanToggle = canToggleDelegate;
            ExtraHoverText = extraHoverText;
        }
    }
}