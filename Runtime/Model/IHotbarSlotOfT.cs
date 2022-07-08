using UnityEngine;

namespace Elysium.Hotbar
{
    public interface IHotbarSlot<T> where T : IUsable
    {
        Vector2Int Index { get; }
        T Usable { get; set; }
    }
}
