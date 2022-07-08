using System.Collections.Generic;

namespace Elysium.Hotbar
{
    public class HotbarViewData<T> where T : HotbarViewSlotData
    {
        public int CurrentRow { get; set; }
        public IEnumerable<T> Slots { get; set; }
    }
}