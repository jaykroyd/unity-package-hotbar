using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public interface IUsableSkill : IUsable
    {
        Sprite Icon { get; }
    }
}