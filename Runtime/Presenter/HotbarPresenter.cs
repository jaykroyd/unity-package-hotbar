using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public class HotbarPresenter : IHotbarPresenter
    {
        private IHotbar hotbar = default;

        public event UnityAction OnShow;
        public event UnityAction OnHide;
        public event UnityAction OnChanged;

        public HotbarPresenter(IHotbar _hotbar)
        {
            this.hotbar = _hotbar;
        }

        public void Show()
        {
            OnShow?.Invoke();
            hotbar.OnValueChanged += TriggerOnValueChanged;
        }

        public void Hide()
        {
            OnHide?.Invoke();
            hotbar.OnValueChanged -= OnChanged.Invoke;
        }

        private void TriggerOnValueChanged()
        {
            OnChanged?.Invoke();
        }
    }
}