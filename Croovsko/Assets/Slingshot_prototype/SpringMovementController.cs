using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpringMovementController : MonoBehaviour
{
    private SpringJoint2D _springJoint2D;
    private bool _touchedCow;
    private float _releaseDelay;
    public float _maxOffsetValue = 2;
    private void Awake()
    {
        _springJoint2D = GetComponent<SpringJoint2D>();
        _springJoint2D.enabled = false;

        _releaseDelay = 1 / (_springJoint2D.frequency * 4);
    }


    private void OnMouseDown()
    {
        Camera main = Camera.main;
        var touchPosWorld = main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 touchPosWorldVector2 = new Vector2(touchPosWorld.x, touchPosWorld.y);

        RaycastHit2D hit2D = Physics2D.Raycast(touchPosWorldVector2, main.transform.forward);

        if (hit2D.collider.gameObject == this.gameObject)
        {
            _touchedCow = true;
            Debug.Log("Touched the Cow!");
            _springJoint2D.connectedAnchor = transform.position;
        }
    }

    private void OnMouseDrag()
    {
        Camera main = Camera.main;
        var touchPosWorld = main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 touchPosWorldVector2 = new Vector2(touchPosWorld.x, touchPosWorld.y);
        Vector3 offset = transform.position - touchPosWorld;
        offset.z = 0;
        if (offset.x > _maxOffsetValue)
        {
            offset.x = _maxOffsetValue;
        }

        if (offset.x < -_maxOffsetValue)
        {
            offset.x = -_maxOffsetValue;
        }

        if (offset.y > _maxOffsetValue)
        {
            offset.y = _maxOffsetValue;
        }
        
        if (offset.y < -_maxOffsetValue)
        {
            offset.y = -_maxOffsetValue;
        }
        Debug.Log(offset);
        Debug.DrawLine(transform.position, touchPosWorld, Color.red);
        Debug.DrawLine(transform.position, transform.position + offset, Color.red);
        
        if (_touchedCow)
        {
            _springJoint2D.connectedAnchor = transform.position + offset;
        }
    }

    private void OnMouseUp()
    {
        if (_touchedCow)
        {
            _springJoint2D.enabled = true;
            StartCoroutine(ReleaseSprint());
            _touchedCow = false;
        }
    }

    IEnumerator ReleaseSprint()
    {
        yield return  new WaitForSeconds(_releaseDelay);
        _springJoint2D.enabled = false;
    }
}
