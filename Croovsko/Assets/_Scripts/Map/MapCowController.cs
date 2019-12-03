using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class MapCowController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private List<MapLevelController> _mapLevelControllers = new List<MapLevelController>();
    private Vector2Variable _mousePosition;
    
    private Rigidbody2D _rigidbody;

    private bool shouldMove = false;
    private Vector3 destination;
    private void Awake()
    {
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
        _rigidbody = GetComponent<Rigidbody2D>();
        AssetLoader.GetAssetFile(out _gameState, "GameStateData");
        GetMapLevelsControllers();
        _mapLevelControllers.Sort((x, y) => string.Compare(x.LevelID, y.LevelID, StringComparison.Ordinal));
    }

    private void Start()
    {
        if (_gameState.LastPlayedLevel != null)
        {
            MoveToLastPlayedLevel();
        }
        else
        {
            MoveToLevel(_mapLevelControllers[0].transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 2f);
            if (destination == transform.position)
            {
                shouldMove = false;
            }
        }
    }

    private void MoveToLevel(Vector2 position)
    {
        destination = position;
        shouldMove = true;
    }

    private void MoveToLastPlayedLevel()
    {
        var level = _mapLevelControllers.Find(x => x.LevelID.Contains(_gameState.LastPlayedLevel));
        MoveToLevel(level.transform.position);
    }

    private void GetMapLevelsControllers()
    {
        var levels = FindObjectsOfType<MapLevelController>();
        for (int i = 0; i < levels.Length; i++)
        {
            _mapLevelControllers.Add(levels[i]);
        }
    }

    public void OnInput()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_mousePosition.Value), Vector2.zero);
        Debug.Log(hit);
        if(hit)
        {
            MapLevelController mapLevelController = hit.transform.GetComponent<MapLevelController>();
            if(!mapLevelController) return;
            Debug.Log(mapLevelController);
            MoveToLevel(mapLevelController.transform.position);
        }
    }
}