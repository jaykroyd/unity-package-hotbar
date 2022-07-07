using Elysium.Core;
using Elysium.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarViewSlot : MonoBehaviour, IHotbarViewSlot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image icon = default;        
        private Vector2Int index = default;
        private Sprite defaultSprite = default;

        protected IDraggable draggable = default;
        protected IDroppable droppable = default;
        protected PointerResolver pointerResolver = default;
        protected IHoverResolver hoverResolver = default;

        public event UnityAction<Vector2Int> OnUse;
        public event UnityAction<Vector2Int, Vector2Int> OnSwap;
        public event UnityAction<Vector2Int> OnClear;

        private void Awake()
        {
            pointerResolver = new PointerResolver();
            hoverResolver = new HoverResolver();

            draggable = gameObject.GetComponent<DuplicatedDraggable>();
            droppable = gameObject.GetComponent<Droppable>();

            if (draggable is null) { draggable = gameObject.AddComponent<DuplicatedDraggable>(); }
            if (droppable is null) { droppable = gameObject.AddComponent<Droppable>(); }

            draggable.CanDrag = false;
            droppable.CanDrop = true;

            droppable.OnReceiveDrop += RequestSwap;

            pointerResolver.OnClick += OnClick;
            pointerResolver.OnHoldStart += OnHoldStart;
            pointerResolver.OnHoldEnd += OnHoldEnd;
            draggable.OnDragBegin += TriggerOnHoldOverride;
            draggable.OnDragEnd += OnDragEnd;

            hoverResolver.OnHoverStart += OnHoverStart;
            hoverResolver.OnHoverEnd += OnHoverEnd;
            Draggable.OnAnyDragBegin += TriggerOnPointerExit;

            defaultSprite = icon.sprite;
            enabled = false;
        }        

        public void Refresh(Vector2Int _index, Sprite _icon, bool _canDrag)
        {
            this.index = _index;
            this.icon.sprite = _icon != null ? _icon : defaultSprite;
            this.draggable.CanDrag = _canDrag;
            this.enabled = true;
        }

        protected virtual void OnClick()
        {
            // Debug.Log($"{gameObject.name} was clicked");
            OnUse?.Invoke(index);
        }

        protected virtual void OnHoldStart()
        {
            // Debug.Log($"{gameObject.name} has started been held");
        }

        protected virtual void OnHoldEnd()
        {
            // Debug.Log($"{gameObject.name} has stopped been held");            
        }

        protected virtual void OnHoverStart()
        {
            // Debug.Log($"{gameObject.name} has started been hovered over");
        }

        protected virtual void OnHoverEnd()
        {
            // Debug.Log($"{gameObject.name} has stopped been hovered over");
        }

        public void OnPointerEnter(PointerEventData _data)
        {
            if (Draggable.DraggablesInDrag.Count > 0) { return; }
            hoverResolver.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData _data)
        {
            hoverResolver.OnPointerExit();
        }

        public void OnPointerDown(PointerEventData _data)
        {
            pointerResolver.OnPointerDown();
        }

        public void OnPointerUp(PointerEventData _data)
        {
            pointerResolver.OnPointerUp();
        }

        private void RequestSwap(IDraggable _draggable)
        {
            if (_draggable.gameObject.TryGetComponent(out HotbarViewSlot otherSlot))
            {
                otherSlot.Swap(index);
            }
        }

        private void Swap(Vector2Int _swapRequesterIndex)
        {
            OnSwap?.Invoke(_swapRequesterIndex, index);
        }

        private void OnDragEnd(PointerEventData _data)
        {            
            if (!_data.hovered.Any(x => x.GetComponent<Droppable>()))
            {
                Debug.Log("dropped on nothing");
                OnClear?.Invoke(index);
            }
        }

        private void TriggerOnHoldOverride(PointerEventData _data)
        {
            pointerResolver.OnHoldOverride();
        }

        private void TriggerOnPointerExit(PointerEventData _data)
        {
            hoverResolver.OnPointerExit();
        }

        private void OnDestroy()
        {
            droppable.OnReceiveDrop -= RequestSwap;

            pointerResolver.OnClick -= OnClick;
            pointerResolver.OnHoldStart -= OnHoldStart;
            pointerResolver.OnHoldEnd -= OnHoldEnd;
            draggable.OnDragBegin -= TriggerOnHoldOverride;

            hoverResolver.OnHoverStart -= OnHoverStart;
            hoverResolver.OnHoverEnd -= OnHoverEnd;
            Draggable.OnAnyDragBegin -= TriggerOnPointerExit;
            draggable.OnDragEnd -= OnDragEnd;
            pointerResolver.Dispose();
            pointerResolver = null;
        }        
    }
}