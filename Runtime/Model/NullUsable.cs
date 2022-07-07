using UnityEditor;
using UnityEngine;

namespace Elysium.Hotbar
{
    public class NullUsable : IUsable
    {
        public Sprite Icon { get; private set; }

        public NullUsable()
        {
            Icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Packages/com.elysium.items/Samples/Demo/Inventory/Textures/empty.png");
        }

        public void Use()
        {

        }
    }
}
