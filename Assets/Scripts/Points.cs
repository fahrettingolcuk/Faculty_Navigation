using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private readonly List<GameObject> _pointList = new List<GameObject>();
    private GameObject Floor0Points;
    private GameObject Floor1Points;
    private GameObject Floor2Points;
    
    public void AddNewPoint()
    {
        _pointList.Add(gameObject.transform.GetChild(gameObject.transform.childCount-1).gameObject);
    }
    // Start is called before the first frame update

    public List<GameObject> GetAllPoints()
    {
        return _pointList;
    }

    private void Awake()
    {
        Floor0Points = GameObject.Find("Floor0").transform.GetChild(1).gameObject;
        Floor1Points = GameObject.Find("Floor1").transform.GetChild(1).gameObject;
        Floor2Points = GameObject.Find("Floor2").transform.GetChild(1).gameObject;
        SetPoints();
    }


    private void SetPoints()
    {
        
            for (var i = 0; i <Floor0Points.transform.childCount; i++)
            {
                _pointList.Add(Floor0Points.transform.GetChild(i).gameObject);
            }
            
            for (var i = 0; i <Floor1Points.transform.childCount; i++)
            {
                _pointList.Add(Floor1Points.transform.GetChild(i).gameObject);
            }

            for (var i = 0; i <Floor2Points.transform.childCount; i++)
            {
                _pointList.Add(Floor2Points.transform.GetChild(i).gameObject);
            }
        
    }
}
