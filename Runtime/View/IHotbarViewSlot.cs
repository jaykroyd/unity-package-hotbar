using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbarViewSlot
    {
        event UnityAction<Vector2Int> OnUse;
        event UnityAction<Vector2Int, Vector2Int> OnSwap;
        event UnityAction<Vector2Int> OnClear;

        void Refresh(Vector2Int _index, Sprite _icon, bool _canDrag);
    }
}