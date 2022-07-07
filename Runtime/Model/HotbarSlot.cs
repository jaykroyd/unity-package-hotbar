using Elysium.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarSlot : IHotbarSlotInternal
    {
        private IUnityLogger logger = default;
        private Vector2Int index = default;
        private IUsable usable = default;

        public Sprite Icon => usable.Icon;
        public Vector2Int Index => index;
        public bool CanUse => !(usable is NullUsable);
        IUsable IHotbarSlotInternal.Usable 
        { 
            get => usable;
            set => usable = value != null ? value : new NullUsable(); 
        }        

        public HotbarSlot(IUnityLogger _logger, Vector2Int _index)
        {
            this.logger = _logger;
            this.index = _index;
            this.usable = new NullUsable();
        }

        public void Use()
        {
            logger.Log($"Hotbar slot {index.x}-{index.y} was clicked");
            this.usable.Use();
        }
    }
}
