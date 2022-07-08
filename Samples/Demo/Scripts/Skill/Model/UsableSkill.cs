using UnityEditor;
using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public class UsableSkill : IUsableSkill
    {
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public bool CanUse => true;

        public UsableSkill(string _name, string _icon)
        {
            this.Name = _name;
            this.Icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Packages/com.elysium.hotbar/Samples/Demo/Textures/{_icon}.png");
        }

        public void Use()
        {
            Debug.Log($"Skill {Name} was used.");
        }
    }
}