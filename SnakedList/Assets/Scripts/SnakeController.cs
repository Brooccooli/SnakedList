using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject snakePartPrefab;
    [SerializeField] private GameObject snakeHeadPrefab;
    [SerializeField] private int framesToSkip = 5;
    [SerializeField] private GameObject explosionEffect;

    // Tiles occupied by snaked
    public static List<Vector2Int> OccupiedTiles = new List<Vector2Int>();

    private int skippedFrames = 0;

    private LinkedList<bodyPart> body;
    
    // Position and direction in grid
    private Vector2Int dir = new Vector2Int(1, 0);
    private Vector2Int currentPos = new Vector2Int(1, 1);

    private struct bodyPart
    {
        public Vector3 GoalPos;
        public GameObject Part;
    }

    private void Start()
    {
        bodyPart bodyPart = new bodyPart();
        bodyPart.Part = Instantiate(snakeHeadPrefab, GridController.Grid[currentPos.x, currentPos.y] + new Vector3(0, 1),
            Quaternion.identity);
        bodyPart.Part.transform.parent = transform;
        bodyPart.Part.gameObject.name += " head";

        body = new LinkedList<bodyPart>(bodyPart);
        for (int i = 1; i < 3; i++)
        {
            bodyPart.Part = Instantiate(snakePartPrefab, GridController.Grid[currentPos.x, currentPos.y + i] + new Vector3(0, 1),
                Quaternion.identity);
            bodyPart.Part.transform.parent = transform;
            body.Add(bodyPart);
        }
    }

    #region Movement
    private void setPos()
    {
        Vector3 currPos = body.data.Part.transform.position;
        body.data.Part.transform.position = new Vector3(GridController.Grid[currentPos.x, currentPos.y].x,
             currPos.y, GridController.Grid[currentPos.x, currentPos.y].z);
        
        LinkedList<bodyPart> temp = body;
        // Set goal pos
        while (true)
        {
            if (temp.next == null) break;
            temp.next.data.GoalPos = temp.data.Part.transform.position;
            temp = temp.next;
        }
    }

    private void move()
    {
        OccupiedTiles.Clear();
        LinkedList<bodyPart> temp = body;
        // Move
        while (true)
        {
            if (temp.next == null) break;
            temp.next.data.Part.transform.position = temp.next.data.GoalPos;
            findTileInGrid(temp);
            temp = temp.next;
        }

        void findTileInGrid(LinkedList<bodyPart> current)
        {
            Vector3[,] grid = GridController.Grid;
            Vector3Int pos = new Vector3Int();
            pos.x = (int)current.next.data.Part.transform.position.x;
            pos.z = (int)current.next.data.Part.transform.position.z;

            for (int  x = 0; x  < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Vector3Int currentTile = new Vector3Int();
                    currentTile.x = (int)grid[x, y].x;
                    currentTile.z = (int)grid[x, y].z;

                    if (pos.x == currentTile.x && pos.z == currentTile.z)
                    {
                        OccupiedTiles.Add(new Vector2Int(x,y));
                        return;
                    }
                }
            }
        }
    }
    #endregion

    public void Grow()
    {
        Vector3 newPos;
        LinkedList<bodyPart> temp = body;
        // Move
        while (true)
        {
            if (temp.next == null)
            {
                newPos = temp.data.Part.gameObject.transform.position;
                break;
            }
            temp = temp.next;
        }
        bodyPart bodyPart = new bodyPart();
        bodyPart.Part = Instantiate(snakePartPrefab, newPos, Quaternion.identity);
        bodyPart.Part.transform.parent = transform;
        bodyPart.GoalPos = newPos;
        body.Add(bodyPart);
    }

    private void controls()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            dir = Vector2Int.zero;
            dir.x = (int)(Input.GetAxisRaw("Horizontal") * 1.5f);
            dir.y = (int)(Input.GetAxisRaw("Vertical") * 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grow();
        }
    }

    private void deathCheck()
    {
        Vector2Int futurePos = currentPos + dir;
        
        try
        {
            if (GridController.Grid[futurePos.x, futurePos.y].y == -1)
            {
                SceneManager.LoadScene("Dead");
            }
        }
        catch (Exception e) // futurePos is outside of grid
        {
            SceneManager.LoadScene("Dead");
        }
    }

    public void Split()
    {
        if (body.length <= 3)
        {
            return;
        }
        
        int splitPos = Random.Range(2, body.length - 3) - 1;

        for (int i = 0; i < splitPos; i++)
        {
            body.DeletLast();
        }

        Vector3 spawnPos = body.LastData.Part.transform.position;
        Instantiate(explosionEffect, spawnPos, Quaternion.identity);
    }
    
    private void Update()
    {
        controls();
    }

    private void FixedUpdate()
    {
        if (skippedFrames == 0)
        {
            currentPos += dir;
            deathCheck();
            move();
            setPos();
            skippedFrames = framesToSkip;
        }

        skippedFrames--;
    }
}