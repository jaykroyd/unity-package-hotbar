using Elysium.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarSlot : IHotbarSlotInternal
    {
        private UnityLogger logger = default;
        private Vector2Int index = default;
        private IUsable usable = default;

        public Vector2Int Index => index;

        public HotbarSlot(UnityLogger _logger, Vector2Int _index)
        {
            this.logger = _logger;
            this.index = _index;
            this.usable = new NullUsable();
        }

        void IHotbarSlotInternal.Register(IUsable _usable)
        {
            this.usable = _usable;
        }

        void IHotbarSlotInternal.Unregister()
        {
            this.usable = new NullUsable();
        }

        void IHotbarSlotInternal.Use()
        {
            logger.Log($"Hotbar slot {index.x}-{index.y} was clicked");
            this.usable.Use();
        }
    }
}
