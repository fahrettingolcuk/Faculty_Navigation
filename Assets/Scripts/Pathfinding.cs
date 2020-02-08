using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid _gridReference;
    public Transform StartPosition;//Starting position to pathfind from      //MAVI KUTU BASLANGIC
    public Transform TargetPosition;//Starting position to pathfind to       //MAVI KUTU BİTİS
    public Transform MiddlePosition;
    
    
    
    [SerializeField] private GameObject _orderPath;      //PATHLERİN DÜZENLENDİĞİ ANA GAMEOBJECT
    [SerializeField] private GameObject _sphere;        //TOPUMUZ
    private GameObject[] _childs;
    private GameObject _pointRoot;
    private UIManager _uiManager;

    private FloorManager _floorManager;
    
    
    private bool _firstLocationSelect;
    private int _index1, _index2;

    private void Start()
    {
        _floorManager = gameObject.GetComponent<FloorManager>();
        _uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        
    }

    private void Awake()//When the program starts
    {
        _firstLocationSelect = false;
        _gridReference = GetComponent<Grid>();
    }

    private void LocationSettings()
    {
        if(Variables.Instance().isLocationSelected)
        {
            switch (_uiManager.StartObject.tag)
            {
                case "Floor0_Places":
                    _floorManager.Floor0Management();
                    break;
                case "Floor1_Places":
                    _floorManager.Floor1Management();
                    break;
                case "Floor2_Places":
                    _floorManager.Floor2Management();
                    break;
            }
            
            //TODO LOCATION SELECT REMOVE BEFORE LOCATIONS
//            if (_firstLocationSelect)
//            {
//                switch (_uiManager.tag1)
//                {
//                    case "Floor0_Places":
//                       GameObject.Find("Floor0").transform.GetChild(0).GetChild(_index1).GetChild(0).gameObject.SetActive(false);
//                        break;
//                    case "Floor1_Places":
//                        GameObject.Find("Floor1").transform.GetChild(0).GetChild(_index1).GetChild(0).gameObject.SetActive(false);
//                        break;
//                    case "Floor2_Places":
//                        GameObject.Find("Floor2").transform.GetChild(0).GetChild(_index1).GetChild(0).gameObject.SetActive(false);
//                        break;
//                }
//                switch (_uiManager.tag2)
//                {
//                    case "Floor0_Places":
//                        GameObject.Find("Floor0").transform.GetChild(0).GetChild(_index2).GetChild(0).gameObject.SetActive(false);
//                        break;
//                    case "Floor1_Places":
//                        GameObject.Find("Floor1").transform.GetChild(0).GetChild(_index2).GetChild(0).gameObject.SetActive(false);
//                        break;
//                    case "Floor2_Places":
//                        GameObject.Find("Floor2").transform.GetChild(0).GetChild(_index2).GetChild(0).gameObject.SetActive(false);
//                        break;
//                }
//            }
            
            
            //TODO CONTROL FLOOR
            if (!_uiManager.StartObject.CompareTag(_uiManager.FinishObject.tag)) //DIFFERENT FLOOR CONTROL
            {
                if (_uiManager.StartObject.CompareTag("Floor0_Places"))
                {
                    
                    //MERDİVENLER SIKINTILI
                    //EN YAKIN MERDİVENE YÖNLENDİR
                    GameObject stair1 = GameObject.Find("Giris-Merdiven").gameObject;
                    GameObject stair2 = GameObject.Find("Kantin-Merdiven").gameObject;
                    Vector3 dist1 = _uiManager.StartObject.transform.position - stair1.transform.position;
                    Vector3 dist2 = _uiManager.FinishObject.transform.position - stair2.transform.position;
                    _pointRoot = GameObject.Find("Floor0").transform.GetChild(1).gameObject;
                    if (dist1.sqrMagnitude < dist2.sqrMagnitude)
                    {
                        Debug.Log("giris daha yakin");
                        StartPosition.position = _pointRoot.transform
                            .GetChild(_uiManager.StartObject.transform.GetSiblingIndex()).transform.position;
                        
                        TargetPosition.position = _pointRoot.transform.GetChild(stair1.transform.GetSiblingIndex())
                            .transform.position;
                        
                        

                    }
                    else
                    {
                        Debug.Log("kantin daha yakin");
                        StartPosition.position = _pointRoot.transform
                            .GetChild(_uiManager.StartObject.transform.GetSiblingIndex()).transform.position;
                        TargetPosition.position = _pointRoot.transform.GetChild(stair2.transform.GetSiblingIndex())
                            .transform.position;

                    }
                }
            }
            else
            {
                if (_uiManager.StartObject.CompareTag("Floor0_Places"))
                {
                    _pointRoot = GameObject.Find("Floor0").transform.GetChild(1).gameObject;
                    //FLOOR 0 ROOT PLACE
                }
                else if (_uiManager.StartObject.CompareTag("Floor1_Places"))
                {
                    _pointRoot = GameObject.Find("Floor1").transform.GetChild(1).gameObject;
                    //FLOOR 1 ROOT PLACE
                }
                else if (_uiManager.StartObject.CompareTag("Floor2_Places"))
                {
                    _pointRoot = GameObject.Find("Floor2").transform.GetChild(1).gameObject;
                    //FLOOR 2 ROOT PLACE
                }

                StartPosition.position = _pointRoot.transform
                    .GetChild(_uiManager.StartObject.transform.GetSiblingIndex()).transform.position;


                TargetPosition.position = _pointRoot.transform
                    .GetChild(_uiManager.FinishObject.transform.GetSiblingIndex()).transform.position;
                
                
            }
                            
            _uiManager.StartObject.transform.GetChild(0).gameObject.SetActive(true);
            _uiManager.FinishObject.transform.GetChild(0).gameObject.SetActive(true);
            
            
            _index1 = _uiManager.StartLocIndex;
            _index2 = _uiManager.FinishLocIndex;
   
            
            FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal


            _gridReference.DrawPath();
            
            Variables.Instance().isLocationSelected = false;
            
            _firstLocationSelect = true;
            
        }
    }

    private void Update()//Every frame
    {

        LocationSettings();
        
        
        if (gameObject.transform.childCount > 0 && Variables.Instance().selectedPlayButton)
        {
            _childs = GameObject.FindGameObjectsWithTag("pathCube");
            //OrderPath(StartPosition.position);
            _sphere.SetActive(true);
            _sphere.transform.position = gameObject.transform.GetChild(0).position;
            SphereMovement();
        }
    }
    
    private void OrderPath(Vector3 startPos)
    {
        for (int i = 0; i < _childs.Length; i++)
        {
            for (int j = 1; j < _childs.Length; j++)
            {
                if (Vector3.Distance(startPos,_childs[j].transform.position) < Vector3.Distance(startPos,_childs[i].transform.position))
                {
                    //Debug.Log(_childs[i].transform.GetSiblingIndex());
                }
            }
        }
        
    }
    private void SphereMovement()
    {
        StopAllCoroutines();
        StartCoroutine(Wait());
        Variables.Instance().selectedPlayButton = false;
        //_sphere.SetActive(false);
    }

    private IEnumerator Wait()
    {
        _sphere.SetActive(true);
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            _sphere.transform.position = Vector3.MoveTowards(_sphere.transform.position,gameObject.transform.GetChild(i).position, 2f);
            yield return new WaitForSeconds(0.1f);
        }
        _sphere.SetActive(false);
    }
    
    private void FindPath(Vector3 aStartPos, Vector3 aTargetPos)
    {
        var startNode = _gridReference.NodeFromWorldPoint(aStartPos);//Gets the node closest to the starting position
        var targetNode = _gridReference.NodeFromWorldPoint(aTargetPos);//Gets the node closest to the target position

        var openList = new List<Node>();
        var closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            var currentNode = openList[0];//Create a node and set it to the first item in the open list
            for (var i = 1; i < openList.Count; i++)//Loop through the open list starting from the second object
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].ihCost < currentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    currentNode = openList[i];//Set the current node to that object
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach (var neighborNode in _gridReference.GetNeighboringNodes(currentNode))
            {
                if (!neighborNode.bIsWall || closedList.Contains(neighborNode))
                {
                    continue;//Skip it
                }
                var moveCost = currentNode.igCost + GetManhattenDistance(currentNode, neighborNode);

                if (moveCost < neighborNode.igCost || !openList.Contains(neighborNode))
                {
                    neighborNode.igCost = moveCost;
                    neighborNode.ihCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }

        }
    }


    private void GetFinalPath(Node aStartingNode, Node aEndNode)
    {
        var finalPath = new List<Node>();
        var currentNode = aEndNode;

        while (currentNode != aStartingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();

        _gridReference.FinalPath = finalPath;

    }

    private int GetManhattenDistance(Node aNodeA, Node aNodeB)
    {
        var ix = Mathf.Abs(aNodeA.iGridX - aNodeB.iGridX);//x1-x2
        var iy = Mathf.Abs(aNodeA.iGridY - aNodeB.iGridY);//y1-y2

        return ix + iy;
    }
}