using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMaker : MonoBehaviour
{
    [Header("World Propeties")]
    [Range(1,64)]
    public int height=1;
    [Range(1, 64)]
    public int width=1;
    [Range(1, 64)]
    public int depth=1;

    [Header("Scaling values")]
    public float min = 16.0f;
    public float max = 24.0f;

    [Header ("Grid")]
    public List<GameObject> grid;

    [Header("Tile Properties")]
    public Transform tileParent;
    public GameObject threeDtile;
    

    // Start is called before the first frame update
    void Start()
    {
        grid = new List<GameObject>();


        //generation

        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < height; y++)       //y
        {
         float rand = Random.Range(min, max);
        
            for (int z = 0; z< depth; z++)         //z 
            {
                for (int x = 0; x < width; x++)        //x
                {
                    if (y< Mathf.PerlinNoise((x+offsetX)/rand, (z + offsetZ) / rand)*depth *0.5)
                    {
                       var tile = Instantiate(threeDtile, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.parent = tileParent;
                        grid.Add(tile);
                    }
                   
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
