namespace Elysium.Hotbar
{
    internal interface IHotbarSlotInternal : IHotbarSlot
    {
        IUsable Usable { get; set; }

        void Use();
    }
}
