using UnityEditor;
using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public class SampleUsableEvent : IUsable
    {
        public Sprite Icon { get; private set; }

        public SampleUsableEvent(string _icon)
        {
            Icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Packages/com.elysium.hotbar/Samples/Demo/Textures/{_icon}.png");
        }

        public void Use()
        {
            Debug.Log("Event was triggered.");
        }
    }
}