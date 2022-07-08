using Elysium.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar.Samples
{
    public class ItemHotbarViewSlot : HotbarViewSlot<ItemHotbarViewSlotData>
    {
        [SerializeField] private bool disableAmountNumberWhenZero = false;
        [SerializeField] private Image icon = default;
        [SerializeField] private TMP_Text amount = default;
        private Sprite defaultSprite = default;

        protected override void OnAwake()
        {
            defaultSprite = icon.sprite;
        }

        protected override void OnRefresh(ItemHotbarViewSlotData _data)
        {
            this.icon.sprite = _data.Icon != null ? _data.Icon : defaultSprite;
            this.icon.color = interactible ? Color.white : Color.white.SetA(0.4f);
            this.amount.text = _data.Amount.ToString();
            this.amount.gameObject.SetActive(!disableAmountNumberWhenZero || _data.Amount > 0);
        }
    }
}