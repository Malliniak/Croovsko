using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool _istargetNotNull;

    private Vector3 _refVelocity;

    [SerializeField] private float _smoothTime;

    public GameObject _target;

    // Update is called once per frame
    private void Start()
    {
        _istargetNotNull = _target != null;
    }

    private void FixedUpdate()
    {
        if (_istargetNotNull)
        {
            Vector3 targetPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, -10f);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _refVelocity,
                _smoothTime * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Target unassigned!");
        }
    }
}