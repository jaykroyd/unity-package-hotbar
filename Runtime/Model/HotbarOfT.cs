using Elysium.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Hotbar
{
    public class Hotbar<T> : Hotbar, IHotbar<T> where T : IUsable
    {
        private IHotbarRow<T>[] rows = default;
        public IHotbarRow<T>[] Rows => rows;

        public Hotbar(IUnityLogger _logger, Config _config) : base(_logger, _config)
        {
            CreateHotbarRows(_config.NumOfRows, _config.NumOfSlots);
        }

        public void Set(Vector2Int _index, T _usable)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlot<T> slot))
            {
                slot.Usable = _usable;
                TriggerOnValueChanged();
            }
        }

        public void Unset(Vector2Int _index)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlot<T> slot))
            {
                slot.Usable = default(T);
                TriggerOnValueChanged();
            }
        }

        public void Use(Vector2Int _index)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlot<T> slot))
            {
                slot.Usable.Use();
            }
        }

        public void Swap(Vector2Int _indexOne, Vector2Int _indexTwo)
        {
            if (TryGetSlotByIndex(_indexOne, out IHotbarSlot<T> slotOne) && TryGetSlotByIndex(_indexTwo, out IHotbarSlot<T> slotTwo))
            {
                T firstUsable = slotOne.Usable;
                T secondUsable = slotTwo.Usable;
                Set(_indexOne, secondUsable);
                Set(_indexTwo, firstUsable);
            }
        }

        private void CreateHotbarRows(int _rows, int _slotsPerRow)
        {
            rows = new IHotbarRow<T>[_rows];
            for (int i = 0; i < _rows; i++)
            {
                rows[i] = new HotbarRow<T>(logger, i + 1, _slotsPerRow);
            }
            TriggerOnValueChanged();
        }

        private bool TryGetSlotByIndex(Vector2Int _index, out IHotbarSlot<T> _slot)
        {
            if (IsInvalidIndex(_index))
            {
                logger.LogError($"Hotbar doesn't contain slot with index {_index}. NumOfRows: {config.NumOfRows} | NumOfSlots: {config.NumOfSlots}");
                _slot = null;
                return false;
            }

            _slot = rows.ElementAt(_index.x - 1).Slots.ElementAt(_index.y - 1);
            return _slot != null;
        }
    }
}
