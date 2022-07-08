using Elysium.Core;
using UnityEngine;

namespace Elysium.Hotbar.Samples
{
    public class ItemHotbar : MonoBehaviour
    {
        [SerializeField] private bool showOnStart = true;
        [SerializeField] private bool enableLogging = false;
        [SerializeField] private Hotbar.Config config = default;
        [SerializeField] private ItemHotbarView view = default;

        private IHotbar<IUsableItem> hotbar = default;
        private IHotbarPresenter presenter = default;

        private void Start()
        {
            IUnityLogger logger = new UnityLogger(enableLogging);

            hotbar = new Hotbar<IUsableItem>(logger, config);
            hotbar.Set(new Vector2Int(1, 1), new UsableItem("test1", 1, "t_usable1"));
            hotbar.Set(new Vector2Int(1, 2), new UsableItem("test2", 2, "t_usable2"));
            hotbar.Set(new Vector2Int(1, 3), new UsableItem("test3", 3, "t_usable3"));
            hotbar.Set(new Vector2Int(2, 1), new UsableItem("test4", 4, "t_usable4"));
            hotbar.Set(new Vector2Int(2, 2), new UsableItem("test5", 5, "t_usable5"));
            hotbar.Set(new Vector2Int(2, 3), new UsableItem("test6", 6, "t_usable6"));

            presenter = new ItemHotbarPresenter(logger, hotbar, view);

            if (showOnStart) { presenter.Show(); }
            else { presenter.Hide(); }
        }
    }
}