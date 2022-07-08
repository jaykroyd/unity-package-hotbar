using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbar
    {
        event UnityAction OnValueChanged;
    }
}
