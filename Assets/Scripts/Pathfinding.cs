using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid _gridReference;
    public Transform StartPosition;//Starting position to pathfind from
    public Transform TargetPosition;//Starting position to pathfind to

    [SerializeField] private GameObject _orderPath;
    [SerializeField] private GameObject _sphere;
    private GameObject[] _childs;
    private GameObject _gameObject;
    private UIManager _uiManager;
    private GameObject _o;
    private GameObject _gameObject1;
    private UIManager _uiManager1;
    private bool _firstLocationSelect;
    private int _index1, _index2;

    private void Start()
    {
        _gameObject1 = GameObject.Find("GameManager");
        _uiManager1 = _gameObject1.GetComponent<UIManager>();
        _o = GameObject.Find("Points");
        _uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        _gameObject = GameObject.Find("Points");
        _childs = GameObject.FindGameObjectsWithTag("pathCube");
    }

    private void Awake()//When the program starts
    {
        _firstLocationSelect = false;
        _gridReference = GetComponent<Grid>();
    }

    private void Update()//Every frame
    {
        if(Variables.Instance().isLocationSelected)
        {
            if (_firstLocationSelect)
            {
                GameObject.Find("Places").transform.GetChild(_index1).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Places").transform.GetChild(_index2).GetChild(0).gameObject.SetActive(false); 
            }
            StartPosition.position = _gameObject.transform
                .GetChild(_uiManager.StartLocIndex).transform.position;
            TargetPosition.position = _o.transform
                .GetChild(_uiManager1.FinishLocIndex).transform.position;
            Debug.Log(_uiManager.StartLocIndex+""+_uiManager.FinishLocIndex);
            GameObject.Find("Places").transform.GetChild(_uiManager.StartLocIndex).GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Places").transform.GetChild(_uiManager.FinishLocIndex).GetChild(0).gameObject.SetActive(true);
            _index1 = _uiManager.StartLocIndex;
            _index2 = _uiManager.FinishLocIndex;
            FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal
            _gridReference.DrawPath();
            Variables.Instance().isLocationSelected = false;
            _firstLocationSelect = true;
        }

        if (gameObject.transform.childCount > 0 && Variables.Instance().selectedPlayButton)
        {
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
                if (Vector3.Distance(startPos,_childs[j].transform.position)< Vector3.Distance(startPos,_childs[i].transform.position))
                {
                    _childs[j].transform.SetSiblingIndex(i);
                    _childs[i].transform.SetSiblingIndex(j);
                    //TODO NEW ORDER PATH OBJECT TO NEW GAMBLE
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

    IEnumerator Wait()
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

    int GetManhattenDistance(Node aNodeA, Node aNodeB)
    {
        var ix = Mathf.Abs(aNodeA.iGridX - aNodeB.iGridX);//x1-x2
        var iy = Mathf.Abs(aNodeA.iGridY - aNodeB.iGridY);//y1-y2

        return ix + iy;
    }
}