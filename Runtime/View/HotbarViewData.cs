using System.Collections.Generic;

namespace Elysium.Hotbar
{
    public struct HotbarViewData
    {
        public int CurrentRow { get; set; }
        public IEnumerable<HotbarViewSlotData> Slots { get; set; }
    }
}