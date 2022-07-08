using UnityEngine;

namespace Elysium.Hotbar
{
    public class HotbarViewSlotData
    {
        public Vector2Int Index { get; set; }
        public bool CanUse { get; set; }
        public bool CanDrag { get; set; }
    }
}