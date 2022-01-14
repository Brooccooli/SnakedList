using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public void Move()
    {
        List<Vector2Int> tiles = clearOccupiedTiles();
        Vector3[,] grid = GridController.Grid;
        int i = Random.Range(0, tiles.Count);
        transform.position = new Vector3(grid[tiles[i].x, tiles[i].y].x, 1, grid[tiles[i].x, tiles[i].y].z);
    }

    private List<Vector2Int> clearOccupiedTiles()
    {
        List<Vector2Int> avaliableTiles = gridToList();
        for (int i = avaliableTiles.Count - 1; i >= 0; i--)
        {
            if (checkIfOccupied(avaliableTiles[i]))
            {
                avaliableTiles.RemoveAt(i);
            }
        }

        return avaliableTiles;

        List<Vector2Int> gridToList()
        {
            List<Vector2Int> posList = new List<Vector2Int>();
            Vector3[,] grid = GridController.Grid;
            for (int x = 1; x < grid.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < grid.GetLength(1) - 1; y++)
                {
                    posList.Add(new Vector2Int(x, y));
                }
            }

            return posList;
        }

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
