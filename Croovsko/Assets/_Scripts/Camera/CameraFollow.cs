using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool _isTargetNull;

    private Vector3 _refVelocity;

    [SerializeField] private float _smoothTime;

    public GameObject _target;
    private Vector3 _targetPosition;

    // Update is called once per frame
    private void Start()
    {
        _isTargetNull = _target == null;
    }

    private void FixedUpdate()
    {
        if (_isTargetNull)
        {
            Debug.LogError("Target unassigned!");
            return;
        }
        _targetPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _refVelocity, 
            _smoothTime * Time.deltaTime);
    }
}