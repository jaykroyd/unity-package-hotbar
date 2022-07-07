using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{

    public struct HotbarViewSlotData
    {
        public Vector2Int Index { get; set; }
        public Sprite Icon { get; set; }
        public bool CanUse { get; set; }
    }
}