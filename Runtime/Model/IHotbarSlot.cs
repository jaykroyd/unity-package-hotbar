using UnityEngine;

namespace Elysium.Hotbar
{
    public interface IHotbarSlot
    {
        Sprite Icon { get; }
        Vector2Int Index { get; }
        bool CanUse { get; }
    }
}
