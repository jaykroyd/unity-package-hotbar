using UnityEditor;
using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public class UsableItem : IUsableItem
    {
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public Sprite Icon { get; private set; }        
        public bool CanUse => Amount > 0;

        public UsableItem(string _name, int _amount, string _icon)
        {
            this.Name = _name;
            this.Amount = _amount;
            this.Icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Packages/com.elysium.hotbar/Samples/Demo/Textures/{_icon}.png");
        }

        public void Use()
        {
            Amount--;
            Debug.Log($"Item {Name} was used.");
        }
    }
}