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

        protected IUnityLogger logger = default;
        protected Config config = default;

        public event UnityAction OnValueChanged;

        public Hotbar(IUnityLogger _logger, Config _config)
        {
            this.logger = _logger;
            this.config = _config;            
        }

        protected bool IsInvalidIndex(Vector2Int _index)
        {
            return _index.x < 1 || _index.x > config.NumOfRows || _index.y < 1 || _index.y > config.NumOfSlots;
        }

        protected void TriggerOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }    
}
