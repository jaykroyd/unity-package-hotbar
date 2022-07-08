using System.Collections.Generic;

namespace Elysium.Hotbar
{
    public interface IHotbarRow<T> where T: IUsable
    {
        int Index { get; }
        IEnumerable<IHotbarSlot<T>> Slots { get; }
    }
}
