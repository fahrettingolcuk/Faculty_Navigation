using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using  UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
    private Points myPoints;
    private readonly List<string> _pointStringList = new List<string>();
    
    private string _startLoc, _finishLoc;
    public int StartLocIndex { get; set; }

    public int FinishLocIndex { get; set; }


    private void Start()
    {
       
        Variables.Instance().locationButton.onClick.AddListener(IsPanelState);
        Variables.Instance().playButton.onClick.AddListener(PlayAnimation);
        SetDropDownList();
    }

    public void PlayAnimation()
    {
        if (Variables.Instance().selectedPlayButton)
        {
            Variables.Instance().selectedPlayButton = false;
        }
        else
        {
            Variables.Instance().selectedPlayButton = true;
        }
        
    }
    public void IsPanelState()
    {
        Variables.Instance().locationSelectPanel.SetActive(!Variables.Instance().locationSelectPanel.activeInHierarchy);
    }

    public void GetStartAndFinishPoint()
    {
        if(_startLoc ==null && _finishLoc==null)
        {
            Debug.Log("seçim yok"); //TODO FOR 1 ELEMENT CONTROL
        }
        else
        {
            StartLocIndex = FindIndex(Variables.Instance().startLocationDropDown.options[Variables.Instance().startLocationDropDown.value].text);
            FinishLocIndex = FindIndex(Variables.Instance().finishLocationDropDown.options[Variables.Instance().finishLocationDropDown.value].text);
        }
    }
    

    private void ConvertListToNames()
    {
        foreach (var points in  gameObject.GetComponent<Points>().GetAllPoints())
        {
            _pointStringList.Add(points.name);
        }
    }
    
    private int FindIndex(string location)
    {
        var _search=0;
        for (var i = 0; i < _pointStringList.Count; i++)
        {
            if(location== _pointStringList[i])
            {
                _search = i;
                break;
            }
        }
        return _search;
    }
    
    
    private void SetDropDownList()
    {
        ConvertListToNames();
        Variables.Instance().startLocationDropDown.AddOptions(_pointStringList);
        Variables.Instance().finishLocationDropDown.AddOptions(_pointStringList);
    }
    public void GetStartValue()
    {
        _startLoc = Variables.Instance().startLocationDropDown.options[Variables.Instance().startLocationDropDown.value].text;
    }
    public void GetFinishValue()
    {
        _finishLoc = Variables.Instance().finishLocationDropDown.options[Variables.Instance().finishLocationDropDown.value].text;
    }
    
    
    
    
    
}
