using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class MapLevelController : MonoBehaviour
{
    public string _levelId;
    [SerializeField] private LevelState _levelState;
    private CurrentLevelHovering _currentMapLevel;
    [SerializeField] private GameEvent _onCowEnterEvent;
    public bool IsUnlocked => _levelState.Unlocked;

    private SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _levelId = gameObject.name;
        AssetLoader.GetAssetFile(out _levelState, $"LvL{_levelId}State");
        AssetLoader.GetAssetFile(out _currentMapLevel, "CurrentLevelMap");
    }

    private void Start()
    {
        if (!IsUnlocked)
        {
            _renderer.color = new Color(1f, 0.07f, 0.07f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<MapCowController>()) return;
        _currentMapLevel.SetData(_levelState);
        other.GetComponent<MapCowController>().CurrentLevelHovering = this;
        _onCowEnterEvent.Raise();
    }
}
