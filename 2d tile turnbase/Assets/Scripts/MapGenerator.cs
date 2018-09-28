using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour {

    public Texture2D dataToGenerateMap;
    
    
    public ColorToPrefav[] colorMaps;

    void Awake()
    {
        GenerateMap();
   
    }

    void GenerateMap()
    {
        //instantiate the variables
        int width = dataToGenerateMap.width;
        int heigth = dataToGenerateMap.height;
        Map map = GameObject.FindObjectOfType<Map>();
        //en caso de tener distintos tipod de creacion de mapa se agraga un enum + swithch

        GenerateByTexture(width,heigth,map);
        CreateCoverMesh(width, heigth, map);

    }


    void GenerateByTexture(int width, int heigth, Map map)
    {
        Tile[,] tiles = new Tile[width, heigth];

        for (int y = 0; y < heigth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = dataToGenerateMap.GetPixel(x, y);
                Tile newTile;
                SpawnTile(x, y, map, pixelColor, out newTile);
                
                tiles[x, y] = newTile;
            }
        }
        //habitar el array de tiles del mapa (ok)
        map.SetTiles(tiles);
    }
    
    

    void SpawnTile( int x, int y, Map map, Color pixelColor , out Tile newTile)
    {
        newTile = null;
        foreach (ColorToPrefav c in colorMaps)
        {
            if (c.color == pixelColor)
            {

                GameObject g = Instantiate(c.prefav, new Vector3(x, y, 0), Quaternion.identity, map.transform);
                newTile = g.GetComponent<Tile>();
                newTile.TileUpdate();
                
                break;
            }
            
        }
        
    }


   

    void CreateCoverMesh(int width, int heigth, Map map)
    {
                     Vector3[] vertices = new Vector3[4]
            {
                new Vector3(0,0,0),
                new Vector3(width,0,0),
                new Vector3(0,heigth,0),
                new Vector3(width,heigth,0)
            };
        int[] tris = new int[6];

        tris[0] = 0;
        tris[1] = 2;
        tris[2] = 1;
        tris[3] = 1;
        tris[4] = 2;
        tris[5] = 3;

        Mesh newMesh = new Mesh();
        newMesh.vertices = vertices;
        newMesh.triangles = tris;
        newMesh.name = "Map_Mesh";

        map.GetComponent<MeshFilter>().sharedMesh = newMesh;
        map.GetComponent<MeshCollider>().sharedMesh = newMesh;



    }

}
