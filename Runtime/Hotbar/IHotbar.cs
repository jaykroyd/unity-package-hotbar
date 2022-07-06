using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbar
    {
        IHotbarRow[] Rows { get; }

        event UnityAction OnValueChanged;

        void Register(Vector2Int _index, IUsable _usable);
        void Unregister(Vector2Int _index);
    }
}
