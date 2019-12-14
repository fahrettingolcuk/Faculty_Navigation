using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variables : MonoBehaviour
{
    public Button locationButton;
    public GameObject locationSelectPanel;
    public Dropdown startLocationDropDown;
    public Dropdown finishLocationDropDown;
    public Button goLocationButton;
    public Button playButton;
    public bool isLocationSelected;
    public bool selectedPlayButton;
    public Button floor0Button;
    public Button floor1Button;
    public Button floor2Button;

    private static Variables _instance;
    
    
    
    public static Variables Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        isLocationSelected = false;
        selectedPlayButton = false;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else _instance = this;
    }
}
