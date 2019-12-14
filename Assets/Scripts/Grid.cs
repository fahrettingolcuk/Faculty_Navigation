using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public LayerMask WallMask;
    public Vector2 vGridWorldSize;
    public float fNodeRadius;
    public float fDistanceBetweenNodes;

    Node[,] NodeArray;
    public List<Node> FinalPath;


    private float _fNodeDiameter;
    private int _iGridSizeX, _iGridSizeY;

    private void Start()
    {
        CreateGrid();//Draw the grid
    }

    public void CreateGrid()
    {
        _fNodeDiameter = fNodeRadius * 2;
        _iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / _fNodeDiameter);
        _iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / _fNodeDiameter);
        NodeArray = new Node[_iGridSizeX, _iGridSizeY];//Declare the array of nodes.
        var bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;//Get the real world position of the bottom left of the grid.
        for (var x = 0; x < _iGridSizeX; x++)//Loop through the array of nodes.
        {
            for (var y = 0; y < _iGridSizeY; y++)//Loop through the array of nodes
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * _fNodeDiameter + fNodeRadius) + Vector3.forward * (y * _fNodeDiameter + fNodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool wall = !Physics.CheckSphere(worldPoint, fNodeRadius, WallMask);

                NodeArray[x, y] = new Node(wall, worldPoint, x, y);//Create a new node in the array.
            }
        }
    }
    
    //Function that gets the neighboring nodes of the given node.
    public IEnumerable<Node> GetNeighboringNodes(Node aNeighborNode)
    {
        var neighborList = new List<Node>();//Make a new list of all available neighbors.
        int icheckX;//Variable to check if the XPosition is within range of the node array to avoid out of range errors.
        int icheckY;//Variable to check if the YPosition is within range of the node array to avoid out of range errors.

        //Check the right side of the current node.
        icheckX = aNeighborNode.iGridX + 1;
        icheckY = aNeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < _iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _iGridSizeY)//If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current node.
        icheckX = aNeighborNode.iGridX - 1;
        icheckY = aNeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < _iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _iGridSizeY)//If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current node.
        icheckX = aNeighborNode.iGridX;
        icheckY = aNeighborNode.iGridY + 1;
        if (icheckX >= 0 && icheckX < _iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _iGridSizeY)//If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current node.
        icheckX = aNeighborNode.iGridX;
        icheckY = aNeighborNode.iGridY - 1;
        if (icheckX >= 0 && icheckX < _iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _iGridSizeY)//If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        return neighborList;//Return the neighbors list.
    }

    //Gets the closest node to the given world position.
    public Node NodeFromWorldPoint(Vector3 aVWorldPos)
    {
        var ixPos = ((aVWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        var iyPos = ((aVWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((_iGridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((_iGridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }

    private List<Vector3> _myArray = new List<Vector3>();
    [SerializeField] private GameObject _pathCube;

    //Function that draws the wireframe
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(vGridWorldSize.x, 1, vGridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector

        if (NodeArray != null)//If the grid is not empty
        {
            foreach (Node n in NodeArray)//Loop through every node in the grid
            {
                if (n.bIsWall)//If the current node is a wall node
                {
                    Gizmos.color = Color.white;//Set the color of the node
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the node
                }


                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that node
                    }

                }


                Gizmos.DrawCube(n.vPosition, Vector3.one * (_fNodeDiameter - fDistanceBetweenNodes));//Draw the node at the position of the node.
            }
     

        }
        
    }

    public void DrawPath()
    {
        if (gameObject.transform.childCount > 0)
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }


        if (NodeArray != null)//If the grid is not empty
        {
            foreach (var n in NodeArray)//Loop through every node in the grid
            {
                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current node is in the final path
                    {
                        var Obj = Instantiate(_pathCube, n.vPosition, Quaternion.identity) as GameObject;
                        Obj.gameObject.transform.SetParent(gameObject.transform);
                        //Destroy(Obj,1);
                       //_sphere.transform.position = (n.vPosition,_sphere.transform.position,5f)
                    }

                }
                
            }
        }
    }
    
    
}