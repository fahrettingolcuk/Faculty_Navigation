using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Vector3 _firstPoint;

    private Vector3 _secondPoint;

    private float _xAngle;

    private float _yAngle;
    
    private float _zAngle;

    private float _xAngleTmp;

    private float _yAngleTmp;

    private float _zAngleTmp;
    // Start is called before the first frame update
    private void Start()
    {
        _xAngle = 0;
        _yAngle = 0;
        _xAngleTmp = 0;
        _yAngleTmp = 0;
        _zAngle = 0;
        _zAngleTmp = 0;
        //transform.rotation = Quaternion.Euler(transfor, _xAngle,0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0 && Variables.Instance().isLocationSelected)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _firstPoint = Input.GetTouch(0).position;
                _xAngleTmp = _xAngle;
                _yAngleTmp = _yAngle;
                _zAngleTmp = _zAngle;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _secondPoint = Input.GetTouch(0).position;
                _xAngle = _xAngleTmp + (_secondPoint.x - _firstPoint.x) * 180 / Screen.width;
                _yAngle = _yAngleTmp + (_secondPoint.y - _firstPoint.y) * 90 / Screen.height;
                _zAngle = _yAngleTmp + (_secondPoint.z - _firstPoint.z) * 180 / Screen.width;
                transform.rotation = Quaternion.Euler(_yAngle,_xAngle,0);
            }
        }
    }
}
