using System.Collections;
using System.Collections.Generic;
using Elysium.Core;
using Elysium.Core.Utils;
using Elysium.Hotbar;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar.Samples
{
    public class SampleHotbar : MonoBehaviour
    {
        [SerializeField] private bool showOnStart = true;
        [SerializeField] private bool enableLogging = false;
        [SerializeField] private HotbarView view = default;

        private IHotbar hotbar = default;
        private IHotbarPresenter presenter = default;

        private void Start()
        {
            IUnityLogger logger = new UnityLogger(enableLogging);

            hotbar = new Hotbar(logger, new Hotbar.Config(2, 3));
            hotbar.SetUsable(new Vector2Int(1, 1), new SampleUsableEvent("t_usable1"));
            hotbar.SetUsable(new Vector2Int(1, 2), new SampleUsableEvent("t_usable2"));
            hotbar.SetUsable(new Vector2Int(1, 3), new SampleUsableEvent("t_usable3"));
            hotbar.SetUsable(new Vector2Int(2, 1), new SampleUsableEvent("t_usable4"));
            hotbar.SetUsable(new Vector2Int(2, 2), new SampleUsableEvent("t_usable5"));
            hotbar.SetUsable(new Vector2Int(2, 3), new SampleUsableEvent("t_usable6"));

            presenter = new HotbarPresenter(logger, hotbar, view);

            if (showOnStart) { presenter.Show(); }
            else { presenter.Hide(); }
        }
    }
}