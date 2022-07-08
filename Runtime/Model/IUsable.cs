using UnityEngine;

namespace Elysium.Hotbar
{
    public interface IUsable
    {
        bool CanUse { get; }

        void Use();
    }    
}
