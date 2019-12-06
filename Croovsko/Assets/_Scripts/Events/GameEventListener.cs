using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")] public GameEvent _event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent _response;

    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    private void OnDisable()
    {
        _event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }
}