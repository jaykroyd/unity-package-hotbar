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
    public class HotbarViewSlot<T> : MonoBehaviour, IHotbarViewSlot<T>, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler where T : HotbarViewSlotData
    {
        protected Vector2Int index = default;
        protected IDraggable draggable = default;
        protected IDroppable droppable = default;
        protected PointerResolver pointerResolver = default;
        protected IHoverResolver hoverResolver = default;
        protected bool interactible = false;

        public event UnityAction<Vector2Int> OnUse;
        public event UnityAction<Vector2Int, Vector2Int> OnSwap;
        public event UnityAction<Vector2Int> OnClear;

        private void Awake()
        {
            draggable = gameObject.GetComponent<IDraggable>();
            droppable = gameObject.GetComponent<IDroppable>();
            if (draggable is null) { draggable = gameObject.AddComponent<DuplicatedDraggable>(); }
            if (droppable is null) { droppable = gameObject.AddComponent<Droppable>(); }
            draggable.CanDrag = false;
            droppable.CanDrop = true;

            draggable.OnDragBegin += TriggerOnHoldOverride;
            draggable.OnDragEnd += OnDragEnd;
            Draggable.OnAnyDragBegin += TriggerOnPointerExit;

            droppable.OnReceiveDrop += RequestSwap;            

            pointerResolver = new PointerResolver();
            pointerResolver.OnClick += OnClick;
            pointerResolver.OnHoldStart += OnHoldStart;
            pointerResolver.OnHoldEnd += OnHoldEnd;            

            hoverResolver = new HoverResolver();
            hoverResolver.OnHoverStart += OnHoverStart;
            hoverResolver.OnHoverEnd += OnHoverEnd;

            enabled = false;
        }        

        public void Refresh(T _data)
        {
            this.index = _data.Index;            
            this.draggable.CanDrag = _data.CanDrag;
            this.interactible = _data.CanUse;
            this.enabled = true;
            OnRefresh(_data);
        }

        protected virtual void OnAwake()
        {

        }

        protected virtual void OnRefresh(T _data)
        {
            
        }

        protected virtual void OnClick()
        {
            // Debug.Log($"{gameObject.name} was clicked");
            if (interactible) { OnUse?.Invoke(index); }            
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
            if (_draggable.gameObject.TryGetComponent(out HotbarViewSlot<T> otherSlot))
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
            draggable.OnDragBegin -= TriggerOnHoldOverride;
            draggable.OnDragEnd -= OnDragEnd;
            Draggable.OnAnyDragBegin -= TriggerOnPointerExit;

            droppable.OnReceiveDrop -= RequestSwap;

            pointerResolver.OnClick -= OnClick;
            pointerResolver.OnHoldStart -= OnHoldStart;
            pointerResolver.OnHoldEnd -= OnHoldEnd;
            pointerResolver.Dispose();
            pointerResolver = null;

            hoverResolver.OnHoverStart -= OnHoverStart;
            hoverResolver.OnHoverEnd -= OnHoverEnd;
        }        
    }
}