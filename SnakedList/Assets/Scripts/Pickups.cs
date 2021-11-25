using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public void Move()
    {
        Vector3[,] grid = GridController.Grid;
        Vector2Int newPos = new Vector2Int(Random.Range(1, grid.GetLength(0) - 1),
            Random.Range(1, grid.GetLength(1) - 1));
        transform.position = new Vector3(grid[newPos.x, newPos.y].x, 1, grid[newPos.x, newPos.y].z);
    }
}
