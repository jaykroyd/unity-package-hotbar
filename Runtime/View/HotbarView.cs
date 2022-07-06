using System;
using System.Collections;
using System.Collections.Generic;
using Elysium.Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Elysium.Hotbar
{
    public class HotbarView : MonoBehaviour
    {
        [SerializeField] private bool showOnStart = false;
        [SerializeField] private View<Image> view = default;
        private IHotbarPresenter presenter = default;

        public HotbarView(IHotbarPresenter _presenter)
        {
            this.presenter = _presenter;
            presenter.OnShow += Show;
            presenter.OnHide += Hide;
            presenter.OnChanged += Refresh;
        }

        private void Start()
        {
            if (showOnStart) { Show(); }
            else { Hide(); }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Refresh()
        {
            // TODO: Instantiate slots
            view.Spawn(1);
        }
    }
}