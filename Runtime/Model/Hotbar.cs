using System;
using System.Collections;
using System.Linq;
using Elysium.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public class Hotbar : IHotbar
    {
        [System.Serializable]
        public class Config
        {
            [SerializeField] private int numOfRows = default;
            [SerializeField] private int numOfSlots = default;

            public int NumOfRows => numOfRows;
            public int NumOfSlots => numOfSlots;

            public Config()
            {

            }

            public Config(int _numOfRows, int _numOfSlots)
            {
                this.numOfRows = _numOfRows;
                this.numOfSlots = _numOfSlots;
            }
        }

        private IUnityLogger logger = default;
        private Config config = default;
        private IHotbarRowInternal[] rows = default;

        public IHotbarRow[] Rows => rows;

        public event UnityAction OnValueChanged;

        public Hotbar(IUnityLogger _logger, Config _config)
        {
            this.logger = _logger;
            this.config = _config;
            CreateHotbarRows(_config.NumOfRows, _config.NumOfSlots);
        }

        public void SetUsable(Vector2Int _index,  IUsable _usable)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlotInternal slot))
            {
                slot.Usable = _usable;
                OnValueChanged?.Invoke();
            }
        }
        
        public void UnsetUsable(Vector2Int _index)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlotInternal slot))
            {
                slot.Usable = new NullUsable();
                OnValueChanged?.Invoke();
            }
        }

        public void Use(Vector2Int _index)
        {
            if (TryGetSlotByIndex(_index, out IHotbarSlotInternal slot))
            {
                slot.Use();
            }
        }

        public void Swap(Vector2Int _indexOne, Vector2Int _indexTwo)
        {
            if (TryGetSlotByIndex(_indexOne, out IHotbarSlotInternal slotOne) && TryGetSlotByIndex(_indexTwo, out IHotbarSlotInternal slotTwo))
            {
                IUsable firstUsable = slotOne.Usable;
                IUsable secondUsable = slotTwo.Usable;
                SetUsable(_indexOne, secondUsable);
                SetUsable(_indexTwo, firstUsable);
            }
        }

        private void CreateHotbarRows(int _rows, int _slotsPerRow)
        {
            rows = new IHotbarRowInternal[_rows];
            for (int i = 0; i < _rows; i++)
            {
                rows[i] = new HotbarRow(logger, i + 1, _slotsPerRow);
            }
            OnValueChanged?.Invoke();
        }

        private bool TryGetSlotByIndex(Vector2Int _index, out IHotbarSlotInternal _slot)
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

        private bool IsInvalidIndex(Vector2Int _index)
        {
            return _index.x < 1 || _index.x > config.NumOfRows || _index.y < 1 || _index.y > config.NumOfSlots;
        }
    }
}
