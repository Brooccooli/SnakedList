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

    private Vector3[,] clearOccupiedTiles()
    {
        Vector3[,] grid = GridController.Grid;
        for (int x = GridController.Grid.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = GridController.Grid.GetLength(1) - 1; y >= 0; y--)
            {
                if(checkIfOccupied(new Vector2Int(x, y)))
                {
                    // take away the position in the grid
                    //grid.
                }
            }
        }

        /*Vector3[,] removeAt(Vector3[,] currGrid, Vector2Int index)
        {
            Vector3[,] newGrid = new Vector3[currGrid.GetLength(0), currGrid.GetLength(1)];
            for (int x = 0; x < currGrid.GetLength(0); x++)
            {
                for (int y = 0; y < currGrid.GetLength(1); y++)
                {

                }
            }
        }*/

        bool checkIfOccupied(Vector2Int index)
        {
            List<Vector2Int> occupiedTiles = SnakeController.OccupiedTiles;
            for (int i = 0; i < occupiedTiles.Count; i++)
            {
                if(index == occupiedTiles[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
