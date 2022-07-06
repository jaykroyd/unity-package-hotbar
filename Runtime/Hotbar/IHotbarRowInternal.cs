using System.Collections.Generic;

namespace Elysium.Hotbar
{
    internal interface IHotbarRowInternal : IHotbarRow
    {
        new IEnumerable<IHotbarSlotInternal> Slots { get; }
    }
}
