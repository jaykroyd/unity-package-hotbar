using System.Collections.Generic;
using Elysium.Core;
using UnityEngine;

namespace Elysium.Hotbar
{
    public class HotbarRow<T> : IHotbarRow<T> where T : IUsable
    {
        private IUnityLogger logger = default;
        private IHotbarSlot<T>[] slots = default;
        private int index = default;

        public int Index => index;
        public IEnumerable<IHotbarSlot<T>> Slots => slots;

        public HotbarRow(IUnityLogger _logger, int _index, int _slots)
        {
            this.logger = _logger;
            this.index = _index;
            CreateRowSlots(_slots);
        }

        private void CreateRowSlots(int _slots)
        {
            slots = new HotbarSlot<T>[_slots];
            for (int i = 0; i < _slots; i++)
            {
                slots[i] = new HotbarSlot<T>(logger, new Vector2Int(index, i + 1));
            }
        }
    }
}
