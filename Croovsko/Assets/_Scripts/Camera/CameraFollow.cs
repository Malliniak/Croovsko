using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;

    private Vector3 _refVelocity;
    [SerializeField]
    private float _smoothTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _refVelocity, _smoothTime * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Target unassigned!");
        }
    }
}
