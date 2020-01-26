using _Scripts.Helpers;
using UnityEngine;

public class MapLevelController : MonoBehaviour
{
    private CurrentLevelHovering _currentMapLevel;
    public string _levelId;
    [SerializeField] private LevelState _levelState;
    [SerializeField] private GameEvent _onCowEnterEvent;

    private SpriteRenderer _renderer;
    public bool IsUnlocked => _levelState.Unlocked;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _levelId = gameObject.name;
        AssetLoader.GetAssetFile(out _levelState, $"LvL{_levelId}State");
        AssetLoader.GetAssetFile(out _currentMapLevel, "CurrentLevelMap");
    }

    private void Start()
    {
        if (!IsUnlocked) _renderer.color = new Color(1f, 0.07f, 0.07f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<MapCowController>()) return;
        _currentMapLevel.SetData(_levelState);
        other.GetComponent<MapCowController>().CurrentLevelHovering = this;
        _onCowEnterEvent.Raise();
    }
}