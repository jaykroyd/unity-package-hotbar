using System.Collections.Generic;
using Elysium.Core;
using UnityEngine;

namespace Elysium.Hotbar
{
    public class HotbarRow : IHotbarRowInternal
    {
        private UnityLogger logger = default;
        private IHotbarSlotInternal[] slots = default;
        private int index = default;

        int IHotbarRow.Index => index;
        IEnumerable<IHotbarSlotInternal> IHotbarRowInternal.Slots => slots;
        IEnumerable<IHotbarSlot> IHotbarRow.Slots => slots;

        public HotbarRow(UnityLogger _logger, int _index, int _slots)
        {
            this.logger = _logger;
            this.index = _index;
            CreateRowSlots(_slots);
        }

        private void CreateRowSlots(int _slots)
        {
            slots = new HotbarSlot[_slots];
            for (int i = 0; i < _slots; i++)
            {
                slots[i] = new HotbarSlot(logger, new Vector2Int(index, i + 1));
            }
        }
    }
}
