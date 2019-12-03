using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class MapLevelController : MonoBehaviour
{
    public string LevelID;
    [SerializeField] private LevelState _levelState;
    [SerializeField] private CurrentLevelHovering CurrentMapLevel;
    [SerializeField] private GameEvent OnCowEnterEvent;
    
    private void Awake()
    {
        if (_levelState == null && LevelID != null)
        {
            AssetLoader.GetAssetFile(out _levelState, $"{LevelID}State");
        }
        AssetLoader.GetAssetFile(out CurrentMapLevel, "CurrentLevelMap");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MapCowController>())
        {
            Debug.Log("COOCOCO");
            CurrentMapLevel.SetData(_levelState);
            OnCowEnterEvent.Raise();
        }
    }
}
