using UnityEngine;

namespace Elysium.Hotbar
{
    public interface IUsable
    {
        Sprite Icon { get; }
        void Use();
    }
}
