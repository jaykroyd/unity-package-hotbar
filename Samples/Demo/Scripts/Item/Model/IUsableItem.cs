using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public interface IUsableItem : IUsable
    {
        Sprite Icon { get; }
        int Amount { get; }
    }
}