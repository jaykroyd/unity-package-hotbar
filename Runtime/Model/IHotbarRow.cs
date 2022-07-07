using System.Collections.Generic;

namespace Elysium.Hotbar
{
    public interface IHotbarRow
    {
        int Index { get; }
        IEnumerable<IHotbarSlot> Slots { get; }
    }
}
