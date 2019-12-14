using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        if (Input.touchCount > 0)
//        {
//            var touch = Input.GetTouch(0);
//            Vector3 finger = touch.position;
//            var castPoint = Camera.main.ScreenPointToRay(finger);
//            RaycastHit hit;
//            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
//            {
//                this.transform.position = hit.point;
//            }
//        }
        
//        if(Input.GetMouseButtonDown(0))
//        {
//            Vector3 mouse = Input.mousePosition;
//            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
//            RaycastHit hit;
//            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
//            {
//                this.transform.position = hit.point;
//            }
//        }
    }
}
