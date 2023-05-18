namespace CalNohitQoL.Content.UI
{
    public struct ToggleBlockInformation
    {
        public delegate bool CanToggleDelegate();

        public CanToggleDelegate CanToggle;

        public string ExtraHoverText;

        public ToggleBlockInformation(CanToggleDelegate canToggleDelegate, string extraHoverText)
        {
            CanToggle = canToggleDelegate;
            ExtraHoverText = extraHoverText;
        }
    }
}
