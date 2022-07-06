namespace Elysium.Hotbar
{
    internal interface IHotbarSlotInternal : IHotbarSlot
    {
        void Register(IUsable _usable);
        void Unregister();
        void Use();
    }
}
