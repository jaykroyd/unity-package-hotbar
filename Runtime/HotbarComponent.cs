using System.Collections;
using System.Collections.Generic;
using Elysium.Core;
using Elysium.Hotbar;
using UnityEngine;

public class HotbarComponent : MonoBehaviour
{
    [SerializeField] private Vector2 indexToUse = new Vector2Int(1, 1);
    private Hotbar hotbar = default;
    private UnityLogger logger = new UnityLogger();

    private void Start()
    {
        hotbar = new Hotbar(logger, new Hotbar.Config(1, 8));
        hotbar.Register(new Vector2Int(1, 1), new UseEvent());
        hotbar.Register(new Vector2Int(1, 2), new UseEvent());
        hotbar.Register(new Vector2Int(1, 3), new UseEvent());
        hotbar.Register(new Vector2Int(1, 4), new UseEvent());
        hotbar.Register(new Vector2Int(1, 5), new UseEvent());
        hotbar.Register(new Vector2Int(1, 6), new UseEvent());
        hotbar.Register(new Vector2Int(1, 7), new UseEvent());
        // hotbar.Register(new Vector2Int(1, 8), new UseEvent());
    }

    [ContextMenu("Use")]
    private void Use()
    {
        Debug.Log($"Using slot {indexToUse}.");
        hotbar.Use(new Vector2Int((int)indexToUse.x, (int)indexToUse.y));
    }
}
