using UnityEngine;

namespace Elysium.Hotbar
{
    public class UseEvent : IUsable
    {
        public void Use()
        {
            Debug.Log("Event was triggered.");
        }
    }
}
