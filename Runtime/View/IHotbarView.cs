using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbarView : IView<HotbarViewData>
    {
        event UnityAction<Vector2Int> OnUse;
        event UnityAction<Vector2Int, Vector2Int> OnSwap;
        event UnityAction<Vector2Int> OnClear;
        event UnityAction OnIncreaseRow;
        event UnityAction OnDecreaseRow;
    }
}