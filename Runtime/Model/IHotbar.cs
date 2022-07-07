using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbar
    {
        IHotbarRow[] Rows { get; }

        event UnityAction OnValueChanged;

        void SetUsable(Vector2Int _index, IUsable _usable);
        void UnsetUsable(Vector2Int _index);
        void Use(Vector2Int _index);
        void Swap(Vector2Int _indexOne, Vector2Int _indexTwo);
    }
}
