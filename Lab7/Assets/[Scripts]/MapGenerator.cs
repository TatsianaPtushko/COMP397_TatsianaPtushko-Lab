using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile Resources")]
    public List<GameObject> tilePrefabs;
    public GameObject startTile;
    public GameObject goalTile;

    [Header("Spawn Point")]
    public GameObject spawnPoint;
    private GameObject sPoint;

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
        var size = tiles.Count;

        for (int i = 0; i < size; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();//remove all tiles
        Destroy(sPoint);
    }

    public void BuildMap()
    {
        var offset = new Vector3(20.0f,0.0f,20.0f);

      //choose a random position of Start tile
       var randomStartRowPosition = Random.Range(1, depth + 1);
       var randomStartColPosition = Random.Range(1, width + 1);

        //choose a random position of Goal tile
        var randomGoalRowPosition = Random.Range(1, depth+1);
        var randomGoalColPosition = Random.Range(1, width+1);

          //check if start and goal tiles positions are the same or adjacent
        while(  
            (randomGoalRowPosition == randomStartRowPosition  &&              //check the same row   AND
            (randomGoalColPosition == randomStartColPosition ||               //check the same column in given row  OR
             randomGoalColPosition ==randomStartColPosition-1 ||              //check the  previous column in given row  OR
             randomGoalColPosition == randomStartColPosition + 1))            //check the  next column in given row

            || (randomGoalRowPosition == randomStartRowPosition-1 &&           //check the previous row   AND
            (randomGoalColPosition == randomStartColPosition ||                //check the same column in given row  OR
            randomGoalColPosition == randomStartColPosition - 1 ||             //check the  previous column in given row  OR
            randomGoalColPosition == randomStartColPosition + 1))              //check the  next column in given row

            || (randomGoalRowPosition == randomStartRowPosition + 1 &&         //check the next row     AND
            (randomGoalColPosition == randomStartColPosition ||                //check the same column in given row   OR
             randomGoalColPosition == randomStartColPosition - 1 ||            //check the  previous column in given row   OR
             randomGoalColPosition == randomStartColPosition + 1))             //check the  next column in given row
        )
        {
            randomGoalRowPosition = Random.Range(1, depth + 1);
            randomGoalColPosition = Random.Range(1, width + 1);
        }


        //generate more tiles if both width & depth > 2
        for (int row = 1; row <= depth; row++)
            {  
                for (int col = 1; col <= width; col++)
                {
                   // if (row == 1 && col == 1) continue;

                 var tilePosition = new Vector3(col * 20.0f, 0.0f, row * 20.0f) - offset;
                 var spawnPosition = new Vector3(col * 20.0f, 5.0f, row * 20.0f) - offset;

                if (row == randomStartRowPosition && col == randomStartColPosition)   //random position of the start tile
                {
                    //place the start tile
                    tiles.Add(Instantiate(startTile, tilePosition, Quaternion.identity, parent));

                    // instantiate a spawn point at position of Start tile
                    sPoint = Instantiate(spawnPoint, spawnPosition, Quaternion.identity);
                }
                else if (row == randomGoalRowPosition && col == randomGoalColPosition)  //random position of the goal tile
                    { //place the goal tile
                        tiles.Add(Instantiate(goalTile, tilePosition, Quaternion.identity, parent));
                    }
                    else
                    {
                        var randomPrefabIndex = Random.Range(0, 4);
                        var randRotation = Quaternion.Euler(0.0f, Random.Range(0, 4) * 90.0f, 0.0f);
                        tiles.Add(Instantiate(tilePrefabs[randomPrefabIndex], tilePosition, randRotation, parent));
                    }
                }
        }
    }
}
