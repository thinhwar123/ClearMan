using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private bool draw;
    [SerializeField] private bool newDraw;
    [SerializeField] private TileBase ruleTile;

    [SerializeField] private int wallX;
    [SerializeField] private int wallY;
    [SerializeField] private int wallThick;


    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject groundGrid;

    public void OnDrawGizmos()
    {
        if (!draw && groundGrid == null)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(wallX, wallY, 0));
        }
        else if(draw && groundGrid == null)
        {
            groundGrid = Instantiate(groundPrefab, transform.position, Quaternion.identity);
            for (int i = 0; i < wallX; i++)
            {
                for (int j = 0; j < wallY; j++)
                {
                    if (i < wallThick || j < wallThick || i >= wallX - wallThick || j >= wallY - wallThick)
                    {
                        groundGrid.GetComponentInChildren<Tilemap>().SetTile(new Vector3Int(i - wallX / 2, j - wallY / 2, 0), ruleTile);
                    }
                    
                }
            }

        }
        else if(!draw && groundGrid != null)
        {
            DestroyImmediate(groundGrid);
            groundGrid = null;
        }
        else if (newDraw)
        {
            groundGrid = null;
            draw = false;
        }
    }
}
