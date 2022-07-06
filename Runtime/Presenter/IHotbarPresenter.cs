using UnityEngine.Events;

namespace Elysium.Hotbar
{
    public interface IHotbarPresenter
    {
        event UnityAction OnShow;
        event UnityAction OnHide;
        event UnityAction OnChanged;
    }
}