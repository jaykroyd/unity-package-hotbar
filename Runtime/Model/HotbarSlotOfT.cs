using Elysium.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarSlot<T> : IHotbarSlot<T> where T : IUsable
    {
        private IUnityLogger logger = default;
        private Vector2Int index = default;
        private T usable = default;

        public Vector2Int Index => index;
        public T Usable
        { 
            get => usable;
            set => usable = value != null
                ? value
                : default(T);//new NullUsable();
        }        

        public HotbarSlot(IUnityLogger _logger, Vector2Int _index)
        {
            this.logger = _logger;
            this.index = _index;
            this.usable = default(T);// new NullUsable();
        }

        public void Use()
        {
            logger.Log($"Hotbar slot {index.x}-{index.y} was clicked");
            this.usable.Use();
        }
    }
}
