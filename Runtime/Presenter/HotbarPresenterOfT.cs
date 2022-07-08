using Elysium.Core;
using Elysium.Core.Utils;
using System;
using System.Linq;
using UnityEngine;

namespace Elysium.Hotbar
{
    public abstract class HotbarPresenter<T1, T2> : IHotbarPresenter where T1 : IUsable
    {
        private IUnityLogger logger = default;
        private IHotbar<T1> hotbar = default;
        private IHotbarView<T2> view = default;
        protected int currentRow = 0;

        public HotbarPresenter(IUnityLogger _logger, IHotbar<T1> _hotbar, IHotbarView<T2> _view)
        {
            this.logger = _logger;
            this.hotbar = _hotbar;
            this.view = _view;
        }

        public void Show()
        {
            hotbar.OnValueChanged += Refresh;
            view.OnSwap += Swap;
            view.OnUse += Use;
            view.OnIncreaseRow += NextRow;
            view.OnDecreaseRow += PrevRow;
            view.OnClear += Clear;
            view.Show();
            Refresh();
            logger.Log($"showing hotbar");
        }        

        public void Hide()
        {
            hotbar.OnValueChanged -= Refresh;
            view.OnSwap -= Swap;
            view.OnUse -= Use;
            view.OnIncreaseRow -= NextRow;
            view.OnDecreaseRow -= PrevRow;
            view.OnClear -= Clear;
            view.Hide();
            logger.Log($"hiding hotbar");
        }

        public void NextRow()
        {
            currentRow = hotbar.Rows.Next(currentRow);
            if (view.Enabled) { Refresh(); }
            logger.Log($"moving to next row {currentRow}");
        }

        public void PrevRow()
        {
            currentRow = hotbar.Rows.Previous(currentRow);
            if (view.Enabled) { Refresh(); }
            logger.Log($"moving to prev row {currentRow}");
        }

        private void Use(Vector2Int _index)
        {
            hotbar.Use(_index);
            Refresh();
        }

        private void Swap(Vector2Int _indexOne, Vector2Int _indexTwo)
        {
            hotbar.Swap(_indexOne, _indexTwo);
        }

        private void Clear(Vector2Int _index)
        {
            hotbar.Unset(_index);
        }

        private void Refresh()
        {
            logger.Log($"refreshing slots");
            IHotbarRow<T1> row = hotbar.Rows.ElementAt(currentRow);
            T2 data = GetViewData(row);
            view.Refresh(data);
        }

        protected abstract T2 GetViewData(IHotbarRow<T1> _row);
    }
}