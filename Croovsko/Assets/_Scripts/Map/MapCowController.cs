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

    private bool _shouldMove = false;
    private Vector3 _destination;
    private void Awake()
    {
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
        _rigidbody = GetComponent<Rigidbody2D>();
        AssetLoader.GetAssetFile(out _gameState, "GameStateData");
        GetMapLevelsControllers();
        _mapLevelControllers.Sort((x, y) => string.Compare(x._levelId, y._levelId, StringComparison.Ordinal));
    }

    private void Start()
    {
        if (_gameState._lastPlayedLevel != null)
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
        if (_shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * 2f);
            if (_destination == transform.position)
            {
                _shouldMove = false;
            }
        }
    }

    private void MoveToLevel(Vector2 position)
    {
        _destination = position;
        _shouldMove = true;
    }

    private void MoveToLastPlayedLevel()
    {
        var level = _mapLevelControllers.Find(x => x._levelId.Contains(_gameState._lastPlayedLevel));
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
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_mousePosition._value), Vector2.zero);
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