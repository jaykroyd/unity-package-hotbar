using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar.Samples
{
    public class SkillHotbarViewSlot : HotbarViewSlot<SkillHotbarViewSlotData>
    {
        [SerializeField] private Image icon = default;
        private Sprite defaultSprite = default;

        protected override void OnAwake()
        {
            defaultSprite = icon.sprite;
        }

        protected override void OnRefresh(SkillHotbarViewSlotData _data)
        {
            this.icon.sprite = _data.Icon != null ? _data.Icon : defaultSprite;
        }
    }
}