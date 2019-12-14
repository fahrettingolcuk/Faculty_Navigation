using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    
    public GameObject start;

    public GameObject finish;

    public Vector3 finishLoc, startLoc;
    // Start is called before the first frame update
    private void Start()
    {
        Variables.Instance().goLocationButton.onClick.AddListener(LocationSelected);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void LocationSelected()
    {
        gameObject.GetComponent<UIManager>().GetStartAndFinishPoint();
        gameObject.GetComponent<UIManager>().IsPanelState();
        Variables.Instance().isLocationSelected = true;
    }
}
