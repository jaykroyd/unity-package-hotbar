using Elysium.Core;
using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public class SkillHotbar : MonoBehaviour
    {
        [SerializeField] private bool showOnStart = true;
        [SerializeField] private bool enableLogging = false;
        [SerializeField] private Hotbar.Config config = default;
        [SerializeField] private SkillHotbarView view = default;

        private IHotbar<IUsableSkill> hotbar = default;
        private IHotbarPresenter presenter = default;

        private void Start()
        {
            IUnityLogger logger = new UnityLogger(enableLogging);

            hotbar = new Hotbar<IUsableSkill>(logger, config);
            hotbar.Set(new Vector2Int(1, 1), new UsableSkill("test1", "t_usable1"));
            hotbar.Set(new Vector2Int(1, 2), new UsableSkill("test2", "t_usable2"));
            hotbar.Set(new Vector2Int(1, 3), new UsableSkill("test3", "t_usable3"));
            hotbar.Set(new Vector2Int(2, 1), new UsableSkill("test4", "t_usable4"));
            hotbar.Set(new Vector2Int(2, 2), new UsableSkill("test5", "t_usable5"));
            hotbar.Set(new Vector2Int(2, 3), new UsableSkill("test6", "t_usable6"));

            presenter = new SkillHotbarPresenter(logger, hotbar, view);

            if (showOnStart) { presenter.Show(); }
            else { presenter.Hide(); }
        }
    }
}