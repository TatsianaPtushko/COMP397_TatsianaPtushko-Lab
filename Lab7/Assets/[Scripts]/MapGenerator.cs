using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile Resources")]
    public List<GameObject> tilePrefabs;
    public GameObject startTile;
    public GameObject goalTile;

    [Header("Map Properties")]

    [Range(3, 30)]
    public int width =3;
    [Range(3,30)]
    public int depth=3;

    public Transform parent;

    [Header("Tile Generateds")]
    public List<GameObject> tiles;

    private int startWidth;
    private int startDepth;

    // Start is called before the first frame update
    void Start()
    {
        startWidth =width;
     startDepth =depth;
    BuildMap();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (width!=startWidth || startDepth != depth)
        {
            ResetMap();
            BuildMap();
        }
    }

    public void ResetMap()
    {
        startWidth = width;
        startDepth = depth;
        var tempTile = tiles[0];
        var size = tiles.Count;

        for (int i = 0; i < size; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();//remove all tiles
        tiles.Add(tempTile);
        
    }

    public void BuildMap()
    {
        /*

        for (int i = 0; i <3; i++)
        {
            var randomPrefabIndex = Random.Range(0, 4);
            var randRotation = Quaternion.Euler(0.0f, Random.Range(0,4)*90.0f,0.0f);
            tiles.Add(Instantiate(tilePrefabs[randomPrefabIndex], Vector3.zero, randRotation, parent.transform));
       }
        tiles[1].transform.position = new Vector3(0.0f,0.0f,20.0f);
        tiles[2].transform.position = new Vector3(20.0f, 0.0f, 20.0f);
        tiles[3].transform.position = new Vector3(20.0f, 0.0f, 0.0f);
        */

        var offset = new Vector3(20.0f,0.0f,20.0f);
        //place the startTile
        tiles.Add(Instantiate(startTile, Vector3.zero, Quaternion.identity, parent));

        //choose a random position of Goal tile

        var randomGoalRowPosition = Random.Range(1, depth+1);
        var randomGoalColPosition = Random.Range(1, width+1);


        //generate more tiles if both width & depth > 2
        for (int row = 1; row <= depth; row++)
            {  
                for (int col = 1; col <= width; col++)
                {
                if (row == 1 && col == 1)
                        continue;
                var tilePosition = new Vector3(col * 20.0f, 0.0f, row * 20.0f) - offset;
                
                if (row == randomGoalRowPosition && col == randomGoalColPosition)
                { 
                    //place the goal tile
                  tiles.Add(Instantiate(goalTile, tilePosition, Quaternion.identity, parent));
                }                   
                    var randomPrefabIndex = Random.Range(0, 4);
                    var randRotation = Quaternion.Euler(0.0f, Random.Range(0, 4) * 90.0f, 0.0f);
               
        //        var newPosition = new Vector3(col * 20.0f, 0.0f, row * 20.0f);
                tiles.Add(Instantiate(tilePrefabs[randomPrefabIndex], tilePosition, randRotation, parent));

                }
                
        }
       
        

    }
}
