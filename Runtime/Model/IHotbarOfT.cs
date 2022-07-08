using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbar<T> : IHotbar where T : IUsable
    {
        IHotbarRow<T>[] Rows { get; }

        void Set(Vector2Int _index, T _usable);
        void Unset(Vector2Int _index);
        void Use(Vector2Int _index);
        void Swap(Vector2Int _indexOne, Vector2Int _indexTwo);
    }
}
