using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elysium.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarView : MonoBehaviour, IHotbarView
    {
        [SerializeField] private PoolSpawner<HotbarViewSlot> slots = default;
        [SerializeField] private Button increaseRowButton = default;
        [SerializeField] private Button decreaseRowButton = default;
        [SerializeField] private TMP_Text currentRow = default;

        public bool Enabled => gameObject.activeSelf;

        public event UnityAction<Vector2Int> OnUse;
        public event UnityAction<Vector2Int, Vector2Int> OnSwap;
        public event UnityAction<Vector2Int> OnClear;
        public event UnityAction OnIncreaseRow;
        public event UnityAction OnDecreaseRow;        

        private List<IHotbarViewSlot> activeSlots = new List<IHotbarViewSlot>();

        private void Awake()
        {
            increaseRowButton?.onClick.AddListener(TriggerOnIncreaseRow);
            decreaseRowButton?.onClick.AddListener(TriggerOnDecreaseRow);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Refresh(HotbarViewData _data)
        {            
            activeSlots.ForEach(x =>
            {
                x.OnUse -= TriggerOnUse;
                x.OnSwap -= TriggerOnSwap;
                x.OnClear -= TriggerOnClear;
            });
            activeSlots.Clear();

            currentRow.text = $"{_data.CurrentRow}";
            int numOfSlots = _data.Slots.Count();
            var objs = slots.Set(numOfSlots);
            for (int i = 0; i < numOfSlots; i++)
            {
                IHotbarViewSlot slotView = objs.ElementAt(i);
                HotbarViewSlotData slot = _data.Slots.ElementAt(i);
                activeSlots.Add(slotView);
                ConfigureSlot(slotView, slot);
            }
        }

        private void ConfigureSlot(IHotbarViewSlot _slotView, HotbarViewSlotData _slot)
        {
            _slotView.Refresh(_slot.Index, _slot.Icon, _slot.CanUse);
            _slotView.OnUse += TriggerOnUse;
            _slotView.OnSwap += TriggerOnSwap;
            _slotView.OnClear += TriggerOnClear;
        }

        private void TriggerOnClear(Vector2Int _index)
        {
            OnClear?.Invoke(_index);
        }

        private void TriggerOnUse(Vector2Int _index)
        {
            OnUse.Invoke(_index);
        }

        private void TriggerOnSwap(Vector2Int _indexOne, Vector2Int _indexTwo)
        {
            OnSwap.Invoke(_indexOne, _indexTwo);
        }

        private void TriggerOnIncreaseRow()
        {
            OnIncreaseRow?.Invoke();
        }

        private void TriggerOnDecreaseRow()
        {
            OnDecreaseRow?.Invoke();
        }

        private void OnDestroy()
        {
            increaseRowButton?.onClick.RemoveListener(TriggerOnIncreaseRow);
            decreaseRowButton?.onClick.RemoveListener(TriggerOnDecreaseRow);
            OnUse = null;
            OnSwap = null;
            OnIncreaseRow = null;
            OnDecreaseRow = null;            
        }
    }    
}