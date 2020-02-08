using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private FloorManager _floorManager;
    public GameObject start;

    public GameObject finish;

    public Vector3 finishLoc, startLoc;
    // Start is called before the first frame update
    private void Start()
    {
        _floorManager = gameObject.GetComponent<FloorManager>();
        Variables.Instance().goLocationButton.onClick.AddListener(LocationSelected);
    }
    
    private void LocationSelected()
    {
        gameObject.GetComponent<UIManager>().GetStartAndFinishPoint();
        gameObject.GetComponent<UIManager>().IsPanelState();
        
        switch (_floorManager.currentFloor)
        {
            case FloorManager.Floor.First:
                _floorManager.Floor0Management();
                break;
            case FloorManager.Floor.Second:
                _floorManager.Floor1Management();
                break;
            case FloorManager.Floor.Third:
                _floorManager.Floor2Management();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Variables.Instance().isLocationSelected = true;
    }
}
