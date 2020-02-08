using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorManager : MonoBehaviour //KATLARI AÇIP KAPATMA BUTON SCRIPTI
{
    private Pathfinding _pathfinding;
    
    public enum Floor
    {
        First,
        Second,
        Third
    }

    public Floor currentFloor;
    private GameObject _floor0, _floor1, _floor2;
    private UIManager _uiManager;
    private void Awake()
    {
        _pathfinding = gameObject.GetComponent<Pathfinding>();
        //TODO START FINISH OBJECT TRUE FALSE
        currentFloor = Floor.First;
        _floor0 = GameObject.FindGameObjectWithTag("Floor0");
        _floor1 = GameObject.FindGameObjectWithTag("Floor1");
        _floor2 = GameObject.FindGameObjectWithTag("Floor2");
    }

    private void Start()
    {
        _uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        _floor1.SetActive(false); //PROBABLY HERE MS PROBLEM
        _floor2.SetActive(false);
        Variables.Instance().floor0Button.onClick.AddListener(Floor0Management);
        Variables.Instance().floor1Button.onClick.AddListener(Floor1Management);
        Variables.Instance().floor2Button.onClick.AddListener(Floor2Management);
    }
    

    public void WholeFloorsState(bool state)
    {
        if (state)
        {
            _floor0.SetActive(true);
            _floor1.SetActive(true);
            _floor2.SetActive(true);
        }
        else
        {
            _floor0.SetActive(false);
            _floor1.SetActive(false);
            _floor2.SetActive(false); 
        }
    }
    
    
    
    public void Floor0Management()
    {
            _floor0.SetActive(true);
            _floor1.SetActive(false);
            _floor2.SetActive(false);
            currentFloor = Floor.First;
        this.gameObject.GetComponent<Grid>().CreateGrid();
    }
    public void Floor1Management()
    {
        
            _floor0.SetActive(false);
            _floor1.SetActive(true);
            _floor2.SetActive(false);
            currentFloor = Floor.Second;
        this.gameObject.GetComponent<Grid>().CreateGrid();
    }
    public void Floor2Management()
    {
            _floor0.SetActive(false);
            _floor1.SetActive(false);
            _floor2.SetActive(true);
            currentFloor = Floor.Third;
            if (Variables.Instance().isLocationSelected)
            {
                if (!_uiManager.StartObject.CompareTag(_uiManager.FinishObject.tag))
                {
                    Debug.Log(_uiManager.StartObject+"sdsds"+_uiManager.FinishObject);
                
                
                }
            }

            this.gameObject.GetComponent<Grid>().CreateGrid();
    }
    
    
}
