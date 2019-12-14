using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorManager : MonoBehaviour
{
    public enum Floor
    {
        First,
        Second,
        Third
    }

    private GameObject _floor0, _floor1, _floor2;
    public Floor _currentFloor;
    private void Awake()
    {
        _floor0 = GameObject.FindGameObjectWithTag("Floor0");
        _floor1 = GameObject.FindGameObjectWithTag("Floor1");
        _floor2 = GameObject.FindGameObjectWithTag("Floor2");
        _currentFloor = Floor.First;
    }

    private void Start()
    {
        _floor1.SetActive(false); //PROBABLY HERE MS PROBLEM
        _floor2.SetActive(false);
        Variables.Instance().floor0Button.onClick.AddListener(Floor0Management);
        Variables.Instance().floor1Button.onClick.AddListener(Floor1Management);
        Variables.Instance().floor2Button.onClick.AddListener(Floor2Management);
    }

    private void Floor0Management()
    {
        if (!_floor0.activeInHierarchy)
        {
            _floor0.SetActive(true);
            _floor1.SetActive(false);
            _floor2.SetActive(false);
        }
        this.gameObject.GetComponent<Grid>().CreateGrid();
    }
    private void Floor1Management()
    {

        if (!_floor1.activeInHierarchy)
        {
            _floor0.SetActive(false);
            _floor1.SetActive(true);
            _floor2.SetActive(false);
        }
        this.gameObject.GetComponent<Grid>().CreateGrid();
    }
    private void Floor2Management()
    {
        if (!_floor2.activeInHierarchy)
        {
            _floor0.SetActive(false);
            _floor1.SetActive(false);
            _floor2.SetActive(true);
        }
        this.gameObject.GetComponent<Grid>().CreateGrid();
    }
}
