using Elysium.Core;
using Elysium.Core.Utils;
using Elysium.Core.Utils.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public class HotbarPresenter : IHotbarPresenter
    {
        private IUnityLogger logger = default;
        private IHotbar hotbar = default;
        private IHotbarView view = default;
        private int currentRow = 0;

        public HotbarPresenter(IUnityLogger _logger, IHotbar _hotbar, IHotbarView _view)
        {
            this.logger = _logger;
            this.hotbar = _hotbar;
            this.view = _view;
        }

        public void Show()
        {
            hotbar.OnValueChanged += UpdateSlots;
            view.OnSwap += hotbar.Swap;
            view.OnUse += hotbar.Use;
            view.OnIncreaseRow += NextRow;
            view.OnDecreaseRow += PrevRow;
            view.OnClear += Clear;
            view.Show();
            UpdateSlots();
            logger.Log($"showing hotbar");
        }        

        public void Hide()
        {
            hotbar.OnValueChanged -= UpdateSlots;
            view.OnSwap -= hotbar.Swap;
            view.OnUse -= hotbar.Use;
            view.OnIncreaseRow -= NextRow;
            view.OnDecreaseRow -= PrevRow;
            view.OnClear -= Clear;
            view.Hide();
            logger.Log($"hiding hotbar");
        }

        public void NextRow()
        {
            currentRow = hotbar.Rows.Next(currentRow);
            if (view.Enabled) { UpdateSlots(); }
            logger.Log($"moving to next row {currentRow}");
        }

        public void PrevRow()
        {
            currentRow = hotbar.Rows.Previous(currentRow);
            if (view.Enabled) { UpdateSlots(); }
            logger.Log($"moving to prev row {currentRow}");
        }

        private void Clear(Vector2Int _index)
        {
            hotbar.UnsetUsable(_index);
        }

        private void UpdateSlots()
        {
            IHotbarRow row = hotbar.Rows.ElementAt(currentRow);
            view.Refresh(new HotbarViewData
            {
                CurrentRow = currentRow,
                Slots = row.Slots.Select(x => new HotbarViewSlotData
                {
                    Index = x.Index,
                    Icon = x.Icon,
                    CanUse = x.CanUse,
                }),
            });
            logger.Log($"refreshing slots");
        }
    }
}