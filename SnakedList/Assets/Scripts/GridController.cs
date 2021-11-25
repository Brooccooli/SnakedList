using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int size;

    public static Vector3[,] Grid;

    private float margin = 0.2f;
    
    // Reminder: set y=-1 to take away tile
    void Awake()
    {
        Grid = new Vector3[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Grid[i, j] = new Vector3(i - (size / 2) + (i * margin), 0, j - (size / 2) + (j * margin));
                GameObject currTile = Instantiate(tile, Grid[i, j], Quaternion.identity);
                if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
                {
                    currTile.GetComponent<MeshRenderer>().material.color = Color.red;
                    currTile.transform.position = currTile.transform.position + new Vector3(0, Random.Range(0.5f, 2), 0);
                }
            }
        }
    }
}
