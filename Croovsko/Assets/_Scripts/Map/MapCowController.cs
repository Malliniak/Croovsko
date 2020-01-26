using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class MapCowController : MonoBehaviour
{
    private readonly List<MapLevelController> _waypoints = new List<MapLevelController>();
    private Vector3 _destination;
    [SerializeField] private GameState _gameState;
    [SerializeField] private List<MapLevelController> _mapLevelControllers = new List<MapLevelController>();
    private Vector2Variable _mousePosition;

    private bool _shouldMove;

    public MapLevelController CurrentLevelHovering { get; set; }

    private void Awake()
    {
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
        AssetLoader.GetAssetFile(out _gameState, "GameStateData");
        GetMapLevelsControllers();
        _mapLevelControllers.Sort((x, y) => string.Compare(x._levelId, y._levelId, StringComparison.Ordinal));
    }

    private void Start()
    {
        if (_gameState._lastPlayedLevel != null)
            MoveToLastPlayedLevel();
        else
            ConfigureRouteToLevel(_mapLevelControllers[0]);
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
        {
            _destination = _waypoints[0].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * 2f);

            if (_destination == transform.position)
            {
                _waypoints.RemoveAt(0);
                if (_waypoints.Count > 0) return;
                _shouldMove = false;
            }
        }
    }

    private void ConfigureRouteToLevel(MapLevelController levelController)
    {
        int currentIndex = _mapLevelControllers.FindIndex(x => x == CurrentLevelHovering);
        int destinationIndex = _mapLevelControllers.FindIndex(x => x == levelController);
        int waypointsIndex = Math.Abs(currentIndex - destinationIndex);

        Debug.Log($"{waypointsIndex}, {destinationIndex}, {currentIndex}");

        int shouldStartWith = currentIndex + 1;
        if (currentIndex > destinationIndex)
            shouldStartWith = destinationIndex;

        for (int i = shouldStartWith; i < shouldStartWith + waypointsIndex; i++)
        {
            if (waypointsIndex <= 0) continue;
            Debug.Log("ADDING");
            _waypoints.Add(_mapLevelControllers[i]);
        }

        if (currentIndex > destinationIndex)
            _waypoints.Sort((x, y) => string.CompareOrdinal(y._levelId, x._levelId));

        _shouldMove = true;
    }

    private void MoveToLastPlayedLevel()
    {
        MapLevelController level = _mapLevelControllers.Find(x => x._levelId.Contains(_gameState._lastPlayedLevel));
        transform.position = level.transform.position;
    }

    private void GetMapLevelsControllers()
    {
        MapLevelController[] levels = FindObjectsOfType<MapLevelController>();
        for (int i = 0; i < levels.Length; i++) _mapLevelControllers.Add(levels[i]);
    }

    public void OnInput()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_mousePosition._value), Vector2.zero);
        if (hit)
        {
            MapLevelController mapLevelController = hit.transform.GetComponent<MapLevelController>();
            if (!mapLevelController) return;
            if (!mapLevelController.IsUnlocked) return;
            Debug.Log(mapLevelController);
            ConfigureRouteToLevel(mapLevelController);
        }
    }
}