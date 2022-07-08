using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbarViewSlot<T> where T : HotbarViewSlotData
    {
        event UnityAction<Vector2Int> OnUse;
        event UnityAction<Vector2Int, Vector2Int> OnSwap;
        event UnityAction<Vector2Int> OnClear;

        void Refresh(T _data);
    }
}